using Mastermind.Game.Interfaces;
using Mastermind.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind.Game
{
    public class CodePatternCheckService : ICodePatternCheckService
    {
        // TODO: Patterns vergleichen,
        // - true zurückgeben, falls alle Farben und Positionen stimmen.
        // - Sonst false.
        public bool AreMatchingCodePatterns(CodePattern codePattern1, CodePattern codePattern2)
        {
            throw new NotImplementedException();
        }

        // TODO: 
        // - Patterns vergleichen
        // - Anzahl "korrekte Position und Farbe" in CheckResult.ColorAndPositionExactCount speichern.
        // - Anzahl "korrekte Farbe" in CheckResult.ColorExactCount speichern.
        public CheckResult GetCheckResult(CodePattern codeMakerPattern, CodePattern codeBreakerPattern)
        { 
            var colorAndPositionExactCount = 0;
            var colorExactCount = 0;



            return new CheckResult(colorAndPositionExactCount, colorExactCount);
        }
    }
}
