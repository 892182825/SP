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

///Add Namespace
using BLL.other.Company;
using BLL.CommonClass;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-23
 * 对应菜单:    系统管理->参数设置
 */

public partial class Company_SetParams_MemBaseLine : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        if (!IsPostBack)
        {
            ///获取会员订单底线金额
            txtMemBaseLine.Value = SetParametersBLL.GetMemOrderLineOrderBaseLine().ToString();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.lbtnReturn, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.btnOK, new string[][] { new string[] { "002190", "确 定" } });
    }
    
    /// <summary>
    /// 确定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (txtMemBaseLine.Value.Trim() == "" || txtMemBaseLine.Value.Trim() == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002248", "会员报单底线不能为空!")));
        }

        else
        {
            try
            {
                double orderBaseLineMoney = Convert.ToDouble(txtMemBaseLine.Value.Trim());
                if (0 <= orderBaseLineMoney && orderBaseLineMoney < Int32.MaxValue)
                {
                    int getCount = 0;
                    ///获取会员订单底线金额行数
                    getCount = SetParametersBLL.GetMemOrderLineCount();
                    if (getCount > 0)
                    {
                        int updCount = 0;
                        ///更新会员订单底线金额
                        updCount = SetParametersBLL.UpdMemOrderLine(orderBaseLineMoney);
                        if (updCount > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002249", "更新会员报单底线成功！")));
                        }

                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002250", "更新会员报单底线失败，请联系管理员！")));
                        }
                    }

                    else
                    {
                        int addCount = 0;
                        ///向会员报单底线表中相关插入记录
                        addCount = SetParametersBLL.AddMemOrderLine(orderBaseLineMoney);
                        if (addCount > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002251", "设置会员报单底线成功！")));
                        }

                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002252", "设置会员报单底线失败，请联系管理员！")));
                        }
                    }
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002253", "输入的数量过大或负数!")));
                    txtMemBaseLine.Value = SetParametersBLL.GetMemOrderLineOrderBaseLine().ToString();
                }
            }

            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002252", "设置会员报单底线失败，请联系管理员！")));
            }
        }
    }
    
    /// <summary>
    /// 返回上级菜单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
}
