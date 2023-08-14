using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        //Một số hàm khác
        //

        //trả về 1 mẫu tin
        public Link getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Links.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }
    }
}
