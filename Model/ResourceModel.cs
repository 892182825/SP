using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 资料管理表
    /// sj
    /// </summary>
   public class ResourceModel
    {
       /// <summary>
       /// 编号
       /// </summary>
       public int ResID
       {get;set;}
       /// <summary>
       /// 文件名字
       /// </summary>
       public string ResName
       {get;set;}
       /// <summary>
       /// 文件名称
       /// </summary>
       public string FileName
       { get; set; }
       /// <summary>
       /// 描述
       /// </summary>
       public string ResDescription
       { get; set; }
       /// <summary>
       /// 文件大小
       /// </summary>
       public string ResSize
       { get; set; }
       /// <summary>
       /// 最后修改时间
       /// </summary>
       public DateTime ResDateTime
       { get; set; }
       /// <summary>
       /// 下载次数
       /// </summary>
       public int ResTimes
       { get; set; }
       public int CPPCode
       {
           get;
           set;
       }
       /// <summary>
       /// 下载对象
       /// </summary>
       public int DownTarget
       {
           get;
           set;
       }
       /// <summary>
       /// 会员级别
       /// </summary>
       public int DownMenberLev
       {
           get;
           set;
       }
    }
}
