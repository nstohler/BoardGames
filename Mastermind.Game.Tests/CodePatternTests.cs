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
        public void Constructor_creates_valid_code_pattern()
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
        [DataRow("GROB", PegColor.Green, PegColor.Red, PegColor.Orange, PegColor.DarkBlue)]
        [DataRow("YLRG", PegColor.Yellow, PegColor.LightBlue, PegColor.Red, PegColor.Green)]
        public void CreateFromCharString_creates_valid_code_pattern(string colorCharString, 
            PegColor color1, PegColor color2, PegColor color3, PegColor color4)
        {
            // Arrange/Act
            var codePattern = CodePattern.CreateFromCharString(colorCharString);

            // Assert
            codePattern.Color1.Should().Be(color1);
            codePattern.Color2.Should().Be(color2);
            codePattern.Color3.Should().Be(color3);
            codePattern.Color4.Should().Be(color4);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("GROX")]
        [DataRow("HANS")]
        [DataRow("GROBIAN")]
        public void CreateFromCharString_throws_on_invalid_coolor_char_string(string colorCharString)
        {
            // Arrange/Act
            Action act = () => CodePattern.CreateFromCharString(colorCharString);

            // Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
