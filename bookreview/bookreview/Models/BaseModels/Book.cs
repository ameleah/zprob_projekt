using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookreview.Models.BaseModels
{

    public class Book
    {       
        public string Name { get; private set; }

        public Author Author { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Category> CategoryList { get; private set; }

        public Book(string name, Author author, DateTime releaseDate, string description)
        {
            Name = name;
            Author = author;
            ReleaseDate = releaseDate;
            Description = description;
            CreatedAt = UpdatedAt = DateTime.Now;
            CategoryList = new List<Category>();
        }
                
    }
}
