using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookreview.Models.BaseModels
{
    public class Book : Rateable
    {

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Zła długość nazwy")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; private set; }

        public int Author_Id { get; private set; }
        [Required(ErrorMessage = "Pole wymagane")]
        
        [ForeignKey("Author_Id")]
        public virtual Author Author { get; private set; }


        [Required(ErrorMessage = "Pole wymagane")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; private set; }

        public string Description { get; private set; }
        
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Category> CategoryList { get; private set; }
        
        public Book()
        {
            CategoryList = new List<Category>();
            RateList = new List<Rate>();
        }
        public Book(string name, Author author, DateTime releaseDate, string description)
        {
            if (name == null) throw new Exception("Pole nazwa nie może być puste");
            if (author == null) throw new Exception("Wybierz autora");

            Name = name;
            Author = author;
            ReleaseDate = releaseDate;
            Description = description;
            CreatedAt = UpdatedAt = DateTime.Now;
            CategoryList = new List<Category>();
            RateList = new List<Rate>();
        }

        public void AddCategory(Category cat)
        {
            this.CategoryList.Add(cat);
        }

        public string ShortText()
        {
            return (Truncate(Description, 250));
        }

        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }
        
    }
}
