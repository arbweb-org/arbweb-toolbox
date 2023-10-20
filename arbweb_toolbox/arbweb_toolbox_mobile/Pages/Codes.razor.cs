using arbweb_toolbox_mobile.Models;
using System.Net.Http.Json;

namespace arbweb_toolbox_mobile.Pages
{
    public partial class Codes
    {
        class _c_code_vars { public string g_phn, g_amt, g_pin; }

        static MainPage r_mpg = (MainPage)Application.Current.MainPage;

        // List of listboxes
        List<(_c_codes g_cod, List<string> g_chd, string g_val)> r_lst { get; set; } =
            new List<(_c_codes, List<string>, string)>();

        // Short code template to dial
        string r_val { get; set; } = string.Empty;

        // Short code variables to dial
        _c_code_vars r_vrs { get; set; } = new _c_code_vars();

        string r_msg { get; set; } = "Loaded";

        // Initialize
        protected override async Task OnInitializedAsync()
        {
            // Load codes from json file
            _c_codes l_cod = await f_get_codes();

            // Add node
            await v_add_node(l_cod);
        }

        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        async Task<_c_codes> f_get_codes()
        {
            _c_codes l_cod = new _c_codes();

            using HttpClient l_cln = new HttpClient();
            try
            {
                l_cod = await l_cln.GetFromJsonAsync<_c_codes>("https://toolbox.arbweb.org/codes.json");
            }
            catch { }

            return l_cod;
        }

        async Task v_add_node(_c_codes p_cod)
        {
            if (p_cod.g_val == null)
            {
                r_val = string.Empty;
            }
            else
            {
                r_val = p_cod.g_val;
                return;
            }

            if (p_cod.g_chd == null) { return; }

            List<string> l_opt = (from i_opt in p_cod.g_chd
                                  select i_opt.g_ttl).ToList();

            r_lst.Add((p_cod, l_opt, string.Empty));
        }

        async Task v_selected(string p_arg)
        {
            string[] l_rgs = p_arg.Split("-");
            int l_lst = int.Parse(l_rgs[0]);
            int l_opt = int.Parse(l_rgs[1]);

            // Clear lists
            r_lst = r_lst.Take(l_lst + 1).ToList();
            r_lst[l_lst] = (r_lst[l_lst].g_cod, r_lst[l_lst].g_chd, l_opt.ToString());

            _c_codes l_cod = r_lst[l_lst].g_cod.g_chd[l_opt];

            // Add node
            await v_add_node(l_cod);

            // Clear vars
            r_vrs = new _c_code_vars();
        }

        async Task v_dial()
        {
            string l_val = r_val.
                Replace("📞", r_vrs.g_phn).
                Replace("💰", r_vrs.g_amt).
                Replace("🔑", r_vrs.g_pin).
                Replace("#", "%23");

            if (IsDebug())
            {
                r_msg = l_val;
            }
            else
            {
                await r_mpg.v_launch("tel:" + l_val);
            }
        }
    }
}