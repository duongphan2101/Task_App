using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("NguoiNhanEmail")]
    public class NguoiNhanEmail
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string? MaEmail { get; set; }

        [Key, Column(Order = 1)]
        public int? MaNguoiDung { get; set; }

        [StringLength(10)]
        public string? VaiTro { get; set; }

        [ForeignKey("MaEmail")]
        public virtual Email? Email { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung? NguoiDung { get; set; }
    }
}