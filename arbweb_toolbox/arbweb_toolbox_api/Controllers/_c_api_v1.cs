using arbweb_toolbox_api.Models.v1;
using arbweb_toolbox_lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace arbweb_toolbox_api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class _c_api_v1 : ControllerBase
    {
        [HttpGet]
        [Route("get_data")]
        public async Task<_c_local_data_v1> f_get_data()
        {
            // Client IP
            var l_cip = "41.95.110.154"; // Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var l_loc = await _c_api_location.f_get_location(l_cip);
            var l_dat = await _c_api_hijri.f_get_date();
            var l_wth = await _c_api_weather.f_get_id(l_loc.g_lat, l_loc.g_lon);

            var l_dta = new _c_local_data_v1
            {
                g_ccd = l_loc.g_ccd,
                g_dat = l_dat,
                g_wth = l_wth
            };

            return l_dta;
        }
    }
}