using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.Other;
using System.Data.SqlClient;
using Model.Other;

namespace BLL.other
{
    public class WTreeBLL
    {
        public static DataTable GetWangLuoT(string BrowserType, string IsPlacement)
        {
            return WTreeDAL.GetWangLuoT(BrowserType, IsPlacement);
        }

        public static string GetNumberParent(string ThNumber)
        {
            return WTreeDAL.GetNumberParent(ThNumber);
        }

        public static string GetNumberParent_II(string ThNumber)
        {
            return WTreeDAL.GetNumberParent_II(ThNumber);
        }

        public static string GetTree(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum,string language)
        {
            return WTreeDAL.GetTree(nodeid, ExpectNum, thNumber, model, BrowserType, IsPlacement, ManageNum, language);
        }
        public static DataTable GetTreePhone(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum, string language)
        {
            return WTreeDAL.GetTreePhone(nodeid, ExpectNum, thNumber, model, BrowserType, IsPlacement, ManageNum, language);
        }
        public static string GetTree_II(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum, string language)
        {
            return WTreeDAL.GetTree_II(nodeid, ExpectNum, thNumber, model, BrowserType, IsPlacement, ManageNum,language);
        }
        public static string GetTree_IIV(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum)
        {
            return WTreeDAL.GetTree_IIV(nodeid, ExpectNum, thNumber, model, BrowserType, IsPlacement, ManageNum);
        }
        public static string SetImage(string thnumber, string img, string ManageNum)
        {
            return WTreeDAL.SetImage(thnumber, img, ManageNum);
        }

        public static string SetImage_II(string thnumber, string img, string ManageNum)
        {
            return WTreeDAL.SetImage_II(thnumber, img, ManageNum);
        }

        public static string SetColor(string thnumber, string model, string ExpectNum, string tuanNumber, string ManageNum)
        {
            return WTreeDAL.SetColor(thnumber, model, ExpectNum, tuanNumber, ManageNum);
        }

        public static string SetColor_II(string thnumber, string model, string ExpectNum, string tuanNumber, string ManageNum)
        {
            return WTreeDAL.SetColor_II(thnumber, model, ExpectNum, tuanNumber, ManageNum);
        }

        public static bool IsRoot(string StartNumber, string qs, string EndNumber)
        {
            return WTreeDAL.IsRoot(StartNumber, qs, EndNumber);
        }

        public static bool IsRoot_II(string StartNumber, string qs, string EndNumber)
        {
            return WTreeDAL.IsRoot_II(StartNumber, qs, EndNumber);
        }

        public static DataTable BindQS()
        {
            return WTreeDAL.BindQS();
        }

        public static string SetLianLuTu(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu(EndNumber, StartNumber, Qs);
        }
        public static string SetLianLuTuPhone(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTuPhone(EndNumber, StartNumber, Qs);
        }
        public static string SetLianLuTu_IIPhone(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_IIPhone(EndNumber, StartNumber, Qs);
        }
        public static string SetLianLuTu_II(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_II(EndNumber, StartNumber, Qs);
        }

        public static DataTable SetLianLuTu_C(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_C(EndNumber, StartNumber, Qs);
        }

        public static DataTable SetLianLuTu_C_II(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_C_II(EndNumber, StartNumber, Qs);
        }

        public static object[] GetLLTTree_II(string qs, string EndNumber, string StartNumber, string BrowserType, string IsPlacement)
        {
            return WTreeDAL.GetLLTTree_II(qs, EndNumber, StartNumber, BrowserType, IsPlacement);
        }

        public static object[] GetLLTTree(string qs, string EndNumber, string StartNumber, string BrowserType, string IsPlacement)
        {
            return WTreeDAL.GetLLTTree(qs, EndNumber, StartNumber, BrowserType, IsPlacement);
        }

        public static string SetLianLuTu_L(string EndNumber, string StartNumber, string Qs, string ysEndNumber)
        {
            return WTreeDAL.SetLianLuTu_L(EndNumber, StartNumber, Qs, ysEndNumber);
        }

        public static string SetLianLuTu_L_II(string EndNumber, string StartNumber, string Qs, string ysEndNumber)
        {
            return WTreeDAL.SetLianLuTu_L_II(EndNumber, StartNumber, Qs, ysEndNumber);
        }

        public static DataTable GetWLTField(string BrowserType, string IsPlacement)
        {
            return WTreeDAL.GetWLTField(BrowserType, IsPlacement);
        }

        public static int UpdWLTField(string f, string v, string id)
        {
            return WTreeDAL.UpdWLTField(f, v, id);
        }

        public static DataTable GetKYWL(string ManageID, string type)
        {
            return WTreeDAL.GetKYWL(ManageID, type);
        }

        public static bool IsExistsNumber(string number)
        {
            return WTreeDAL.IsExistsNumber(number);
        }

        public static string GetMaxQS()
        {
            return WTreeDAL.GetMaxQS();
        }

        public static CYWLTModel GetCYWLTModel(string number, string qs, string orderby)
        {
            return WTreeDAL.GetCYWLTModel(number, qs, orderby);
        }

        public static CYWLTModel GetCYWLTModelII(string number, string qs, string cs, string notnumber)
        {
            return WTreeDAL.GetCYWLTModelII(number, qs, cs, notnumber);
        }

        public static string GetNumberQuShu(string number)
        {
            return WTreeDAL.GetNumberQuShu(number);
        }
        public static string SetLianLuTu_CYWL(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_CYWL(EndNumber, StartNumber, Qs);
        }

        public static string SetLianLuTu_CYWLII(string EndNumber, string StartNumber, string Qs)
        {
            return WTreeDAL.SetLianLuTu_CYWLII(EndNumber, StartNumber, Qs);
        }

        public static DataTable GetGraphNet_AZ(string number, string qs, string isFirst)
        {
            return WTreeDAL.GetGraphNet_AZ(number, qs, isFirst);
        }

        public static bool IsExistsAZ(string number, string qs)
        {
            return WTreeDAL.IsExistsAZ(number, qs);
        }

        public static double GetYj(string field, string qs, string District, string placement)
        {
            return WTreeDAL.GetYj(field, qs, District, placement);
        }
    }
}