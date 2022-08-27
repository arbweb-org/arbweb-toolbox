using arbweb_toolbox_mobile.Components;
using SkiaSharp;

namespace arbweb_toolbox_mobile.Pages
{
    public partial class Index
    {
        string r_msg { get; set; } = "Hello";

        byte[] r_byt = new byte[] { };              // Image bytes
        string r_src { get; set; } = string.Empty;  // Image as base64

        CmOCRCrop r_scn { get; set; }               // OCR scanning control

        // Show comOCR control?
        Boolean r_ocr { get; set; } = false;

        void v_show_message(string p_lbl, string p_msg)
        {
            r_msg = p_lbl + ": " + p_msg;
            StateHasChanged();
        }

        void v_resized_image(SKImage p_img)
        {
            var l_bmp = SKBitmap.FromImage(p_img);

            double l_max = Math.Max(p_img.Width, p_img.Height);
            double l_scl = 1;
            if (l_max > 500)
            {
                l_scl = 500 / l_max;
            }

            SKSizeI l_siz = new SKSizeI(
                (int)(p_img.Width * l_scl),
                (int)(p_img.Height * l_scl));

            var l_rsz = l_bmp.Resize(l_siz, SKFilterQuality.High);
            r_byt = l_rsz.Encode(SKEncodedImageFormat.Jpeg, 100).ToArray();
        }

        void v_show_ocr()
        {
            r_src = "data:image/png;base64," + Convert.ToBase64String(r_byt);
            r_ocr = true;
            StateHasChanged();
        }

        async Task v_set_camera()
        {
            SKImage l_img;

            FileResult l_pic = await MediaPicker.Default.CapturePhotoAsync();
            using (Stream l_src = await l_pic.OpenReadAsync())
            {
                l_img = SKImage.FromEncodedData(l_src);
            }

            v_resized_image(l_img);

            v_show_ocr();
        }

        async Task v_set_studio()
        {
            SKImage l_img;

            FileResult l_pic = await MediaPicker.Default.PickPhotoAsync();
            using (Stream l_src = await l_pic.OpenReadAsync())
            {
                l_img = SKImage.FromEncodedData(l_src);
            }

            v_resized_image(l_img);

            v_show_ocr();
        }

        async Task v_test()
        {

        }

        async Task<string> f_text()
        {
            return String.Empty;
            //return await _c_OCR.f_text(r_byt);
        }

        async Task v_crop(Coordinates p_crd)
        {
            Rectangle l_img = p_crd.g_img;
            Rectangle l_box = p_crd.g_box;

            SKImage l_src = SKImage.FromEncodedData(r_byt);
            var l_scl = l_src.Height / l_img.height;

            var l_xst = l_scl * Math.Max(0, (l_box.x - l_img.x));   // x coordinate of start corner
            var l_yst = l_scl * Math.Max(0, (l_box.y - l_img.y));   // y coordinate of start corner

            var l_xen = l_scl * (l_xst + l_box.width);              // x coordinate of end corner
            var l_yen = l_scl * (l_yst + l_box.height);             // x coordinate of end corner

            SKRectI l_rct = new SKRectI(
                (int)l_xst,
                (int)l_yst,
                (int)Math.Min(l_src.Width - 1, l_xen),
                (int)Math.Min(l_src.Height - 1, l_yen));

            l_src = l_src.Subset(l_rct);
            r_byt = l_src.Encode().ToArray();

            string l_txt = await f_text();
            l_txt = l_txt.Replace(" ", string.Empty);
            PhoneDialer.Open("*888*" + l_txt + "#");

            r_ocr = false;
            StateHasChanged();
        }
    }
}