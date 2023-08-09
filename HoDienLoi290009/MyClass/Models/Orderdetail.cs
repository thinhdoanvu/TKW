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
    [Table ("Orderdetail")]
    public class Orderdetail
    {
        [Key]
        public int Id { get; set; }

        public int OrderID { get; set; }//bắt buộc, không được để trống

        public int ProductID { get; set; }//bắt buộc

        public decimal Price { get; set; }//bắt buộc

        public int Qty { get; set; }//bắt buộc

        public decimal Amount { get; set; }//bắt buộc

       //có thể thêm vào giá giảm, chiết khấu....
    }
}
