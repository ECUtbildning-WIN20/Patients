using Microsoft.Data.SqlClient;
using Patients.Models.Domain;
using System;
using System.Collections.Generic;
using System.Resources;
using static System.Console;

namespace Patients
{
    class Program
    {
        static string connectionString = "Server=.;Database=PatientJournal; Trusted_Connection=True;";
        //static string connectionString = "Data Source=.;Initial Catalog=PatientJournal; Integrated Security=True;";

        static void Main(string[] args)
        {
            CursorVisible = false;

            bool applicationRunning = true;

            do
            {
                WriteLine("1. Register patient");
                WriteLine("2. Search patient");
                WriteLine("3. Exit");

                ConsoleKeyInfo input = ReadKey(true);

                Clear();

                switch (input.Key)
                {
                    case ConsoleKey.D1:

                        RegisterPatient();

                        break;

                    case ConsoleKey.D2:

                        SearchPatient();

                        break;

                    case ConsoleKey.D3:

                        applicationRunning = false;

                        break;
                }

                Clear();

            } while (applicationRunning);

        }

        private static void SearchPatient()
        {
            Write("Social Security Number: ");

            string socialSecurityNumber = ReadLine();

            Clear();

            var patient = FindPatient(socialSecurityNumber);

            if (patient != null)
            {
                WriteLine(patient.FullName);
                WriteLine(patient.SocialSecurityNumber);
                WriteLine();

                LoadJournal(patient);

                foreach (var journalEntry in patient.Journal.Entries)
                {
                    WriteLine("---------------------------------------");
                    WriteLine($"Date: {journalEntry.EntryDate}");
                    WriteLine($"Entry By: {journalEntry.EntryBy}");
                    WriteLine($"Entry: {journalEntry.Entry}");
                }
            }
            else
            {
                WriteLine("Patient not found");
            }

            ReadKey();
        }

        private static Patient FindPatient(string socialSecurityNumber)
        {
            var sql = @"SELECT Id, FirstName, LastName, SocialSecurityNumber 
                        FROM Patients
                        WHERE SocialSecurityNumber = @SocialSecurityNumber";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@SocialSecurityNumber", socialSecurityNumber);

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            Patient patient = null;

            if (dataReader.Read())
            {
                patient = new Patient(
                    id: (int) dataReader["Id"],
                    firstName: (string) dataReader["FirstName"],
                    lastName: (string) dataReader["LastName"],
                    socialSecurityNumber: (string) dataReader["SocialSecurityNumber"]);
            }

            connection.Close();

            return patient;
        }

        private static void LoadJournal(Patient patient)
        {
            int journalId = FetchJournalId(patient);

            var sql = @"SELECT Id, EntryBy, EntryDate, Entry 
                            FROM JournalEntries
	                        WHERE JournalId = @JournalId";

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@JournalId", journalId);

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            var journalEntryList = new List<JournalEntry>();

            while (dataReader.Read())
            {
                journalEntryList.Add(new JournalEntry(
                    id: (int)dataReader["Id"],
                    entryBy: (string)dataReader["EntryBy"],
                    entryDate: (DateTime)dataReader["EntryDate"],
                    entry: (string)dataReader["Entry"]));
            }

            connection.Close();

            patient.Journal = new Journal
            {
                Entries = journalEntryList
            };
        }

        private static int FetchJournalId(Patient patient)
        {
            var sql = @"SELECT Id FROM Journals
                WHERE PatientId = @PatientId";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@PatientId", patient.Id);

            connection.Open();

            SqlDataReader dataReader = command.ExecuteReader();

            dataReader.Read();

            int journalId = (int)dataReader["Id"];

            return journalId;
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
