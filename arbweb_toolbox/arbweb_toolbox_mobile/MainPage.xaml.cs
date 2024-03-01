using System.Collections.ObjectModel;
using System.ComponentModel;

namespace arbweb_toolbox_mobile
{
    public class _c_item
    {
        public string g_nam { get; set; }

        public _c_item(string p_nam) 
        { 
            g_nam = p_nam;
        }
    }

    public class _c_model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<_c_item> g_itm { get; private set; } = 
            new ObservableCollection<_c_item>
            {
                new _c_item("A"),
                new _c_item("B"),
                new _c_item("C"),
                new _c_item("D")
            };
    }

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}