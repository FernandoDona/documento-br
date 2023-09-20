using System.Collections.Generic;

namespace Documento.BR.Rules.Configuration
{
    internal static class CPFConfiguration
    {
        internal static readonly IDictionary<int, char> PunctuationIndexes = new Dictionary<int, char>
        {
            { 3, '.' },
            { 7, '.' },
            { 11, '-' }
        };
        internal const int DigitsSize = 11;
        internal const int MaximumSize = 14;
    }
}
