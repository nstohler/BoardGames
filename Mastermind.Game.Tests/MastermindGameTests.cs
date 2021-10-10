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
using Mastermind.Game.Models.Helpers;

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
            var sut = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Assert
            randomPegColorMoq.Verify(x => x.GetRandomPegColor(), Times.Exactly(4));
            var isMatch = await sut.IsExactMatchAsync(color1, color2, color3, color4);
            isMatch.Should().Be(true);
        }

        [TestMethod]
        [DataRow("YYYY", "OOOO", 0, 0)]
        [DataRow("YOBG", "RLRL", 0, 0)]
        [DataRow("LOLO", "GRGR", 0, 0)]
        [DataRow("YOGR", "YOGR", 4, 0)]
        [DataRow("YYYY", "YYYY", 4, 0)]
        [DataRow("GROB", "GROB", 4, 0)]
        [DataRow("BLOB", "BLOB", 4, 0)]
        [DataRow("YOGR", "YLLL", 1, 0)]
        [DataRow("YOGR", "LOLL", 1, 0)]
        [DataRow("YOGR", "LLGL", 1, 0)]
        [DataRow("YOGR", "LLLR", 1, 0)]
        [DataRow("YOGR", "YLLR", 2, 0)]
        [DataRow("YOGR", "LOLR", 2, 0)]
        [DataRow("YOGR", "YLGR", 3, 0)]
        [DataRow("YOGR", "LYLL", 0, 1)]
        [DataRow("YOGR", "LLOL", 0, 1)]
        [DataRow("YOGR", "LLLG", 0, 1)]
        [DataRow("YOGR", "RLLL", 0, 1)]
        [DataRow("YOGR", "LLYO", 0, 2)]
        [DataRow("YOGR", "GRYO", 0, 4)]
        [DataRow("YOGR", "YLLO", 1, 1)]
        [DataRow("YOGR", "ROGL", 2, 1)]
        [DataRow("YOGR", "ROGY", 2, 2)]
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
        [DataRow("RYOR", "ROYR", 2, 2)]
        public async Task SubmitAndCheckCodeBreakerCodePatternAsync_and_IsExactMatchAsync_return_correct_results(
            string makerColorCharString, string breakerColorCharString,
            int expectedColorAndPositionExactCount, int expectedColorExactCount)
        {
            // Arrange
            var randomPegColorMoq = new Mock<IRandomPegColorService>();
            var codeMasterPattern = CodePattern.CreateFromCharString(makerColorCharString);
            var codeBreakerPattern = CodePattern.CreateFromCharString(breakerColorCharString);
            randomPegColorMoq.SetupSequence(x => x.GetRandomPegColor())
                .Returns(codeMasterPattern.Color1)
                .Returns(codeMasterPattern.Color2)
                .Returns(codeMasterPattern.Color3)
                .Returns(codeMasterPattern.Color4);
            var sut = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Act
            var codePatternWithResult = await sut.SubmitAndCheckCodeBreakerCodePatternAsync(
                codeBreakerPattern.Color1,
                codeBreakerPattern.Color2,
                codeBreakerPattern.Color3,
                codeBreakerPattern.Color4
                );

            // Assert
            codePatternWithResult.Result.ColorAndPositionExactCount.Should().Be(expectedColorAndPositionExactCount);
            codePatternWithResult.Result.ColorExactCount.Should().Be(expectedColorExactCount);
            
            var isMatch = await sut.IsExactMatchAsync(
                codeBreakerPattern.Color1,
                codeBreakerPattern.Color2,
                codeBreakerPattern.Color3,
                codeBreakerPattern.Color4);

            codePatternWithResult.Result.IsGameWon.Should().Be(isMatch);
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

            randomPegColorMoq.Setup(x => x.GetRandomPegColor())
                .Returns(color1);
            var sut = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Act
            for (int i = 0; i < expectedFinalCount; i++)
            {
                await sut.SubmitAndCheckCodeBreakerCodePatternAsync(color1, color2, color3, color4);
                sut.GetCodeBreakerCombinationCount.Should().Be(i + 1);
            }

            // Assert
            sut.GetCodeBreakerCombinationCount.Should().Be(expectedFinalCount);
        }

        [TestMethod]
        [DataRow("GROB")]
        [DataRow("RYOG")]
        [DataRow("BLOB")]
        public void GetCodeMakerPattern_returns_secret_code(string makerColorCharString)
        {
            // Arrange
            var color1 = PegColorConverters.CharToPegColorMap[makerColorCharString[0].ToString()];
            var color2 = PegColorConverters.CharToPegColorMap[makerColorCharString[1].ToString()];
            var color3 = PegColorConverters.CharToPegColorMap[makerColorCharString[2].ToString()];
            var color4 = PegColorConverters.CharToPegColorMap[makerColorCharString[3].ToString()];
            var randomPegColorMoq = new Mock<IRandomPegColorService>();

            randomPegColorMoq.SetupSequence(x => x.GetRandomPegColor())
                .Returns(color1)
                .Returns(color2)
                .Returns(color3)
                .Returns(color4);
            var game = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Act
            var secretCodePattern = game.GetCodeMakerPattern();

            // Assert
            secretCodePattern.Color1.Should().Be(color1);
            secretCodePattern.Color2.Should().Be(color2);
            secretCodePattern.Color3.Should().Be(color3);
            secretCodePattern.Color4.Should().Be(color4);
        }

        [TestMethod]
        public async Task IsGameLost_should_be_true_after_max_attempts_have_been_submitted()
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

            var sut = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Act / Assert
            for (int i = 0; i < sut.MaxPlayerAttempts; i++)
            {
                sut.IsGameLost().Should().BeFalse();
                var badResult = await sut.SubmitAndCheckCodeBreakerCodePatternAsync(PegColor.Red, PegColor.Red, PegColor.Red, PegColor.Red); // wrong code
                sut.GetCodeBreakerCombinationCount.Should().Be(i + 1);
            }

            // Assert
            sut.IsGameLost().Should().BeTrue();
        }

        [TestMethod]
        public async Task IsGameLost_should_be_false_when_correct_code_submitted_in_last_attempt()
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

            var sut = new MastermindGame(randomPegColorMoq.Object, new CodePatternCheckService());

            // Act / Assert
            for (int i = 0; i < sut.MaxPlayerAttempts - 1; i++)
            {
                sut.IsGameLost().Should().BeFalse();
                var badResult = await sut.SubmitAndCheckCodeBreakerCodePatternAsync(PegColor.Red, PegColor.Red, PegColor.Red, PegColor.Red); // wrong code
                sut.IsGameLost().Should().BeFalse();
                sut.GetCodeBreakerCombinationCount.Should().Be(i + 1);
            }

            var winResult = await sut.SubmitAndCheckCodeBreakerCodePatternAsync(color1, color2, color3, color4); // correct code

            // Assert
            winResult.Result.IsGameWon.Should().BeTrue();
            sut.IsGameLost().Should().BeTrue();
        }
    }
}
