namespace FlightManager.Libraries
{
    public class Success : Result
    {
        public string Message { get; set; }

        public Success(string message)
        {
            Message = message;
        }
    }
    
    public class Success<T> : Result<T>
    {
        
        public T Value { get; }

        public Success(T value)
        {
            Value = value;
        }
    }
}