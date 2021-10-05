using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Mastermind.Game.Models;
using Mastermind.Game.Interfaces;

namespace Mastermind.Game.Tests
{
    [TestClass]
    public class RandomPegColorTests
    {
        [TestMethod]
        public void GetRandomPegColor_returns_random_peg_color()
        {
            // Arrange
            int invocations = 100;
            var resultMap = Enum.GetValues<PegColor>().ToDictionary(k => k, v => 0);
            IRandomPegColorService randomPegColorService = new RandomPegColorService();

            // Act
            for (int i = 0; i < invocations; i++)
            {
                var pegColor = randomPegColorService.GetRandomPegColor();
                resultMap[pegColor]++;                               
            }

            // Assert
            foreach (var pegColor in Enum.GetValues<PegColor>())
            {
                resultMap[pegColor].Should().BeGreaterThan(0);
            }
        }
    }
}
