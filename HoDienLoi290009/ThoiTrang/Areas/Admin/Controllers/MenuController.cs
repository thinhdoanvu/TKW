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
            ViewBag.ListCategory = categoryDAO.getList();
            ViewBag.ListTopic = topicDAO.getList();
            ViewBag.ListPage = postDAO.getList();
            List<Menu> menu = menuDAO.getList();
            return View("Index", menu);//truyn duoi dang model == menu
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
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
            return RedirectToAction("Index", "Menu");
        }
    }
}
    
