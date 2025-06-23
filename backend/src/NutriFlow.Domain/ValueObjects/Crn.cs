using NutriFlow.Domain.Settings;

namespace NutriFlow.Domain.ValueObjects
{
    public class Crn : ValueObject
    {
        public const int RequiredLength = 5;

        public string Value { get; private set; }

        public Crn() { }

        public Crn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CRN é obrigatório.", nameof(value));

            value = value.Trim();
            ValidateLength(value);
            ValidateDigits(value);
            Value = value;
        }

        private void ValidateLength(string value)
        {
            if (value.Length != RequiredLength)
                throw new ArgumentException($"CRN inválido. Deve ter exatamente {RequiredLength} caracteres.", nameof(value));
        }

        private void ValidateDigits(string value)
        {
            foreach (var c in value)
            {
                if (!char.IsDigit(c))
                    throw new ArgumentException("CRN inválido. Deve conter apenas dígitos.", nameof(value));
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}