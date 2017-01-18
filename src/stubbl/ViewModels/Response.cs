namespace stubbl.ViewModels
{
    public class Response
    {
        public string Body { get; set; }
        public Header[] Headers { get; set; }
        public int StatusCode { get; set; }
    }
}