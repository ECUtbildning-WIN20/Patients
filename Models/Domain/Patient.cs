using System.Collections.Generic;

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

        public Patient(int id, string firstName, string lastName, string socialSecurityNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSecurityNumber { get; }
        public string FullName => $"{FirstName} {LastName}";

        public Journal Journal { get; set; }
    }
}
