using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Một số lưu ý:
//Trường nào là khóa chính: KEY
//Trường nào bắt buộc có dự liệu !=NULL:
//Dữ liệu dạng String: mặc định là không bắt buộc (NULL), khi bắt buộc cần có [Required]
//Int, Date, Time: mặc định là bắt buộc, khi không bắt buộc int?, datetime?

namespace MyClass.Models
{
    [Table ("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }


        public int? TopID { get; set; }//không bắt buộc

        [Required]
        public String Title { get; set; }//bắt buộc phải có tiêu đề cho bài viết

        public String Slug { get; set; }//bo sung chu tu dau khong co


       public string Detail { get; set; }//bắt buộc do Slug được sinh ra từ name

        public string Image { get; set; }//không bắt buộc, do lấy từ ID cấp cha

       
        public string PostType { get; set; }//bắt buộc để viết trang đơn hay???

        [Required]
        public String MetaDesc { get; set; }//bắt buộc do là kiểu String

        [Required]
        public String MetaKey { get; set; }//bắt buộc do là kiểu String

        public int CreatedBy { get; set; }//bắt buộc, tạo bới ai không có [Required] vì kiểu int

        public DateTime? CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
