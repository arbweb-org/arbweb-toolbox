using System.Text.Json;

namespace arbweb_toolbox_api.Models.v1
{
    // Helper class to make 3rd party request
    internal static class _c_api_weather
    {
        // API Key
        public static string g_key { get; set; }

        // Json response from api.openweathermap.org
        class _c_resp_weather
        {
            public _c_data[] weather { get; set; }
            public _c_main main { get; set; }
            public int visibility { get; set; }
            public _c_wind wind { get; set; }
        }

        class _c_data
        {
            public int id { get; set; }
        }

        class _c_main
        {
            public double temp { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int humidity { get; set; }
        }

        class _c_wind
        {
            public double speed { get; set; }
        }

        public static async Task<arbweb_toolbox_lib.Models._c_weather> f_get_id(double p_lat, double p_lon)
        {
            string l_url = $"https://api.openweathermap.org/data/2.5/weather?lat={p_lat}&lon={p_lon}&appid={g_key}";

            HttpClient l_cln = new HttpClient();
            var l_rsp = await l_cln.GetAsync(l_url);
            var l_txt = await l_rsp.Content.ReadAsStringAsync();

            var l_wth = JsonSerializer.Deserialize<_c_resp_weather>(l_txt);

            return new arbweb_toolbox_lib.Models._c_weather
            {
                g_wid = l_wth.weather[0].id,
                g_wnd = l_wth.wind.speed,
                g_vis = l_wth.visibility,
                g_min = l_wth.main.temp_min,
                g_max = l_wth.main.temp_max
            };
        }
    }
}