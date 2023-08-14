using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ConfigDAO
    {
        private MyDBContext db = new MyDBContext();
        //
        //Một số hàm khác
        //
        //trả về 1 mẫu tin
        public Config getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Configs.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Config row)
        {
            db.Configs.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Config row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Config row)
        {
            db.Configs.Remove(row);
            return db.SaveChanges();      
        }
    }
}
