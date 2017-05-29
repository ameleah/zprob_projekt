using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bookreview.Models.BaseModels
{

    public class Author
    {
        [Key]
        public int Id { get; private set; }
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Bio { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Book> BookList { get; private set; }
        public List<Review> ReviewList { get; private set; }
        public List<Rate> RateList { get; private set; }

        public Author() { }

        public Author(string firstName, string lastName, DateTime birthDate, string bio)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Bio = bio;
            CreatedAt = UpdatedAt = DateTime.Now;
            BookList = new List<Book>();
            ReviewList = new List<Review>();
            RateList = new List<Rate>();
        }

    }
}
