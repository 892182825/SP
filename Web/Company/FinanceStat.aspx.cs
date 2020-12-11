using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.MoneyFlows;
using System.Collections.Generic;
using Model.Other;
using BLL.CommonClass;
using BLL.Registration_declarations;

public partial class Company_FinanceStat : BLL.TranslationBase
{
    double rateNum = 1;//防止除数为0
    public string msg;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceFinanceStat);

        if (!IsPostBack)
        {

            //绑定币种  www-b874dce8700
            BindCurrency();
            CommonDataBLL.BindQishuList(this.DropQiShu1, false);
            CommonDataBLL.BindQishuList(this.DropQiShu2, false);
            this.DropQiShu2.SelectedValue = CommonDataBLL.getMaxqishu().ToString();
            int value = CommonDataBLL.getMaxqishu();
                       
            if (value <= 10)
            {
                this.DropQiShu1.SelectedValue = "1";
                
            }
            else
            {
                this.DropQiShu1.SelectedValue = (value-10).ToString();
            }

            this.DropQiShu2.SelectedValue = value.ToString();

            rateNum = ReleaseBLL.GetRate(Convert.ToInt32(this.DropCurrency.SelectedValue));
            Graph();
        }

        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.DropDownList1,
                new string[][]{
                    new string []{"000322","金额"},
                    new string []{"001321","PV"}});
        this.TranControls(this.Button1, new string[][] { new string[] { "000340", "查询" } });
    }
    //绑定币种
    private void BindCurrency()
    {
        
        IList<CurrencyModel> list = RemittancesBLL.GetCurrency();
        foreach (CurrencyModel info in list)
        {
            string str = CommonDataBLL.GetLanguageStr(info.ID, "Currency", "Name");
            this.DropCurrency.Items.Add(new ListItem(str, info.ID.ToString()));
        }

        
    }
    public void Graph()
    {
        rateNum = ReleaseBLL.GetRate(Convert.ToInt32(this.DropCurrency.SelectedValue));       
        string vqs = Request["qs"];
        if (vqs == null) // 收支走势图
        {
            this.td1.Visible = true;
            this.GO_Back.Visible = false;
            this.Label1.Text = GetTran("001323", "收支走势图");
            int hang, lie;
            
            if (Convert.ToInt32(this.DropQiShu2.SelectedValue) < Convert.ToInt32(this.DropQiShu1.SelectedValue))
            {
                msg = "<script>alert('终止期数必须大于等于起始期数！');</script>";
                return;
            }
            hang = Convert.ToInt32(this.DropQiShu2.SelectedValue) - Convert.ToInt32(this.DropQiShu1.SelectedValue) + 1+1 ;// 加1是因为第1行为标题所占用
            lie = 6;								 // 总共6列
            string[,] zoushi = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = GetTran("000045", "期数");
            zoushi[0, 1] = GetTran("001360", "细节");
            if (ViewState["PV"] != null)
            {
                zoushi[0, 2] = GetTran("001324", "总收入PV");
                zoushi[0, 3] = GetTran("001325", "总支出(PV)");
            }
            else
            {
                zoushi[0, 2] = GetTran("001327", "总收入(元)");
                zoushi[0, 3] = GetTran("001328", "总支出(元)");
            }
            zoushi[0, 4] = GetTran("001329", "拨出率（%）");
            zoushi[0, 5] = GetTran("001330", "拨出率走势");

            
            ////是显示pv还是金额
            IList<BochulvModel> list = null;
           

            if (ViewState["PV"] != null)
            {
                list = ReleaseBLL.GetTotalPV(this.DropQiShu1.SelectedValue, this.DropQiShu2.SelectedValue);
            }
            else
            {
                list = ReleaseBLL.GetTotalBonus(rateNum, this.DropQiShu1.SelectedValue, this.DropQiShu2.SelectedValue);
            }

            int num = 0;
            foreach (BochulvModel info in list)
            {
                num++;
                zoushi[num, 0] = info.ExpectNum.ToString();
                zoushi[num, 1] = "FinanceStat.aspx?qs=" + info.ExpectNum.ToString();
                zoushi[num, 3] = (Convert.ToDouble(info.TotalBonus)).ToString("f2");
                zoushi[num, 2] = (Convert.ToDouble(info.Totalmoney)).ToString("f2");

                double a = Convert.ToDouble(info.TotalBonus);
                double b = Convert.ToDouble(info.Totalmoney);
                double c = 0.0;
                if (b == 0)
                {
                    c = 0;
                }
                else
                {
                    c = a * 100 / b;
                }

                if (c <= 0)
                {
                    zoushi[num, 4] = "0";
                    zoushi[num, 5] = "0";
                }
                else
                {
                    zoushi[num, 4] = Convert.ToDouble(Math.Round(c, 2).ToString()).ToString("f2");
                    zoushi[num, 5] = Convert.ToDouble(Math.Round(c, 2).ToString()).ToString("f2");
                }
                
            }

            zoushi[hang - 1, 0] = DropQiShu2.SelectedValue;						// 期数。
            zoushi[hang - 1, 1] = "FinanceStat.aspx?qs=" + this.DropQiShu2.SelectedValue;  // 连接，根据期数参数来显示详细信息。
            // 从数据库读取并处理,tempmoney
            double CurrentSolidSend = 0;
            double CurrentOneMoney = 0;
            int ExpectNum = Convert.ToInt32(this.DropQiShu2.SelectedValue);
            if (ExpectNum == CommonDataBLL.getMaxqishu())
            {
                //PV
                if (ViewState["PV"] != null)
                {
                    ReleaseBLL.GetCurrentPV(ExpectNum, out CurrentSolidSend, out CurrentOneMoney);
                }
                else
                {
                    ReleaseBLL.GetTotalMoney(rateNum, ExpectNum, out CurrentSolidSend, out CurrentOneMoney);
                }
            }
            
            // 给数据赋值
            // 总支出
            zoushi[hang - 1, 3] = CurrentSolidSend.ToString();
            if (zoushi[hang - 1, 3] == String.Empty)
            {
                zoushi[hang - 1, 3] = "0";
            }

            // 总收入/拨出率/走势图
            zoushi[hang - 1, 2] = CurrentOneMoney.ToString();
            if (zoushi[hang - 1, 2] == String.Empty)
            {
                zoushi[hang - 1, 2] = "0";
            }
            if (zoushi[hang - 1, 2] == "0")
            {
                zoushi[hang - 1, 4] = "0";
                zoushi[hang - 1, 5] = "0";
            }
            else
            {
                double a = Convert.ToDouble(zoushi[hang - 1, 3]);
                double b = Convert.ToDouble(zoushi[hang - 1, 2]);
                double c = 0.0;
                if (b == 0)
                {
                    c = 0;
                }
                else
                {
                    c = a * 100 / b;
                }

                if (c <= 0)
                {
                    zoushi[hang - 1, 4] = "0";
                    zoushi[hang - 1, 5] = "0";
                }
                else
                {
                    zoushi[hang - 1, 4] = Convert.ToDouble(Math.Round(c, 2).ToString()).ToString("f2");
                    zoushi[hang - 1, 5] = Convert.ToDouble(Math.Round(c, 2).ToString()).ToString("f2");
                }
            }
            //}
            for (int m = 0; m < hang; m++)
            {
                TableRow myRow2 = new TableRow();
                for (int i = 0; i < lie; i++)
                {
                    TableCell myCell = new TableCell();
                    if (m == 0) // 标题行－第0行
                    {
                        #region
                        switch (i)
                        {
                            case 0:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 1:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 2:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 3:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 4:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 5:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;

                        }
                        myCell.Style.Add("background-image", "images/tabledp.gif");
                        myCell.ForeColor = System.Drawing.Color.White;
                        #endregion
                    }
                    else     // 数据行－第1行以后
                    {
                        #region
                        switch (i)// 区分不同的显示，此处第一列为超级连接，最后以列为图形，其他为文字
                        {
                            case 0:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 1:
                                HyperLink HP = new HyperLink();
                                HP.Text = GetTran("000399", "查看详细");
                                HP.NavigateUrl = zoushi[m, i].ToString();
                                myCell.Controls.Add(HP);
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 4:

                                myCell.Text = (Convert.ToDouble(zoushi[m, i]) / 100).ToString("P");
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 5:
                                int a = Convert.ToInt32(Convert.ToDouble(zoushi[m, 4]));
                                Image Img = new Image();
                                Img.ImageUrl = "images/jsline.gif";
                                
                                if (a <= 0)
                                {
                                    
                                    a = 0;
                                }
                                Img.Width = a;
                                Img.Height = 20;
                                myCell.Controls.Add(Img);
                                
                                myCell.HorizontalAlign = HorizontalAlign.Left;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;

                            default:
                                
                                if (ViewState["PV"] != null)
                                    myCell.Text = String.Format("{0:f0}", Convert.ToDecimal(zoushi[m, i]));
                                else
                                    myCell.Text = String.Format("{0:f2}", Convert.ToDecimal(zoushi[m, i]));
                                
                                myCell.HorizontalAlign = HorizontalAlign.Right;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                        }
                        #endregion
                    }

                }

                myRow2.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
                myRow2.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
                this.Table1.Rows.Add(myRow2);
            }

            GetTotal();
        }
        else // 详细信息
        {
            this.td1.Visible = false;
            this.Label1.Text = GetTran("000156", "第 ") + vqs.ToString() + GetTran("001337", " 期奖金收支详图");
            int hang, lie;
            hang = 2;
                //7;					  // 总共是多少项奖金就是奖金数+1
            lie = 5;						 // 总共4列，分别是奖金名、奖金数额、拨出率、拨出率图
            string[,] zoushi = new string[hang, lie];
            // 初始化
            for (int m = 0; m < hang; m++)
            {
                for (int n = 0; n < lie; n++)
                {
                    zoushi[m, n] = "";
                }
            }
            // 赋值－标题－总共6列
            zoushi[0, 0] = GetTran("000012", "序号");
            zoushi[0, 1] = GetTran("000243", "奖金");

            zoushi[0, 2] = GetTran("001346", "数额");
            zoushi[0, 3] = GetTran("001329", "拨出率(%)");
            zoushi[0, 4] = GetTran("001351", "拨出率图");  // 不显示

            //模板的代码

            //zoushi[1, 1] = "无";
            zoushi[1, 1] = "业绩奖";
            //zoushi[2, 1] = "回本奖";
            //zoushi[3, 1] = "大区奖";
            //zoushi[4, 1] = "小区奖";
            //zoushi[5, 1] = "永续奖";
            //zoushi[6, 1] = "进步奖";
            //zoushi[7, 1] = "网平台综合管理费";
            //zoushi[8, 1] = "网扣福利奖金";
            //zoushi[9, 1] = "网扣重复消费";

            //zoushi[1, 4] = "Bonus0";
            zoushi[1, 4] = "Bonus0";
            //zoushi[2, 4] = "Bonus2";
            //zoushi[3, 4] = "Bonus3";
            //zoushi[4, 4] = "Bonus4";
            //zoushi[5, 4] = "Bonus5";
            //zoushi[6, 4] = "Bonus6";
            //zoushi[7, 4] = "Kougl";
            //zoushi[8, 4] = "Koufl";
            //zoushi[9, 4] = "Koufx";
          


            double moneyZongShouRu = 1;
            string[,] zoushi1 = new string[hang, lie];
            ReleaseBLL.GetOneBonus(rateNum, int.Parse(vqs), hang, zoushi, out zoushi1, out moneyZongShouRu);
            zoushi = zoushi1;

            for (int m = 1; m < hang; m++)
            {
                for (int n = 0; n < lie - 1; n++)
                {
                    if (n == 3 || n == 4)
                    {
                        double a = Convert.ToDouble(zoushi[m, 2]);
                        double c = 0;

                        if (moneyZongShouRu == 0)
                        {
                            c = 0;
                        }
                        else
                        {
                            c = a * 100 / moneyZongShouRu;
                        }

                        if (c<0)
                        {
                            c = 0;
                        }


                        zoushi[m, n] = c.ToString();
                    }
                    else
                    {
                        if (zoushi[m, n] == String.Empty)
                        {
                            zoushi[m, n] = "0";
                        }
                    }

                }
            }

            for (int m = 0; m < hang; m++)
            {
                TableRow myRow2 = new TableRow();
                for (int i = 0; i < lie; i++)
                {
                    TableCell myCell = new TableCell();
                    if (m == 0) // 标题行－第0行
                    {
                        myCell.Text = zoushi[m, i].ToString();
                        myCell.Height = 25;
                        myCell.HorizontalAlign = HorizontalAlign.Center;
                        myCell.Wrap = false;
                        myCell.Style.Add("background-image", "images/tabledp.gif");
                        myCell.ForeColor = System.Drawing.Color.White;
                        myRow2.Cells.Add(myCell);

                    }
                    else     // 数据行－第1行以后
                    {
                        switch (i)// 区分不同的显示，此处第一列为超级连接，最后以列为图形，其他为文字
                        {
                            case 4:
                                int a = 0;
                                try
                                {
                                    a = Convert.ToInt32(Convert.ToDouble(zoushi[m, 3]));
                                }
                                catch
                                {
                                    a = 0;
                                }

                                Image Img = new Image();
                                Img.ImageUrl = "images/jsline.gif";
                                try
                                {
                                    Img.Width = a;
                                }

                                catch
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + a.ToString() + "')</script>");
                                }
                                Img.Height = 20;
                                myCell.Controls.Add(Img);
                                
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 3:
                                myCell.Text = (Convert.ToDouble(zoushi[m, i]) / 100).ToString("P");
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 2:
                                myCell.Text = (Convert.ToDecimal(zoushi[m, i])).ToString("f2");
                                
                                myCell.HorizontalAlign = HorizontalAlign.Right;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            case 1:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                            default:
                                myCell.Text = zoushi[m, i].ToString();
                                
                                myCell.HorizontalAlign = HorizontalAlign.Center;
                                myCell.Wrap = false;
                                myRow2.Cells.Add(myCell);
                                break;
                        }
                    }
                }
                myRow2.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
                myRow2.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
                this.Table1.Rows.Add(myRow2);
            }
        }
    }
    private void GetTotal()
    {
        #region "Total"
        double totalIncome = 0.0;
        double totalPayout = 0.0;


        //PV
        if (ViewState["PV"] != null)
        {
            
            ReleaseBLL.GetPV(Convert.ToInt32(this.DropQiShu1.SelectedValue), Convert.ToInt32(this.DropQiShu2.SelectedValue), out totalPayout, out totalIncome);
        }
        else
        {
            
            ReleaseBLL.GetBonus(Convert.ToInt32(this.DropQiShu1.SelectedValue), Convert.ToInt32(this.DropQiShu2.SelectedValue), rateNum, out totalIncome, out totalPayout);
        }
        
        TableRow tr = new TableRow();
        TableCell myCell;

        myCell = new TableCell();
        myCell.Text = GetTran("001358", "所有期");
        myCell.HorizontalAlign = HorizontalAlign.Center;
        tr.Cells.Add(myCell);

        myCell = new TableCell();
        myCell.Text = "________ ";
        myCell.HorizontalAlign = HorizontalAlign.Center;
        tr.Cells.Add(myCell);

        if (ViewState["PV"] != null)
        {
            myCell = new TableCell();
            myCell.Text = totalIncome.ToString("f0");
            myCell.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(myCell);

            myCell = new TableCell();
            myCell.Text = totalPayout.ToString("f0");
            myCell.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(myCell);
        }
        else
        {
            myCell = new TableCell();
            myCell.Text = totalIncome.ToString("C");
            myCell.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(myCell);

            myCell = new TableCell();
            myCell.Text = totalPayout.ToString("C");
            myCell.HorizontalAlign = HorizontalAlign.Right;
            tr.Cells.Add(myCell);
        }

        double per = 0.0;
        if (totalIncome!=0)
        {
            per = totalPayout * 100 / totalIncome;
        }
        if (per<0)
        {
            per = 0.0;
        }
        myCell = new TableCell();
        myCell.Text = (per / 100).ToString("P");
        myCell.HorizontalAlign = HorizontalAlign.Center;
        tr.Cells.Add(myCell);

        myCell = new TableCell();
        myCell.HorizontalAlign = HorizontalAlign.Left;
        Image Img = new Image();
        Img.ImageUrl = "images/jsline.gif";
        try
        {
            Img.Width = (int)per;
        }
        catch
        {
            
        }
        Img.Height = 20;
        myCell.Controls.Add(Img);
        myCell.Height = 25;
        myCell.Width = 100;
        myCell.Wrap = false;
        tr.Cells.Add(myCell);

        Table1.Rows.Add(tr);
        #endregion
    }
    protected void DropCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Default_Currency"] = DropCurrency.SelectedItem.Text;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Table1.Rows.Clear();


        if (this.DropDownList1.SelectedValue == "1")
        {
            ViewState["PV"] = "PV";
        }
        else
        {
            ViewState["PV"] = null;
        }
        Graph();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rateNum = ReleaseBLL.GetRate(Convert.ToInt32(this.DropCurrency.SelectedValue));
        DropDownList1_SelectedIndexChanged(null, null);
        this.Page.RegisterStartupScript("", "<script>typechange();</script>");
    }
}
