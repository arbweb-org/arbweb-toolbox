using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Pages
{
    public partial class Index
    {
        [Inject]
        NavigationManager g_nav { get; set; }

        string[] r_itm { get; set; } = new string[]
        {
            "الكل",
            "الرموز المختصرة",
            "محادثة سريعة",
            "المفكرة",
            "الإعدادت"
        };

        string f_item_color(int p_ndx)
        {
            switch (p_ndx)
            {
                case 0:
                    return "gold";

                case < 4:
                    return "aliceblue";

                default:
                    return "whitesmoke";
            }
        }

        async Task v_nav(string p_pag)
        {
            g_nav.NavigateTo(p_pag);            
        }
    }
}