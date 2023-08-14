using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class TopicDAO
    {
        private MyDBContext db = new MyDBContext();
        //trả về danh sách các mẫu tin
        public List<Topic> getList(string status = "All")
        {
            if (status == "All")
            {
                return db.Topics.ToList();//Select * from Category
            }
            else
            {
                if (status == "Index")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Topics.Where(m => m.Status != 0).ToList();
                }
                if (status == "Trash")//lay  ra những mẫu tin có trạng thái khác 0
                {
                    return db.Topics.Where(m => m.Status == 0).ToList();
                }
            }
            return db.Topics.ToList();
        }

        //trả về 1 mẫu tin
        public Topic getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Find(id);
            }
        }

        //thêm vào mẫu tin
        public int Insert(Topic row)
        {
            db.Topics.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        public int Update(Topic row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        public int Delete(Topic row)
        {
            db.Topics.Remove(row);
            return db.SaveChanges();
        }
    }
}
