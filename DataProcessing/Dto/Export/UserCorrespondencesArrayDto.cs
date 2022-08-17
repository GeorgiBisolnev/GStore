using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.DataProcessing.Dto.Export
{
    [JsonObject]
    public class UserCorrespondencesArrayDto
    {
        public string? DtCode { get; set; }

        public string? KtCode { get; set; }

        public string? DotCode { get; set; }
    }
}
