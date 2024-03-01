using Communication = Microsoft.Maui.ApplicationModel.Communication;

namespace arbweb_toolbox_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += v_loaded;
        }

        private async void v_loaded(object sender, EventArgs e)
        {
#if WINDOWS
            var l_wbv = blazorWebView.Handler.PlatformView as Microsoft.UI.Xaml.Controls.WebView2;
            await l_wbv.EnsureCoreWebView2Async();
            
            var l_stn = l_wbv.CoreWebView2.Settings;
            l_stn.IsZoomControlEnabled = false;
            l_stn.AreBrowserAcceleratorKeysEnabled = false;
#endif

#if ANDROID
            var l_wbv = blazorWebView.Handler.PlatformView as Android.Webkit.WebView;
            l_wbv.Settings.LoadWithOverviewMode = false;
            l_wbv.Settings.TextZoom = 100;
#endif
        }

        public async Task v_msg(string p_msg)
        {
            await DisplayAlert("صندوق الأدوات", p_msg, "تم", FlowDirection.RightToLeft);
        }

        public async Task<Boolean> f_confirm(string p_msg, string p_cnf) 
        {
            return await DisplayAlert("صندوق الأدوات", p_msg, p_cnf, "إلغاء", FlowDirection.RightToLeft);
        }

        public async Task<(string g_res, Boolean g_rtr)> f_prompt(string p_ttl, string p_msg, string g_scd)
        {
            string l_res = await DisplayPromptAsync(p_ttl, p_msg, g_scd, "إلغاء");

            return (l_res, !(string.IsNullOrWhiteSpace(l_res)));
        }

        public async Task<string> f_sheet(string p_ttl, string[] p_opt)
        {
            return await DisplayActionSheet(p_ttl, "إلغاء", null, p_opt);
        }

        public async Task<string> f_contact()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.ContactsRead>();
                Contact l_ctc = await Communication.Contacts.Default.PickContactAsync();

                if (l_ctc == null)
                { return string.Empty; }

                List<ContactPhone> l_lst = l_ctc.Phones;

                string[] l_phn = (from i_phn in l_lst
                                  select i_phn.PhoneNumber.Replace(" ", string.Empty)).ToArray();

                string l_res = await f_sheet("اختر رقم الهاتف", l_phn);
                if (string.IsNullOrWhiteSpace(l_res)) { return string.Empty; }

                char l_chr = l_res[0];
                if (!(char.IsDigit(l_chr) || l_chr == '+')) { return string.Empty; }

                return l_res;
            }
            catch 
            { 
                return string.Empty;
            }
        }

        public async Task v_launch(string p_url)
        {
            await Launcher.OpenAsync(p_url);
        }

        public async Task<string> f_json(string p_nam)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(p_nam + ".json");
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch { }

            return null;
        }

        public async Task<string> f_get_secure(string g_key)
        {
            try
            {
                return await SecureStorage.Default.GetAsync(g_key);
            }
            catch
            {
                return null;
            }
        }

        public async Task v_set_secure(string p_key, string p_val)
        {
            await SecureStorage.Default.SetAsync(p_key, p_val);
        }

        public async Task<double> f_density()
        {
            double l_dns = 0;

            l_dns = DeviceDisplay.Current.MainDisplayInfo.Density;

            return l_dns;
        }

        public async Task<(double g_wdt, double g_hgt)> f_size()
        {
            double l_wdt = 0;
            double l_hgt = 0;

            l_wdt = DeviceDisplay.Current.MainDisplayInfo.Width;
            l_hgt = DeviceDisplay.Current.MainDisplayInfo.Height;

            return (l_wdt, l_hgt);
        }
    }
}