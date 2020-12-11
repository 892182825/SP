using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using Model.Other;

namespace DAL.Other
{
    public class WTreeDAL
    {
        public static DataTable GetWangLuoT(string BrowserType, string IsPlacement)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from NetWorkTitle where BrowserType=@BrowserType and IsPlacement=@IsPlacement and IsVisible=1 order by Sort asc";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@BrowserType",BrowserType),
                new SqlParameter("@IsPlacement",IsPlacement)
            };
            cmd.Parameters.AddRange(param);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        //安置
        public static string GetNumberParent(string ThNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();

            con.Open();

            cmd.Connection = con;

            cmd.CommandText = "select Placement from MemberInfo where Number=@number";
            cmd.Parameters.AddWithValue("@number", ThNumber);
            string rt = (cmd.ExecuteScalar() + "");

            cmd.Dispose();
            con.Close();

            return rt;
        }

        //推荐
        public static string GetNumberParent_II(string ThNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand();

            con.Open();

            cmd.Connection = con;

            cmd.CommandText = "select Direct from MemberInfo where Number=@number";
            cmd.Parameters.AddWithValue("@number", ThNumber);
            string rt = (cmd.ExecuteScalar() + "");

            cmd.Dispose();
            con.Close();

            return rt;
        }

        public static DataTable GetTreePhone(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum, string language)
        {

            DataTable dttre = null;
            string selectField = "m.Number as Number,m.MobileTele,b.TotalNetRecord,b.DTotalNetRecord,m.RegisterDate,";
          
               
                  

                    if (IsPlacement == "0")
                    {
                        selectField = selectField + "ZongRen=isnull(dTotalNetNum,0),";
                    }
                    else
                    {
                        selectField = selectField + "ZongRen=isnull(TotalNetNum,0),";
                    }
                    selectField = selectField + "Name=isnull(Name ,'姓名'),";
           
            selectField = selectField.Substring(0, selectField.Length - 1);


            if (IsPlacement == "0")
            {
                string sqls = @"select " + selectField + @" from MemberInfo m left join MemberInfoBalance" + @ExpectNum + @" b on m.Number=b.Number
                                            where b.direct=@direct and ExpectNum<=@ExpectNum  order by m.RegisterDate asc  ";
                SqlParameter[] sps = new SqlParameter[] { 
           new SqlParameter("@direct", thNumber),
           new SqlParameter("@ExpectNum", ExpectNum) 
           };
                dttre = DBHelper.ExecuteDataTable(sqls, sps, CommandType.Text);
            }
            return dttre;
        }


        public static string GetTree(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum,string language)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //判断自身是否是团队变色
            cmd.CommandText = "select PlacementColor from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
            cmd.Parameters.AddWithValue("@number", nodeid);
            cmd.Parameters.AddWithValue("@ManageNum", ManageNum);

            string rt = cmd.ExecuteScalar() + "";

            if (rt == "2")
            {
                cmd.CommandText = @"insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum)
                                    select Number,'#','#','#','#',@ManageNum from MemberInfo where placement=@nodeid and not exists(select 1 from NetWorkInfo where Number=MemberInfo.Number and ManageNum=@ManageNum)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.Parameters.AddWithValue("@nodeid", nodeid);

                cmd.ExecuteNonQuery();
            }

            if (rt == "2" || rt == "3")
            {
                cmd.CommandText = "update NetWorkInfo set PlacementColor='" + rt + "' where Number in (select Number from MemberInfo where placement=@nodeid) and ManageNum=@ManageNum and isnull(PlacementColor,'#')!='1' and isnull(PlacementColor,'#')!='0'";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nodeid", nodeid);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "select isnull(LayerBit1,0) from MemberInfoBalance" + @ExpectNum + @" where Number=@number";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@number", thNumber);
            int cs = Convert.ToInt32(cmd.ExecuteScalar());
            if (cs < 0)
                cs = 0;


            DataTable dt = GetWangLuoT(BrowserType, IsPlacement);

            string selectField = "m.Number as Number,IsChild=dbo.W_IsChild(m.Number,@ExpectNum),PlacementColor=isnull((select PlacementColor from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'),PlacementImg=isnull((select PlacementImg from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'),";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string n = dt.Rows[i]["Field"].ToString();
                if (n == "DaiShu")
                    selectField = selectField + "DaiShu=isnull(LayerBit1,0)-" + cs + ",";
                else if (n == "JiBie")
                {
                    if (language=="L001")
	                {
                        selectField = selectField + "JiBie=(select levelstr from bsco_level where levelflag = 0 and levelint=isnull(level,0)),";
	                }
                    else
                    {
                        selectField = selectField + "JiBie=(select L002 from T_translation t join BSCO_Level b on b.levelint=t.primarykey and t.tableName='BSCO_Level' and b.levelflag=0 and b.levelstr=t.description and levelint=isnull(level,0)),";
                    }
                }
                    
                else if (n == "XinGe")
                    selectField = selectField + "XinGe=isnull(CurrentOneMark,0),";
                else if (n == "XinWang")
                    selectField = selectField + "XinWang=isnull(CurrentTotalNetRecord,0),";
                else if (n == "TJ")
                    selectField = selectField + "b.Direct,";
                else if (n == "XinRen")
                    selectField = selectField + "XinRen=isnull(CurrentNewNetNum,0),";
                else if (n == "ZongRen")
                    selectField = selectField + "ZongRen=isnull(TotalNetNum,0),";
                else if (n == "ZongFen")
                    selectField = selectField + "ZongFen=isnull(TotalNetRecord ,0),";
                else if (n == "Name")
                    selectField = selectField + "Name=isnull(Name ,'昵称'),";
            }
            selectField = selectField.Substring(0, selectField.Length - 1);

            cmd.CommandText = @"select " + selectField + @" from MemberInfo m left outer join MemberInfoBalance" + @ExpectNum + @" b on m.Number=b.Number
                                            where b.placement=@placement and 1=1 and ExpectNum<=@ExpectNum  order by m.RegisterDate asc for xml raw('Hang'),elements,Root('Root')";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@placement", nodeid);
            cmd.Parameters.AddWithValue("@ExpectNum", ExpectNum);

            if (model == "s")
            {
                cmd.CommandText = cmd.CommandText.Replace("1=1", "m.Number=@Number");
                cmd.Parameters.AddWithValue("@Number", thNumber);
            }

            XmlReader reader = cmd.ExecuteXmlReader();
            reader.MoveToContent();

            string resultXml = reader.ReadOuterXml();

            reader.Close();
            cmd.Dispose();
            con.Close();

            return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + resultXml;
        }

        public static string GetTree_II(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum, string language)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //判断自身是否是团队变色
            cmd.CommandText = "select DirectColor from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
            cmd.Parameters.AddWithValue("@number", nodeid);
            cmd.Parameters.AddWithValue("@ManageNum", ManageNum);

            string rt = cmd.ExecuteScalar() + "";

            if (rt == "2")
            {
                cmd.CommandText = @"insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum)
                                    select Number,'#','#','#','#',@ManageNum from MemberInfo where Direct=@nodeid and not exists(select 1 from NetWorkInfo where Number=MemberInfo.Number and ManageNum=@ManageNum)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.Parameters.AddWithValue("@nodeid", nodeid);

                cmd.ExecuteNonQuery();
            }

            if (rt == "2" || rt == "3")
            {
                cmd.CommandText = "update NetWorkInfo set DirectColor='" + rt + "' where Number in (select Number from MemberInfo where Direct=@nodeid) and ManageNum=@ManageNum and isnull(DirectColor,'#')!='1' and isnull(DirectColor,'#')!='0'";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nodeid", nodeid);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "select isnull(LayerBit2,0) from MemberInfoBalance" + @ExpectNum + @" where Number=@number";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@number", thNumber);
            int cs = Convert.ToInt32(cmd.ExecuteScalar());
            if (cs < 0)
                cs = 0;


            DataTable dt = GetWangLuoT(BrowserType, IsPlacement);

            string selectField = @"m.Number as Number,IsChild=dbo.W_IsChild2(m.Number,@ExpectNum),PlacementColor=isnull((select DirectColor from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'),PlacementImg=isnull((select DirectImg from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'),";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string n = dt.Rows[i]["Field"].ToString();
                if (n == "DaiShu")
                    selectField = selectField + "DaiShu=isnull(LayerBit2,0)-" + cs + ",";
                else if (n == "JiBie")
                {
                    if (language == "L001")
                    {
                        selectField = selectField + "JiBie=(select levelstr from bsco_level where levelflag = 0 and levelint=isnull(level,0)),";
                    }
                    else
                    {
                        selectField = selectField + "JiBie=(select L002 from T_translation t join BSCO_Level b on b.levelint=t.primarykey and t.tableName='BSCO_Level' and b.levelflag=0 and b.levelstr=t.description and levelint=isnull(level,0)),";
                    }
                }

                else if (n == "XinGe")
                    selectField = selectField + "XinGe=isnull(CurrentOneMark,0),";
                else if (n == "XinWang")
                    selectField = selectField + "XinWang=isnull(DCurrentTotalNetRecord,0),";
                else if (n == "AZ")
                    selectField = selectField + "b.Placement,";
                else if (n == "XinRen")
                    selectField = selectField + "XinRen=isnull(DCurrentNewNetNum,0),";
                else if (n == "ZongRen")
                    selectField = selectField + "ZongRen=isnull(DTotalNetNum,0),";
                else if (n == "ZongFen")
                    selectField = selectField + "ZongFen=isnull(DTotalNetRecord ,0),";
                else if (n == "Name")
                    selectField = selectField + "Name=isnull(Name ,'昵称'),";
            }
            selectField = selectField.Substring(0, selectField.Length - 1);

            cmd.CommandText = @"select " + selectField + @" from MemberInfo m left outer join MemberInfoBalance" + @ExpectNum + @" b on m.Number=b.Number
                                            where b.Direct=@Direct and 1=1 and ExpectNum<=@ExpectNum  order by m.RegisterDate asc for xml raw('Hang'),elements,Root('Root')";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Direct", nodeid);
            cmd.Parameters.AddWithValue("@ExpectNum", ExpectNum);

            if (model == "s")
            {
                cmd.CommandText = cmd.CommandText.Replace("1=1", "m.Number=@Number");
                cmd.Parameters.AddWithValue("@Number", thNumber);
            }

            XmlReader reader = cmd.ExecuteXmlReader();
            reader.MoveToContent();

            string resultXml = reader.ReadOuterXml();

            reader.Close();
            cmd.Dispose();
            con.Close();

            return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + resultXml;
        }

        public static string GetTree_IIV(string nodeid, string ExpectNum, string thNumber, string model, string BrowserType, string IsPlacement, string ManageNum)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //判断自身是否是团队变色
            cmd.CommandText = "select DirectColor from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
            cmd.Parameters.AddWithValue("@number", nodeid);
            cmd.Parameters.AddWithValue("@ManageNum", ManageNum);

            string rt = cmd.ExecuteScalar() + "";

            if (rt == "2")
            {
                cmd.CommandText = @"insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum)
                                    select Number,'#','#','#','#',@ManageNum from MemberInfo where Direct=@nodeid and not exists(select 1 from NetWorkInfo where Number=MemberInfo.Number and ManageNum=@ManageNum)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.Parameters.AddWithValue("@nodeid", nodeid);

                cmd.ExecuteNonQuery();
            }

            if (rt == "2" || rt == "3")
            {
                cmd.CommandText = "update NetWorkInfo set DirectColor='" + rt + "' where Number in (select Number from MemberInfo where Direct=@nodeid) and ManageNum=@ManageNum and isnull(DirectColor,'#')!='1' and isnull(DirectColor,'#')!='0'";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nodeid", nodeid);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "select isnull(LayerBit2,0) from MemberInfoBalance" + @ExpectNum + @" where Number=@number";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@number", thNumber);
            int cs = Convert.ToInt32(cmd.ExecuteScalar());
            if (cs < 0)
                cs = 0;


            DataTable dt = GetWangLuoT(BrowserType, IsPlacement);

            string selectField = @"m.Number as Number,IsChild=dbo.W_IsChild2(m.Number,@ExpectNum),PlacementColor=isnull((select DirectColor from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'),PlacementImg=isnull((select DirectImg from dbo.NetWorkInfo where Number=m.Number and ManageNum='" + ManageNum + "'),'#'), isnull( case  when (SumVitality -  (select   min(SumVitality) from Vitality where SumVitality>0 and  exceptnum=@ExpectNum ))<0  then -1    else  ((SumVitality -  (select   min (SumVitality) from Vitality where SumVitality>0 and exceptnum=@ExpectNum))*256) /(select   max (SumVitality) - min (SumVitality) from Vitality where SumVitality>0 and exceptnum=@ExpectNum)   end ,0) as vitalitycount,";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string n = dt.Rows[i]["Field"].ToString();
                if (n == "DaiShu")
                    selectField = selectField + "DaiShu=isnull(b.LayerBit2,0)-" + cs + ",";
                else if (n == "JiBie")
                    selectField = selectField + "JiBie=(select levelstr from bsco_level where levelflag = 0 and levelint=isnull(level,0)),";
                else if (n == "XinGe")
                    selectField = selectField + "XinGe=isnull(b.CurrentOneMark,0),";
                else if (n == "XinWang")
                    selectField = selectField + "XinWang=isnull(b.CurrentTotalNetRecord,0),";
                else if (n == "AZ")
                    selectField = selectField + "b.Placement,";
                else if (n == "XinRen")
                    selectField = selectField + "XinRen=isnull(b.CurrentNewNetNum,0),";
                else if (n == "ZongRen")
                    selectField = selectField + "ZongRen=isnull(b.TotalNetNum,0),";
                else if (n == "ZongFen")
                    selectField = selectField + "ZongFen=isnull(b.TotalNetRecord ,0),";
                else if (n == "Name")
                    selectField = selectField + "Name=isnull(m.Name ,'昵称'),";
            }
            selectField = selectField.Substring(0, selectField.Length - 1);

//            cmd.CommandText = @"select " + selectField + @" from MemberInfo m left outer join MemberInfoBalance" + @ExpectNum + @" b on m.Number=b.Number left join Vitality v on m.number=v.number 
//                                            where b.Direct=@Direct and 1=1 and v.exceptnum=@ExpectNum and ExpectNum<=@ExpectNum order by m.RegisterDate asc for xml raw('Hang'),elements,Root('Root')";
            cmd.CommandText = @"select " + selectField + @" from MemberInfo m left outer join MemberInfoBalance" + @ExpectNum + @" b on m.Number=b.Number left join Vitality v on (m.number=v.number and v.exceptnum=@ExpectNum) 
                                            where b.Direct=@Direct and 1=1 and ExpectNum<=@ExpectNum order by m.RegisterDate asc for xml raw('Hang'),elements,Root('Root')";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Direct", nodeid);
            cmd.Parameters.AddWithValue("@ExpectNum", ExpectNum);

            if (model == "s")
            {
                cmd.CommandText = cmd.CommandText.Replace("1=1", "m.Number=@Number");
                cmd.Parameters.AddWithValue("@Number", thNumber);
            }

            XmlReader reader = cmd.ExecuteXmlReader();
            reader.MoveToContent();

            string resultXml = reader.ReadOuterXml();

            reader.Close();
            cmd.Dispose();
            con.Close();

            return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + resultXml;
        }


        //常用网络图
        //orderby="1"  左区
        //orderby="2"  右区
        public static CYWLTModel GetCYWLTModel(string number, string qs, string orderby)
        {

            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 Number,Name from MemberInfo where PlaceMent=@Number" + " and District=@QuShu", con);

            cmd.Parameters.AddWithValue("@Number", number);
            cmd.Parameters.AddWithValue("@QuShu", orderby);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            CYWLTModel cm = new CYWLTModel();

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                cm.Number = dt.Rows[0]["Number"] + "";
                cm.PetName = dt.Rows[0]["Name"] + "";
            }

            cmd.Parameters.Clear();
            cmd.CommandText = @" select TotalNetRecord,CurrentTotalNetRecord,sqsyyj=0,Level=(select Levelstr from dbo.BSCO_level where levelflag='0' and Levelint=m.level)
                                 from MemberInfoBalance" + qs + " m where Number='" + cm.Number + "'";

            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "tab2");
            dt = ds.Tables["tab2"];

            if (dt.Rows.Count == 0)
            {
                cm.Level = "无";
                cm.ZY = "0.00";
                cm.XY = "0.00";
                cm.SY = "0.00";
            }
            else
            {
                cm.Level = dt.Rows[0]["Level"] + "";
                cm.ZY = dt.Rows[0]["TotalNetRecord"] + "";
                cm.XY = dt.Rows[0]["CurrentTotalNetRecord"] + "";
                cm.SY = dt.Rows[0]["sqsyyj"] + "";
            }

            //获取他下级的左右区是否有业绩
            cmd.Parameters.Clear();
            cmd.CommandText = @" select count(1) as js from dbo.MemberOrder where Number=
                                (
                                select top 1 Number from MemberInfo where PlaceMent='" + cm.Number + @"' and District=1
                                ) and OrderExpectNum=" + qs;

            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
                cm.Left = "是";
            else
                cm.Left = "否";

            cmd.Parameters.Clear();
            cmd.CommandText = @" select count(1) as js from dbo.MemberOrder where Number=
                                (
                                select top 1 Number from MemberInfo where PlaceMent='" + cm.Number + @"' and District=2
                                ) and OrderExpectNum=" + qs;

            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
                cm.Right = "是";
            else
                cm.Right = "否";

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return cm;
        }

        public static CYWLTModel GetCYWLTModelII(string number, string qs, string cs, string notnumber)
        {

            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            string sql = "select top 1 Number,Name from MemberInfo where " + (cs == "1" ? "Number=@Number" : "PlaceMent=@Number and 1=1") + " order by district asc";

            if (notnumber != "")
                sql = sql.Replace("1=1", "Number!='" + notnumber + "'");

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@Number", number);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            CYWLTModel cm = new CYWLTModel();

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                cm.Number = dt.Rows[0]["Number"] + "";
                cm.PetName = dt.Rows[0]["Name"] + "";
            }

            cmd.Parameters.Clear();
            cmd.CommandText = @" select TotalNetRecord,CurrentTotalNetRecord,sqsyyj=0,Level=(select Levelstr from dbo.BSCO_level where levelflag='0' and Levelint=m.level)
                                 from MemberInfoBalance" + qs + " m where Number='" + cm.Number + "'";

            da = new SqlDataAdapter(cmd);
            da.Fill(ds, "tab2");
            dt = ds.Tables["tab2"];

            if (dt.Rows.Count == 0)
            {
                cm.Level = "无";
                cm.ZY = "0.00";
                cm.XY = "0.00";
                cm.SY = "0.00";
            }
            else
            {
                cm.Level = dt.Rows[0]["Level"] + "";
                cm.ZY = Convert.ToDouble(dt.Rows[0]["TotalNetRecord"]).ToString("0.00");
                cm.XY = Convert.ToDouble(dt.Rows[0]["CurrentTotalNetRecord"]).ToString("0.00");
                cm.SY = Convert.ToDouble(dt.Rows[0]["sqsyyj"]).ToString("0.00");
            }

            //获取他下级的左右区是否有业绩
            cmd.Parameters.Clear();
            cmd.CommandText = @" select count(1) as js from dbo.MemberOrder where Number=
                                (
                                select top 1 Number from MemberInfo where PlaceMent='" + cm.Number + @"' order by district asc
                                ) and OrderExpectNum=" + qs;

            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
                cm.Left = "是";
            else
                cm.Left = "否";

            if (cm.Left == "是")
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "select top 1 Number from MemberInfo where PlaceMent='" + cm.Number + @"' order by district asc";
                string leftNumber = cmd.ExecuteScalar().ToString();

                cmd.Parameters.Clear();
                cmd.CommandText = @" select count(1) as js from dbo.MemberOrder where Number=
                                (
                                select top 1 Number from MemberInfo where PlaceMent='" + cm.Number + @"' and Number!='" + leftNumber + @"' order by district asc
                                ) and OrderExpectNum=" + qs;

                if (Convert.ToInt32(cmd.ExecuteScalar()) >= 1)
                    cm.Right = "是";
                else
                    cm.Right = "否";
            }
            else
            {
                cm.Right = "否";
            }

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return cm;
        }

        public static string SetImage(string thnumber, string img, string ManageNum)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select count(1) from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    cmd.CommandText = "insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "select PlacementImg from NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                string rt = (cmd.ExecuteScalar() + "").ToLower();

                if (rt.IndexOf("#") == -1)
                    rt = "#" + rt;

                if (rt.IndexOf(img) == -1)
                    rt = rt + "^" + img;
                else
                    rt = rt.Replace("^" + img, "");


                cmd.CommandText = "update NetWorkInfo set PlacementImg='" + rt + "' where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.ExecuteNonQuery();

                return "OK";
            }
            catch
            {
                return "Error";
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }

        //获取会员区数
        public static string GetNumberQuShu(string number)
        {

            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select District from MemberInfo where Number=@Number", con);
            cmd.Parameters.AddWithValue("@Number", number);

            string qs = cmd.ExecuteScalar() + "";



            cmd.Dispose();
            con.Close();

            return qs;

        }

        public static string SetImage_II(string thnumber, string img, string ManageNum)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select count(1) from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    cmd.CommandText = "insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "select DirectImg from NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                string rt = (cmd.ExecuteScalar() + "").ToLower();

                if (rt.IndexOf("#") == -1)
                    rt = "#" + rt;

                if (rt.IndexOf(img) == -1)
                    rt = rt + "^" + img;
                else
                    rt = rt.Replace("^" + img, "");


                cmd.CommandText = "update NetWorkInfo set DirectImg='" + rt + "' where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                cmd.ExecuteNonQuery();

                return "OK";
            }
            catch
            {
                return "Error";
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        //12 - 11
        public static string SetColor(string thnumber, string model, string ExpectNum, string tuanNumber, string ManageNum)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = null;
            SqlCommand cmd = null;
            string color = "0";  //0 取消单个变色，1变色单个，2变色团队,3取消团队变色

            try
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;

                if (model == "s")
                {
                    cmd.CommandText = "select count(1) from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        cmd.CommandText = "insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@number", thnumber);
                        cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (model == "m")
                {
                    string[] hyarr = tuanNumber.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < hyarr.Length; i++)
                    {
                        cmd.CommandText = @"if(not exists(select 1 from NetWorkInfo where Number=@Number and ManageNum=@ManageNum))
                                            begin
	                                            insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)
                                            end";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@number", hyarr[i]);
                        cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                        cmd.ExecuteNonQuery();
                    }


                }

                cmd.CommandText = "select PlacementColor from NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                string rt = cmd.ExecuteScalar() + "";

                if (model == "s")  //单个变色
                {
                    if (rt == "1" || rt == "2")
                        color = "0";
                    else
                        color = "1";

                    cmd.CommandText = "update NetWorkInfo set PlacementColor='" + color + "' where Number=@number and ManageNum=@ManageNum";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();

                    return "OK";

                }
                else if (model == "m")  //团队变色
                {
                    if (rt == "1" || rt == "2")
                        color = "3";
                    else
                        color = "2";

                    tuanNumber = tuanNumber.Substring(0, tuanNumber.Length - 1);
                    string tempT = "'" + tuanNumber.Replace(",", "','") + "'";

                    cmd.CommandText = "update NetWorkInfo set PlacementColor='" + color + "' where Number in(" + tempT + ") and ManageNum=@ManageNum";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();

                    return "OK," + tuanNumber;
                }
                return "Error";

            }
            catch
            {
                return "Error";
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        public static string SetColor_II(string thnumber, string model, string ExpectNum, string tuanNumber, string ManageNum)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            SqlConnection con = null;
            SqlCommand cmd = null;
            string color = "0";//0 取消单个变色，1变色单个，2变色团队,3取消团队变色

            try
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;

                if (model == "s")
                {
                    cmd.CommandText = "select count(1) from dbo.NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    {
                        cmd.CommandText = "insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@number", thnumber);
                        cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (model == "m")
                {
                    string[] hyarr = tuanNumber.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < hyarr.Length; i++)
                    {
                        cmd.CommandText = @"if(not exists(select 1 from NetWorkInfo where Number=@Number and ManageNum=@ManageNum))
                                            begin
	                                            insert into NetWorkInfo(Number, PlacementColor, DirectColor, PlacementImg, DirectImg, ManageNum) values(@Number, '#', '#', '#', '#', @ManageNum)
                                            end";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@number", hyarr[i]);
                        cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                        cmd.ExecuteNonQuery();
                    }


                }

                cmd.CommandText = "select DirectColor from NetWorkInfo where Number=@number and ManageNum=@ManageNum";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@number", thnumber);
                cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                string rt = cmd.ExecuteScalar() + "";

                if (model == "s")  //单个变色
                {
                    if (rt == "1" || rt == "2")
                        color = "0";
                    else
                        color = "1";

                    cmd.CommandText = "update NetWorkInfo set DirectColor='" + color + "' where Number=@number and ManageNum=@ManageNum";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();

                    return "OK";

                }
                else if (model == "m")  //团队变色
                {
                    if (rt == "1" || rt == "2")
                        color = "3";
                    else
                        color = "2";

                    tuanNumber = tuanNumber.Substring(0, tuanNumber.Length - 1);
                    string tempT = "'" + tuanNumber.Replace(",", "','") + "'";

                    cmd.CommandText = "update NetWorkInfo set DirectColor='" + color + "' where Number in(" + tempT + ") and ManageNum=@ManageNum";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@number", thnumber);
                    cmd.Parameters.AddWithValue("@ManageNum", ManageNum);
                    cmd.ExecuteNonQuery();

                    return "OK," + tuanNumber;
                }
                return "Error";

            }
            catch
            {
                return "Error";
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }

        //判断是否有权限查看该网咯
        public static bool IsRoot(string StartNumber, string qs, string EndNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();


            if (dt.Rows.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('该网络图不存在')</script>");
                return false;
            }
            else
            {
                string _s = dt.Rows[0]["Number"].ToString().Split(' ')[0];
                if (_s == EndNumber)
                {
                    return true;
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('您没有权限查看！')</script>");
                    return false;
                }
            }
        }

        public static bool IsRoot_II(string StartNumber, string qs, string EndNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();


            if (dt.Rows.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('该网络图不存在')</script>");
                return false;
            }
            else
            {
                string _s = dt.Rows[0]["Number"].ToString().Split(' ')[0];
                if (_s == EndNumber)
                {
                    return true;
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('您没有权限查看！')</script>");
                    return false;
                }
            }
        }

        public static DataTable BindQS()
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select ExpectNum from CONFIG order by ExpectNum desc", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            ds.Dispose();
            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;


        }

        public static bool IsExistsNumber(string number)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(1) from MemberInfo where Number=@Number", con);
            cmd.Parameters.AddWithValue("@Number", number);

            int hs = Convert.ToInt32(cmd.ExecuteScalar())
                ;
            cmd.Dispose();
            con.Close();

            if (hs == 1)
                return true;

            return false;
        }


        public static string SetLianLuTu(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + "<a id='lltAID" + i + "' style='color:gray' href='SST_AZ.aspx?number=" + dt.Rows[i]["Placement"] + "&thNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&ExpectNum=" + Qs + "&EndNumber=" + EndNumber + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return _str;

        }

        public static string SetLianLuTuPhone(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + "  <li><a class='btn btn-primary' href='SST_AZ.aspx?topnum=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "'  >" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + " ></a> </li> ";
            }

            dt.Dispose();

            return _str;

        }


        public static string SetLianLuTu_II(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + "<a id='lltAID" + i + "' style='color:gray' href='SST_TJ.aspx?number=" + dt.Rows[i]["Direct"] + "&thNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&ExpectNum=" + Qs + "&EndNumber=" + EndNumber + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return _str;
        }

        public static string SetLianLuTu_IIPhone(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + " <li><a class='btn btn-primary' href='SST_TJ.aspx?topnum=" + dt.Rows[i]["name"].ToString().Split(' ')[0] + "'  >" + dt.Rows[i]["name"].ToString().Split(' ')[0] + " ></a> </li> ";
            }

            dt.Dispose();

            return _str;
        }

        public static object[] GetLLTTree(string qs, string EndNumber, string StartNumber, string BrowserType, string IsPlacement)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            ds.Dispose();
            da.Dispose();

            cmd.CommandText = "select Field, FieldName,IsVisible from NetWorkTitle where BrowserType=@BrowserType and IsPlacement=@IsPlacement order by sort asc";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@BrowserType",BrowserType),
                new SqlParameter("@IsPlacement",IsPlacement)
            };
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(param);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return new object[] { dr, dt };
        }

        public static object[] GetLLTTree_II(string qs, string EndNumber, string StartNumber, string BrowserType, string IsPlacement)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            ds.Dispose();
            da.Dispose();

            cmd.CommandText = "select Field, FieldName,IsVisible from NetWorkTitle where BrowserType=@BrowserType and IsPlacement=@IsPlacement order by sort asc";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@BrowserType",BrowserType),
                new SqlParameter("@IsPlacement",IsPlacement)
            };
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(param);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return new object[] { dr, dt };
        }

        public static string SetLianLuTu_L(string EndNumber, string _StartNumber, string Qs, string ysEndNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", _StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string str = "";
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                str = str + "<a href='LLT_AZ.aspx?endNumber=" + ysEndNumber + "&ThNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&qs=" + Qs + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return str;
        }

        public static string SetLianLuTu_L_II(string EndNumber, string _StartNumber, string Qs, string ysEndNumber)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", _StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string str = "";
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                str = str + "<a href='LLT_TJ.aspx?endNumber=" + ysEndNumber + "&ThNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&qs=" + Qs + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return str;
        }

        public static DataTable GetWLTField(string BrowserType, string IsPlacement)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "select ID,Field, FieldName,IsVisible from dbo.NetWorkTitle where BrowserType=" + BrowserType + " and IsPlacement=" + IsPlacement + "  order by sort asc";
            cmd.CommandText = "select a.ID,a.Field,b." + System.Web.HttpContext.Current.Session["languageCode"] + " as FieldName,a.IsVisible from NetWorkTitle a,T_translation b where a.FieldName=b.keyCode and a.BrowserType=" + BrowserType + " and a.IsPlacement=" + IsPlacement + " order by a.sort asc";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        public static int UpdWLTField(string f, string v, string id)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            /*cmd.CommandText = "update NetWorkTitle set FieldName=@FieldName,IsVisible=@Visible where ID=@ID";
            cmd.Parameters.AddWithValue("@FieldName", f);
            cmd.Parameters.AddWithValue("@Visible", v);
            cmd.Parameters.AddWithValue("@ID", id);*/

            SqlTransaction tran = null;
            tran = con.BeginTransaction();
            cmd.Transaction = tran;

            cmd.Parameters.AddWithValue("@FieldName", f);
            cmd.Parameters.AddWithValue("@Visible", v);
            cmd.Parameters.AddWithValue("@ID", id);

            cmd.CommandText = "update a set a.IsVisible=@Visible from NetWorkTitle a,T_translation b where a.FieldName=b.keyCode and a.ID=@ID";
            int hs = cmd.ExecuteNonQuery();

            cmd.CommandText = "update b set b." + System.Web.HttpContext.Current.Session["languageCode"] + "=@FieldName from NetWorkTitle a,T_translation b where a.FieldName=b.keyCode and a.ID=@ID";
            hs = hs + cmd.ExecuteNonQuery();

            if (hs == 2)
                tran.Commit();
            else
                tran.Rollback();

            cmd.Dispose();
            con.Close();

            return hs;
        }

        public static DataTable GetKYWL(string ManageID, string type)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select number From ViewManage Where ManageId = @ManageID and Type=@Type Order by Id asc", con);

            cmd.Parameters.AddWithValue("@ManageID", ManageID);
            cmd.Parameters.AddWithValue("@Type", type);


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        public static string GetMaxQS()
        {

            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select max(ExpectNum) from CONFIG", con);

            string maxqs = cmd.ExecuteScalar() + "";



            cmd.Dispose();
            con.Close();

            return maxqs;
        }

        public static string SetLianLuTu_CYWL(string EndNumber, string StartNumber, string Qs)
        {
            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + "<a id='lltAID" + i + "' style='color:gray' href='CommonlyNetwork.aspx?qsNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&EndNumber=" + EndNumber + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return _str;
        }

        public static string SetLianLuTu_CYWLII(string EndNumber, string StartNumber, string Qs)
        {
            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            string _str = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _str = _str + "<a id='lltAID" + i + "' style='color:gray' href='CommonlyNetworkII.aspx?qsNumber=" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "&EndNumber=" + EndNumber + "'>" + dt.Rows[i]["Number"].ToString().Split(' ')[0] + "(" + dt.Rows[i]["Number"].ToString().Split(' ')[1] + ")" + "</a> >> ";
            }

            dt.Dispose();

            return _str;
        }

        /// <summary>
        /// 安置
        /// </summary>
        /// <param name="EndNumber"></param>
        /// <param name="StartNumber"></param>
        /// <param name="Qs"></param>
        /// <returns></returns>
        public static DataTable SetLianLuTu_C(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="EndNumber"></param>
        /// <param name="StartNumber"></param>
        /// <param name="Qs"></param>
        /// <returns></returns>
        public static DataTable SetLianLuTu_C_II(string EndNumber, string StartNumber, string Qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("W_GetLianLuT2", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EndNumber", EndNumber);
            cmd.Parameters.AddWithValue("@StartNumber", StartNumber);
            cmd.Parameters.AddWithValue("@ExpectNum", Qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        public static DataTable GetGraphNet_AZ(string number, string qs, string isFirst)
        {
            string sql = "";

            if (isFirst == "1")
            {
                sql = "select m.Number,m.Name,m.MemberState,JiBie=(select levelstr from bsco_level where levelflag = 0 and levelint=isnull(level,0)),TotalNetRecord,CurrentOneMark,syyj from MemberInfo m left outer join MemberInfoBalance" + qs + " b on m.Number=b.Number where m.Number=@Number and ExpectNum<=@ExpectNum";
            }
            else
            {
                sql = "select m.Number,m.Name,m.MemberState,JiBie=(select levelstr from bsco_level where levelflag = 0 and levelint=isnull(level,0)),TotalNetRecord,CurrentOneMark,syyj from MemberInfo m left outer join MemberInfoBalance" + qs + " b on m.Number=b.Number where b.Placement=@Number and ExpectNum<=@ExpectNum order by m.District asc";
            }

            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Number", number);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds, "tab");

            DataTable dt = ds.Tables["tab"];

            da.Dispose();
            cmd.Dispose();
            con.Close();

            return dt;
        }

        public static bool IsExistsAZ(string number, string qs)
        {
            string constr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand("select count(1) from MemberInfo where Placement=@number and ExpectNum<=@ExpectNum", con);

            cmd.Parameters.AddWithValue("@number", number);
            cmd.Parameters.AddWithValue("@ExpectNum", qs);

            int ex = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.Dispose();
            con.Close();

            return ex > 0;
        }

        public static double GetYj(string field, string qs, string District, string placement)
        {
            string sql = @"select isnull(" + field + ",0) from MemberInfoBalance" + qs + @" where number=
                        (select top 1 Number from MemberInfo where placement=@placement and District=@District  and MemberState=1)";

            string constr = DBHelper.connString;

            SqlConnection con = new SqlConnection(constr);
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@placement", placement);
            cmd.Parameters.AddWithValue("@District", District);

            double yj = Convert.ToDouble(cmd.ExecuteScalar());

            cmd.Dispose();
            con.Close();

            return yj;
        }
    }
}