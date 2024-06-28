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

        public List<tb_su> getList()
        {
            return db.tb_Unit.ToList();
        }
    }
}
