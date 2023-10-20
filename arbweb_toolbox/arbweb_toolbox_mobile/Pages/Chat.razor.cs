using System.Net;

namespace arbweb_toolbox_mobile.Pages
{
    public partial class Chat
    {
        static MainPage r_mpg = (MainPage)Application.Current.MainPage;
        string r_msg { get; set; } = string.Empty;
        string r_phn { get; set; } = string.Empty;

        async Task v_send()
        {
            string l_msg = WebUtility.UrlEncode(r_msg);
            await r_mpg.v_launch($"whatsapp://send?phone={r_phn}&text={l_msg}");
        }
    }
}