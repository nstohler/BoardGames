using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Mastermind.Game.Interfaces;
using Moq;
using Mastermind.Game.Models;

namespace Mastermind.Game.Tests
{
    [TestClass]
    public class MastermindGameTests
    {
        [TestMethod]
        public async Task Constructor_sets_up_a_new_game()
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Green;
            var color3 = PegColor.LightBlue;
            var color4 = PegColor.Orange;
            var randomPegColorMoq = new Mock<IRandomPegColorService>();

            randomPegColorMoq.SetupSequence(x => x.GetRandomPegColor())
                .Returns(color1)
                .Returns(color2)
                .Returns(color3)
                .Returns(color4);

            // Act
            var sut = new MastermindGame(randomPegColorMoq.Object);

            // Assert
            randomPegColorMoq.Verify(x => x.GetRandomPegColor(), Times.Exactly(4));
            var isMatch = await sut.IsExactMatchAsync(color1, color2, color3, color4);
            isMatch.Should().Be(true);
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
        public async Task SubmitAndCheckCodeBreakerCodePatternAsync_and_IsExactMatchAsync_return_correct_results(
            PegColor makerColor1, PegColor makerColor2, PegColor makerColor3, PegColor makerColor4,
            PegColor breakerColor1, PegColor breakerColor2, PegColor breakerColor3, PegColor breakerColor4,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange
            var randomPegColorMoq = new Mock<IRandomPegColorService>();
            randomPegColorMoq.SetupSequence(x => x.GetRandomPegColor())
                .Returns(makerColor1)
                .Returns(makerColor2)
                .Returns(makerColor3)
                .Returns(makerColor4);
            var sut = new MastermindGame(randomPegColorMoq.Object);

            // Act
            var checkResult = await sut.SubmitAndCheckCodeBreakerCodePatternAsync(breakerColor1, breakerColor2, breakerColor3, breakerColor4);

            // Assert
            checkResult.ColorAndPositionExactCount.Should().Be(expectedColorAndPositionExactCount);
            checkResult.ColorExactCount.Should().Be(expectedColorExactCount);
            var isMatch = await sut.IsExactMatchAsync(breakerColor1, breakerColor2, breakerColor3, breakerColor4);
            checkResult.IsGameWon.Should().Be(isMatch);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(11)]
        public async Task SubmitAndCheckCodeBreakerCodePatternAsync_stores_player_code_pattern(int expectedFinalCount)
        {
            // Arrange
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Green;
            var color3 = PegColor.LightBlue;
            var color4 = PegColor.Orange;
            var randomPegColorMoq = new Mock<IRandomPegColorService>();

            randomPegColorMoq.SetupSequence(x => x.GetRandomPegColor())
                .Returns(color1)
                .Returns(color2)
                .Returns(color3)
                .Returns(color4);
            var sut = new MastermindGame(randomPegColorMoq.Object);

            // Act
            for (int i = 0; i < expectedFinalCount; i++)
            {
                await sut.SubmitAndCheckCodeBreakerCodePatternAsync(color1, color2, color3, color4);
                sut.GetCodeBreakerCombinationCount.Should().Be(i + 1);
            }

            // Assert
            sut.GetCodeBreakerCombinationCount.Should().Be(expectedFinalCount);
        }
    }
}
