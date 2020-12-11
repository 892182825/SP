using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class AlbumsPhotosModel
    {
        public AlbumsPhotosModel()
        { }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int albumsID;
        public int AlbumsID
        {
            get { return albumsID; }
            set { albumsID = value; }
        }

        private string photoName;
        public string PhotoName
        {
            get { return photoName; }
            set { photoName = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string photopath;
        public string PhotoPath
        {
            get { return photopath; }
            set { photopath = value; }
        }

        private int xuhao;
        public int Xuhao
        {
            get { return xuhao; }
            set { xuhao = value; }
        }

        private DateTime uploaddate;
        public DateTime UploadDate
        {
            get { return uploaddate; }
            set { uploaddate = value; }
        }

        private string uploadIp;
        public string UploadIp
        {
            get { return uploadIp; }
            set { uploadIp = value; }
        }
    }
}
