using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookreview.Models.BaseModels
{
    public class Category
    {
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
