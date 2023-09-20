using Documento.BR.Rules.Formatters;
using Documento.BR.Rules.Validators;
using System;

namespace Documento.BR.Models
{
    public class CPF : IDocument, IEquatable<CPF>
    {
        private readonly string _value;
        private bool _isEmpty;
        public string Value => _value;
        public bool IsEmpty => _isEmpty;
        public static CPF Empty = new CPF(string.Empty);

        private CPF(string value)
        {
            _isEmpty = value == string.Empty;
            _value = value;
        }

        /// <summary>
        /// Cria uma instância de <see cref="CPF"/> caso o input seja válido.
        /// </summary>
        /// <exception cref="ArgumentException">Lança uma exceção quando o input é inválido</exception>
        public static CPF Create(string value)
        {
            CPF cpf = TryCreate(value);
            if (cpf.IsEmpty)
                throw new ArgumentException("The value is in a invalid format.");
            
            return cpf;
        }

        /// <summary>
        /// Tenta criar uma instância de <see cref="CPF"/>.
        /// </summary>
        /// <returns>Retorna <see cref="CPF"/> com valor caso o input seja válido ou <see cref="CPF"/> vazio caso seja inválido</returns>
        public static CPF TryCreate(string value)
        {
            if (!Validate(value))
                return CPF.Empty;

            return new CPF(CPFFormatter.Format(value));
        }

        public static bool Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return CPFValidator.Validate(value);
        }

        public override string ToString() => _value;
        public bool Equals(CPF other) => _value.Equals(other._value);
        public bool Equals(IDocument other)
        {
            if (other is CPF otherDocument)
            {
                return _value.Equals(otherDocument._value);
            }

            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is CPF other)
            {
                return _value.Equals(other);
            }

            return false;
        }
        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(CPF left, CPF right) => left.Equals(right);
        public static bool operator !=(CPF left, CPF right) => !left.Equals(right);

        public static implicit operator CPF(string input) => TryCreate(input);
    }
}
