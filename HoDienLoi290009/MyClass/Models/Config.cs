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
    [Table ("Configs")]
    public class Config
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
