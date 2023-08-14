using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
         private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public List<Order> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Orders.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Orders.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Orders.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Orders.ToList();
        }

        //trả về 1 mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();      
        }
    }
}
