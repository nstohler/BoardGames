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
        public async Task StartNewGame_sets_up_a_new_game()
        {
            var color1 = PegColor.Yellow;
            var color2 = PegColor.Green;
            var color3 = PegColor.LightBlue;
            var color4 = PegColor.Orange;

            // Arrange
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
            var isMatch = await sut.IsExactMatch(color1, color2, color3, color4);
            isMatch.Should().Be(true);
        }
    }
}
