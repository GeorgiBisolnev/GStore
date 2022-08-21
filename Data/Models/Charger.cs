using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.Data.Models
{
    public class Charger
    {
        [Key]
        public int SCid { get; set; }

        public string SCCode { get; set; }

        public string SCName { get; set; }
    }
}
