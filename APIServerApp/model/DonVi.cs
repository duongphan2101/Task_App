using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("DonVi")]
    public class DonVi
    {
        [Key]
        [StringLength(50)]
        public string MaDonVi { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDonVi { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<PhongBan> PhongBans { get; set; }
        public virtual ICollection<MaCongViecSequence> MaCongViecSequences { get; set; }
    }
}