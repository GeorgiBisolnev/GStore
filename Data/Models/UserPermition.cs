using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorKoorespondencii.Data.Models
{
    public class UserPermition
    {      
        [Required]
        [ForeignKey(nameof(User))]
        public int SUid { get; set; }

        [Required]
        public User User { get; set; }

        public string SCDtCode { get; set; }

        public string SCCtCode { get; set; }

        public string SDOTCode { get; set; }

        public int? SDSTid { get; set; }

        public byte ve_SDoc { get; set; }

        public byte v_SDoc { get; set; }

        public byte vc_Price { get; set; }

        public byte ve_ISDocDt { get; set; }

        public byte ve_ISDocCt { get; set; }

        public byte? can_Block { get; set; }

        public byte? can_Unblock  { get; set; }

        [Key]
        public int ID { get; set; }

        public int? RoleID { get; set; }
    }
}
