using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderdetailDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        //Một số hàm còn thiếu
        //

        //thêm vào mẫu tin
        public int Insert(Orderdetail row)
        {
            db.Orderdetails.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Orderdetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Orderdetail row)
        {
            db.Orderdetails.Remove(row);
            return db.SaveChanges();
        }
    }
}
