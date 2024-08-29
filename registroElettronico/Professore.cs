using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registroElettronico
{
    public class Professore
    {
        [JsonProperty("matricola")]
        public int matricola { get; set; }

        [JsonProperty("nome")]
        public string nome { get; set; }

        [JsonProperty("cognome")]
        public string cognome { get; set; }
    }
}
