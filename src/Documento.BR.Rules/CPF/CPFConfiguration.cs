using System.Collections.Generic;

namespace Documento.BR.Rules.CPF
{
    internal class CPFConfiguration
    {
        internal static readonly IDictionary<int, char> PunctuationIndexes = new Dictionary<int, char>
        {
            { 3, '.' },
            { 7, '.' },
            { 11, '-' }
        };
        internal const int DigitsSize = 11;
        internal const int MaximunSize = 14;
    }
}
