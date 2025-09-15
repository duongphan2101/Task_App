using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIServerApp.Model
{
    [Table("Email")]
    public class Email
    {
        [Key]
        [StringLength(200)]
        public string? MaEmail { get; set; }

        [ForeignKey("NguoiGuiObj")]
        public int NguoiGui { get; set; }

        [ForeignKey("ChiTietCongViec")]
        public int MaChiTietCV { get; set; }

        [StringLength(255)]
        public string? TieuDe { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? NoiDung { get; set; }

        public DateTime? NgayGui { get; set; }
        public int TrangThai { get; set; }

        [StringLength(255)]
        public string? MessageId { get; set; }   // định danh duy nhất cho email này

        [StringLength(255)]
        public string? InReplyTo { get; set; }   // trỏ đến MessageId của email cha

        [Column(TypeName = "nvarchar(max)")]
        public string? References { get; set; }  // chuỗi MessageId liên quan (cách nhau bởi dấu cách)

        public virtual ChiTietCongViec? ChiTietCongViec { get; set; }
        public virtual NguoiDung? NguoiGuiObj { get; set; }
        [JsonIgnore]
        public virtual ICollection<NguoiNhanEmail>? NguoiNhanEmails { get; set; }
        [JsonIgnore]
        public virtual ICollection<TepDinhKemEmail>? TepDinhKemEmails { get; set; }
    }
}