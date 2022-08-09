namespace arbweb_OCR
{
    public class _c_JsonResponce
    {
        public Parsedresult[] ParsedResults { get; set; }
        public bool IsErroredOnProcessing { get; set; }
    }

    public class Parsedresult
    {
        public string ParsedText { get; set; }
    }
}
