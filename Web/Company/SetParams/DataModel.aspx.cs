using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_SetParams_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_Click(object sender, EventArgs e)
    {
        string html = @" <ol>";
        if (HiddenField1.Value == "sctl")
        { 
        if (rj.Text == "" || dz.Text == "" || shijian.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请填满参数！');</script>", false);
            return;
        }
        else
        {
            
            double rujin =Convert.ToDouble(rj.Text);
            double dizeng = Convert.ToDouble(dz.Text);
            double date = Convert.ToDouble(shijian.Text);
            double maichuU = Convert.ToDouble(maichu.Text);
            double X = 0;
            double num = 0;
            double num1 = 0;
            for (int a = 0; a < date; a = a + 1)
            {
                if (a == 0)
                {
                    num += rujin;
                    num1 += rujin;
                    X = rujin;
                }
                else
                {
                    num1 += (num * maichuU / 100);
                    num +=X+( X * dizeng/100);
                   
                    X =X+(X * dizeng/100);
                }
                
            }
            
            html += @"  <li  > 总入金获得U："+ (num+num*0.05+num1*0.2).ToString("0.0000") + " </li>";
            html += @"  <li  > 用户卖出U：" + num1.ToString("0.0000") + " </li>";
            html += @"  <li  > 入金燃烧5%E获得U：" + (num * 0.05).ToString("0.0000") + " </li>";
            html += @"  <li  > 用户出金燃烧20%E获得U：" + (num1 * 0.2).ToString("0.0000") + " </li>";
            html += @"  <li  > 去掉用户卖出获得U：" + ((num + num * 0.05 + num1 * 0.2) - num1).ToString("0.0000") + " </li>";
            html += @"  <li  > 备注：用户卖出U的计算没办法考虑用户是否出局，所以仅供参考。 </li>";
            html += " </ol>";
            modelshow.InnerHtml = html;
            rj.Text = ""; 
            dz.Text = ""; 
            shijian.Text = "";
            maichu.Text = "";
            //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总共获得:"+num.ToString("0.0000")+"USDT！');</script>", false);
            //return;
        }
        }

        if (HiddenField1.Value == "rstl")
        {
            if (TextBox2.Text == "" || TextBox4.Text == "" || TextBox3.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('人数、递增、时间必填，衡定矿机等级默认0U！');</script>", false);
                return;
            }
            else
            {
                if (TextBox1.Text == "")
                {
                    TextBox1.Text = "0";
                }
                double dj = Convert.ToDouble(TextBox1.Text);
                double rujin = Convert.ToDouble(TextBox2.Text);
                double dizeng = Convert.ToDouble(TextBox4.Text);
                double date = Convert.ToDouble(TextBox3.Text);
               
                double X = 0;
                double num = 0;//人数
                double num1 = 0;//体量
                for (int a = 0; a < date; a = a + 1)
                {
                    if (a == 0)
                    {
                        num += rujin;
                       
                        X = rujin;
                    }
                    else
                    {
                      
                        num += X + (X * dizeng / 100);

                        X = X + (X * dizeng / 100);
                    }

                }
                num1 = num * dj;

                html += @"  <li  > 总入金获得U：" + num1.ToString("0.0000") + " </li>";
                html += @"  <li  > 入金燃烧5%E获得U：" + (num1 * 0.05).ToString("0.0000") + " </li>";
                html += @"  <li  > 总注册人数：" + num.ToString("0.0000") + " </li>";
               
                html += " </ol>";
                modelshow.InnerHtml = html;
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox4.Text = "";
                TextBox3.Text = "";
                //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总共获得:"+num.ToString("0.0000")+"USDT！');</script>", false);
                //return;
            }
        }

        if (HiddenField1.Value == "cjjs")
        {
            if (rj.Text == "" || dz.Text == "" || shijian.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请填满参数！');</script>", false);
                return;
            }
            else
            {

                double rujin = Convert.ToDouble(rj.Text);
                double dizeng = Convert.ToDouble(dz.Text);
                double date = Convert.ToDouble(shijian.Text);
                double maichuU = Convert.ToDouble(maichu.Text);
                double X = 0;
                double num = 0;
                double num1 = 0;
                for (int a = 0; a < date; a = a + 1)
                {
                    if (a == 0)
                    {
                        num += rujin;
                        num1 += rujin;
                        X = rujin;
                    }
                    else
                    {
                        num1 += (num * maichuU / 100);
                        num += X + (X * dizeng / 100);

                        X = X + (X * dizeng / 100);
                    }

                }

                html += @"  <li  > 总入金获得U：" + (num + num * 0.05 + num1 * 0.2).ToString("0.0000") + " </li>";
                html += @"  <li  > 用户卖出U：" + num1.ToString("0.0000") + " </li>";
                html += @"  <li  > 入金燃烧E获得U：" + (num * 0.05).ToString("0.0000") + " </li>";
                html += @"  <li  > 用户出金燃烧E获得U：" + (num1 * 0.2).ToString("0.0000") + " </li>";
                html += @"  <li  > 去掉用户卖出获得U：" + ((num + num * 0.05 + num1 * 0.2) - num1).ToString("0.0000") + " </li>";
                html += @"  <li  > 备注：用户卖出U的计算没办法考虑用户是否出局，所以仅供参考。 </li>";
                html += " </ol>";
                modelshow.InnerHtml = html;
                rj.Text = "";
                dz.Text = "";
                shijian.Text = "";
                maichu.Text = "";
                //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('总共获得:"+num.ToString("0.0000")+"USDT！');</script>", false);
                //return;
            }
        }
    }
}