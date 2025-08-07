using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIServerApp.Model
{
    [Table("TepDinhKemEmail")]
    public class TepDinhKemEmail
    {
        [Key, Column(Order = 0)]
        [StringLength(50)]
        public string MaEmail { get; set; }

        [Key, Column(Order = 1)]
        public int MaTep { get; set; }

        [ForeignKey("MaEmail")]
        public virtual Email Email { get; set; }

        [ForeignKey("MaTep")]
        public virtual TepTin TepTin { get; set; }
    }
}