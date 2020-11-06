using Microsoft.Data.SqlClient;
using Patients.Models.Domain;
using System;
using static System.Console;

namespace Patients
{
    class Program
    {
        //static string connectionString = "Server=.;Database=PatientJournal; Trusted_Connection=True;";
        static string connectionString = "Data Source=.;Initial Catalog=PatientJournal; Integrated Security=True;";

        static void Main(string[] args)
        {
            CursorVisible = false;

            bool applicationRunning = true;

            do
            {
                WriteLine("1. Register patient");
                WriteLine("2. Exit");

                ConsoleKeyInfo input = ReadKey(true);

                Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:

                        RegisterPatient();

                        break;

                    case ConsoleKey.D2:

                        applicationRunning = false;

                        break;
                }

                Clear();

            } while (applicationRunning);

        }

        private static void RegisterPatient()
        {
            Write("First name: ");

            string firstName = ReadLine();

            Write("Last name: ");

            string lastName = ReadLine();

            Write("Social security number: ");

            string socialSecurityNumber = ReadLine();

            var patient = new Patient(firstName, lastName, socialSecurityNumber);

            InsertPatient(patient);
        }

        private static void InsertPatient(Patient patient)
        {
            var sql = $@"
                INSERT INTO Patients (FirstName, LastName, SocialSecurityNumber)
                VALUES(@FirstName, @LastName, @SocialSecurityNumber)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@FirstName", patient.FirstName);
            command.Parameters.AddWithValue("@LastName", patient.LastName);
            command.Parameters.AddWithValue("@SocialSecurityNumber", patient.SocialSecurityNumber);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
