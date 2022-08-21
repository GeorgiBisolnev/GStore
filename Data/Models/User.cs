using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.Data.Models
{
    public class User
    {
        public User()
        {
            this.Permitions = new List<UserPermition>();
        }
        [Key]
        public int SUid { get; set; }

        public string SUName { get; set; }

        //public string SUFullName { get; set; }

        //public string SUComment { get; set; }

        //public string SUFName { get; set; }

        //public string SUSName { get; set; }

        //public string SULName { get; set; }

        public int SDid { get; set; }

        public ICollection<UserPermition> Permitions { get; set; }
    }
}
