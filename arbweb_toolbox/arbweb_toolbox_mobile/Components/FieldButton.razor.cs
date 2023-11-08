using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldButton : _c_component
    {
        [Parameter]
        public string g_icn { get; set; }

        public EventCallback g_clicked { get; set; }

        string f_icon()
        {
            return $"bi bi-{g_icn}";
        }
    }
}