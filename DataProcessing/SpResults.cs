using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.DataProcessing
{
    [Keyless]
    public class SpResults
    {
        public int ReturnValue { get; set; }
        public int Sum { get; set; }
    }
}
