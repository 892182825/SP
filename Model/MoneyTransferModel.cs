using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MoneyTransferModel
    {
        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
      string outNumber;

      int isMember;
      public int IsMember
      {
          get { return isMember; }
          set { isMember = value; }
      }

      public string OutNumber
      {
          get { return outNumber; }
          set { outNumber = value; }
      }
      string trunNumber;

      public string TrunNumber
      {
          get { return trunNumber; }
          set { trunNumber = value; }
      }
      int transferType;
       /// <summary>
       /// 1.报单账户，2.提现账户
       /// </summary>
      public int TransferType
      {
          get { return transferType; }
          set { transferType = value; }
      }
      double money;

      public double Money
      {
          get { return money; }
          set { money = value; }
      }
      DateTime transferTime;

      public DateTime TransferTime
      {
          get { return transferTime; }
          set { transferTime = value; }
      }
      DateTime outConfirmationTime;

      public DateTime OutConfirmationTime
      {
          get { return outConfirmationTime; }
          set { outConfirmationTime = value; }
      }
      DateTime trunConfirmationTime;

      public DateTime TrunConfirmationTime
      {
          get { return trunConfirmationTime; }
          set { trunConfirmationTime = value; }
      }
      int outIsConfirm;

      public int OutIsConfirm
      {
          get { return outIsConfirm; }
          set { outIsConfirm = value; }
      }

     
      int trunIsConfirm;

      public int TrunIsConfirm
      {
          get { return trunIsConfirm; }
          set { trunIsConfirm = value; }
      }
      string remark;

      public string Remark
      {
          get { return remark; }
          set { remark = value; }
      }
    }
}
