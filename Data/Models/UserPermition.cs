using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.Data.Models
{
    public class UserPermition
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int SUid { get; set; }

        [Required]
        public User User { get; set; }

        public string SCDtCode { get; set; }

        public string SCCtCode { get; set; }

        public string SDOTCode { get; set; }

    }
}
