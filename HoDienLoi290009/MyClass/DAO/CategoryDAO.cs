using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class CategoryDAO
    {
        private MyDBContext db = new MyDBContext();

        //trả về danh sách các mẫu tin
        public List<Category> getList(string status = "All")
        {
            if(status == "All")
            {
                return db.Categorys.ToList();//Select * from Category
            }
            else
            {
                if(status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Categorys.Where(m => m.Status !=0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Categorys.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Categorys.ToList();
        }

        //trả về 1 mẫu tin
        public Category getRow(int? id)
        {
            if (id==null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }

        

    }
    
}
