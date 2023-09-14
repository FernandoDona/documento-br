using System;

namespace Documento.BR.Rules.CPF
{
    public class CPFValidator
    {
        public static bool Validate(ReadOnlySpan<char> input)
        {
            if (input.Length > CPFConfiguration.MaximumSize)
                return false;
            if (input.Length > CPFConfiguration.DigitsSize && !NumericData.CheckFormatting(input, CPFConfiguration.PunctuationIndexes))
                return false;

            if (!NumericData.CheckQuantityOfNumericChars(input, CPFConfiguration.DigitsSize))
                return false;

            Span<char> numericOnlyInput = stackalloc char[CPFConfiguration.DigitsSize];
            NumericData.GetOnlyDigits(input, ref numericOnlyInput);

            return ValidateCPFNumericRule(numericOnlyInput);
        }

        private static bool ValidateCPFNumericRule(ReadOnlySpan<char> numericOnlyInput)
        {
            return NumericData.CheckIfAllNumbersAreNotTheSame(numericOnlyInput)
                && CheckVerificationDigits(numericOnlyInput);
        }

        private static bool CheckVerificationDigits(ReadOnlySpan<char> input, int digitsToCheck = 2)
        {
            if (digitsToCheck == 0)
                return true;

            double sum = 0;
            int multiplier = digitsToCheck == 2 ? 10 : 11;

            for (int i = 0; i < input.Length - digitsToCheck; i++)
            {
                sum += char.GetNumericValue(input[i]) * multiplier;
                multiplier--;
            }

            var rest = sum % 11;
            var verificationDigit = 11 - rest;
            verificationDigit = verificationDigit >= 10 ? 0 : verificationDigit;
            if (verificationDigit != (int)char.GetNumericValue(input[CPFConfiguration.DigitsSize - digitsToCheck]))
                return false;

            return CheckVerificationDigits(input, digitsToCheck - 1);
        }
    }
}
