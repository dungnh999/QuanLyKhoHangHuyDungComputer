using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CategoryProducts
    {
        Entities db;

        public CategoryProducts() 
        { 
            db = Entities.CreateEntities();
        }

        public List<CategoryProductsDTO> getAll()
        {
            var sortedList = db.tb_Category.ToList();
            var list = sortedList.OrderByDescending(x => x.STATUS).ToList();
            List<CategoryProductsDTO> listNew = new List<CategoryProductsDTO>();
            CategoryProductsDTO gvDTO;
            int number = 1;

            foreach (var item in list)
            {
                gvDTO = new CategoryProductsDTO();
                gvDTO.STT = number++.ToString();
                gvDTO.ID = item.ID;
                gvDTO.CODE = item.CODE;
                gvDTO.NAME = item.NAME;
                gvDTO.CREATE_BY = (item.CREATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.CREATE_BY).NAME : "" ;
                gvDTO.UPDATE_BY = (item.UPDATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.UPDATE_BY).NAME : "";
                gvDTO.CREATE_AT = item.CREATE_AT;
                gvDTO.UPDATE_AT = item.UPDATE_AT;
                gvDTO.STATUS = (item.STATUS == 1) ? "Đang hoạt động" : "Tạm ngưng";     
                listNew.Add(gvDTO);
            }
            return listNew;
        }

        public tb_Category Add(tb_Category sp)
        {
            tb_Category result = null;
            ExceptionHandler.Handle(() =>
            {
                db.tb_Category.Add(sp);
                db.SaveChanges();
                result = sp;
            });

            return result;
        }

        public tb_Category getItem(int id)
        {
            return db.tb_Category.FirstOrDefault(x => x.ID == id);
        }

        public tb_Category Update(tb_Category sp)
        {
            try
            {
                var _data = db.tb_Category.FirstOrDefault(x => x.ID == sp.ID);
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

        public tb_Category changeStatus(tb_Category sp) 
        {
            try
            {
                var _data = db.tb_Category.FirstOrDefault(x => x.ID == sp.ID);
                if (_data != null)
                {
                    if(sp.STATUS == 1)  
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
