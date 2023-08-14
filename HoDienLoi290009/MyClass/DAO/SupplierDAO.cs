using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SupplierDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public List<Supplier> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Suppliers.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Suppliers.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Suppliers.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Suppliers.ToList();
        }

        //trả về 1 mẫu tin
        public Supplier getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Supplier row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Supplier row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Supplier row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }
}
