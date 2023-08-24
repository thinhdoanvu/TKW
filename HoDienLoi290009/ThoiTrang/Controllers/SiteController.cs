using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrang.Controllers
{
    public class SiteController : Controller
    {
        //kiem tra Link
        LinkDAO linkDAO = new LinkDAO();
        ProductDAO productDAO = new ProductDAO();
        PostDAO postDAO = new PostDAO();
        CategoryDAO categoryDAO = new CategoryDAO();

        // URL mặc định hoặc bất kỳ
        public ActionResult Index(string slug = null)
        {
            if (slug == null)
            {
                //trang chu
                return this.Home();
            }
            else
            {
                //tim slug co trong bang link
                Link link = linkDAO.getRow(slug);
                if (link != null)
                {
                    //slug co trong bang link
                    //lay ra kieu link
                    string typelink = link.Type;

                    if (typelink == "category")
                    {
                        return this.ProductCategory(slug);
                    }
                    if (typelink == "topic")
                    {
                        return this.PostTopic(slug);
                    }
                    if (typelink == "page")
                    {
                        return this.PostPage(slug);
                    }
                    else
                    {
                        return this.Error404(slug);
                    }
                }
                else
                {
                    //slug khong co trong bang link
                    //slug co trong bang Product khong?
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        //goi den Product
                        return this.ProductDetail(slug);
                    }
                    else//Slug co trong bang Post có posttype=post khong?
                    {
                        Post post = postDAO.getRow(slug);
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }

                    }

                }
            }
        }

        //Khai báo URL cố định

        //TRANG HOME
        public ActionResult Home()
        {
            List<Category> list = categoryDAO.getListBypararentId(0);//parent ID=0
            return View("Home",list);
        }

        //TRANG HOME PRODUCT
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            //lay ra danh sach cac sản phẩm
            List<Product> list = productDAO.getListByCatId(id,4);//Do tim theo ID cua Cater, gioi hạn so mau tin cho dep
            return View("HomeProduct",list);
        }

        //Nhom action lien quan den san pham
        //Danh cho product
        public ActionResult Product()
        {
            return View("Product");
        }

        public ActionResult ProductCategory(string slug)
        {
            return View("ProductCategory");
        }

        public ActionResult ProductDetail(string product)
        {
            return View("ProductDetail");
        }


        //nhom action lien quan den bai viet
        //Danh cho post
        public ActionResult Post()
        {
            return View("Post");
        }
        public ActionResult PostTopic(string slug)
        {
            return View("PostTopic");
        }

        public ActionResult PostPage(string slug)
        {
            return View("PostPage");
        }

        public ActionResult PostDetail(Post post)
        {
            return View("PostDetail");
        }

        //Ham loi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}