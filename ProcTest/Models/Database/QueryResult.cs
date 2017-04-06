namespace ProcTest.Models.Database
{
    public class QueryResult<T>
    {
        public QueryResult(T data)
        {
            Data = data;
            Success = true;
        }

        public QueryResult(string error)
        {
            Error = error;
            Success = false;
        }

        public T Data { get; set; }

        public bool Success { get; set; }

        public string Error { get; set; }
    }
}