using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public List<Product> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Products.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Products.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Products.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Products.ToList();
        }

        //trả về 1 mẫu tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
