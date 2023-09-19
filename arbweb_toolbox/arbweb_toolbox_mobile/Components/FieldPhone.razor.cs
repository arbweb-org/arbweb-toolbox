using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldPhone : _c_component
    {
        async Task v_select_phone()
        {
            var l_val = await r_mpg.f_contact();
            await g_valChanged.InvokeAsync(l_val);
        }
    }
}