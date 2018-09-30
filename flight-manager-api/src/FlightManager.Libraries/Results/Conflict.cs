namespace FlightManager.Libraries
{
    public class Conflict : Result
    { 
        public string Error { get; }

        internal Conflict(string error)
        {
            Error = error;
        }
    }
    
    public class Conflict<T> : Result<T>
    { 
        public string Error { get; }

        internal Conflict(string error)
        {
            Error = error;
        }
    }
}