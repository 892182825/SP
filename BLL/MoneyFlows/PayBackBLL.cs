using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
/*
 * 工资退回
 * **/
namespace BLL
{
    public class PayBackBLL
    {
        /// <summary>
        /// 条件查询
        /// select * from ChongHong where isdele=0 and bianhao like '%a00001%' and qishu=2 and cast (jine as varchar (20)) like '%10%'
        /// </summary>
        public List<ChongHongModel> GetChongHong(ChongHongModel chonghong)
        {
            return null;
        }
        /// <summary>
        /// 删除工资退回
        /// update  ChongHong set isdele=1,deleteDate='2009-8-30 10:03:03'  WHERE id=12
        /// update H_info set zongji=zongji-100.00,ectzongji=ectzongji-100.00 where bianhao='a00001'
        /// </summary>
        /// <returns></returns>
        public static Boolean DelChongHong(int ID,double money,string MemeberNum)
        {
            return ReleaseDAL.DelChongHong(ID,money,MemeberNum);
        }

        /// <summary>
        /// 条件查询
        /// SELECT bianhao,Name,case Sex when 0 then '女' when 1 then '男' end Sex,Address FROM h_info  WHERE bianhao='a00001'
        /// </summary>
        /// <param name="MemberNum"></param>
        /// <returns></returns>
        public MemberInfoModel GetMemberInfo(string MemberNum)
        {
            return null;
        }
        /// <summary>
        /// 添加“工资退回”
        /// update h_info set zongji=zongji+100,ectzongji=ectzongji+100 where bianhao='a00001'
        /// insert into chonghong (bianhao,qishu,jine,beizhu,isdele) values ('a00001',2,100,'给你发钱了 你高兴了吧',0)
        /// </summary>
        /// <returns></returns>
        public static Boolean AddChongHong(ChongHongModel chonghong)
        {
            return ReleaseDAL.AddChongHong(chonghong);
        }
    }
}
