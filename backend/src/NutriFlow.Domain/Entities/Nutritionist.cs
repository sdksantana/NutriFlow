using NutriFlow.Domain.Enums;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Domain.Entities
{
    public class Nutritionist
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public EGender Gender { get; private set; }
        public Crn Crn { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        protected Nutritionist() { }

        public Nutritionist(string firstName,
                            string lastName,
                            EGender gender,
                            Crn crn,
                            Email email)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Crn = crn ?? throw new ArgumentNullException(nameof(crn));
            Email = email;
        }

        public Nutritionist(Guid id,
                            string firstName,
                            string lastName,
                            Crn crn,
                            EGender gender,
                            Email email) : this(firstName,
                                                lastName,
                                                gender,
                                                crn,
                                                email)
        {
            Id = id;
        }

        public void Update(string firstName,
                           string lastName,
                           EGender gender,
                           Email email)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
