using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using ThoiTrang.Library;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAO ProductDAO = new ProductDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(ProductDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatID,Name,Slug,Detail,Image,Price,PriceSale,Number,MetaDesc,MetaKey,CreatedBy,CreatedAt,UpdateBy,UpdateAt,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductDAO.Insert(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatID,Name,Slug,Detail,Image,Price,PriceSale,Number,MetaDesc,MetaKey,CreatedBy,CreatedAt,UpdateBy,UpdateAt,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                ProductDAO.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = ProductDAO.getRow(id);
            ProductDAO.Delete(product);
            return RedirectToAction("Index");
        }

        //Status
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdateAt = DateTime.Now;
            ProductDAO.Update(product);
            TempData["message"] = new Xmessage("success", "Cập nhật trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }

        //Làm thêm//
        //Xóa vào thùng rác
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0; //Trạng thái Trash = 0
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());//người update
            product.UpdateAt = DateTime.Now;
            ProductDAO.Update(product);
            TempData["message"] = new Xmessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }

        //Lục thùng rác
        public ActionResult Trash(int? id)
        {
            return View(ProductDAO.getList("Trash"));
        }


        //Phục hồi từ thùng rác
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = ProductDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2; //Trạng thái Recover = 2
            product.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());//người update
            product.UpdateAt = DateTime.Now;
            ProductDAO.Update(product);
            TempData["message"] = new Xmessage("success", "Phục hồi danh mục thành công");
            return RedirectToAction("Trash", "Product");
        }

    }
}
