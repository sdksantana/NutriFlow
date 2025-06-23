namespace NutriFlow.Domain.Entities
{
    public class BodyMeasurement
    {
        public Guid Id { get; }
        public int Age { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public decimal? Neck { get; private set; }
        public decimal? Chest { get; private set; }
        public decimal? Waist { get; private set; }
        public decimal? Abdomen { get; private set; }
        public decimal? Hip { get; private set; }
        public decimal? RightArm { get; private set; }
        public decimal? LeftArm { get; private set; }
        public decimal? RightForearm { get; private set; }
        public decimal? LeftForearm { get; private set; }
        public decimal? RightThigh { get; private set; }
        public decimal? LeftThigh { get; private set; }
        public decimal? RightCalf { get; private set; }
        public decimal? LeftCalf { get; private set; }
        public Patient Patient { get; private set; } = null!;

        public BodyMeasurement(
            int age,
            int height,
            int weight,
            decimal? neck = null,
            decimal? chest = null,
            decimal? waist = null,
            decimal? abdomen = null,
            decimal? hip = null,
            decimal? rightArm = null,
            decimal? leftArm = null,
            decimal? rightForearm = null,
            decimal? leftForearm = null,
            decimal? rightThigh = null,
            decimal? leftThigh = null,
            decimal? rightCalf = null,
            decimal? leftCalf = null,
            Patient patient = null!)
        {
            Age = age;
            Height = height;
            Weight = weight;
            Neck = neck;
            Chest = chest;
            Waist = waist;
            Abdomen = abdomen;
            Hip = hip;
            RightArm = rightArm;
            LeftArm = leftArm;
            RightForearm = rightForearm;
            LeftForearm = leftForearm;
            RightThigh = rightThigh;
            LeftThigh = leftThigh;
            RightCalf = rightCalf;
            LeftCalf = leftCalf;
            Patient = patient;
        }

        public BodyMeasurement(
            Guid id,
            int age,
            int height,
            int weight,
            decimal? neck = null,
            decimal? chest = null,
            decimal? waist = null,
            decimal? abdomen = null,
            decimal? hip = null,
            decimal? rightArm = null,
            decimal? leftArm = null,
            decimal? rightForearm = null,
            decimal? leftForearm = null,
            decimal? rightThigh = null,
            decimal? leftThigh = null,
            decimal? rightCalf = null,
            decimal? leftCalf = null,
            Patient patient = null!)
            : this(age, height, weight, neck, chest, waist, abdomen, hip, rightArm, leftArm, rightForearm, leftForearm, rightThigh, leftThigh, rightCalf, leftCalf, patient)
        {
            Id = id;
        }

        public void Update(
            int age,
            int height,
            int weight,
            decimal? neck = null,
            decimal? chest = null,
            decimal? waist = null,
            decimal? abdomen = null,
            decimal? hip = null,
            decimal? rightArm = null,
            decimal? leftArm = null,
            decimal? rightForearm = null,
            decimal? leftForearm = null,
            decimal? rightThigh = null,
            decimal? leftThigh = null,
            decimal? rightCalf = null,
            decimal? leftCalf = null,
            Patient patient = null!)
        {
            Age = age;
            Height = height;
            Weight = weight;
            Neck = neck;
            Chest = chest;
            Waist = waist;
            Abdomen = abdomen;
            Hip = hip;
            RightArm = rightArm;
            LeftArm = leftArm;
            RightForearm = rightForearm;
            LeftForearm = leftForearm;
            RightThigh = rightThigh;
            LeftThigh = leftThigh;
            RightCalf = rightCalf;
            LeftCalf = leftCalf;
            Patient = patient;
        }

        public void SetPatient(Patient patient)
        {
            Patient = patient;
        }
    }
}
