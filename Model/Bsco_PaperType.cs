using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// Bsco_PaperType实体类
    /// </summary>
    [Serializable]
    public class Bsco_PaperType
    {
        public Bsco_PaperType() {
        
        }

        public Bsco_PaperType(int id)
        {
            this.id = id;
        }
        int id;

        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
             get { return id; }
            set { id = value; }
          
        }
        string paperTypeCode;

        /// <summary>
        /// 证件类型code
        /// </summary>
        public string PaperTypeCode
        {
            get { return paperTypeCode; }
            set { paperTypeCode = value; }
        }
        string paperType;

        /// <summary>
        /// 证件类型名
        /// </summary>
        public string PaperType
        {
            get { return paperType; }
            set { paperType = value; }
        }
    }
}
