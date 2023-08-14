using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ContactDAO
    {
        private MyDBContext db = new MyDBContext();

        //trả về danh sách các mẫu tin
        public List<Contact> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Contacts.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Contacts.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Contacts.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Contacts.ToList();
        }


        //trả về 1 mẫu tin
        public Contact getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Contacts.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Contact row)
        {
            db.Contacts.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
