using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Brand
    {
        Entities db;
        Brand _brand;

        public Brand()
        {
            db = Entities.CreateEntities();
        }

        public List<BrandDTO> getAll()
        {
            var sortedList = db.tb_Brand.ToList();
            var list = sortedList.OrderByDescending(x => x.STATUS).ToList();
            List<BrandDTO> listNew = new List<BrandDTO>();
            BrandDTO gvDTO;
            int number = 1;

            foreach (var item in list)
            {
                gvDTO = new BrandDTO();
                gvDTO.STT = number++;
                gvDTO.ID = item.ID;
                gvDTO.CODE = item.CODE;
                gvDTO.NAME = item.NAME;
                gvDTO.CREATE_BY = (item.CREATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.CREATE_BY).NAME : "";
                gvDTO.UPDATE_BY = (item.UPDATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.UPDATE_BY).NAME : "";
                gvDTO.CREATE_AT = item.CREATE_AT;
                gvDTO.UPDATE_AT = item.UPDATE_AT;
                gvDTO.STATUS = (item.STATUS == 1) ? "Đang hoạt động" : "Tạm ngưng";
                listNew.Add(gvDTO);
            }
            return listNew;
        }

        public tb_Brand Add(tb_Brand sp)
        {
            tb_Brand result = null;
            ExceptionHandler.Handle(() =>
            {
                db.tb_Brand.Add(sp);
                db.SaveChanges();
                result = sp;
            });

            return result;
        }

        public tb_Brand getItem(int id)
        {
            return db.tb_Brand.FirstOrDefault(x => x.ID == id);
        }

        public tb_Brand Update(tb_Brand sp)
        {
            try
            {
                var _data = db.tb_Brand.FirstOrDefault(x => x.ID == sp.ID);
                if (_data != null)
                {
                    _data.NAME = sp.NAME;
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

        public tb_Brand changeStatus(tb_Brand sp)
        {
            try
            {
                var _data = db.tb_Brand.FirstOrDefault(x => x.ID == sp.ID);
                if (_data != null)
                {
                    if (sp.STATUS == 1)
                        sp.STATUS = 0;
                    else
                        sp.STATUS = 1;

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
