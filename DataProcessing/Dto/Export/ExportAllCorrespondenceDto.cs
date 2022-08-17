using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.DataProcessing.Dto.Export
{
    [JsonObject]
    public class ExportAllCorrespondenceDto
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Correspondences")]
        public UserCorrespondencesArrayDto[] Correspondences { get; set; }
    }
}
