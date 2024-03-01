namespace arbweb_toolbox_mobile.Pages
{
    public partial class Test
    {
        static MainPage r_mpg = (MainPage)Application.Current.MainPage;
        double r_dns { get; set; } = 0;
        (double g_wdt, double g_hgt) r_siz { get; set; } = (0, 0);

        protected override async Task OnInitializedAsync()
        {
            r_dns = await r_mpg.f_density();
            r_siz = await r_mpg.f_size();
        }

        string f_style()
        {
            double l_wdt = 204;
            double l_hgt = 324;
            return $"background-color:antiquewhite; width:{l_wdt}px; height:{l_hgt}px; border-radius:12px; margin:16px; padding:16px";
        }
    }
}