using System;

namespace Patients
{
    class Patients
    {
        public Patients (string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))    
                {
                    throw new ArgumentException("Parameter firstName cannot be empty or NULL");
                }
                firstName = value;
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Parameter firstName cannot be empty or NULL");
                }
                lastName = value;
            }
        }

        private string socialSecurityNumber;
        public string SocialSecurityNumber
        {
            get
            {
                return socialSecurityNumber;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Parameter firstName cannot be empty or NULL");
                }
                socialSecurityNumber = value;
            }
        }
    }
}