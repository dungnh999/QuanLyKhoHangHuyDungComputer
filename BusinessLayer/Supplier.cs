using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Supplier
    {
        Entities db;
        public Supplier() 
        { 
            db = Entities.CreateEntities();
        }

        public List<tb_Supplier>getAll()
        {
            return db.tb_Supplier.ToList();
        }
    }
}
