using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Aspose.App.Api.Models
{
    public class Error2ForumRequest
    {
        [JsonPropertyName("session")]
        public string Session { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("application")]
        public string Application { get; set; }
    }
}
