using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace arbweb_toolbox_mobile.Components
{
    public partial class NavBar : _c_component
    {
        [Inject]
        public IJSRuntime JS { get; set; }

        async Task v_back() 
        {
            JS.InvokeAsync<IJSObjectReference>("v_back");
        }
    }
}