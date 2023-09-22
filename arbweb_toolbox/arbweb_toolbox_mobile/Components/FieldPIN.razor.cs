using System.Text.Json;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldPIN : _c_component
    {
        // No saved PIN is selected, can edit input
        Boolean r_edt { get; set; } = true;
        // Saved PINs
        static Dictionary<string, string> r_pns { get; set; }
        string r_key { get; set; } = null;

        async Task v_load_pins()
        {
            if (r_pns != null) { return; }

            string l_jsn = await r_mpg.f_get_secure("saved_pins");
            if (string.IsNullOrEmpty(l_jsn))
            {
                r_pns = new Dictionary<string, string>();
                await v_save_pins();
            }
            else
            {
                r_pns = JsonSerializer.Deserialize<Dictionary<string, string>>(l_jsn);
            }
        }

        async Task v_save_pins()
        {
            string l_jsn = JsonSerializer.Serialize(r_pns);
            await SecureStorage.Default.SetAsync("saved_pins", l_jsn);
        }

        async Task v_add_pin()
        {
            await v_load_pins();

            var l_prm = await r_mpg.f_prompt("الاسم", "اختر عنوانا لكلمة المرور", "حفظ");
            if (!l_prm.g_rtr) { return; }

            if (r_pns.ContainsKey(l_prm.g_res))
            {
                await r_mpg.v_msg("الاسم موجود بالفعل");
                return;
            }

            r_key = l_prm.g_res;
            r_pns.Add(r_key, g_val);
            await v_save_pins();

            r_edt = false;
        }

        async Task v_select_pin()
        {
            await v_load_pins();

            var l_pns = (from i_pin in r_pns
                         select "□ " + i_pin.Key).ToArray();

            string l_pin = await r_mpg.f_sheet("الأرقام السرية المحفوظة", l_pns);
            if (!(l_pin.StartsWith("□"))) { return; }

            r_key = l_pin.Substring(2);
            var l_val = r_pns[r_key];

            r_edt = false;
            await g_valChanged.InvokeAsync(l_val);
        }

        async Task v_edit()
        {
            r_edt = true;
            await g_valChanged.InvokeAsync(string.Empty);
        }

        async Task v_delete()
        {
            var l_rtr = await r_mpg.f_confirm("هل تريد حذف الرقم السري؟", "حذف");
            if (!l_rtr) { return; }

            r_pns.Remove(r_key);
            await v_save_pins();

            r_edt = true;
            await g_valChanged.InvokeAsync(string.Empty);
        }
    }
}