using System;

namespace Documento.BR.Models
{
    public interface IDocument: IEquatable<IDocument>
    {
        public string Value { get; }
        public bool IsEmpty { get; }
    }
}
