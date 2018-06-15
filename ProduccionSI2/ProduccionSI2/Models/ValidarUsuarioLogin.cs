using System;
using System.Collections.Generic;
using System.Text;

namespace ProduccionSI2.Models
{
    using Newtonsoft.Json;
    class ValidarUsuarioLogin
    {
        [JsonProperty("user")]
        public string user { get; set; }

        [JsonProperty("pass")]
        public string pass { get; set; }
    }

    
}
