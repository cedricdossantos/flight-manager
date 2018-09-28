using System.Collections.Generic;

namespace FlightManager.Libraries
{
    public abstract class Result
    {
        internal Result(){}

        public static Failure Fail( List<string> errors)
        {
            return new Failure(errors);
        }
        
        public static Success Ok(string message)
        {
            return new Success(message);
        }
        
        public static NotFound NotFound(string error)
        {
            return new NotFound(error);
        }
        
        public  bool IsFailure() => this is Failure;
        
        
        public  bool IsSuccess() => this is Success;
        
        public  bool IsNotFound() => this is NotFound;
        
    }


    public abstract class Result<T>
    {
        internal Result(){}

        public static Failure<T> Fail( List<string> errors)
        {
            return new Failure<T>(errors);
        }
        
        public static Success<T> Ok(T value)
        {
            return new Success<T>(value);
        }
        
        public static NotFound<T> NotFound(string error)
        {
            return new NotFound<T>(error);
        }
        
        public  bool IsFailure() => this is Failure<T>;
        
        public  bool IsNotFound() => this is NotFound<T>;
        
        public  bool IsSuccess() => this is Success<T>;
        
    }



}