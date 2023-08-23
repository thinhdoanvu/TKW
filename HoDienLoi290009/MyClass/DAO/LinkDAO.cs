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

        public List<Link> getList()
        {
            return db.Links.ToList();
        }

        //Lay mot mau tin
        public Link getRow(int? id)
        {
            return db.Links.Find(id);
        }

        //bo sung khi thiet lap URL tuy bien
        public Link getRow(string slug)
        {
            return db.Links.Where(m=>m.Slug == slug).FirstOrDefault();
        }

        public Link getRow(int tableid, string typelink)
        {
            return db.Links.Where(m => m.TableId == tableid && m.Type == typelink).FirstOrDefault();
        }
        //Một số hàm khác
        //

        ////trả về 1 mẫu tin (bổ sung)
        //public Link getRow(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return db.Links.Find(id);
        //    }
        //}

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
