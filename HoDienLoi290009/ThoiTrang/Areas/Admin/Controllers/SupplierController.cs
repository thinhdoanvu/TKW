using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using ThoiTrang.Library;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class SupplierController : Controller
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        //them phan LỊNK
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin tự động
                supplier.Slug = Xstring.Str_Slug(supplier.Name);

                if (supplier.Order == null)
                {
                    supplier.Order = 1;
                }
                else
                {
                    supplier.Order = supplier.Order + 1;
                }

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = supplier.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        //upload hinh
                        string PathDir = "~/Public/images/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh


                supplier.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;
                //sua them ngay 16-Aug, them duong link vao table Link
                if (supplierDAO.Insert(supplier) == 1)
                {
                    ////tao ra link
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.Type = "supplier";
                    linkDAO.Insert(link);
                }

                TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Order", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {

                //them phan nay vao 16-8-2023 chu truoc day khong co
                supplier.Slug = Xstring.Str_Slug(supplier.Name);

                if (supplier.Order == null)
                {
                    supplier.Order = 1;
                }
                else
                {
                    supplier.Order += 1;
                }

                //xu ly cho phan upload hình ảnh
                var img = Request.Files["img"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = supplier.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Img = imgName;
                        //upload hinh
                        string PathDir = "~/Public/images/suppliers/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //cap nhat thi phai xoa file cu
                        //Xoa file
                        if (supplier.Img != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                            System.IO.File.Delete(DelPath);
                        }
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh


                supplier.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;

                //sua them ngay 16-Aug, them duong link vao table Link
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);

                }
                supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.UpdateAt = DateTime.Now;
                //het phan moi bo sung

                TempData["message"] = new Xmessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            //updata 16-8-2023
            Link link = linkDAO.getRow(supplier.Id, "supplier");

            if (supplierDAO.Delete(supplier) == 1)
            {
                //xoa hinh anh truoc khi xoa mau tin
                string PathDir = "~/Public/images/suppliers/";
                //cap nhat thi phai xoa file cu
                //Xoa file
                if (supplier.Img != null)
                {
                    string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Img);
                    System.IO.File.Delete(DelPath);
                }
                //xoa mau tin
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
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new Xmessage("success", "Cập nhật trạng thái thành công");
            return RedirectToAction("Index", "Supplier");
        }

        //Xóa vào thùng rác
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 0; //Trạng thái Trash = 0
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new Xmessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Supplier");
        }

        //Lục thùng rác
        public ActionResult Trash(int? id)
        {
            return View(supplierDAO.getList("Trash"));
        }


        //Phục hồi từ thùng rác
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            supplier.Status = 2; //Trạng thái Recover = 2
            supplier.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdateAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new Xmessage("success", "Phục hồi danh mục thành công");
            return RedirectToAction("Trash", "Supplier");
        }
    }
}