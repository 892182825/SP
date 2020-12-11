using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

namespace BLL.other.Company
{
    public class ConfigSetBLL
    {
        /// <summary>
        /// 根据期数获取期数的参数设置
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <returns></returns>
        public static ConfigModel GetConfig(int expectNum)
        {
            return ConfigDAL.GetConfig(expectNum);
        }

        /// <summary>
        /// 根据期数获取期数的参数设置
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <returns></returns>
        public static ConfigModel GetConfig2(int expectNum)
        {
            return ConfigDAL.GetConfig2(expectNum);
        }

        public static int UpdateConfig(ConfigModel model)
        {
            return ConfigDAL.UpdateConfig(model);
        }

        public static int UpdateConfig2(ConfigModel model)
        {
            return ConfigDAL.UpdateConfig2(model);
        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expectNum"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static int UpdatetkType(int type, int expectNum, string p)
        {
            return ConfigDAL.AddtkType(type, expectNum, p, DateTime.Now);
        }

        public static bool HastkType(int type, int expectNum, string number)
        {
            return ConfigDAL.GettkType(type, expectNum, number) >= 1;
        }

        public static int DelTkType(int p)
        {
            return ConfigDAL.DelTkType(p);
        }
    }
}