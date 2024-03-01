#if ANDROID
using Android.Content.Res;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#elif WINDOWS
using Windows.UI;
#endif


namespace arbweb_toolbox_mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            v_add_entry_handler();
            v_add_button_handler();
        }

        void v_add_entry_handler()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(IEntry), (p_hnd, p_viw) =>
            {
#if ANDROID
                p_hnd.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                p_hnd.PlatformView.SetPadding(0, 0, 0, 0);

#elif IOS || MACCATALYST
                //
                p_hnd.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                
#elif WINDOWS
                p_hnd.PlatformView.Style = null;
                p_hnd.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
                p_hnd.PlatformView.CornerRadius = new Microsoft.UI.Xaml.CornerRadius(0);
                p_hnd.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(0);
#endif
            });
        }

        void v_add_button_handler()
        {
            Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping(nameof(IButton), (p_hnd, p_viw) =>
            {
#if ANDROID
                //

#elif IOS || MACCATALYST
                //

#elif WINDOWS
                //
#endif
            });
        }
    }
}