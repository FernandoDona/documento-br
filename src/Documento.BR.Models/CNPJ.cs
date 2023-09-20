using Documento.BR.Rules.Formatters;
using Documento.BR.Rules.Validators;
using System;

namespace Documento.BR.Models
{
    public class CNPJ : IDocument, IEquatable<CNPJ>
    {
        private readonly string _value;
        private bool _isEmpty;
        public string Value => _value;
        public bool IsEmpty => _isEmpty;
        public static CNPJ Empty = new CNPJ(string.Empty);

        private CNPJ(string value)
        {
            _isEmpty = value == string.Empty;
            _value = value;
        }

        /// <summary>
        /// Cria uma instância de <see cref="CNPJ"/> caso o input seja válido.
        /// </summary>
        /// <exception cref="ArgumentException">Lança uma exceção quando o input é inválido</exception>
        public static CNPJ Create(string value)
        {
            CNPJ cpf = TryCreate(value);
            if (cpf.IsEmpty)
                throw new ArgumentException("The value is in a invalid format.");

            return cpf;
        }

        /// <summary>
        /// Tenta criar uma instância de <see cref="CNPJ"/>.
        /// </summary>
        /// <returns>Retorna <see cref="CNPJ"/> com valor caso o input seja válido ou <see cref="CNPJ"/> vazio caso seja inválido</returns>
        public static CNPJ TryCreate(string value)
        {
            if (!Validate(value))
                return CNPJ.Empty;

            return new CNPJ(CNPJFormatter.Format(value));
        }

        public static bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return CNPJValidator.Validate(value);
        }

        public override string ToString() => _value;
        public bool Equals(CNPJ other) => _value.Equals(other._value);
        public bool Equals(IDocument other)
        {
            if (other is CNPJ otherDocument)
            {
                return _value.Equals(otherDocument._value);
            }

            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is CNPJ other)
            {
                return _value.Equals(other);
            }

            return false;
        }
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(CNPJ left, CNPJ right) => left.Equals(right);
        public static bool operator !=(CNPJ left, CNPJ right) => !left.Equals(right);

        public static implicit operator CNPJ(string input) => TryCreate(input);
    }
}
