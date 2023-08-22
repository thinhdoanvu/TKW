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
    public class MenuController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        TopicDAO topicDAO = new TopicDAO();
        PostDAO postDAO = new PostDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        MenuDAO menuDAO = new MenuDAO();
        public ActionResult Index()
        {
            //ViewBag.ListCategory = categoryDAO.getList();//de nhu the nay thi khi xoa mau tin muc CAT thi menu van con
            ViewBag.ListCategory = categoryDAO.getList("Index");//menu sẽ khôgn hiển thị những mẫu tin đã xoá va đang trong thùng rác
            ViewBag.ListTopic = topicDAO.getList("Index");//tat ca deu them muc Index vao cung ly do
            ViewBag.ListPage = postDAO.getList("Index", "Page");//tat ca deu them muc Index vao cung ly do
            List<Menu> menu = menuDAO.getList("Index");//tat ca deu them muc Index vao cung ly do
            return View("Index", menu);//truyen duoi dang model == menu
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            //Xu ly cho nút ThemCategory ben Index
            if (!string.IsNullOrEmpty(form["ThemCategory"]))
            {
                if (!string.IsNullOrEmpty(form["nameCategory"]))//check box được nhấn
                {
                    var listitem = form["nameCategory"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);//ep kieu int
                        Category category = categoryDAO.getRow(id);
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.Name = category.Name;
                        menu.Link = category.Slug;
                        menu.TableId = category.Id;
                        menu.TypeMenu = "category";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Order = 0;
                        menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.Status = 2;//chưa xuất bản
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                }
                else//check box chưa được nhấn
                {
                    TempData["message"] = new Xmessage("danger", "Chưa chọn danh mục loại sản phẩm");
                }
            }

            //Xử lý cho nút ThemTopic o Index
            if (!string.IsNullOrEmpty(form["ThemTopic"]))
            {
                if (!string.IsNullOrEmpty(form["nameTopic"]))//check box được nhấn tu phia Index
                {
                    var listitem = form["nameTopic"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);//ep kieu int
                        Topic topic = topicDAO.getRow(id);
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.Name = topic.Name;
                        menu.Link = topic.Slug;
                        menu.TableId = topic.Id;
                        menu.TypeMenu = "topic";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Order = 0;
                        menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.Status = 2;//chưa xuất bản
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                }
                else//check box chưa được nhấn
                {
                    TempData["message"] = new Xmessage("danger", "Chưa chọn danh mục bài viết");
                }
            }

            //Xử lý cho nut Thempage ben Index
            if (!string.IsNullOrEmpty(form["ThemPage"]))
            {
                if (!string.IsNullOrEmpty(form["namePage"]))//check box được nhấn tu phia Index
                {
                    var listitem = form["namePage"];
                    //chuyen danh sach thanh dang mang: vi du 1,2,3,...
                    var listarr = listitem.Split(',');//cat theo dau ,
                    foreach (var row in listarr)//row = id cua các mau tin
                    {
                        int id = int.Parse(row);//ep kieu int
                        Post post = postDAO.getRow(id);
                        //tao ra menu
                        Menu menu = new Menu();
                        menu.Name = post.Title;
                        menu.Link = post.Slug;
                        menu.TableId = post.Id;
                        menu.TypeMenu = "page";
                        menu.Position = form["Position"];
                        menu.ParentId = 0;
                        menu.Order = 0;
                        menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        menu.CreatedAt = DateTime.Now;
                        menu.Status = 2;//chưa xuất bản
                        menuDAO.Insert(menu);
                    }
                    TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                }
                else//check box chưa được nhấn
                {
                    TempData["message"] = new Xmessage("danger", "Chưa chọn danh mục bài viết");
                }
            }


            //Xử lý cho nút ThemCustom ben Index
            if (!string.IsNullOrEmpty(form["ThemCustom"]))
            {
                if (!string.IsNullOrEmpty(form["name"]) && !string.IsNullOrEmpty(form["link"]))//o name, link text được gõ tu phia Index
                {
                    //tao ra menu
                    Menu menu = new Menu();
                    menu.Name = form["name"];
                    menu.Link = form["link"];
                    //menu.TableId = post.Id;//vi Table Id allow NULL nen bỏ đi
                    menu.TypeMenu = "custom";//sửa lại nha
                    menu.Position = form["Position"];
                    menu.ParentId = 0;
                    menu.Order = 0;
                    menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                    menu.CreatedAt = DateTime.Now;
                    menu.Status = 2;//chưa xuất bản
                    menuDAO.Insert(menu);

                    TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                }

                else//check box chưa được nhấn
                {
                    TempData["message"] = new Xmessage("danger", "Chưa đủ thông tin cho mục tùy chọn Menu");
                }
            }

            return RedirectToAction("Index", "Menu");
        }

        //Edit Menu
        public ActionResult Edit(int? id)
        {
            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Order", "Name");
            Menu menu = menuDAO.getRow(id);
            return View("Edit", menu);
        }

        //Save sau khi Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {

                if (menu.ParentId == null)
                {
                    menu.ParentId = 0;
                }
                if (menu.Order == null)
                {
                    menu.Order = 1;
                }
                else
                {
                    menu.Order += 1;
                }

                menu.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                menu.CreatedAt = DateTime.Now;

                //sua them ngay 16-Aug, them duong link vao table Link
                menuDAO.Update(menu);
                menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                menu.UpdateAt = DateTime.Now;
                //het phan moi bo sung

                TempData["message"] = new Xmessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListMenu = new SelectList(menuDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(menuDAO.getList("Index"), "Order", "Name");
            return View(menu);
        }

        //Cho nút Status
        public ActionResult Status(int? id)
        {
            if (id==null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.Status = (menu.Status == 1) ? 2 : 1;
            menuDAO.Update(menu);
            TempData["message"] = new Xmessage("success", "Cập nhật thành công");
            return RedirectToAction("Index");
        }

        //Cho nút xoa vao thung TRash
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Index");
            }
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.Status = 0; //Xoa vao thung rac
            menuDAO.Update(menu);
            TempData["message"] = new Xmessage("success", "Cập nhật thành công");
            return RedirectToAction("Index");
        }

        //View trong thung rac
        public ActionResult Trash()
        {
            List<Menu> menu = menuDAO.getList("Trash");
            return View("Trash", menu);
        }

        //Phuc hoi da xoa trong TRash
        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Trash");
            }
            Menu menu = menuDAO.getRow(id);
            if (menu == null)
            {
                TempData["message"] = new Xmessage("danger", "Cập nhật không thành công");
                return RedirectToAction("Trash");
            }
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            menu.Status = 2; //Undo
            menuDAO.Update(menu);
            TempData["message"] = new Xmessage("success", "Cập nhật thành công");
            return RedirectToAction("Trash");
        }

    }

}

