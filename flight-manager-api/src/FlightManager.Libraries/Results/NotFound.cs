namespace FlightManager.Libraries
{
    public class NotFound : Result
    {
            
        public string Error { get; }

        internal NotFound(string error)
        {
            Error = error;
        }
    }
    
    public class NotFound<T> : Result<T>
    {
            
        public string Error { get; }

        internal NotFound(string error)
        {
            Error = error;
        }
    }
}