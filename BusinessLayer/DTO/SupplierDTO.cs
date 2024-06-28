using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class SupplierDTO
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string PHONE { get; set; }
        public string TAX { get; set; }
        public string TYPE { get; set; }
        public string CREATE_BY { get; set; }
        public Nullable<int> UPDATE_BY{ get; set; }
        public string ADDRESS { get; set; }
        public string NOTE { get; set; }
        public string NAME_ACCOUNT { get; set; }
        public string NUMBER_BANK { get; set; }
        public string NAME_BANK { get; set; }
        public string BRANCH_BANK { get; set; }
        public string CODE { get; set; }
        public string EMAIL { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> CREATE_AT { get; set; }
        public Nullable<System.DateTime> UPDATE_AT { get; set; }
    }
}
