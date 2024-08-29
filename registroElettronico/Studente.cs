using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace registroElettronico
{
    public class Studente
    {
        [JsonProperty("matricola")]
        public int matricola {get; set; }

        [JsonProperty("nome")]
        public string nome { get; set; }

        [JsonProperty("cognome")]
        public string cognome { get; set; }

        [JsonProperty("data_nascita")]
        public DateTime dataNascita { get; set; }

        [JsonProperty("luogo_nascita")]
        public string luogoNascita { get; set; }

        [JsonProperty("classe")]
        public string classe { get; set; }

        [JsonProperty("voti")]
        public Dictionary<string, List<int>> voti { get; set; }
    }
}
