<%@ Application Language="C#" %>

<script runat="server">

    public static System.Timers.Timer ptime;
    void Application_Start(object sender, EventArgs e)
    {
        // BLL.CommonClass.Login.MaxDateTime = Convert.ToDateTime("2018-08-31 11:09:15");//限制时间
        //在应用程序启动时运行的代码
        Application["jinzhi"] = "F";
        Application["maxqishu"] = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        Application["TopManageID"] = BLL.CommonClass.CommonDataBLL.GetTopManageID(1);
        // Application["TopStoreID"] = BLL.CommonClass.CommonDataBLL.GetTopManageID(2);
        Application["TopMemberID"] = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);
        Application["NetWorkDisplayStatus"] = "1111111";
        Application["Dict_English"] = new System.Collections.Hashtable();

        /*
        //自动结算  
        ptime = new System.Timers.Timer(Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["zdjs"]));
        ptime.Elapsed += new System.Timers.ElapsedEventHandler(TimeMethod);
        ptime.AutoReset = true;
        ptime.Enabled = true;
        GC.KeepAlive(ptime);
        ptime.Start();*/
        ptime = new System.Timers.Timer(3600000);
        ptime.Elapsed += new System.Timers.ElapsedEventHandler(  CleaMemberProc );
        ptime.AutoReset = true;
        ptime.Enabled = true;
        GC.KeepAlive(ptime);
        ptime.Start();

        DAL.DBHelper.ExecuteNonQuery("insert into ZiDongJieSuanJiLu(ExceptNum, Remark) values('-1','应用程序开始，开始时间：" + DateTime.Now + "')");
    }
    //24小时删除未激活的会员信息
    void CleaMemberProc(object sender, System.Timers.ElapsedEventArgs e)
    {

        DAL.DBHelper.ExecuteNonQuery("ClearMemberOuttimeNopay", System.Data.CommandType.StoredProcedure);


    }


    void TimeMethod(object sender, System.Timers.ElapsedEventArgs e)
    {
        string maxqs="";

        System.Data.SqlClient.SqlConnection con = null;
        System.Data.SqlClient.SqlTransaction tran = null;
        System.Data.SqlClient.SqlCommand cmd = null;

        int strG = -100;
        int strD = -100;
        int strH = -100;
        int strB = -100;

        try
        {
            string nowTime = DateTime.Now.ToString();

            con = new System.Data.SqlClient.SqlConnection(DAL.DBHelper.connString);
            con.Open();//先打开在事务

            tran = con.BeginTransaction();

            cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Transaction = tran;
            cmd.Connection = con;


            cmd.CommandText="select qiyong from dbo.zidongjiesuan";
            cmd.CommandType = System.Data.CommandType.Text;

            if (cmd.ExecuteScalar().ToString() == "1")//启用
            {
                cmd.CommandText = "procZDJS";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@nowDataTime", nowTime);
                cmd.Parameters.Add("@rt", System.Data.SqlDbType.Int);

                cmd.Parameters["@rt"].Direction = System.Data.ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                if (cmd.Parameters["@rt"].Value.ToString() == "1")
                {
                    //调用结算--创建新一期

                    string server = DAL.DBHelper.connString;
                    char[] de = { ';', '=' };
                    string[] dd = server.Split(de);

                    string host = dd[1].Replace("(", "").Replace(")", "");
                    string jijieSTR1 = "1";

                    maxqs = DAL.DBHelper.ExecuteScalar("SELECT max(expectnum) FROM config ").ToString();

                    string exeParam = "," + maxqs + "," + host + "," + dd[5] + "," + dd[7] + "," + dd[3] + "," + jijieSTR1 + "," + "0";

                    string bdpath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();

                    string path = bdpath + "Company\\jiesuan\\config.txt";

                    using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write))
                    {
                        System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fs);
                        streamWriter.WriteLine(exeParam);
                        streamWriter.Close();
                        fs.Close();
                    }

                    int temp = Convert.ToInt32(maxqs);


                    int exist_got = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("SELECT COUNT(1) FROM PayControl WHERE ExpectNum=" + temp));
                    if (exist_got > 0)	//奖金已添加  不能结算
                    {
                        throw new Exception("当前期奖金已发放，不能自动结算");
                    }
                    else
                    {
                        if (!new BLL.CommonClass.RunExe().IsRun(BLL.CommonClass.CommonDataBLL.JiesuanProgramFilename))//判断进程中是否含有结算进程
                        {
                            strG = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select SystemValue from setSystem where SystemName='G'"));
                            strD = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select SystemValue from setSystem where SystemName='D'"));
                            strH = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select SystemValue from setSystem where SystemName='H'"));
                            strB = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select SystemValue from setSystem where SystemName='B'"));

                            DAL.DBHelper.ExecuteNonQuery(tran, "update setsystem set SystemValue='0'", null, System.Data.CommandType.Text);


                            System.Diagnostics.Process.Start(bdpath + "\\Company\\jiesuan" + "\\" + BLL.CommonClass.CommonDataBLL.JiesuanProgramFilename + ".exe");

                            if (DAL.DBHelper.ExecuteScalar("select isCNewQi from dbo.zidongjiesuan").ToString() == "1")
                            {
                                //创建新一期
                                cmd.CommandText = "CreateNewQi";
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("@newqishu", temp + 1);

                                cmd.ExecuteNonQuery();

                                cmd.CommandText = "UPDATE CONFIG SET Date=CONVERT(varchar(100), GETDATE(), 112) WHERE expectnum=(SELECT MAX(expectnum) FROM CONFIG)";

                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.ExecuteNonQuery();
                            }

                            cmd.CommandText = "update zidongjiesuan set jiesuantime=convert(nvarchar(50),convert(datetime,jiesuantime)+jiesuanZQ,20)";
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.Clear();
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update Config set jsflag=1,jsnum=jsnum+1 WHERE expectnum=" + temp;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.Clear();
                            cmd.ExecuteNonQuery();

                            tran.Commit();
                            HttpContext.Current.Application["maxqishu"] = null;
                            InsertZDJSJiLu(maxqs, "自动结算成功");
                        }
                        else
                        {
                            throw new Exception("进程中包含结算进程，不能自动结算，如果一直出现这个问题请与管理员联系");
                        }
                    }
                }
                else
                    tran.Commit();
            }
            else
                tran.Commit();

        }
        catch (Exception ee)
        {
            if (tran != null)
                tran.Rollback();

            InsertZDJSJiLu(maxqs, ee.Message);

        }
        finally
        {
            if (strG != -100 && strD != -100 && strH != -100 && strB != -100)
            {
                DAL.DBHelper.ExecuteNonQuery(@"update setsystem set SystemValue='" + strG + @"' where SystemName='G'
                                            update setsystem set SystemValue='" + strD + @"' where SystemName='D'
                                            update setsystem set SystemValue='" + strH + @"' where SystemName='H'
                                            update setsystem set SystemValue='" + strB + @"' where SystemName='B'");
            }

            if(cmd!=null)
                cmd.Dispose();
            if (tran != null)
                tran.Dispose();
            if (con != null)
                con.Close();
        }
    }

    void InsertZDJSJiLu(string qs, string remark)
    {
        DAL.DBHelper.ExecuteNonQuery("insert into ZiDongJieSuanJiLu(ExceptNum, Remark) values('" + qs + "','" + remark + "')");
    }

    void Application_End(object sender, EventArgs e)
    {
        //在应用程序关闭时运行的代码
        DAL.DBHelper.ExecuteNonQuery("insert into ZiDongJieSuanJiLu(ExceptNum, Remark) values('-1','应用程序停止，停止时间：" + DateTime.Now + "')");

        //防止应用程序池清空后，没有人访问网站，无法启动app_strar
        System.Threading.Thread.Sleep(1000);

        //这里设置你的web地址，可以随便指向你的任意一个aspx页面甚至不存在的页面，目的是要激发Application_Start
        string url = System.Configuration.ConfigurationSettings.AppSettings["redurl"].ToString();
        System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
        System.IO.Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流

    }

    void Application_Error(object sender, EventArgs e)
    {
        //在出现未处理的错误时运行的代码

    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        string url = Request.Url.ToString().ToLower();
        if (url.IndexOf("fckeditor") < 0)
        {//排除编辑器页面xyc
            ProcessRequest pr = new ProcessRequest();
            pr.StartProcessRequest();
        }
        //判断当前时间是否在工作时间段内
        DateTime timeStr = DateTime.Now;
        string _strWorkingDayAM = "00:00";//工作时间上午08:30
        string _strWorkingDayPM = "00:01";
        TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
        TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

        //string time1 = "2017-2-17 8:10:00";
        DateTime t1 = Convert.ToDateTime(timeStr);

        TimeSpan dspNow = t1.TimeOfDay;
        if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
        {

            int postdz = CommandAPI.CoinPrice("CoinA");
            //rmoney.Text = rspp;
            
        }

        string rawUrl = Request.RawUrl;
        rawUrl = rawUrl.Replace("html", "aspx");
        Context.RewritePath(rawUrl);

    }

    void Application_EndRequest(object sender, EventArgs e)
    {
        //if (System.Web.HttpContext.Current.Request.Url.ToString().ToLower().IndexOf("index.aspx") == -1)
        //{
        //    if (System.Web.HttpContext.Current.Session["UserType"] == null)
        //        System.Web.HttpContext.Current.Response.Redirect("../Logout.aspx");
        //}
    }

    void Application_AcquireRequestState(object sender, EventArgs e)
    {
        isTimeruse();
        string url = Request.Url.ToString().ToLower();

        if (url.Contains(".aspx")) //更新超时时间-重新定义为30分钟
        {
            Session.Timeout = 30;
            Session["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
        }

        if (url.ToLower().IndexOf("refurbish.aspx") < 0 && url.ToLower().IndexOf("ajaxclass") < 0 && url.ToLower().IndexOf("fckediter") > 0)
        {
            //更新超时时间
            try
            {
                Session["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
            }
            catch(Exception)
            {
            }
            //Application["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(Convert.ToInt32(Application["out"]));
        }
        if (url.IndexOf("index.aspx") > 0)
        {
            if (BLL.CommonClass.Login.isDenyLogin())//是不否超出限时
            {
                Response.Redirect(BLL.CommonClass.CommonDataBLL.GetPath("error.aspx", 1));
                Response.End();
            }
        }
    }
    void isTimeruse()
    {
        if (!(ptime != null && ptime.Enabled))
        {
            if (ptime != null && ptime.Enabled == false)
            {
                ptime.Enabled = true;
                GC.KeepAlive(ptime);
                ptime.Start();
            }
            else
                if (ptime == null)
            {
                ptime = new System.Timers.Timer(Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings["zdjs"]));
                ptime.Elapsed += new System.Timers.ElapsedEventHandler(TimeMethod);
                ptime.AutoReset = true;
                ptime.Enabled = true;
                GC.KeepAlive(ptime);
                ptime.Start();
            }
        }
    }
    void Session_Start(object sender, EventArgs e)
    {
        //Application["out"] = HttpContext.Current.Session.Timeout;
        Session["nowqishu"] = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        Session["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);

    }

    void Session_End(object sender, EventArgs e)
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

        //if (System.Web.HttpContext.Current.Request.Url.ToString().IndexOf("index.aspx") != -1)
        //{
        //    if (System.Web.HttpContext.Current.Session.Count == 0)
        //        System.Web.HttpContext.Current.Response.Redirect("../Logout.aspx");
        //}
        //System.Web.HttpContext.Current.Response.Redirect("../Logout.aspx");
    }

</script>