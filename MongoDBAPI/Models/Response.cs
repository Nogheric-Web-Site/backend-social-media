namespace MongoDBAPI.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Body { get; set; }
    }
}
