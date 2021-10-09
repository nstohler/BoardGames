using FluentAssertions;
using Mastermind.Game.Interfaces;
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
    public class CodePatternCheckServiceTests
    {
        [TestMethod]
        public void MatchesOtherPattern_returns_true_for_same_patterns()
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Orange;
            var color3 = PegColor.Green;
            var color4 = PegColor.Red;
            var codeMasterPattern = new CodePattern(color1, color2, color3, color4);
            var codeMakerPattern = new CodePattern(color1, color2, color3, color4);
            ICodePatternCheckService service = new CodePatternCheckService();

            // Act / Assert
            service.AreMatchingCodePatterns(codeMasterPattern, codeMakerPattern).Should().BeTrue();
            service.AreMatchingCodePatterns(codeMakerPattern, codeMasterPattern).Should().BeTrue();
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
            var codeMakerPattern = new CodePattern(color1, color2, PegColor.LightBlue, color4);
            ICodePatternCheckService service = new CodePatternCheckService();

            // Act / Assert
            service.AreMatchingCodePatterns(codeMasterPattern, codeMakerPattern).Should().BeFalse();
            service.AreMatchingCodePatterns(codeMakerPattern, codeMasterPattern).Should().BeFalse();
        }

        [TestMethod]
        [DataRow("YYYY", "OOOO", 0, 0)]
        [DataRow("YOBG", "RLRL", 0, 0)]
        [DataRow("LOLO", "GRGR", 0, 0)]
        public void GetCheckResult_returns_correct_result_when_nothing_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow("YOGR", "YOGR", 4, 0)]
        [DataRow("YYYY", "YYYY", 4, 0)]
        [DataRow("GROB", "GROB", 4, 0)]
        [DataRow("BLOB", "BLOB", 4, 0)]
        public void GetCheckResult_returns_correct_result_when_everything_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);

        }

        [TestMethod]
        [DataRow("YOGR", "YLLL", 1, 0)]
        [DataRow("YOGR", "LOLL", 1, 0)]
        [DataRow("YOGR", "LLGL", 1, 0)]
        [DataRow("YOGR", "LLLR", 1, 0)]
        [DataRow("YOGR", "YLLR", 2, 0)]
        [DataRow("YOGR", "LOLR", 2, 0)]
        [DataRow("YOGR", "YLGR", 3, 0)]
        public void GetCheckResult_returns_correct_result_when_exact_color_and_position_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow("YOGR", "LYLL", 0, 1)]
        [DataRow("YOGR", "LLOL", 0, 1)]
        [DataRow("YOGR", "LLLG", 0, 1)]
        [DataRow("YOGR", "RLLL", 0, 1)]
        [DataRow("YOGR", "LLYO", 0, 2)]
        [DataRow("YOGR", "GRYO", 0, 4)]
        public void GetCheckResult_returns_correct_result_when_only_color_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow("YOGR", "YLLO", 1, 1)]
        [DataRow("YOGR", "ROGL", 2, 1)]
        [DataRow("YOGR", "ROGY", 2, 2)]
        public void GetCheckResult_returns_correct_result_for_mixed_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        [TestMethod]
        [DataRow("BOYB", "YBOO", 0, 3)]
        [DataRow("BOYB", "OBOO", 0, 2)]
        [DataRow("BBYB", "YYYB", 2, 0)]
        [DataRow("BBYB", "BYBB", 2, 2)]
        [DataRow("RRYR", "RRRY", 2, 2)]
        [DataRow("RRYY", "RRRR", 2, 0)]
        [DataRow("RRYY", "RRRL", 2, 0)]
        [DataRow("RRYY", "RRRL", 2, 0)]
        [DataRow("RRYY", "YRYY", 3, 0)]
        [DataRow("RYOG", "RRRR", 1, 0)]
        [DataRow("RYOR", "ROYR", 2,2)]
        public void GetCheckResult_returns_correct_for_special_matches(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // special cases example:
            // maker:    RRYY
            // breaker:  RRYR => last R should not get reported as exact color count!
            // expected: (2,1)

            // Arrange/Act/Assert
            AssertCorrectCheckResultCreated(
                makerColorCharString , breakerColorCharString,
                expectedColorAndPositionExactCount, expectedColorExactCount);
        }

        private void AssertCorrectCheckResultCreated(
            string makerColorCharString , string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange
            var codeMasterPattern = CodePattern.CreateFromCharString(makerColorCharString );
            var codeBreakerPattern = CodePattern.CreateFromCharString(breakerColorCharString);
            ICodePatternCheckService service = new CodePatternCheckService();

            // Act
            var checkResult = service.GetCheckResult(codeMasterPattern, codeBreakerPattern);

            // Assert
            checkResult.ColorAndPositionExactCount.Should().Be(expectedColorAndPositionExactCount);
            checkResult.ColorExactCount.Should().Be(expectedColorExactCount);

            var isGameWon = expectedColorAndPositionExactCount == 4 && expectedColorExactCount == 0;
            checkResult.IsGameWon.Should().Be(isGameWon);
        }
    }
}
