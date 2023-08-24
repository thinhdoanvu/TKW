using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace ThoiTrang.Controllers
{
    public class ModuleController : Controller
    {
        private MenuDAO menuDAO = new MenuDAO();
        private SliderDAO sliderDAO = new SliderDAO();

        // GET: Module
        public ActionResult MainMenu()
        {
            List<Menu> list = menuDAO.getListByParentId("mainmenu", 0);//pareant id=0 de hien ra menu (cap 1): khong co menu con
            return View("MainMenu",list);
        }

        public ActionResult MainMenuSub(int id)
        {
            Menu menu = menuDAO.getRow(id);
            List<Menu> list = menuDAO.getListByParentId("mainmenu", id);//pareant id=0 de hien ra menu (cap 1): khong co menu con
            if (list.Count==0)//menu khong co cap con
            {
                return View("MainMenuSub1", menu);
            }
            else//menu co cap con
            {
                ViewBag.Menu = menu;
                return View("MainMenuSub2", list);
            }
            
        }

        //SLIDE SHOW
        public ActionResult SlideShow()
        {
            List<Slider> list = sliderDAO.getListByPosition("slideshow");//ten ham dat t uy y theo chuc nang
            return View("SlideShow",list);
        }
    }
}
