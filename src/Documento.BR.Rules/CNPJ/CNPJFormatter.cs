using System;

namespace Documento.BR.Rules.CNPJ
{
    public static class CNPJFormatter
    {
        /// <summary>
        /// Adiciona a formatação padrão do documento.
        /// </summary>
        /// <exception cref="ArgumentException">Lança exceção quando é colocado um input inválido para o documento.</exception>
        public static string Format(ReadOnlySpan<char> input)
        {
            if (TryFormat(input, out string? output) == false)
            {
                throw new ArgumentException("The string is not in a valid format.");
            }

            return output!;
        }

        /// <summary>
        /// Tenta adicionar a formatação padrão do documento.
        /// </summary>
        public static bool TryFormat(ReadOnlySpan<char> input, out string? output)
        {
            output = null;

            if (CNPJValidator.Validate(input) == false)
                return false;

            Span<char> outputAsSpan = stackalloc char[CNPJConfiguration.MaximunSize];
            NumericData.FormatData(CNPJConfiguration.PunctuationIndexes, input, ref outputAsSpan);

            output = new string(outputAsSpan);
            return true;
        }

        /// <summary>
        /// Retorna uma string sem os caracteres de pontuação.
        /// </summary>
        public static string Unformat(ReadOnlySpan<char> input)
        {
            Span<char> numericOnlyInput = stackalloc char[CNPJConfiguration.DigitsSize];
            NumericData.GetOnlyDigits(input, ref numericOnlyInput);

            return new string(numericOnlyInput);
        }
    }
}
