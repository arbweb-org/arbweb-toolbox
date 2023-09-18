using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Components
{
    public class _c_component : ComponentBase
    {
        protected static MainPage r_mpg = (MainPage)Application.Current.MainPage;
        [Parameter]
        public string g_val { get; set; }
        [Parameter]
        public EventCallback<string> g_valChanged { get; set; }

        public async Task v_val_changed(ChangeEventArgs p_arg)
        {
            g_val = p_arg?.Value?.ToString();
            await g_valChanged.InvokeAsync(g_val);
        }
    }
}