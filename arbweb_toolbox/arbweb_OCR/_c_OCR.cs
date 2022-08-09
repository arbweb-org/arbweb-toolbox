using System.Text.Json;

namespace arbweb_OCR
{
    public static class _c_OCR
    {
        /// <summary>
        /// Recognise first line of text in given image
        /// </summary>
        /// <param name="p_img">Encoded image</param>
        /// <returns>First line of text in image</returns>
        public static async Task<string> f_text(byte[] p_img)
        {
            string l_out;
            using (var l_cln = new HttpClient())
            {
                using (var l_req = new HttpRequestMessage(new HttpMethod("POST"), "https://api.ocr.space/Parse/Image"))
                {
                    l_req.Headers.TryAddWithoutValidation("apikey", "K89982542288957");

                    var l_con = new MultipartFormDataContent();
                    l_con.Add(new ByteArrayContent(p_img), "file", "pic.jpg");
                    l_con.Add(new StringContent("eng"), "language");
                    l_con.Add(new StringContent("false"), "isOverlayRequired");
                    l_req.Content = l_con;

                    var response = await l_cln.SendAsync(l_req);
                    l_out = await response.Content.ReadAsStringAsync();
                }
            }

            var l_obj = JsonSerializer.Deserialize<_c_JsonResponce>(l_out);
            if (l_obj == null)
            { return String.Empty; }

            return l_obj.ParsedResults[0].ParsedText;
        }
    }
}