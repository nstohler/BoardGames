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
        public void MatchesOtherPattern_returns_false_for_different_patterns()
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
        [DataRow(
            PegColor.Yellow, PegColor.Yellow, PegColor.Yellow, PegColor.Yellow,
            PegColor.Orange, PegColor.Orange, PegColor.Orange, PegColor.Orange,
            0, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.DarkBlue, PegColor.Green,
            PegColor.Red, PegColor.LightBlue, PegColor.Red, PegColor.LightBlue,
            0, 0)]
        public void GetCheckResult_returns_correct_result_when_nothing_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            4, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Yellow, PegColor.Yellow, PegColor.Yellow,
            PegColor.Yellow, PegColor.Yellow, PegColor.Yellow, PegColor.Yellow,
            4, 0)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            4, 0)]
        [DataRow(
            PegColor.Orange, PegColor.Green, PegColor.LightBlue, PegColor.DarkBlue,
            PegColor.Orange, PegColor.Green, PegColor.LightBlue, PegColor.DarkBlue,
            4, 0)]
        public void GetCheckResult_returns_correct_result_when_everything_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);

        }

        [TestMethod]
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
        public void GetCheckResult_returns_correct_result_when_exact_color_and_position_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
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
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.LightBlue, PegColor.LightBlue, PegColor.Yellow, PegColor.Orange,
            0, 2)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Green, PegColor.Red, PegColor.Yellow, PegColor.Orange,
            0, 4)]
        public void GetCheckResult_returns_correct_result_when_only_color_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Yellow, PegColor.LightBlue, PegColor.LightBlue, PegColor.Orange,
            1, 1)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Red, PegColor.Orange, PegColor.Green, PegColor.LightBlue,
            2, 1)]
        [DataRow(
            PegColor.Yellow, PegColor.Orange, PegColor.Green, PegColor.Red,
            PegColor.Red, PegColor.Orange, PegColor.Green, PegColor.Yellow,
            2, 2)]
        public void GetCheckResult_returns_correct_result_for_mixed_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow(
            PegColor.DarkBlue, PegColor.Orange, PegColor.Yellow, PegColor.DarkBlue,
            PegColor.Yellow, PegColor.DarkBlue, PegColor.Orange, PegColor.Orange,
            0, 3)]
        [DataRow(
            PegColor.DarkBlue, PegColor.Orange, PegColor.Yellow, PegColor.DarkBlue,
            PegColor.Orange, PegColor.DarkBlue, PegColor.Orange, PegColor.Orange,
            0, 2)]
        [DataRow(
            PegColor.DarkBlue, PegColor.DarkBlue, PegColor.Yellow, PegColor.DarkBlue,
            PegColor.Yellow, PegColor.Yellow, PegColor.Yellow, PegColor.DarkBlue,
            2, 0)]
        [DataRow(
            PegColor.DarkBlue, PegColor.DarkBlue, PegColor.Yellow, PegColor.DarkBlue,
            PegColor.DarkBlue, PegColor.Yellow, PegColor.DarkBlue, PegColor.DarkBlue,
            2, 2)]
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
            3, 0)]
        [DataRow(
            PegColor.Red, PegColor.Yellow, PegColor.Orange, PegColor.Green,
            PegColor.Red, PegColor.Red, PegColor.Red, PegColor.Red,
            1, 0)]
        [DataRow(
            PegColor.Red, PegColor.Yellow, PegColor.Orange, PegColor.Red,
            PegColor.Red, PegColor.Orange, PegColor.Yellow, PegColor.Red,
            2, 2)]
        public void GetCheckResult_returns_correct_for_special_matches(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // special cases example:
            // maker:    RRYY
            // breaker:  RRYR => last R should not get reported as exact color count!
            // expected: (2,1)

            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColor1, makerColor2, makerColor3, makerColor4,
                breakerColor1, breakerColor2, breakerColor3, breakerColor4,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        private void AssertCorrectCheckResultCreated(
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

            var isGameWon = expectedColorAndPositionExactCount == 4 && expectedColorExactCount == 0;
            checkResult.IsGameWon.Should().Be(isGameWon);
        }
    }
}
