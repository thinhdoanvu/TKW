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
    [Table ("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(200)]
        public string Username { get; set; }//bắt buộc

        [Required]
        public string Password { get; set; }//bắt buộc

        [Required]
        public string Fullname { get; set; }//không bắt buộc

        [Required]
        public string Email { get; set; }//không bắt buộc

        public string Phone { get; set; }//không bắt buộc để sắp xếp danh mục sản phẩm

        public int? Gender { get; set; }//bắt buộc do là kiểu String

        public string Address { get; set; }//bắt buộc do là kiểu String

        public DateTime CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
