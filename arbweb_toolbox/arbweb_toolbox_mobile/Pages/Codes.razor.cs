using arbweb_toolbox_mobile.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace arbweb_toolbox_mobile.Pages
{
    class _c_code_vars { public string g_phn, g_amt, g_pin; }

    public partial class Codes
    {
        static MainPage r_mpg = (MainPage)Application.Current.MainPage;

        // List of listboxes
        List<(_c_codes g_cod, List<string> g_opt, string g_val)> r_lst { get; set; } = 
            new List<(_c_codes, List<string>, string)> ();

        // Short code template to dial
        string r_val { get; set; } = string.Empty;

        // Short code variables to dial
        _c_code_vars r_vrs = new _c_code_vars ();

        string r_msg { get; set; } = "Start";

        // Initialize
        protected override async Task OnInitializedAsync()
        {
            // Load codes from json file
            _c_codes l_cod = await f_saved_codes();

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

        async Task<_c_codes> f_saved_codes()
        {
            string l_jsn = await r_mpg.f_json("codes");

            _c_codes l_cod = new _c_codes();

            JsonSerializerOptions l_opt = new JsonSerializerOptions();
            l_opt.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            l_opt.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;

            return JsonSerializer.Deserialize<_c_codes>(l_jsn, l_opt);
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

        async Task v_selected(ChangeEventArgs p_arg)
        {
            string[] l_val = p_arg.Value.ToString().Split("-");
            int l_lst = int.Parse(l_val[0]);
            int l_opt = int.Parse(l_val[1]);

            // Clear lists
            r_lst = r_lst.Take(l_lst + 1).ToList();
            r_lst[l_lst] = (r_lst[l_lst].g_cod, r_lst[l_lst].g_opt, l_opt.ToString());

            _c_codes l_cod = r_lst[l_lst].g_cod.g_chd[l_opt];

            // Add node
            await v_add_node(l_cod);

            // Refresh
            StateHasChanged();
        }

        async Task v_dial()
        {
            string l_val = r_val.
                Replace("📞", r_vrs.g_phn).
                Replace("💰", r_vrs.g_amt).
                Replace("🔑", r_vrs.g_pin).
                Replace("#", "%23");

            await r_mpg.v_dial(l_val);
        }
    }
}