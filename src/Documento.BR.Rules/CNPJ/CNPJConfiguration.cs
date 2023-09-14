using System.Collections.Generic;

namespace Documento.BR.Rules.CNPJ
{
    internal class CNPJConfiguration
    {
        internal static readonly IDictionary<int, char> PunctuationIndexes = new Dictionary<int, char>
        {
            { 2, '.' },
            { 6, '.' },
            { 10, '/' },
            { 15, '-' }
        };
        internal const int DigitsSize = 14;
        internal const int MaximumSize = 18;
    }
}
