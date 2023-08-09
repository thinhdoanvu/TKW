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
    [Table ("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }//không bắt buộc do lấy dữ liệu từ bảng User

        public String Fullname { get; set; }//không bắt buộc do lấy dữ liệu từ bảng User

        public String Phone { get; set; }//không bắt buộc do lấy dữ liệu từ bảng User

        public String Email { get; set; }//không bắt buộc do lấy dữ liệu từ bảng User

        [Required]
        public String Title { get; set; }//bắt buộc vì mỗi User có thể có nhiều lượt liên hệ

        [Required]
        public String Detail { get; set; }//bắt buộc vì mỗi User có thể có nhiều lượt liên hệ

        public DateTime CreatedAt { get; set; }//bắt buộc có ngày liên hệ

        public int? UpdateBy { get; set; }//không bắt buộc

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
