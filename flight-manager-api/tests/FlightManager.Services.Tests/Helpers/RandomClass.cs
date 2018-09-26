using System;
using System.Collections.Generic;
using FlightManager.Services.Helpers;

namespace FlightManager.Services.Tests
{
    internal class RandomClass
    {
        internal Result ReturnSuccess()
        {
            return Result.Ok("Yeah!");
        } 
        
        internal Result ReturnFailure()
        {
            return Result.Fail(new List<string>()
            {
                "oops",
                "an error occured"
            });
        } 
        internal Result<string> ReturnGenericSuccess()
        {
            return Result<string>.Ok("Yeah!", "test");
        } 
        
        internal Result<string> ReturnGenericFailure()
        {
            return Result<string>.Fail(new List<string>()
            {
                "oops",
                "an error occured"
            });
        } 
    }
}