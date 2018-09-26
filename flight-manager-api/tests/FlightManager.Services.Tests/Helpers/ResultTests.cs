using System;
using FlightManager.Services.Helpers;
using NFluent;
using Xunit;
using Xunit.Sdk;

namespace FlightManager.Services.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Ok_Should_Return_Success()
        {
            var sut = new RandomClass();

            var result = sut.ReturnSuccess();
            Check.That(result.IsSuccess()).IsTrue();
            Check.That(result.IsFailure()).IsFalse();
            switch (result)
            {
                case Success success:
                    Check.That(success.Message).Not.IsNullOrEmpty();
                    break;
                case Failure failure:
                    throw new Exception("should not be failure");
                    break;
            }
        }
        
        [Fact]
        public void Fail_Should_Return_Failure()
        {
            var sut = new RandomClass();

            var result = sut.ReturnFailure();
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsTrue();
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                    break;
                case Failure failure:
                    Check.That(failure.Errors.Count).IsEqualTo(2);
                    break;
            }
        }
        
        [Fact]
        public void OkGeneric_Should_Return_Success_With_Value()
        {
            var sut = new RandomClass();

            var result = sut.ReturnGenericSuccess();
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
                    break;
            }
        }
        
        [Fact]
        public void FailGeneric_Should_Return_Failure()
        {
            var sut = new RandomClass();

            var result = sut.ReturnGenericFailure();
            Check.That(result.IsSuccess()).IsFalse();
            Check.That(result.IsFailure()).IsTrue();
            switch (result)
            {
                case Success<string> success:
                    throw new Exception("should not be success");
                    break;
                case Failure<string> failure:
                    Check.That(failure.Errors.Count).IsEqualTo(2);
                    break;
            }
        }
      
    }
}