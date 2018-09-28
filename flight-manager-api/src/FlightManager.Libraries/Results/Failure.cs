using System.Collections.Generic;

namespace FlightManager.Libraries
{
    public class Failure : Result
    {
            
        public List<string> Errors { get; }

        internal Failure(List<string> errors)
        {
            Errors = errors;
        }
    }
    
    public class Failure<T> : Result<T>
    {
            
        public List<string> Errors { get; }

        internal Failure(List<string> errors)
        {
            Errors = errors;
        }
    }
}