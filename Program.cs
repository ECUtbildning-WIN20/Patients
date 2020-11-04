using System;
using System.Data.SqlClient;
using System.Threading;
using Patients.Domain;
using static System.Console;

namespace Patients
{
    class Program
    {

        static string connectionString = "Server=localhost;Database=PatientJournal;Integrated Security=True";

        static void Main(string[] args)
        {

            CursorVisible = false;

            var isApplicationRunning = true;

            do
            {
                WriteLine("1. Add patient");
                WriteLine("2. Search patient");
                WriteLine("3. Exit");

                ConsoleKeyInfo menuSelection;

                bool invalidInput;
                do
                {
                    menuSelection = ReadKey(true);


                    invalidInput = !(menuSelection.Key == ConsoleKey.D1 || menuSelection.Key == ConsoleKey.D2 ||
                                   menuSelection.Key == ConsoleKey.D3);

                } while (invalidInput);

                Clear();

                switch (menuSelection.Key)
                {
                    case ConsoleKey.D1:

                        try
                        {
                            AddPatient();
                        }
                        catch (Exception e)
                        {
                            Clear();
                            WriteLine(e.Message);
                        }
                        
                        Thread.Sleep(2000);

                        break;

                    case ConsoleKey.D2:

                        Write("Not yet implemented :)");

                        Thread.Sleep(2000);

                        break;

                    case ConsoleKey.D3:

                        isApplicationRunning = false;

                        break;

                }

                Clear();

            } while (isApplicationRunning);

        }

        private static void AddPatient()
        {
            CursorVisible = true;

            Write("First name: ");
            var firstName = ReadLine();

            Write("Last name: ");
            var lastName = ReadLine();

            Write("Social Security Number (yyyyMMdd-xxxx): ");
            var socialSecurityNumber = ReadLine();

            var patientInfo = new Patient(firstName, lastName, socialSecurityNumber);

            SqlAddingToDatabase(patientInfo);

            CursorVisible = false;

            WriteLine("Patient added.");

            Thread.Sleep(2000);

        }

        private static void SqlAddingToDatabase(Patient patientInfo)
        {
            var sql = @"
                INSERT INTO Patients (FirstName, LastName, SocialSecurityNumber)
                VALUES (@FirstName, @LastName, @SocialSecurityNumber)";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@FirstName", patientInfo.FirstName);
            command.Parameters.AddWithValue("@LastName", patientInfo.LastName);
            command.Parameters.AddWithValue("@SocialSecurityNumber", patientInfo.SocialSecurityNumber);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
