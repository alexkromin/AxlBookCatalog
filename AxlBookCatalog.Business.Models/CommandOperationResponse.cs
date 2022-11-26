namespace AxlBookCatalog.Business.Models
{
    public class CommandOperationResponse<T> where T: class
    {
        public T? OperatedObject { get; set; }

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }
    }
}
