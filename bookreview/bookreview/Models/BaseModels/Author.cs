using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bookreview.Models.BaseModels
{

    public class Author
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string FirstName { get; private set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string LastName { get; private set; }

        [Required]
        public DateTime BirthDate { get; private set; }

        public string Bio { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public virtual ICollection<Book> BookList { get; private set; }
        public List<Review> ReviewList { get; private set; }
        public List<Rate> RateList { get; private set; }

        public Author() { }

        public Author(string firstName, string lastName, DateTime birthDate, string bio)
        {
            if (firstName == null) throw new Exception("Pole imię nie może być puste");
            if (lastName == null) throw new Exception("Pole nazwisko nie może być puste");
            if (birthDate > DateTime.Now) throw new Exception("Podaj poprawną datę");

            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Bio = bio;
            CreatedAt = UpdatedAt = DateTime.Now;
            BookList = new List<Book>();
            ReviewList = new List<Review>();
            RateList = new List<Rate>();
        }

        public string ShortText()
        {
            return (Truncate(Bio, 250));
        }

        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }

        public int GetNumberOfRates()
        {
            return RateList.Count;
        }

        public float GetAverageOfRates()
        {
            float sum = 0;
            foreach (Rate r in RateList)
            {
                sum += r.Value;
            }
            return sum / RateList.Count;
        }

    }
}
