using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("MaCongViecSequence")]
    public class MaCongViecSequence
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string MaDonVi { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(50)]
        public string MaPhongBan { get; set; }

        public int? STT { get; set; }

        [ForeignKey("MaDonVi")]
        public virtual DonVi DonVi { get; set; }

        [ForeignKey("MaPhongBan")]
        public virtual PhongBan PhongBan { get; set; }
    }
}