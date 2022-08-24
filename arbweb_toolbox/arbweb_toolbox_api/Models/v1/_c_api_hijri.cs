using System.Text.Json;

namespace arbweb_toolbox_api.Models.v1
{
    // Helper class to make 3rd party request
    internal static class _c_api_hijri
    {
        // Json responce form api.aladhan.com
        class _c_resp_hijri
        {
            public _c_data data { get; set; }
        }

        class _c_data
        {
            public _c_date hijri { get; set; }
        }

        class _c_date
        {
            public string day { get; set; }
            public _c_month month { get; set; }
            public string year { get; set; }
        }

        class _c_month
        {
            public int number { get; set; }
            public string en { get; set; }
            public string ar { get; set; }
        }

        public static async Task<arbweb_toolbox_lib.Models._c_hijri> f_get_date()
        {
            string l_url = "http://api.aladhan.com/v1/gToH"; // No api key

            HttpClient l_cln = new HttpClient();
            var l_rsp = await l_cln.GetAsync(l_url);
            var l_txt = await l_rsp.Content.ReadAsStringAsync();

            var l_hij = JsonSerializer.Deserialize<_c_resp_hijri>(l_txt);
            var l_dat = l_hij.data.hijri;
            return new arbweb_toolbox_lib.Models._c_hijri
            {
                g_day = byte.Parse(l_dat.day),
                g_mnt = l_dat.month.ar,
                g_yer = int.Parse(l_dat.year)
            };
        }
    }
}