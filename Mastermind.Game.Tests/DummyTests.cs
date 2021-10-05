using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace Mastermind.Game.Tests
{
    [TestClass]
    public class DummyTests
    {
        [TestMethod]
        [DataRow(1,2, 3)]
        [DataRow(-10, 10, 0)]
        [DataRow(10, -10, 0)]
        [DataRow(-10, -10, -20)]
        public void Add_creates_valid_sum_when_adding_numbers(int nr1, int nr2, int expectedResult)
        {
            // Arrange
            var sut = new Dummy();

            // Act
            var result = sut.Add(nr1, nr2);

            // Assert
            Assert.AreEqual(expectedResult, result);
            result.Should().Be(expectedResult);
        }
    }
}
