using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.GZip;
//using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

	#region ZipClass ѹ���ļ�
	public class ZipClass
	{
		public void ZipFile(string FileToZip, string ZipedFile ,int CompressionLevel, int BlockSize,string password)
		{
			//����ļ�û���ҵ����򱨴�
			if (! System.IO.File.Exists(FileToZip)) 
			{
				throw new System.IO.FileNotFoundException("The specified file " + FileToZip + " could not be found. Zipping aborderd");
			}
  
			System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip,System.IO.FileMode.Open , System.IO.FileAccess.Read);
			System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
			ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
			ZipEntry ZipEntry = new ZipEntry("ZippedFile");
			ZipStream.PutNextEntry(ZipEntry);
			ZipStream.SetLevel(CompressionLevel);
			byte[] buffer = new byte[BlockSize];
			System.Int32 size =StreamToZip.Read(buffer,0,buffer.Length);
			ZipStream.Write(buffer,0,size);
			try 
			{
				while (size < StreamToZip.Length) 
				{
					int sizeRead =StreamToZip.Read(buffer,0,buffer.Length);
					ZipStream.Write(buffer,0,sizeRead);
					size += sizeRead;
				}
			} 
			catch(System.Exception ex)
			{
				throw ex;
			}
			ZipStream.Finish();
			ZipStream.Close();
			StreamToZip.Close();
		}
 
		public void ZipFileMain(string[] args)
		{
			//string[] filenames = Directory.GetFiles(args[0]);
			string[] filenames = new string[]{args[0]};
  
			Crc32 crc = new Crc32();
			ZipOutputStream s = new ZipOutputStream(File.Create(args[1]));
  
			s.SetLevel(6); // 0 - store only to 9 - means best compression
  
			foreach (string file in filenames) 
			{
				//��ѹ���ļ�
				FileStream fs = File.OpenRead(file);
   
				byte[] buffer = new byte[fs.Length];
				fs.Read(buffer, 0, buffer.Length);
				ZipEntry entry = new ZipEntry(file);
   
				entry.DateTime = DateTime.Now;
   
				// set Size and the crc, because the information
				// about the size and crc should be stored in the header
				// if it is not set it is automatically written in the footer.
				// (in this case size == crc == -1 in the header)
				// Some ZIP programs have problems with zip files that don't store
				// the size and crc in the header.
				entry.Size = fs.Length;
				fs.Close();
   
				crc.Reset();
				crc.Update(buffer);
   
				entry.Crc  = crc.Value;
   
				s.PutNextEntry(entry);
   
				s.Write(buffer, 0, buffer.Length);
   
			}  
			s.Finish();
			s.Close();
		}
        /// <summary>
        /// ���ܣ�ѹ���ļ�����ʱֻѹ���ļ�����һ��Ŀ¼�е��ļ����ļ��м����Ӽ������ԣ�
        /// </summary>
        /// <param name="dirPath">��ѹ�����ļ��м�·��</param>
        /// <param name="zipFilePath">����ѹ���ļ���·����Ϊ����Ĭ���뱻ѹ���ļ���ͬһ��Ŀ¼������Ϊ���ļ�����+.zip</param>
        /// <param name="err">������Ϣ</param>
        /// <returns>�Ƿ�ѹ���ɹ�</returns>
        public bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "Ҫѹ�����ļ��в���Ϊ�գ�";
                return false;
            }
            if (!File.Exists(dirPath))
            {
                err = "Ҫѹ�����ļ������ڣ�";
                return false;
            }
            //ѹ���ļ���Ϊ��ʱʹ���ļ�������.zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("\\"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = new string[] { dirPath };//Directory.GetFiles(dirPath); //
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
           
            return true;
        }
	}
	#endregion



	#region UnZipClass ��ѹ�ļ�
	public class UnZipClass
	{   
		/// <summary>
		/// ��ѹ�ļ�
		/// </summary>
		/// <param name="args">����Ҫ��ѹ���ļ�����Ҫ��ѹ����Ŀ¼������</param>
		public void UnZip(string[] args)
		{
			ZipInputStream s = new ZipInputStream(File.OpenRead(args[0]));
			try
			{				
				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null) 
				{   
					string directoryName = Path.GetDirectoryName(args[1]);
					string fileName      = Path.GetFileName(theEntry.Name);
   
					//���ɽ�ѹĿ¼
					Directory.CreateDirectory(directoryName);
   
					if (fileName != String.Empty) 
					{   
						//��ѹ�ļ���ָ����Ŀ¼
						FileStream streamWriter = File.Create(args[1]+fileName);
    
						int size = 2048;
						byte[] data = new byte[2048];
						while (true) 
						{
							size = s.Read(data, 0, data.Length);
							if (size > 0) 
							{
								streamWriter.Write(data, 0, size);
							} 
							else 
							{
								break;
							}
						}
    
						streamWriter.Close();
					}
				}
				s.Close();
			}
			catch(Exception eu)
			{
				throw eu;
			}
			finally
			{
				s.Close();
			}

		}//end UnZip


	}//end UnZipClass
	#endregion

	#region AttachmentUnZip
	public class AttachmentUnZip
	{
		public AttachmentUnZip()
		{			
		}
		public static void UpZip(string zipFile)
		{
			string []FileProperties=new string[2];

			FileProperties[0]=zipFile;//����ѹ���ļ�

			FileProperties[1]=zipFile.Substring(0,zipFile.LastIndexOf("\\")+1);//��ѹ����õ�Ŀ��Ŀ¼

			UnZipClass UnZc=new UnZipClass();

			UnZc.UnZip(FileProperties);
		}

	}
	#endregion
