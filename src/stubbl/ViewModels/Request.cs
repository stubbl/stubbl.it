namespace stubbl.ViewModels
{
    public class Request
    {
        public BodyToken[] BodyTokens { get; set; }
        public Header[] Headers { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public QuerystringParameter[] QueryStringParameters { get; set; }
    }
}