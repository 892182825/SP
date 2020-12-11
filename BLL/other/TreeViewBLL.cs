using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

/**
 * 
 * 
 * 
 */
namespace BLL.other
{
    public class TreeViewBLL
    {
        ViewTreeDAL viewTreeDAL = new ViewTreeDAL();
        public DataTable GetExtendTreeView(string number, string tree, int state, int leyerNumber, int excempt, string storeid)
        {
            return viewTreeDAL.GetExtendTreeView(number, tree, state, leyerNumber, excempt, storeid);
        }

        public DataTable GetExtendTreeView_NewAz(string number, string tree, int leyerNumber, int excempt, int type)
        {
            return viewTreeDAL.GetExtendTreeView_newAz(number, tree, leyerNumber, excempt, type);
        }

        public DataTable GetExtendTreeView_NewAz(string number, string tree, int leyerNumber, int excempt,int type,int cw)
        {
            return viewTreeDAL.GetExtendTreeView_newAz(number, tree,  leyerNumber, excempt,type,cw);
        }

        public DataTable GetExtendTreeView_NewTj(string number, string tree, int leyerNumber, int excempt,int type)
        {
            return viewTreeDAL.GetExtendTreeView_NewTj(number, tree, leyerNumber, excempt,type);
        }

        public DataTable GetExtendTreeView_NewTj(string number, string tree, int leyerNumber, int excempt, int type,int cw)
        {
            return viewTreeDAL.GetExtendTreeView_NewTj(number, tree, leyerNumber, excempt, type,cw);
        }


        public DataTable GetExtendTreeView_NewAz1(string number, string tree, int leyerNumber, int excempt, int type, int cw)
        {
            return viewTreeDAL.GetExtendTreeView_newAz1(number, tree, leyerNumber, excempt, type, cw);
        }
        /***************************************************************************************************************************************************************/
        //DS2012
        //网络图显示字段控制表
        //NetWorkDisplayStatus表
        //网路图是有后台拼接出来的
        //type=0公司子系统里的网络图字段
        //Flag=1显示该字段
        public DataTable returnIsShowField()
        {
            string sql = "select * from NetWorkDisplayStatus where type=0 and Flag=1";
            return DAL.DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }
    }
}
