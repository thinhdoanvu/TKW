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
        //duyet tat ca san pham co trong muc Category

        public List<ProductInfo> getListByCatId(int catid, int limit)
        {
            List<ProductInfo> list = db.Products
                //join 2 bang
                .Join(
                db.Categorys,
                p => p.CatID,
                c => c.Id,
                  (p, c) => new ProductInfo
                  {
                      Id = p.Id,
                      CatID = p.CatID,
                      CatName = c.Name,
                      Supplier = p.Supplier,
                      Slug = p.Slug,
                      Detail = p.Detail,
                      Image = p.Image,
                      Price = p.Price,
                      PriceSale = p.PriceSale,
                      Number = p.Number,
                      MetaDesc = p.MetaDesc,
                      MetaKey = p.MetaKey,
                      CreatedBy = p.CreatedBy,
                      CreatedAt = p.CreatedAt,
                      UpdateBy = p.UpdateBy,
                      UpdateAt = p.UpdateAt,
                      Status = p.Status
                  }
                )
                .Where(m => m.CatID == catid && m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .Take(limit)
                .ToList();
            return list;
        }

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

        //bo sung cho phan Menu
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
        }

        //thêm vào mẫu tin
        Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }

        //cập nhật mẫu tin
        Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }

        //xóa mẫu tin
        Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }
}
