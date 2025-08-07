using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("NguoiLienQuanCongViec")]
    public class NguoiLienQuanCongViec
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string MaCongViec { get; set; }

        [Key, Column(Order = 1)]
        public int MaNguoiDung { get; set; }

        [Required]
        [StringLength(10)]
        public string VaiTro { get; set; } // 'to', 'cc', 'bcc'

        [ForeignKey("MaCongViec")]
        public virtual CongViec CongViec { get; set; }

        [ForeignKey("MaNguoiDung")]
        public virtual NguoiDung NguoiDung { get; set; }
    }
}