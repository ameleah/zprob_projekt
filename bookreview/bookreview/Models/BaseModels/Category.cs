using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookreview.Models.BaseModels
{
    public class Category
    {
        public string Name { get; private set; }

        // lista

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }


        public Category(string name)
        {
            Name = name;
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}
