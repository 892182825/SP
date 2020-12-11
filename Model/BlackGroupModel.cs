using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// BlackGroupModel 的摘要说明
/// </summary>
public class BlackGroupModel
{
    #region 公共属性
    private int iD ;

    /// <summary>
    /// 自增长，标识列 (ID)
    /// </summary>
    public int ID
    {
        get { return iD; }
    }
    private string intGroupValue;

    /// <summary>
    /// 组屏蔽值(intGroupValue)
    /// </summary>
    public string IntGroupValue
    {
        get { return intGroupValue; }
        set { intGroupValue = value; }
    }

    private int intGroupType ;

    /// <summary>
    /// 屏蔽组类型 3、地区屏蔽(intGroupType),4 店辖网络关系屏蔽,5 安置网络关系屏蔽,6 推荐网络关系屏蔽
    /// </summary>
    public int IntGroupType
    {
        get { return intGroupType; }
        set { intGroupType = value; }
    }

    #endregion
    #region 公共方法
    /// <summary>
    /// 
    /// </summary>
    public BlackGroupModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    public  BlackGroupModel(int id)
    {
        this.iD = id;
    }
    #endregion

}
