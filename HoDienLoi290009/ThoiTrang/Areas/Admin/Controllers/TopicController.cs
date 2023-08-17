using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using ThoiTrang.Library;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class TopicController : Controller
    {
        TopicDAO topicDAO = new TopicDAO();
        //them phan LỊNK
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            return View(topicDAO.getList("Index"));
        }

        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Order", "Name", 0);
            return View();
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                //Xử lý thêm thông tin tự động
                topic.Slug = Xstring.Str_Slug(topic.Name);

                if (topic.ParentId == null)
                {
                    topic.ParentId = 0;
                }

                if (topic.Order == null)
                {
                    topic.Order = 1;
                }
                else
                {
                    topic.Order = topic.Order + 1;
                }

                topic.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                topic.CreatedAt = DateTime.Now;
                //sua them ngay 16-Aug, them duong link vao table Link
                if (topicDAO.Insert(topic) == 1)
                {
                    ////tao ra link
                    Link link = new Link();
                    link.Slug = topic.Slug;
                    link.TableId = topic.Id;
                    link.Type = "topic";
                    linkDAO.Insert(link);
                }

                TempData["message"] = new Xmessage("success", "Thêm danh mục thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name", 0);
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Order", "Name", 0);
            return View(topic);
        }

        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {

                //them phan nay vao 16-8-2023 chu truoc day khong co
                topic.Slug = Xstring.Str_Slug(topic.Name);
                if (topic.ParentId == null)
                {
                    topic.ParentId = 0;
                }
                if (topic.Order == null)
                {
                    topic.Order = 1;
                }
                else
                {
                    topic.Order += 1;
                }
                topic.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                topic.CreatedAt = DateTime.Now;

                //sua them ngay 16-Aug, them duong link vao table Link
                if (topicDAO.Update(topic) == 1)
                {
                    Link link = linkDAO.getRow(topic.Id, "topic");
                    link.Slug = topic.Slug;
                    linkDAO.Update(link);
                    
                }
                topic.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
                topic.UpdateAt = DateTime.Now;
                //het phan moi bo sung

                TempData["message"] = new Xmessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = topicDAO.getRow(id);
            //updata 16-8-2023
            Link link = linkDAO.getRow(topic.Id, "topic");

            if (topicDAO.Delete(topic) == 1)
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
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = (topic.Status == 1) ? 2 : 1;
            topic.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdateAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new Xmessage("success", "Cập nhật trạng thái thành công");
            return RedirectToAction("Index", "Topic");
        }

        //Xóa vào thùng rác
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Topic");
            }
            topic.Status = 0; //Trạng thái Trash = 0
            topic.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdateAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new Xmessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Topic");
        }

        //Lục thùng rác
        public ActionResult Trash(int? id)
        {
            return View(topicDAO.getList("Trash"));
        }


        //Phục hồi từ thùng rác
        public ActionResult Recover(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new Xmessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            Topic topic = topicDAO.getRow(id);
            if (topic == null)
            {
                TempData["message"] = new Xmessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Topic");
            }
            topic.Status = 2; //Trạng thái Recover = 2
            topic.UpdateBy = Convert.ToInt32(Session["UserId"].ToString());
            topic.UpdateAt = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new Xmessage("success", "Phục hồi danh mục thành công");
            return RedirectToAction("Trash", "Topic");
        }
    }
}
