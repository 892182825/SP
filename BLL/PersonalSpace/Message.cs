using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BLL.PersonalSpace
{
    public class Message
    {
        public Message()
        { 
        }

        public static DataTable GetAlbumsPhoto(string albumsID,string storeID)
        {
            return DAL.PersonalSpace.Message.GetAlbumsPhoto(albumsID,storeID);
        }

        public static bool MessageAdd(Model.MemberMessage Msg)
        {
            return DAL.PersonalSpace.Message.MessageAdd(Msg);
        }

        public static bool MessageDel(string Id)
        {
            return DAL.PersonalSpace.Message.MessageDel(Id);
        }

        public static DataTable GetAlbumsName(string number)
        {
            return DAL.PersonalSpace.Message.GetAlbumsName(number);
        }

        public static DataTable DataAlbumsName(string number)
        {
            return DAL.PersonalSpace.Message.DataAlbumsName(number);
        }

        public static DataTable GetAlbumsName(string number,string albumsId)
        {
            return DAL.PersonalSpace.Message.GetAlbumsName(number,albumsId);
        }

        public static DataTable DataAlbumsName(string number, string albumsId)
        {
            return DAL.PersonalSpace.Message.DataAlbumsName(number, albumsId);
        }

        public static Model.AlbumsPhotosModel GetPhotoMsg(string Id)
        {
            return DAL.PersonalSpace.Message.GetPhotoMsg(Id);
        }

        public static bool PhotoDel(string Id,string xuhao,string AlbumnID)
        {
            return DAL.PersonalSpace.Message.PhotoDel(Id, xuhao, AlbumnID);
        }

        public static bool UpdatePhoto(Model.AlbumsPhotosModel model)
        {
            return DAL.PersonalSpace.Message.UpdatePhoto(model);
        }

        public static bool MovePhoto(string Id, string oldAlbumsId, string newAlbumsId, string number)
        {
            return DAL.PersonalSpace.Message.MovePhoto(Id, oldAlbumsId, newAlbumsId, number);
        }

        public static bool LessPhoteXuhao(int num, string albnumsId, string Id, string number)
        {
            return DAL.PersonalSpace.Message.LessPhoteXuhao(num, albnumsId, Id, number);
        }


        public static bool AddPhoteXuhao(int num, string albnumsId, string Id, string number)
        {
            return DAL.PersonalSpace.Message.AddPhoteXuhao(num, albnumsId, Id, number);
        }

        public static bool LessAlbumsXuhao(int num, string Id, string number)
        {
            return DAL.PersonalSpace.Message.LessAlbumsXuhao(num, Id, number);
        }

        public static bool AddAlbumsXuhao(int num, string Id, string number)
        {
            return DAL.PersonalSpace.Message.AddAlbumsXuhao(num, Id, number);
        }

        public static bool AlbumsAdd(Model.AlbumsModel model)
        {
            return DAL.PersonalSpace.Message.AlbumsAdd(model);
        }

        public static bool UpdateAlbumsName(string Id,string newAlbumsName)
        {
            return DAL.PersonalSpace.Message.UpdateAlbumsName(Id,newAlbumsName);
        }

        public static bool DeleteAlbums(string Id, string type, string newId, string number)
        {
            return DAL.PersonalSpace.Message.DeleteAlbums(Id, type, newId, number);
        }

        public static bool SetAlbumsPwd(string pwd, string Id)
        {
            return DAL.PersonalSpace.Message.SetAlbumsPwd(pwd, Id);
        }

        public static bool AddAlbumsPhotos(Model.AlbumsPhotosModel pModel)
        {
            return DAL.PersonalSpace.Message.AddAlbumsPhotos(pModel); 
        }
    }
}
