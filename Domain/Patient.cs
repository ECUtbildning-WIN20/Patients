using System;
using System.Text.RegularExpressions;

namespace Patients.Domain
{
    class Patient
    {
        public Patient(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First Name (and possibly other fields) was null or empty");
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Last Name (and possibly other fields) was null or empty");
                }
                lastName = value;
            }
        }

        public string SocialSecurityNumber 
        { 
            get => socialSecurityNumber;
            set
            {
                var ssnRegex = new Regex(@"^\d{8}[-\s]{0,1}\d{4}$");
                if (!ssnRegex.IsMatch(value))
                {
                    throw new ArgumentException("Social Security Number was in the wrong format");
                }
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Social Security Number (and possibly other fields) was null or empty");
                }
                socialSecurityNumber = value;
            }
        }


        private string firstName;
        private string lastName;
        private string socialSecurityNumber;
    }
}