using FluentAssertions;
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

        [TestMethod]
        public void MatchesOtherPattern_returns_true_for_same_patterns()
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Orange;
            var color3 = PegColor.Green;
            var color4 = PegColor.Red;
            var codeMasterPattern = new CodePattern(color1, color2, color3, color4);

            // Act
            var sut = new CodePattern(color1, color2, color3, color4);

            // Assert
            sut.MatchesOtherPattern(codeMasterPattern).Should().BeTrue();
        }

        [TestMethod]
        public void MatchesOtherPattern_returns_false_for_same_patterns()
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Orange;
            var color3 = PegColor.DarkBlue; // diff
            var color4 = PegColor.Red;
            var codeMasterPattern = new CodePattern(color1, color2, color3, color4);

            // Act
            var sut = new CodePattern(color1, color2, PegColor.LightBlue, color4);

            // Assert
            sut.MatchesOtherPattern(codeMasterPattern).Should().BeFalse();
        }

        [TestMethod]
        // nothing matches 
        [DataRow(
            PegColor.Yellow, PegColor.Yellow, PegColor.Yellow, PegColor.Yellow,
            PegColor.Orange, PegColor.Orange, PegColor.Orange, PegColor.Orange,
            0, 0)]
        // everything matches
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            4, 0)]
        // exact color and position 
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.LightBlue, PegColor.LightBlue, PegColor.LightBlue,
            1, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.Orange, PegColor.LightBlue, PegColor.LightBlue,
            1, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.LightBlue, PegColor.Green, PegColor.LightBlue,
            1, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.LightBlue, PegColor.LightBlue, PegColor.Red,
            1, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.LightBlue, PegColor.LightBlue, PegColor.Red,
            2, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.Orange, PegColor.LightBlue, PegColor.Red,
            2, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.LightBlue, PegColor.Green, PegColor.Red,
            3, 0)]
        // only color
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.Yellow, PegColor.LightBlue, PegColor.LightBlue,
            0, 1)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.LightBlue, PegColor.Orange, PegColor.LightBlue,
            0, 1)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.LightBlue, PegColor.LightBlue, PegColor.Green,
            0, 1)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Red, PegColor.LightBlue, PegColor.LightBlue, PegColor.LightBlue,
            0, 1)]
        // mixed
        // ...
        // special
        [DataRow(
            PegColor.Red, PegColor.Red, PegColor.Yellow, PegColor.Red,
            PegColor.Red, PegColor.Red, PegColor.Red, PegColor.Yellow,
            2, 2)]
        [DataRow(
            PegColor.Red, PegColor.Red, PegColor.Yellow, PegColor.Yellow,
            PegColor.Red, PegColor.Red, PegColor.Red, PegColor.Red,
            2, 0)]
        [DataRow(
            PegColor.Red, PegColor.Red, PegColor.Yellow, PegColor.Yellow,
            PegColor.Red, PegColor.Red, PegColor.Red, PegColor.LightBlue,
            2, 0)]
        [DataRow(
            PegColor.Red, PegColor.Red, PegColor.Yellow, PegColor.Yellow,
            PegColor.Red, PegColor.Red, PegColor.Red, PegColor.LightBlue,
            2, 0)]
        [DataRow(
            PegColor.Red, PegColor.Red, PegColor.Yellow, PegColor.Yellow,
            PegColor.Yellow, PegColor.Red, PegColor.Yellow, PegColor.Yellow,
            2, 1)]
        public void GetCheckResult_returns_correct_result(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange
            var codeMasterPattern = new CodePattern(makerColor1, makerColor2, makerColor3, makerColor4);
            var codeBreakerPattern = new CodePattern(breakerColor1, breakerColor2, breakerColor3, breakerColor4);

            // Act
            var checkResult = codeMasterPattern.GetCheckResult(codeBreakerPattern);

            // Assert
            checkResult.ColorAndPositionExactCount.Should().Be(expectedColorAndPositionExactCount);
            checkResult.ColorExactCount.Should().Be(expectedColorExactCount);
        }
    }
}
