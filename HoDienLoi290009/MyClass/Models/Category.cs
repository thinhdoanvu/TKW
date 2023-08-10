using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Một số lưu ý:
//Trường nào là khóa chính: KEY
//Trường nào bắt buộc có dự liệu !=NULL:
//Dữ liệu dạng String: mặc định là không bắt buộc (có thể có NULL), khi bắt buộc cần có [Required]
//Int, Date, Time: mặc định là bắt buộc, khi không bắt buộc int?, datetime?


namespace MyClass.Models
{
    [Table("Categorys")]//sử dụng để khi chương trình thực hiện sẽ tạo ra bảng, Table thì có s còn class ko s
    public class Category
    {
        [Key]
        public int Id {get;set;}        
        
        [Required(ErrorMessage ="Tên loại sản phẩm không được để trống")]
        [StringLength(200)]

        //[Display(Name = "tên danh mục")]//có thể chèn dòng này để thay đổi tên trường thành tên của mình đặt
        public string Name { get; set; }//bắt buộc

        public string Slug { get; set; }//không bắt buộc do Slug được sinh ra từ name

        public int? ParentId { get; set; }//không bắt buộc, do lấy từ ID cấp cha

        public int? Order { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm
        
        [Required]
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
