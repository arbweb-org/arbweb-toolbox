using arbweb_OCR;
using System.Reflection;
using System.Text.Json.Nodes;

namespace arbweb_toolbox_mobile.Pages
{
    public partial class Index
    {
        string r_msgs { get; set; } = "Hello, Yassin!";
        byte[] r_byt = new byte[] { };

        void v_show_message(string p_lbl, string p_msg)
        {
            r_msgs = p_lbl + ": " + p_msg;
            StateHasChanged();
        }

        async Task v_set_embedded()
        {
            Microsoft.Maui.Graphics.IImage l_img;
            Assembly l_asm = GetType().GetTypeInfo().Assembly;
            using (Stream l_src = l_asm.GetManifestResourceStream("arbweb_toolbox_mobile.Resources.Images.test.png"))
            {
                l_img = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(l_src);
            }
            r_byt = l_img.AsBytes();

            await v_end();
        }

        async Task v_set_camera()
        {
            FileResult l_pic = await MediaPicker.Default.CapturePhotoAsync();

            using (Stream l_src = await l_pic.OpenReadAsync())
            {
                using (MemoryStream l_mem = new MemoryStream())
                {
                    l_src.CopyTo(l_mem);
                    r_byt = l_mem.ToArray();
                }
            }

            Microsoft.Maui.Graphics.IImage l_img;
            l_img = Microsoft.Maui.Graphics.Platform.PlatformImage.FromStream(new MemoryStream(r_byt));
            var l_dwn = l_img.Downsize(500);

            using (MemoryStream l_mem = new MemoryStream())
            {
                l_dwn.Save(l_mem, ImageFormat.Jpeg);
                r_byt = l_mem.ToArray();
            }

            await v_end();
        }

        async Task v_set_studio()
        {
            FileResult l_pic = await MediaPicker.Default.PickPhotoAsync();

            using (Stream l_src = await l_pic.OpenReadAsync())
            {
                using (MemoryStream l_mem = new MemoryStream())
                {
                    l_src.CopyTo(l_mem);
                    r_byt = l_mem.ToArray();
                }
            }

            await v_end();
        }

        async Task<string> f_text()
        {
            var l_out = "Failed";

            return await _c_OCR.f_text(r_byt);

            return l_out;
        }

        async Task v_end()
        {
            v_show_message("Bytes", r_byt.Length.ToString());

            v_show_message("Text", await f_text());
        }
    }
}