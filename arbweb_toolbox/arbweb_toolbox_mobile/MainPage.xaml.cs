using Communication = Microsoft.Maui.ApplicationModel.Communication;

namespace arbweb_toolbox_mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async Task v_msg(string p_msg)
        {
            await DisplayAlert("صندوق الأدوات", p_msg, "تم");
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

                return await f_sheet("اختر رقم الهاتف", l_phn);
            }
            catch (Exception p_exp)
            {
                return string.Empty;
            }
        }

        public async Task v_dial(string p_num)
        {
            await Launcher.OpenAsync("tel:" + p_num);
        }

        public async Task<string> f_json(string p_nam)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(p_nam + ".json");
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}