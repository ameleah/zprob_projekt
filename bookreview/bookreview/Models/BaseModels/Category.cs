using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bookreview.Models.BaseModels
{
    public class Category
    {
        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }

        public List<Book> BookList { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }


        public Category(string name)
        {
            Name = name;
            BookList = new List<Book>();
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}
