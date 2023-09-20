using System;
using Documento.BR.Rules.Common;
using Documento.BR.Rules.Configuration;

namespace Documento.BR.Rules.Validators
{
    public class CNPJValidator
    {
        public static bool Validate(ReadOnlySpan<char> input)
        {
            if (input.Length > CNPJConfiguration.MaximumSize)
                return false;
            if (input.Length > CNPJConfiguration.DigitsSize && !NumericData.CheckFormatting(input, CNPJConfiguration.PunctuationIndexes))
                return false;

            if (!NumericData.CheckQuantityOfNumericChars(input, CNPJConfiguration.DigitsSize))
                return false;

            Span<char> numericOnlyInput = stackalloc char[CNPJConfiguration.DigitsSize];
            NumericData.GetOnlyDigits(input, ref numericOnlyInput);

            return ValidateCNPJNumericRule(numericOnlyInput);
        }

        private static bool ValidateCNPJNumericRule(ReadOnlySpan<char> numericOnlyInput)
        {
            return NumericData.CheckIfAllNumbersAreNotTheSame(numericOnlyInput)
                && CheckVerificationDigits(numericOnlyInput);
        }

        private static bool CheckVerificationDigits(ReadOnlySpan<char> input, int digitsToCheck = 2)
        {
            if (digitsToCheck == 0)
                return true;

            double sum = 0;
            int multiplier = digitsToCheck == 2 ? 5 : 6;

            for (int i = 0; i < input.Length - digitsToCheck; i++)
            {
                sum += char.GetNumericValue(input[i]) * multiplier;
                multiplier--;

                if (multiplier == 1)
                    multiplier = 9;
            }

            var rest = sum % 11;
            var verificationDigit = 11 - rest;
            verificationDigit = verificationDigit >= 10 ? 0 : verificationDigit;
            if (verificationDigit != (int)char.GetNumericValue(input[CNPJConfiguration.DigitsSize - digitsToCheck]))
                return false;

            return CheckVerificationDigits(input, digitsToCheck - 1);
        }
    }
}
