using arbweb_toolbox_lib.Models;
using System.Text;
using System.Text.Json;

namespace arbweb_toolbox_mobile.Components
{
    public partial class CmWidget
    {
        string r_day { get; set; } = string.Empty;  // Hijri day
        string r_mnt { get; set; } = string.Empty;  // Hijri month
        string r_yer { get; set; } = string.Empty;  // Hijri year

        Boolean r_vis { get; set; } = false;        // Is weather image visible
        string r_wid { get; set; } = string.Empty;  // Weather image id

        static string[][] r_typ = new string[][]
        {
            new string[] {"2xx"},                   // Thunderstorm
            new string[] {"3xx", "5xx"},            // Rain
            new string[] {"6xx"},                   // Snow
            new string[] {"800"},                   // Clear
            new string[] {"801", "802", "803"},     // Partial Cloud
            new string[] {"804"},                   // Overcast Cloud
        };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var l_cln = new HttpClient();
            l_cln.Timeout = new TimeSpan(0, 0, 4);

            var l_rsp = await l_cln.GetAsync("http://localhost/api/v1/get_data/");
            var l_str = await l_rsp.Content.ReadAsStringAsync();
            var l_dta = JsonSerializer.Deserialize<_c_local_data_v1>(l_str);

            v_set_date(l_dta.g_dat);
            v_set_weather(l_dta.g_wth);

            r_vis = true;
            StateHasChanged();
        }

        // Set hijri date
        void v_set_date(_c_hijri p_dat)
        {
            // Hijri date
            var l_day = p_dat.g_day.ToString();
            r_day = f_hindu(l_day);
            r_mnt = p_dat.g_mnt;
            r_yer = f_hindu(p_dat.g_yer.ToString()) + " هـ";
        }

        // Set weathe icon
        void v_set_weather(_c_weather p_wth)
        {
            var l_wid = p_wth.g_wid.ToString();
            byte i_ndx;
            for (i_ndx = 0; i_ndx < r_typ.Length; i_ndx++)
            {
                var l_ary = r_typ[i_ndx];

                if (l_ary.Contains(l_wid))
                { break; }
                if (l_ary.Contains(l_wid.Substring(0, 1) + "xx"))
                { break; }
            }

            r_wid = i_ndx.ToString();
        }

        // Convert arabic numerals to hindu
        string f_hindu(string p_arb)
        {
            var l_hnd = new StringBuilder();
            for (byte i_ndx = 0; i_ndx < p_arb.Length; i_ndx++)
            {
                var l_dec = char.ConvertToUtf32(p_arb, i_ndx);
                l_dec += 1584; // Difference between hindu unicode value and arabic one
                l_hnd.Append(char.ConvertFromUtf32(l_dec));
            }

            return l_hnd.ToString();
        }
    }
}