using System.Text.Json.Serialization;

namespace arbweb_toolbox_mobile.Models
{
    internal class _c_codes
    {
        [JsonPropertyName("العنوان")]
        public string g_ttl { get; set; }
        [JsonPropertyName("القيمة")]
        public string g_val { get; set; }
        [JsonPropertyName("العناصر")]
        public _c_codes[] g_chd { get; set; }
    }
}