using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;

namespace BLL.other.Member
{
    public class MemberInfoModifyBll
    {
        /// <summary>
        /// 获取会员总网业绩
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <returns></returns>
        public static double GetMemberNetYeJi(string number, int qishu)
        {
            return MemberInfoDAL.GetMemberNetYeJi(number, qishu);
        }

        /// <summary>
        /// 编号获取会员昵称
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetPetNameByNumber(string number)
        {
            return MemberInfoDAL.GetPetNameByNumber(number);
        }

        /// <summary>
        /// 根据会员编号获取会员的图片
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public object GetMemberPhoto(string number)
        {
            MemberInfoDAL dao = new MemberInfoDAL();
            return dao.GetMemberPhoto(number);
        }
        /// <summary>
        /// 根据会员编号查询会员的安置编号是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetMemberPlacement(string number)
        {
            MemberInfoDAL dao = new MemberInfoDAL();
            return dao.GetMemberPlacement(number);
        }
        /// <summary>
        /// 根据Number获得单个会员的信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static MemberInfoModel getMemberInfo(string Number)
        {
            return MemberInfoDAL.getMemberInfo(Number);
        }
        /// <summary>
        /// 根据Number获得单个会员的信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static DataTable getMemberInfoTable(string Number)
        {
            return MemberInfoDAL.getMemberInfoTable(Number);
        }
        /// <summary>
        /// ds2012——tianfen
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="Petname"></param>
        /// <param name="Birthday"></param>
        /// <param name="sex"></param>
        /// <param name="HomeQuhao"></param>
        /// <param name="HomeTele"></param>
        /// <param name="OfficeQuhao"></param>
        /// <param name="OfficeTele"></param>
        /// <param name="OfficeFjh"></param>
        /// <param name="MobileTele"></param>
        /// <param name="FaxQuhao"></param>
        /// <param name="FaxTele"></param>
        /// <param name="FaxFjh"></param>
        /// <param name="Country"></param>
        /// <param name="Province"></param>
        /// <param name="City"></param>
        /// <param name="Address"></param>
        /// <param name="PostalCode"></param>
        /// <param name="PaperType"></param>
        /// <param name="PaperNumber"></param>
        /// <param name="Remark"></param>
        /// <param name="Healthy"></param>
        /// <param name="photopath"></param>
        /// <param name="photoW"></param>
        /// <param name="photoH"></param>
        /// <param name="cpccode"></param>
        /// <param name="jjtx"></param>
        /// <param name="bcpccode"></param>
        /// <param name="bankaddress"></param>
        /// <param name="bankcard"></param>
        /// <param name="bankcode"></param>
        /// <param name="bankbreachname"></param>
        /// <returns></returns>
        public bool updateMember(string number, string name, string Petname, DateTime Birthday, int sex, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PostalCode, string PaperType, string PaperNumber, string Remark, string photopath, int photoW, int photoH, string cpccode, string bcpccode, string bankaddress, string bankcard, string bankcode, string bankbreachname)
        {
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            return memberInfoDAL.updateMember(number, name, Petname, Birthday, sex, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PostalCode, PaperType, PaperNumber, Remark, photopath, photoW, photoH, cpccode, bcpccode, bankaddress, bankcard, bankcode, bankbreachname);
        }
        public bool updateMember(string number, string name, string Petname, DateTime Birthday, int sex, string HomeQuhao, string HomeTele, string OfficeQuhao, string OfficeTele, string OfficeFjh, string MobileTele, string FaxQuhao, string FaxTele, string FaxFjh, string Country, string Province, string City, string Address, string PostalCode, string PaperType, string PaperNumber, string Remark, int Healthy, string photopath, int photoW, int photoH, string cpccode, int jjtx)
        {
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            return memberInfoDAL.updateMember(number, name, Petname, Birthday, sex, HomeQuhao, HomeTele, OfficeQuhao, OfficeTele, OfficeFjh, MobileTele, FaxQuhao, FaxTele, FaxFjh, Country, Province, City, Address, PostalCode, PaperType, PaperNumber, Remark, Healthy, photopath, photoW, photoH, cpccode, jjtx);
        }
        public bool updMemberInfo(string number, string petname, string homeTele, string faxTele, string officeTele, string mobileTele, string cpccode, string addr, string postalCode, string bankcode, string bankcpccode, string bankbrachname, string bankcard, string remark)
        {
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            return memberInfoDAL.updMemberInfo(number, petname, homeTele, faxTele, officeTele, mobileTele, cpccode, addr, postalCode, bankcode, bankcpccode, bankbrachname, bankcard, remark);
        }
        /// <summary>
        /// 对单个会员的信息进行编辑
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public int updateMember(string Number, string Placement, string Direct, string Name, string PetName, DateTime Birthday, int Sex, string PostalCode, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PaperType, string PaperNumber, string BankCountry, string BankProvince, string BankCity, string Bank, string BankAddress, string BankCard, string BankBook, int ExpectNum, string Remark, string OrderId, string StoreId, string ChangeInfo, string OperateIp, string OperaterNum)
        {
            MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
            return memberInfoDAL.updateMember(Number, Placement, Direct, Name, PetName, Birthday, Sex, PostalCode, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PaperType, PaperNumber, BankCountry, BankProvince, BankCity, Bank, BankAddress, BankCard, BankBook, ExpectNum, Remark, OrderId, StoreId, ChangeInfo, OperateIp, OperaterNum);
        }
        /// <summary>
        /// 读取当期会员奖金
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="number">会员编号</param>
        public static MemberInfoBalanceNModel getBonus(int ExpectNum, string number)
        {
            return MemberInfoDAL.getBonus(ExpectNum, number);
        }
        /// <summary>
        /// 获取当期补款额
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static double GetDeductMoney(string number, int ExpectNum)
        {
            return MemberInfoDAL.GetDeductMoney(number, ExpectNum);
        }
        /// <summary>
        /// 读取当期会员奖金
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="number">会员编号</param>
        public static DataTable getBonusTable(int ExpectNum, string number)
        {
            return MemberInfoDAL.getBonusTable(ExpectNum, number);
        }

        public static ConsigneeInfo getconsigneeInfo(string Number, bool Isdefault)
        {
            return MemberInfoDAL.getconsigneeInfo(Number, Isdefault);
        }
    }
}