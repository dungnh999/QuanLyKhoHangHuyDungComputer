using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class GroupOption
    {
        Entities db;

        public GroupOption()
        {
            db = Entities.CreateEntities();
        }

        public List<GroupOptionDTO> getAll()
        {
            var sortedList = db.tb_Group_Option.ToList();
            var list = sortedList.OrderByDescending(x => x.STATUS).ToList();
            List<GroupOptionDTO> listNew = new List<GroupOptionDTO>();
            GroupOptionDTO gvDTO;
            int number = 1;

            foreach (var item in list)
            {
                gvDTO = new GroupOptionDTO();
                gvDTO.STT = number++.ToString();
                gvDTO.ID = item.ID;
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

        public tb_Group_Option Add(tb_Group_Option sp)
        {
            tb_Group_Option result = null;
            ExceptionHandler.Handle(() =>
            {
                db.tb_Group_Option.Add(sp);
                db.SaveChanges();
                result = sp;
            });

            return result;
        }

        public tb_Group_Option getItem(int id)
        {
            return db.tb_Group_Option.FirstOrDefault(x => x.ID == id);
        }

        public tb_Group_Option Update(tb_Group_Option sp)
        {
            try
            {
                var _data = db.tb_Group_Option.FirstOrDefault(x => x.ID == sp.ID);
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

        public tb_Group_Option changeStatus(tb_Group_Option sp)
        {
            try
            {
                var _data = db.tb_Group_Option.FirstOrDefault(x => x.ID == sp.ID);
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
