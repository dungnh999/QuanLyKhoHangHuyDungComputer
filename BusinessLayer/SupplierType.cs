using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SupplierType
    {
        Entities db;

        public SupplierType()
        {
            db = Entities.CreateEntities();
        }

        public List<tb_Supplier_Type> getList()
        {
            return db.tb_Supplier_Type.ToList();
        }
    }
}
