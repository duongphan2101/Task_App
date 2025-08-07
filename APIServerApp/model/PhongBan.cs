using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("PhongBan")]
    public class PhongBan
    {
        [Key]
        [StringLength(50)]
        public string MaPhongBan { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhongBan { get; set; }

        [StringLength(50)]
        public string MaDonVi { get; set; }

        [ForeignKey("MaDonVi")]
        public virtual DonVi DonVi { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<MaCongViecSequence> MaCongViecSequences { get; set; }
    }
}