using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OptionProduct
    {
        Entities db;
        public OptionProduct() {
            db = Entities.CreateEntities();
        }

        public List<OptionProductDTO> getAll()
        {
            var sortedList = db.tb_Option.ToList();
            var list = sortedList.OrderByDescending(x => x.STATUS).ToList();
            List<OptionProductDTO> listNew = new List<OptionProductDTO>();
            OptionProductDTO gvDTO;
            int number = 1;

            foreach (var item in list)
            {
                gvDTO = new OptionProductDTO();
                gvDTO.STT = number++;
                gvDTO.ID = item.ID;
                gvDTO.CODE = item.CODE;
                gvDTO.NAME = item.NAME;
                gvDTO.VALUE = "1";
                gvDTO.PARENT = "1";
                gvDTO.CREATE_BY = (item.CREATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.CREATE_BY).NAME : "";
                gvDTO.UPDATE_BY = (item.UPDATE_BY != null) ? db.tb_Users.FirstOrDefault(x => x.ID == item.UPDATE_BY).NAME : "";
                gvDTO.CREATE_AT = item.CREATE_AT;
                gvDTO.UPDATE_AT = item.UPDATE_AT;
                gvDTO.STATUS = (item.STATUS == 1) ? "Đang hoạt động" : "Tạm ngưng";
                listNew.Add(gvDTO);
            }
            return listNew;
        }

    }
}
