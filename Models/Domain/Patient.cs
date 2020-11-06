namespace Patients.Models.Domain
{
    class Patient
    {
        public Patient(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSecurityNumber { get; }
    }
}
