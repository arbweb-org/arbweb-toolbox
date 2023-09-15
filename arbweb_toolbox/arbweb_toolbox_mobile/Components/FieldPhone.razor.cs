namespace arbweb_toolbox_mobile.Components
{
    public partial class FieldPhone : _c_component
    {
        public string g_phn { get; set; }

        async Task v_select_phone()
        {
            g_phn = await r_mpg.f_contact();
        }
    }
}