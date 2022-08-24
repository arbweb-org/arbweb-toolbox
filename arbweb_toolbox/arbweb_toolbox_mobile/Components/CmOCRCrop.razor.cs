using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace arbweb_toolbox_mobile.Components
{
    public class Coordinates
    {
        public Rectangle g_img { get; set; }
        public Rectangle g_box { get; set; }
    }

    public class Rectangle
    {
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public partial class CmOCRCrop
    {
        [Parameter]
        public string r_src { get; set; }

        [Parameter]
        public EventCallback<Coordinates> g_closed      // Notify the page to get the rectangle andclose the element
        { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("set_resizable");
            StateHasChanged();

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task<Coordinates> f_coordinates()
        {
            return await JS.InvokeAsync<Coordinates>("get_coordinates");
        }

        async Task v_close()
        {
            var l_crd = await f_coordinates();
            await g_closed.InvokeAsync(l_crd);
        }
    }
}