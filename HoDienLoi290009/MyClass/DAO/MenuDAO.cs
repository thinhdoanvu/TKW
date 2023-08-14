using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class MenuDAO
    {
         private MyDBContext db = new MyDBContext();

        //trả về danh sách các mẫu tin
        public List<Menu> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Menus.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Menus.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Menus.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Menus.ToList();
        }


        //trả về 1 mẫu tin
        public Menu getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Menus.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Menu row)
        {
            db.Menus.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Menu row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Menu row)
        {
            db.Menus.Remove(row);
            return db.SaveChanges();  
        }
    }
}
