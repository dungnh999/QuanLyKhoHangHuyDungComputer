using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Users
    {
        Entities db;
        public Users() {
            db = Entities.CreateEntities();
        }

        public bool checkLogin( string username, string password)
        {
            var check = db.tb_Users.FirstOrDefault(x => x.USERNAME == username && x.PASSWORD == password);
            if(check != null)
                return true;
            return false;
        }

    }
}
