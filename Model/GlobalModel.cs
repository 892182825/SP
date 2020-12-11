#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：GlobalModel.cs
 * 文件功能描述：全局配置实体类
 *
 *
 * 创建标识：董晨东 2009/08/26
 * 
 * 修改标识：
 * 
 * 修改描述：
 * 
 * 
 * 
 * 
 * 
 //----------------------------------------- **/
#endregion
using System;

namespace Model
{
/// <summary>
/// GlobalModel 的摘要说明
/// </summary>
public class GlobalModel
{
    #region 公有属性
    private int iD;
    /// <summary>
    /// 自增长，标识列 (ID)
    /// </summary>
    protected int ID
    {
        get { return iD; }
    }
    private int currentRegisterMode ;
    /// <summary>
    /// 当前报单模式 1:开启 0 禁止(currentRegisterMode)默认值：1 
    /// </summary>
    protected int CurrentRegisterMode
    {
        get { return currentRegisterMode; }
        set { currentRegisterMode = value; }
    }
    private int currentStoreMode;

    /// <summary>
    /// 5.none,6.店铺团购(currentStoreMode)默认值：5 
    /// </summary>
    protected int CurrentStoreMode
    {
        get { return currentStoreMode; }
        set { currentStoreMode = value; }
    }
    private int currentChangeMode;

    /// <summary>
    /// 0为不实行优惠政策,1为实行优惠(currentChangeMode)默认值：0 
    /// </summary>
    protected int CurrentChangeMode
    {
        get { return currentChangeMode; }
        set { currentChangeMode = value; }
    }
    #endregion
    #region 公共方法
    /// <summary>
    /// 
    /// </summary>
    public GlobalModel()
    {
 
    }
    /// <summary>
    /// 根据Id获取XXX
    /// </summary>
    /// <param name="id">标识列</param>
    public GlobalModel(int id)
    {
        this.iD = id;
    }
    #endregion
}
}