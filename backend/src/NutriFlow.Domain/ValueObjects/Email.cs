using NutriFlow.Domain.Settings;

namespace NutriFlow.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public const int MaxLenghtAdress = 256;
        public string Adress { get; private set; }

        public Email() { }

        public Email(string adress)
        {
            adress = adress.Trim();
            ValidateMaxLength(adress);
            ValidateAtSymbol(adress);
            Adress = adress;
        }

        private void ValidateMaxLength(string adress)
        {
            if (!(adress.Length <= MaxLenghtAdress))
                throw new ArgumentException($"Endereço de email não pode exceder {MaxLenghtAdress} caracteres.", nameof(adress));
        }

        private void ValidateAtSymbol(string endereco)
        {
            var count = endereco.Count(c => c == '@');
            if (count != 1 || endereco.StartsWith("@") || endereco.EndsWith("@"))
                throw new ArgumentException("Endereço de email deve conter exatamente um '@' válido.", nameof(endereco));
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Adress;
        }

        public override string ToString() => Adress;
    }
}