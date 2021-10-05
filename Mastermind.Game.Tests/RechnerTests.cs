using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Mastermind.Game.Tests
{
    [TestClass]
    public class RechnerTests
    {
        [TestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(10, 22, 32)]
        [DataRow(-10, 100, 90)]
        [DataRow(0, 2, 2)]
        [DataRow(-10, -20, -30)]
        [DataRow(int.MaxValue, 0, int.MaxValue)]
        [DataRow(int.MaxValue, -int.MaxValue, 0)]
        [DataRow(int.MaxValue, 1, int.MinValue)]
        public void FunctionUnderTest_ExpectedResult_UnderCondition(int zahl1, int zahl2, int erwartetesResultat)
        {
            // Arrange
            var rechner = new Rechner();

            // Act
            var resultat = rechner.Addiere(zahl1, zahl2);

            // Assert
            resultat.Should().Be(erwartetesResultat);
        }
    }
}
