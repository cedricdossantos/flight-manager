using System;
using System.Collections.Generic;

namespace FlightManager.Services.Helpers
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
        
        public  bool IsFailure() => this is Failure;
        
        
        public  bool IsSuccess() => this is Success;
        
        
        
    }
    public class Failure : Result
    {
            
        public IReadOnlyList<string> Errors { get; }

        internal Failure(List<string> errors)
        {
            Errors = errors;
        }
    }

    public class Success : Result
    {
        public string Message { get; set; }

        public Success(string message)
        {
            Message = message;
        }
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
        
        public  bool IsFailure() => this is Failure<T>;
        
        
        public  bool IsSuccess() => this is Success<T>;
        
    }
    
    public class Failure<T> : Result<T>
    {
            
        public IReadOnlyList<string> Errors { get; }

        internal Failure(List<string> errors)
        {
            Errors = errors;
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