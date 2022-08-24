using System.Text.Json;

namespace arbweb_toolbox_api.Models.v1
{
    // Helper class to make 3rd party request
    internal static class _c_api_location
    {
        // Json responce form ip-api.com
        class _c_resp_location
        {
            public string countryCode { get; set; }
            public float lat { get; set; }
            public float lon { get; set; }
        }

        public static async 
            Task<(string g_ccd, double g_lat, double g_lon)>
            f_get_location(string p_cip)
        {
            string l_url = "http://ip-api.com/json/" + p_cip; // No api key

            HttpClient l_cln = new HttpClient();
            var l_rsp = await l_cln.GetAsync(l_url);
            var l_txt = await l_rsp.Content.ReadAsStringAsync();

            var l_loc = JsonSerializer.Deserialize<_c_resp_location>(l_txt);
            return (l_loc.countryCode, l_loc.lat, l_loc.lon);
        }
    }
}