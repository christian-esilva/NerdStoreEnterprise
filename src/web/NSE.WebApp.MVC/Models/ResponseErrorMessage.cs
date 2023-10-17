using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NSE.WebApp.MVC.Models
{
    public class ResponseErrorMessage
    {
        [JsonPropertyName("Mensagens")]
        public List<string> Messages { get; set; }
    }
}
