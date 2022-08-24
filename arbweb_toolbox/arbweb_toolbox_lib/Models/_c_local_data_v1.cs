namespace arbweb_toolbox_lib.Models
{
    public class _c_hijri
    {
        public byte g_day { get; set; }         // Hijri day
        public string g_mnt { get; set; }       // Hijri month name
        public int g_yer { get; set; }          // Hijri year
    }

    public class _c_weather
    {
        public int g_wid { get; set; }          // Weather ID
        public double g_max { get; set; }       // Max temperature
        public double g_min { get; set; }       // Min temperature
        public double g_wnd { get; set; }       // Wind speed
        public int g_vis { get; set; }       // Visibility
    }

    // Json response for api v1
    public class _c_local_data_v1
    {
        public string g_ccd { get; set; }       // ISO country code, ex: (SD)
        public _c_hijri g_dat { get; set; }      // date
        public _c_weather g_wth { get; set; }   // Weather
    }
}