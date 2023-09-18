using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldPhone : _c_component
    {
        async Task v_select_phone()
        {
            g_val = await r_mpg.f_contact();
        }
    }
}