using System;
using System.Threading;
using static System.Console;
using System.Data.SqlClient;

namespace Patients
{
    class Program
    {
        static string connectionString = "Server=localhost;Database=PatientJournal;Integrated Security=true";

        static void Main(string[] args)
        {
            do
            {
                CursorVisible = false;

                WriteLine(" 1. Add patient");
                WriteLine(" 2. Search patient");

                ConsoleKeyInfo input = ReadKey(true);

                Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:

                        AddPatient();

                        break;

                    case ConsoleKey.D2:

                        WriteLine(" Under construction");
                        Thread.Sleep(2000);

                        Clear();

                        break;
                }

            } while (true);

        }

        private static void AddPatient()
        {
            CursorVisible = true;

            WriteLine($"{"First name: ",24}");
            WriteLine($"{"Last name: ",24}");
            WriteLine($"{"Social security number: ",24}");

            SetCursorPosition(24, 0);
            string firstName = ReadLine();

            SetCursorPosition(24, 1);
            string lastName = ReadLine();

            SetCursorPosition(24, 2);
            string socialSecurityNumber = ReadLine();

            Patients patient = new Patients(firstName: firstName, lastName: lastName, socialSecurityNumber: socialSecurityNumber);

            InsertPatientIntoDb(patient);

            CursorVisible = false;

            WriteLine("\nPatient registered");
            Thread.Sleep(2000);

            Clear();
        }

        private static void InsertPatientIntoDb(Patients patient)
        {
            var query = @"INSERT INTO Patients (FirstName, LastName, SocialSecurityNumber)
                          VALUES (@FirstName, @LastName, @SocialSecurityNumber)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(query, connection);
            {
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@SocialSecurityNumber", patient.SocialSecurityNumber);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }
        }
    }
}
