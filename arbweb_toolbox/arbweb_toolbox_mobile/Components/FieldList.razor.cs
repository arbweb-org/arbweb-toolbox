using Microsoft.AspNetCore.Components;

namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldList : _c_component
    {
        [Parameter]
        public string g_ndx { get; set; } // Index of listbox in page

        [Parameter]
        public List<string> g_opt { get; set; } = new List<string>();
    }
}