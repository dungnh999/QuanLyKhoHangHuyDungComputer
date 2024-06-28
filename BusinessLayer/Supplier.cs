using BusinessLayer.DTO;
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

        public List<SupplierDTO> getAll()
        {
            var list = db.tb_Supplier.ToList();
            List<SupplierDTO> listNew = new List<SupplierDTO>();
            SupplierDTO gvDTO;
            foreach (var item in list) 
            {
                gvDTO = new SupplierDTO();
                gvDTO.ID = item.ID;
                gvDTO.NAME = item.NAME;
                gvDTO.EMAIL = item.EMAIL;
                gvDTO.TAX = item.TAX;
                gvDTO.PHONE = item.PHONE;
                gvDTO.CREATE_BY = db.tb_Users.FirstOrDefault(x => x.ID == item.CREATE_BY).NAME;
                gvDTO.TYPE = (item.TYPE == 1) ? "Cá nhân" : "Doanh Nghiệp"; ;
                gvDTO.STATUS = (item.STATUS == 1) ? "Đang hoạt động" : "Tạm ngưng";
                listNew.Add(gvDTO);
            }
            return listNew;
        }

        public tb_Supplier Add(tb_Supplier sp)
        {
            try
            {
                db.tb_Supplier.Add(sp);
                db.SaveChanges();
                return sp;
            }
            catch (Exception ex) {
                throw new Exception("lỗi", ex);
            }
        }

        public tb_Supplier getItem(int id)
        {
            return db.tb_Supplier.FirstOrDefault(x => x.ID == id);
        }

        public tb_Supplier Update(tb_Supplier sp)
        {
            try
            {
                var _data = db.tb_Supplier.FirstOrDefault(x => x.ID == sp.ID);
                if( _data != null)
                {
                    _data.ID = sp.ID;
                    _data.NAME = sp.NAME;
                    _data.CODE = sp.CODE;
                    _data.NAME_BANK = sp.NAME_BANK;
                    _data.PHONE = sp.PHONE;
                    _data.EMAIL = sp.EMAIL;
                    _data.BRANCH_BANK = sp.BRANCH_BANK;
                    _data.TAX = sp.TAX;
                    _data.NUMBER_BANK = sp.NUMBER_BANK;
                    _data.TYPE = sp.TYPE;
                    _data.NAME_ACCOUNT = sp.NAME_ACCOUNT;
                    _data.ADDRESS = sp.ADDRESS;
                    _data.NOTE = sp.NOTE;
                    _data.STATUS = sp.STATUS;
                    _data.UPDATE_BY = sp.UPDATE_BY;
                    _data.UPDATE_AT = sp.UPDATE_AT;
                    db.SaveChanges();
                }
                return sp;
            }
            catch (Exception ex)
            {
                throw new Exception("lỗi", ex);
            }
        }
    }
}
