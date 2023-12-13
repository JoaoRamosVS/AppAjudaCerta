using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AjudaCertaApp.Models
{
    [Serializable]
    public class ViaCEPModel
    {
        [JsonProperty(PropertyName = "cep")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "logradouro")]
        public string Address1 { get; set; }

        [JsonProperty(PropertyName = "complemento")]
        public string Complement { get; set; }

        [JsonProperty(PropertyName = "bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty(PropertyName = "localidade")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "uf")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "unidade")]
        public string Unity { get; set; }

        [JsonProperty(PropertyName = "ibge")]
        public string IBGE { get; set; }

        [JsonProperty(PropertyName = "gia")]
        public string GIA { get; set; }
    }
}
