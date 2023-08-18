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
//Dữ liệu dạng String: mặc định là không bắt buộc (có thể có NULL), khi bắt buộc cần có [Required]
//Int, Date, Time: mặc định là bắt buộc, khi không bắt buộc int?, datetime?

namespace MyClass.Models
{
    [Table ("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên menu không được để trống")]
        [StringLength(200)]
        public string Name { get; set; }//bắt buộc

        public int? TableId { get; set; }//moi them

        public string TypeMenu { get; set; }//moi them

        public string Position { get; set; }//moi them

        public string Link { get; set; }//bắt buộc do cần biết  link tới menu nào

        public int? ParentId { get; set; }//không bắt buộc, do lấy từ ID cấp cha

        public int? Order { get; set; }//không bắt buộc để sắp xếp thứ tự menu

        public int CreatedBy { get; set; }//bắt buộc, tạo bới ai không có [Required] vì kiểu int

        public DateTime CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
