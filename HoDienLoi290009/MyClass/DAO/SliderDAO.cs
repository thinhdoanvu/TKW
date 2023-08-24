using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SliderDAO
    {
        private MyDBContext db = new MyDBContext();

        //trả về 1 mẫu tin co dieu kien
        public List<Slider> getListByPosition(string position)
        {
            return db.Sliders.Where(m=> m.Position == position && m.Status == 1)
                .OrderBy(m=>m.CreatedAt)
                .ToList();
        }

        //trả về danh sách các mẫu tin
        public List<Slider> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Sliders.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Sliders.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Sliders.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Sliders.ToList();
        }

        //trả về 1 mẫu tin
        public Slider getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }

  
        //thêm vào mẫu tin
        public int Insert(Slider row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();
        }
    }
}
