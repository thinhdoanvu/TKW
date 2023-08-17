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
    [Table ("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int CatID { get; set; }//bắt buộc

        [Required]
        public string Name { get; set; } //bắt buộc phải có tên cho sản phẩm

        public string Supplier { get; set; } //
        
        public string Slug { get; set; }//không bắt buộc do Slug được sinh ra từ name

        [Required]
        public string Detail { get; set; }//không bắt buộc

        public string Image { get; set; }//không bắt buộc

        public decimal Price { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public decimal PriceSale { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public int Number { get; set; }//bắt buộc phải có số lượng sản phẩm


        public int MetaDesc { get; set; }//bắt buộc do là kiểu String

        [Required]
        public int MetaKey { get; set; }//bắt buộc do là kiểu String

        public int CreatedBy { get; set; }//bắt buộc, tạo bới ai không có [Required] vì kiểu int

        public DateTime CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
