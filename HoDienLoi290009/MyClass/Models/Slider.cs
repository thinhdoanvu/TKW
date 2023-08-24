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
    [Table ("Sliders")]
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }//bắt buộc

       
        public string Url { get; set; }//bắt buộc, link tới hình

        public string Img { get; set; }//bắt buộc, link tới hình

        public int? Order { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public string Position { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm


        public int? CreatedBy { get; set; }//bắt buộc, tạo bới ai không có [Required] vì kiểu int

        public DateTime? CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
