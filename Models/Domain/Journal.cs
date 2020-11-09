using System.Collections.Generic;
using System.Text;

namespace Patients.Models.Domain
{
    class Journal
    {
        public Journal()
        {

        }

        public Journal(int id)
        {
            Id = id;
        }

        public int Id { get; }

        public List<JournalEntry> Entries { get; set; } = new List<JournalEntry>();
    }
}
