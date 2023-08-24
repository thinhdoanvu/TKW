using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }

        public int CatID { get; set; }//bắt buộc

        public string CatName { get; set; } //bắt buộc phải có tên cho sản phẩm

        public string Supplier { get; set; } //

        public string Slug { get; set; }//không bắt buộc do Slug được sinh ra từ name

        public string Detail { get; set; }//không bắt buộc

        public string Image { get; set; }//không bắt buộc

        public decimal Price { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public decimal PriceSale { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public int Number { get; set; }//bắt buộc phải có số lượng sản phẩm

        public int MetaDesc { get; set; }//bắt buộc do là kiểu String

        public int MetaKey { get; set; }//bắt buộc do là kiểu String

        public int CreatedBy { get; set; }//bắt buộc, tạo bới ai không có [Required] vì kiểu int

        public DateTime CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
