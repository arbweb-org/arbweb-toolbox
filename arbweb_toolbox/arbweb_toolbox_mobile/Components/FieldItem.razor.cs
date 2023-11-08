using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldItem : _c_component
    {
        Boolean r_sel { get; set; } = false;

        [Parameter]
        public DateTime g_dat { get; set; }

        [Parameter]
        public string g_ttl { get; set; }

        [Parameter]
        public string g_icn { get; set; }

        string f_date()
        {
            var l_clt = new CultureInfo("ar-ar");
            var l_dat = g_dat.ToString("dddd d MMMM yyyy | h:m t", l_clt);

            return l_dat;
        }

        string f_icon()
        {
            return $"bi-{g_icn}";
        }

        async Task v_selected()
        {
            r_sel = true;
        }

        string f_background()
        {
            return "background-color:" + (r_sel ? "whitesmoke" : "transparent");
        }
    }
}