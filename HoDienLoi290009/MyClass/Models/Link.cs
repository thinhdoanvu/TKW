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
    [Table ("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên link không được để trống")]
        [StringLength(200)]
        public string Name { get; set; }//bắt buộc

        [Required]
        public string Slug { get; set; }//không bắt buộc do Slug được sinh ra từ name

        [Required]
        public int TableId { get; set; }//bắt buộc, lấy từ bảng nào

        [Required]
        public string Type { get; set; }//không bắt buộc, kiểu link
    }
}
