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
    public class CategoryController : Controller
    {

        CategoryDAO categoryDAO = new CategoryDAO();
        //them phan LỊNK
        LinkDAO linkDAO = new LinkDAO();
        
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDAO.getList("Index"));
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name",0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin tự động
                category.Slug = Xstring.Str_Slug(category.Name);

                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }

                if (category.Order == null)
                {
                    category.Order = 1;
                }
                else
                {
                    category.Order = category.Order + 1;
                }

                category.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                category.CreatedAt = DateTime.Now;
                //sua them ngay 16-Aug, them duong link vao table Link
                if(categoryDAO.Insert(category) == 1)
                {
                    ////tao ra link
                    Link link = new Link();
                    link.Slug = category.Slug;
                    link.TableId = category.Id;
                    link.Type = "category";
                    linkDAO.Insert(link);
                }
                
                TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"), "Order", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {

                //them phan nay vao 16-8-2023 chu truoc day khong co
                category.Slug = Xstring.Str_Slug(category.Name);
                if (category.ParentId == null)
                {
                    category.ParentId = 0;
                }
                if (category.Order == null)
                {
                    category.Order = 1;
                }
                else
                {
                    category.Order += 1;
                }
                category.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                category.CreatedAt = DateTime.Now;
                
                //sua them ngay 16-Aug, them duong link vao table Link
                if (categoryDAO.Update(category) == 1)
                {
                    Link link = linkDAO.getRow(category.Id,"category");
                    link.Slug = category.Slug;
                    linkDAO.Update(link);
                }
                category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                category.UpdateAt = DateTime.Now;
                //het phan moi bo sung
               
                TempData["message"] = new Xmessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryDAO.getRow(id);
            //updata 16-8-2023
            Link link = linkDAO.getRow(category.Id,"category");

            if (categoryDAO.Delete(category) == 1)
            {
                linkDAO.Delete(link);
            }
            //sau khi xoa xong, thi  dong thoi cung xoa luon link
            //updata 16-8-2023

            TempData["message"] = new Xmessage("success", "Xóa danh mục thành công");
            return RedirectToAction("Trash");//chuyển hướng về Trash
        }

        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status ==1)?2:1;
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new Xmessage("success", "Cập nhật trạng thái thành công");
            return RedirectToAction("Index", "Category");
        }

        //Xóa vào thùng rác
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Category");
            }
            category.Status = 0; //Trạng thái Trash = 0
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new Xmessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Category");
        }

        //Lục thùng rác
        public ActionResult Trash(int? id)
        {
            return View(categoryDAO.getList("Trash"));
        }


        //Phục hồi từ thùng rác
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            Category category = categoryDAO.getRow(id);
            if (category == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Category");
            }
            category.Status = 2; //Trạng thái Recover = 2
            category.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            category.UpdateAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new Xmessage("success", "Phục hồi danh mục thành công");
            return RedirectToAction("Trash", "Category");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
