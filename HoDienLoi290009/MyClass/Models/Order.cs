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
    [Table ("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? UserID { get; set; }//bắt buộc

        public string ReceiverAddress { get; set; }//không bắt buộc vì có khả năng của chính User đăng nhập

        public string ReceiverPhone { get; set; }//không bắt buộc vì có khả năng của chính User đăng nhập

        public string ReceiverEmail { get; set; }//không bắt buộc vì có khả năng của chính User đăng nhập

        public string Note { get; set; }//không bắt buộc

        public DateTime CreatedAt { get; set; }//bắt buộc khi khởi tạo

        public int? UpdateBy { get; set; }//không bắt buộc (trạng thái thay đổi ghi chú hoặc đơn hàng)

        public DateTime? UpdateAt { get; set; }//không bắt buộc khi cập nhật

        public int Status { get; set; }//bắt buộc
    }
}
