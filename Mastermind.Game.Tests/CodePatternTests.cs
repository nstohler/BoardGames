using FluentAssertions;
using Mastermind.Game.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game.Tests
{
    [TestClass]
    public class CodePatternTests
    {
        [TestMethod]
        public void Constructor_creates_valid_pattern()
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Orange;
            var color3 = PegColor.Green;
            var color4 = PegColor.Red;

            // Act
            var sut = new CodePattern(color1, color2, color3, color4);

            // Assert
            sut.Color1.Should().Be(color1);
            sut.Color2.Should().Be(color2);
            sut.Color3.Should().Be(color3);
            sut.Color4.Should().Be(color4);
        }
    }
}
