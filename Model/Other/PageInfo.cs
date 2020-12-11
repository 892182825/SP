using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Model.Other
{
    [Serializable]
    public class PageInfo
    {
        public PageInfo()
        {
            this._ContainID = "AjaxData";
            this._TableName = string.Empty;
            this._IdentityField = "ID";
            this._PageSize = 10;
            this._Fields = "*";
            this._IsDesc = true;
            this._Content = string.Empty;
            this._ConnectStringName = string.Empty;
        }

        private string _RepeaterUniqueID;
        private string _ContainID = "AjaxData";
        private string _TableName = string.Empty;
        private string _IdentityField = "ID";
        private int _PageSize = 10;
        private string _Fields = "*";
        private bool _IsDesc = true;
        private string _Content = string.Empty;
        private string _ConnectStringName = string.Empty;


        public string RepeaterUniqueID
        {
            get { return _RepeaterUniqueID; }
            set { _RepeaterUniqueID = value; }
        }
        public string ContainID
        {
            get { return _ContainID; }
            set { _ContainID = value; }
        }
        public int PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = int.Parse(value.ToString());
            }
        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        public string IdentityField
        {
            get { return _IdentityField; }
            set { _IdentityField = value; }
        }
        public string Fields
        {
            get { return _Fields; }
            set { _Fields = value; }
        }
        public bool IsDesc
        {
            get { return _IsDesc; }
            set { _IsDesc = value; }
        }
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        public string ConnectStringName
        {
            get { return _ConnectStringName; }
            set { _ConnectStringName = value; }
        }
    }
}
