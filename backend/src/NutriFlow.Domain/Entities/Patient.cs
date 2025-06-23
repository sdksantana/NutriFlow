using NutriFlow.Domain.Enums;
using NutriFlow.Domain.ValueObjects;

namespace NutriFlow.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public EGender Gender { get; private set; }
        public Email Email { get; private set; } = null!;
        public bool IsMeasurementComplete { get; private set; } = false;

        protected Patient() { }

        public Patient(string firstName,
                       string lastName,
                       EGender gender,
                       Email email,
                       bool isMeasurementComplete)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            IsMeasurementComplete = IsMeasurementComplete;
        }

        public Patient(Guid id,
                        string firstName,
                        string lastName,
                        EGender gender,
                        Email email,
                        bool isMeasurementComplete) : this(firstName,
                                                    lastName,
                                                    gender,
                                                    email,
                                                    isMeasurementComplete)
        {
            Id = id;
        }

        public void MarkMeasurementAsComplete()
        {
            IsMeasurementComplete = true;
        }
    }
}
