using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();

        //trả về danh sách các mẫu tin
        public List<User> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Users.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Users.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Users.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Users.ToList();
        }

        //trả về 1 mẫu tin
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }

        public User getRow(string username)
        {
            if (username == null)
            {
                return null;
            }
            else
            {
                return db.Users.Where(m => m.Username == username).FirstOrDefault();
            }
        }

        //thêm vào mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }
    }
}
