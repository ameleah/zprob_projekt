using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookreview.Models.BaseModels
{
    public class Author
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Bio { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }


        public Author(string firstName, string lastName, DateTime birthDate, string bio)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Bio = bio;
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}
