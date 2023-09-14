using System;
using System.Collections.Generic;

namespace Documento.BR.Rules
{
    internal static class NumericData
    {
        internal static bool CheckQuantityOfNumericChars(ReadOnlySpan<char> input, int quantityOfNumericCharsRequired)
        {
            if (input.Length < quantityOfNumericCharsRequired)
                return false;

            var numericCounter = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsLetter(input[i]))
                    return false;

                if (char.IsNumber(input[i]))
                    numericCounter++;

                if (numericCounter > quantityOfNumericCharsRequired)
                    return false;
            }

            if (numericCounter == quantityOfNumericCharsRequired)
                return true;

            return false;
        }

        internal static void GetOnlyDigits(ReadOnlySpan<char> input, ref Span<char> output)
        {
            var resultSize = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (resultSize >= output.Length)
                    break;

                if (char.IsNumber(input[i]) == false)
                    continue;

                output[resultSize] = input[i];
                resultSize++;
            }
        }

        /// <summary>
        /// Get a formatted output from a valid input.
        /// </summary>
        internal static void FormatData(IDictionary<int, char> nonNumericCharsPositions, ReadOnlySpan<char> input, ref Span<char> formattedOutput)
        {
            var addedChars = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsNumber(input[i]))
                    continue;

                if (nonNumericCharsPositions.ContainsKey(addedChars))
                    formattedOutput[addedChars] = nonNumericCharsPositions[addedChars++];

                formattedOutput[addedChars++] = input[i];
            }
        }

        internal static bool CheckIfAllNumbersAreNotTheSame(ReadOnlySpan<char> numericOnlyInput)
        {
            var lastChar = numericOnlyInput[0];
            for (int i = 0; i < numericOnlyInput.Length; i++)
            {
                if (lastChar != numericOnlyInput[i])
                {
                    return true;
                }

                lastChar = numericOnlyInput[i];
            }

            return false;
        }

        internal static void TryParseNumberToSpanChar(long input, ref Span<char> numericOnlyOutput)
        {
            var index = numericOnlyOutput.Length - 1;
            while (input > 0)
            {
                var rest = input % 10;
                var restChar = Convert.ToChar(rest + '0');
                numericOnlyOutput[index--] = Convert.ToChar(restChar);
                input /= 10;
            }

            for (int i = 0; i < numericOnlyOutput.Length; i++)
            {
                if (numericOnlyOutput[i] == '\0')
                    numericOnlyOutput[i] = '0';

                if (numericOnlyOutput[i] != '\0')
                    break;
            }
        }

        /// <summary>
        /// Valida se o input está formatado corretamente.
        /// </summary>
        internal static bool CheckFormatting(ReadOnlySpan<char> input, IDictionary<int, char> nonNumericCharsPositions)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (nonNumericCharsPositions.ContainsKey(i))
                {
                    if (nonNumericCharsPositions[i] != input[i])
                        return false;
                }
                else if (!char.IsNumber(input[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
