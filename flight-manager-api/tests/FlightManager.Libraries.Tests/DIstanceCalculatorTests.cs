using System;
using System.Collections.Generic;
using FlightManager.Libraries.Distance;
using NFluent;
using Xunit;
   

namespace FlightManager.Libraries.Tests
{
    public class DistanceCalculatorTests
    {
        [Fact]
        public void GetDistanceBetween_Paris_And_NewYork_Should_Be_Correct()
        {
            // Arrange
            double CorrectDistanceMin = 5836;
            double CorrectDistanceMax = 5838;
            
            double ParisLat = 48.856667;
            double ParisLon = 2.350987;
            
            double NewYorLat = 40.714268;
            double NewYorLon = -74.005974;
            
            // Act
            var sut = new DistanceCalculator();
            var result = sut.GetDistanceBetween(ParisLat, ParisLon, NewYorLat, NewYorLon);
            
            // Assert
            Check.That(result).IsStrictlyLessThan(CorrectDistanceMax);
            Check.That(result).IsStrictlyGreaterThan(CorrectDistanceMin);
        }
    }
}