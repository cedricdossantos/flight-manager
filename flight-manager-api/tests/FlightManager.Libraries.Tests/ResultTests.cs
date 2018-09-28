using System;
using System.Collections.Generic;
using NFluent;
using Xunit;

namespace FlightManager.Libraries.Tests
{
    public class ResultTests
    {
        internal class MyClass
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
            
            internal Result ReturnNotFound()
            {
                return Result.NotFound("oops, an error occured");
            }
            
            internal Result<string> ReturnGenericSuccess()
            {
                return Result<string>.Ok("test");
            } 
        
            internal Result<string> ReturnGenericFailure()
            {
                return Result<string>.Fail(new List<string>()
                {
                    "oops",
                    "an error occured"
                });
            } 
            
            internal Result<string> ReturnGenericNotFound()
            {
                return Result<string>.NotFound("oops, an error occured");
            } 
        }
        
        [Fact]
        public void Ok_Should_Return_Success()
        {
            // Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnSuccess();
            
            // Assert
            Check.That(result.IsSuccess()).IsTrue();
            Check.That(result.IsFailure()).IsFalse();
            switch (result)
            {
                case Success success:
                    Check.That(success.Message).Not.IsNullOrEmpty();
                    break;
                case Failure failure:
                    throw new Exception("should not be failure");
            }
        }
        
        [Fact]
        public void Fail_Should_Return_Failure()
        {
            // Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnFailure();
            
            // Assert
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsTrue();
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case Failure failure:
                    Check.That(failure.Errors.Count).IsEqualTo(2);
                    break;
            }
        }
        
        [Fact]
        public void NotFound_Should_Return_Not_Found()
        {
            // Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnNotFound();
            
            // Assert
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsFalse();
            Check.That(result.IsNotFound()).IsTrue();
            
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case Failure failure:
                    throw new Exception("should not be failure");
                case NotFound _:
                    break;
            }
        }
        
        [Fact]
        public void OkGeneric_Should_Return_Success_With_Value()
        {
            //Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnGenericSuccess();
            
            // Assert
            Check.That(result.IsSuccess()).IsTrue();
            Check.That(result.IsFailure()).IsFalse();
            switch (result)
            {
                case Success<string> success:
                    Check.That(success.Value).IsInstanceOf<string>();
                    Check.That(success.Value).IsEqualTo("test");
                    break;
                case Failure<string> failure:
                    throw new Exception("should not be failure");
            }
        }
        
        [Fact]
        public void FailGeneric_Should_Return_Failure()
        {
            // Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnGenericFailure();
            
            // Assert
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsTrue();
            switch (result)
            {
                case Success<string> success:
                    throw new Exception("should not be success");
                case Failure<string> failure:
                    Check.That(failure.Errors.Count).IsEqualTo(2);
                    break;
            }
        }
        
        [Fact]
        public void NotFoundGeneric_Should_Return_Not_Found()
        {
            //Arrange
            var sut = new MyClass();

            // Act
            var result = sut.ReturnGenericNotFound();
            
            // Assert
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsFalse();
            Check.That(result.IsNotFound()).IsTrue();
            switch (result)
            {
                case Success<string> success:
                    throw new Exception("should not be success");
                case Failure<string> failure:
                    throw new Exception("should not be success");
                case NotFound<string> _:
                    break;
            }
        }
    }
}