using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RemittanceUseType
    {

        public int ID { set; get; }
        public string TypeName { set; get; }
        public RemittanceUseType()
        { }
        public RemittanceUseType(int id,string typeName)
        {
            this.ID = id;
            this.TypeName = typeName;
        }

        public static IList<RemittanceUseType> GetRemittacneUserType()
        {
            IList<RemittanceUseType> rus = new List<RemittanceUseType>();
            rus.Add(new RemittanceUseType(1, "报单款"));
            rus.Add(new RemittanceUseType(2, "周转货款"));
            rus.Add(new RemittanceUseType(3, "加盟金"));
            rus.Add(new RemittanceUseType(4, "押金"));
            rus.Add(new RemittanceUseType(5, "其他"));
            return rus;
        }


    }
}
