using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.Data.Models
{
    public class SDOType
    {
        [Key]
        public int SDOTid { get; set; }

        public string SDOTName { get; set; }

        public string SDOTCode { get; set; }
    }
}
