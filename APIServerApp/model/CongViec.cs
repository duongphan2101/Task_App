using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("CongViec")]
    public class CongViec
    {
        [Key]
        [StringLength(50)]
        public string? MaCongViec { get; set; }

        [ForeignKey("NguoiGiaoObj")]
        public int? NguoiGiao { get; set; }

        public DateTime NgayGiao { get; set; }
        public bool? LapLai { get; set; }

        [StringLength(50)]
        public string? TanSuat { get; set; }

        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual NguoiDung? NguoiGiaoObj { get; set; }
        public virtual ICollection<ChiTietCongViec>? ChiTietCongViecs { get; set; }
        public virtual ICollection<NguoiLienQuanCongViec>? NguoiLienQuanCongViecs { get; set; }
        public virtual ICollection<PhanHoiCongViec>? PhanHoiCongViecs { get; set; }
    }
}