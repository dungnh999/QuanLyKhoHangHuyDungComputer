using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Unit
    {
        Entities db;
        public Unit()
        {
            db = Entities.CreateEntities();
        }

        public List<tb_Unit> getList()
        {
            return db.tb_Unit.ToList();
        }
    }
}
