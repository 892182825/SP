using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL.PersonalSpace
{
    public class Message
    {
        public Message()
        { 
        }

        public static DataTable GetAlbumsPhoto(string albumsId, string number)
        {
            string sql = "Select * From AlbumsPhotos Where AlbumsId=@AlumsID And number=@number";
            SqlParameter[] para = {
                                      new SqlParameter("@AlumsID",albumsId),
                                      new SqlParameter("@number",number)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static bool MessageAdd(Model.MemberMessage msg)
        {
            string sql = @"Insert Into MemberMessage(FromNumber,ToNumber,MessageDate,MessageIP,[Content])
                            Values(@FromNumber,@ToNumber,@MessageDate,@MessageIP,@Content)";

            SqlParameter[] para = {
                                      new SqlParameter("@FromNumber",SqlDbType.NVarChar,30),
                                      new SqlParameter("@ToNumber",SqlDbType.NVarChar,30),
                                      new SqlParameter("@MessageDate",SqlDbType.NVarChar,30),
                                      new SqlParameter("@MessageIP",SqlDbType.NVarChar,30),
                                      new SqlParameter("@Content",SqlDbType.Text,4000)
                                  };

            para[0].Value = msg.FromNumber;
            para[1].Value = msg.ToNumber;
            para[2].Value = msg.MessageDate;
            para[3].Value = msg.MessageIP;
            para[4].Value = msg.Content;

            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool MessageDel(string Id)
        {
            string sql = "Delete From MemberMessage Where ID=@ID";
            SqlParameter[] para = {
                                      new SqlParameter("@ID",Id)
                                  };
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static DataTable GetAlbumsName(string number)
        {
            string sql = "Select * From Albums Where number=@number Order By Xuhao";
            SqlParameter[] para = {
                                      new SqlParameter("@number",number)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static DataTable DataAlbumsName(string number)
        {
            string sql = "Select ID,AlbumsName From Albums Where number=@number Order By Xuhao";
            SqlParameter[] para = {
                                      new SqlParameter("@number",number)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static DataTable DataAlbumsName(string number, string albumsId)
        {
            string sql = "Select ID,AlbumsName  From Albums Where number=@number And ID<>@AlbumsID Order By Xuhao";
            SqlParameter[] para = {
                                      new SqlParameter("@number",number),
                                      new SqlParameter("@AlbumsID",albumsId)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static DataTable GetAlbumsName(string number,string albumsId)
        {
            string sql = "Select * From Albums Where number=@number And ID<>@AlbumsID Order By Xuhao";
            SqlParameter[] para = {
                                      new SqlParameter("@number",number),
                                      new SqlParameter("@AlbumsID",albumsId)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }


        public static bool PhotoDel(string Id, string xuhao, string AlbumnID)
        {
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string sql = "Delete From AlbumsPhotos Where ID=@ID";
                    SqlParameter[] para = {
                                  new SqlParameter("@ID",Id)
                              };
                    int count = (int)DBHelper.ExecuteNonQuery(tran,sql, para, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    sql = "Update AlbumsPhotos Set Xuhao = Xuhao-1 Where albumsid=@albumsid And Xuhao>@Xuhao";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@albumsid",AlbumnID),
                                              new SqlParameter("@Xuhao",xuhao)
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    //if (count == 0)
                    //{
                    //    tran.Rollback();
                    //    return false;
                    //}

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static Model.AlbumsPhotosModel GetPhotoMsg(string Id)
        {
            Model.AlbumsPhotosModel albModel = new Model.AlbumsPhotosModel();
            string sql = "Select * From AlbumsPhotos Where ID=@ID";
            SqlParameter[] para = {
                                      new SqlParameter("@ID",Id)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            albModel.Id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
            albModel.Number = dt.Rows[0]["Number"].ToString();
            albModel.AlbumsID = Convert.ToInt32(dt.Rows[0]["AlbumsID"].ToString());
            albModel.PhotoName = dt.Rows[0]["PhotoName"].ToString();
            albModel.Description = dt.Rows[0]["Description"].ToString();
            albModel.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
            albModel.Xuhao = Convert.ToInt32(dt.Rows[0]["Xuhao"].ToString());
            albModel.UploadDate = Convert.ToDateTime(dt.Rows[0]["UploadDate"].ToString());
            albModel.UploadIp = dt.Rows[0]["UploadIp"].ToString();

            return albModel;
        }

        public static bool UpdatePhoto(Model.AlbumsPhotosModel model)
        {
            string sql = "Update AlbumsPhotos Set PhotoName=@PhotoName,Description=@Description Where ID=@ID";
            SqlParameter[] para = {
                                      new SqlParameter("@PhotoName",model.PhotoName),
                                      new SqlParameter("@Description",model.Description),
                                       new SqlParameter("@ID",model.Id)
                                  };

            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }


        public static bool MovePhoto(string Id, string oldAlbumsId, string newAlbumsId, string number)
        {
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int oldXuhao = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Xuhao From AlbumsPhotos Where ID=" + Id,CommandType.Text);
                    string sql = "Update AlbumsPhotos Set Xuhao=Xuhao-1 Where AlbumsID=@AlbumsID And Xuhao>@Xuhao";
                    SqlParameter[] para = {
                                              new SqlParameter("@AlbumsID",oldAlbumsId),
                                              new SqlParameter("@Xuhao",oldXuhao)
                                          };
                    int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    //if (count == 0)
                    //{
                    //    tran.Rollback();
                    //    return false;
                    //}

                    int Xuhao = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Count(Xuhao) From AlbumsPhotos Where number='" + number + "' And AlbumsID=" + newAlbumsId, CommandType.Text);
                    sql = "Update AlbumsPhotos Set AlbumsID=@NewAlbumsID,Xuhao=@Xuhao Where ID=@ID";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@NewAlbumsID",newAlbumsId),
                                              new SqlParameter("@ID",Id),
                                              new SqlParameter("@Xuhao",(Xuhao+1))
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static bool LessPhoteXuhao(int num, string albumsId, string Id, string number)
        {
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int oldXuhao = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Xuhao From AlbumsPhotos Where ID=" + Id, CommandType.Text);

                    int c = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Count(0) From AlbumsPhotos Where AlbumsID=" + albumsId + " And number='" + number + "' And Xuhao>" + oldXuhao, CommandType.Text);
                    if (c < num)
                    {
                        num = c;
                    }

                    string sql = "Update AlbumsPhotos Set Xuhao=Xuhao-1 Where AlbumsID=@AlbumsID And number=@number And Xuhao>@StartXuhao And Xuhao<=@EndXuhao";
                    SqlParameter[] para = {
                                              new SqlParameter("@AlbumsID",albumsId),
                                              new SqlParameter("@number",number),
                                              new SqlParameter("@StartXuhao",oldXuhao),
                                              new SqlParameter("@EndXuhao",(oldXuhao+num))
                                          };

                    int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    sql = "Update AlbumsPhotos Set Xuhao=@Xuhao Where ID=@ID";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@Xuhao",(oldXuhao+num)),
                                              new SqlParameter("@ID",Id)
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool AddPhoteXuhao(int num, string albumsId, string Id, string number)
        {
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int oldXuhao = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Xuhao From AlbumsPhotos Where ID=" + Id, CommandType.Text);

                    int c = (int)DAL.DBHelper.ExecuteScalar(tran, "Select Count(0) From AlbumsPhotos Where AlbumsID=" + albumsId + " And number='" + number + "' And Xuhao<" + oldXuhao, CommandType.Text);
                    if (c < num)
                    {
                        num = c;
                    }
                    string sql = "Update AlbumsPhotos Set Xuhao=Xuhao+1 Where AlbumsID=@AlbumsID And number=@number And Xuhao<@StartXuhao And Xuhao>=@EndXuhao";
                    SqlParameter[] para = {
                                              new SqlParameter("@AlbumsID",albumsId),
                                              new SqlParameter("@number",number),
                                              new SqlParameter("@StartXuhao",oldXuhao),
                                              new SqlParameter("@EndXuhao",(oldXuhao-num))
                                          };

                    int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    sql = "Update AlbumsPhotos Set Xuhao=@Xuhao Where ID=@ID";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@Xuhao",(oldXuhao-num)),
                                              new SqlParameter("@ID",Id)
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool LessAlbumsXuhao(int num, string Id, string number)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int oldXuhao = (int)DBHelper.ExecuteScalar(tran, "Select Xuhao From Albums Where ID=" + Id, CommandType.Text);
                    int c = (int)DBHelper.ExecuteScalar(tran, "Select Count(0) From Albums Where number='" + number + "' And Xuhao>" + oldXuhao, CommandType.Text);
                    if (c < num)
                    {
                        num = c;
                    }

                    string sql = "Update Albums Set Xuhao=Xuhao-1 Where number=@number And Xuhao>@StartXuhao And Xuhao<=@EndXuhao";
                    SqlParameter[] para = {
                                              new SqlParameter("@number",number),
                                              new SqlParameter("@StartXuhao",oldXuhao),
                                              new SqlParameter("@EndXuhao",(oldXuhao+num))
                                          };

                    int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    sql = "Update Albums Set Xuhao=@Xuhao Where ID=@ID";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@Xuhao",(oldXuhao+num)),
                                              new SqlParameter("@ID",Id)
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool AddAlbumsXuhao(int num, string Id, string number)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int oldXuhao = (int)DBHelper.ExecuteScalar(tran, "Select Xuhao From Albums Where ID=" + Id, CommandType.Text);
                    int c = (int)DBHelper.ExecuteScalar(tran, "Select Count(0) From Albums Where number='" + number + "' And Xuhao<" + oldXuhao, CommandType.Text);
                    if (c < num)
                    {
                        num = c;
                    }

                    string sql = "Update Albums Set Xuhao=Xuhao+1 Where number=@number And Xuhao<@StartXuhao And Xuhao>=@EndXuhao";
                    SqlParameter[] para = {
                                              new SqlParameter("@number",number),
                                              new SqlParameter("@StartXuhao",oldXuhao),
                                              new SqlParameter("@EndXuhao",(oldXuhao-num))
                                          };

                    int count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    sql = "Update Albums Set Xuhao=@Xuhao Where ID=@ID";
                    SqlParameter[] para1 = {
                                              new SqlParameter("@Xuhao",(oldXuhao-num)),
                                              new SqlParameter("@ID",Id)
                                          };
                    count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                    if (count == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static bool AlbumsAdd(Model.AlbumsModel model)
        {
            int xuhao = (int)DBHelper.ExecuteScalar("Select Count(0) From Albums Where number='" + model.Number + "'");
            string sql = @"Insert Into Albums(number,AlbumsName,Xuhao,CreateDate,CreateIp)
                            values(@number,@AlbumsName,@Xuhao,@CreateDate,@CreateIp)";
            SqlParameter[] para = {
                                      new SqlParameter("@number",model.Number ),
                                      new SqlParameter("@AlbumsName",model.AlbumsName),
                                      new SqlParameter("@Xuhao",(xuhao+1)),
                                      new SqlParameter("@CreateDate",model.CreateDate),
                                      new SqlParameter("@CreateIp",model.CreateIP)
                                  };
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool UpdateAlbumsName(string Id,string newAlbumsName)
        {
            string sql = "Update Albums Set AlbumsName=@AlbumsName Where ID=@ID";
            SqlParameter[] para = {
                                      new SqlParameter("@AlbumsName",newAlbumsName),
                                      new SqlParameter("@ID",Id)
                                  };
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }

        public static bool DeleteAlbums(string Id, string type, string newId, string number)
        {
            if (type == "1")
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();

                    try
                    {
                        int xuhao = (int)DBHelper.ExecuteScalar(tran, "Select Xuhao From Albums Where ID=" + Id, CommandType.Text);

                        string sql = "Delete From Albums Where Id=" + Id;
                        int count = (int)DBHelper.ExecuteNonQuery(tran,sql);
                        if (count == 0)
                        {
                            tran.Rollback();
                            return false;
                        }

                        sql = "Update Albums Set Xuhao=Xuhao-1 Where number=@number And Xuhao>@Xuhao";
                        SqlParameter[] para = {
                                                  new SqlParameter("@number",number),
                                                  new SqlParameter("@Xuhao",xuhao)
                                              };
                        count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else if (type == "2")
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        int xuhao = (int)DBHelper.ExecuteScalar(tran, "Select Xuhao From Albums Where ID=" + Id, CommandType.Text);

                        string sql = "Delete From Albums Where Id=" + Id;
                        int count = (int)DBHelper.ExecuteNonQuery(tran,sql);
                        if (count == 0)
                        {
                            tran.Rollback();
                            return false;
                        }

                        sql = "Update Albums Set Xuhao=Xuhao-1 Where number=@number And Xuhao>@Xuhao";
                        SqlParameter[] para = {
                                                  new SqlParameter("@number",number),
                                                  new SqlParameter("@Xuhao",xuhao)
                                              };
                        count = (int)DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                        //if (count == 0)
                        //{
                        //    tran.Rollback();
                        //    return false;
                        //}

                        sql = "Update AlbumsPhotos Set AlbumsID=@NewID Where number=@number And AlbumsID=@ID";
                        SqlParameter[] para1 = {
                                                   new SqlParameter("@NewID",newId),
                                                   new SqlParameter("@number",number),
                                                   new SqlParameter("@ID",Id)
                                               };

                        count = (int)DBHelper.ExecuteNonQuery(tran, sql, para1, CommandType.Text);
                        if (count == 0)
                        {
                            tran.Rollback();
                            return false;
                        }

                        tran.Commit();
                        return true;
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return true;
        }

        public static bool SetAlbumsPwd(string pwd, string id)
        {
            string sql = "Update Albums Set ViewPwd=@Pwd Where ID=@ID";
            SqlParameter[] para = {
                                      new SqlParameter("@Pwd",pwd),
                                      new SqlParameter("@ID",id)
                                  };
            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }


        public static bool AddAlbumsPhotos(Model.AlbumsPhotosModel pModel)
        {
            string sql = @"Insert Into AlbumsPhotos(number,AlbumsID,PhotoName,Description,PhotoPath,Xuhao,UploadDate,UploadIP)
                            Values(@number,@AlbumsID,@PhotoName,@Description,@PhotoPath,@Xuhao,@UploadDate,@UploadIP)";
            SqlParameter[] para = {
                                      new SqlParameter("@number",pModel.Number),
                                      new SqlParameter("@AlbumsID",pModel.AlbumsID),
                                      new SqlParameter("@PhotoName",pModel.PhotoName),
                                      new SqlParameter("@Description",pModel.Description),
                                      new SqlParameter("@PhotoPath",pModel.PhotoPath),
                                      new SqlParameter("@Xuhao",pModel.Xuhao),
                                      new SqlParameter("@UploadDate",pModel.UploadDate),
                                      new SqlParameter("@UploadIP",pModel.UploadIp)
                                  };
            int count = (int)DAL.DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (count == 0)
                return false;
            return true;
        }



    }
}
