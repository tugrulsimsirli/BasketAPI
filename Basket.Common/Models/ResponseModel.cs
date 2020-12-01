namespace Basket.Common.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public ResponseStatus Status { get; set; }
        public object Data { get; set; }
    }
    public enum ResponseStatus
    {
        Error = 0,
        Success = 1,
        Warning = 2
    }
}