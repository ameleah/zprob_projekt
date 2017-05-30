﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace bookreview.Models.BaseModels
{
    public class Book
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; private set; }

        [Required]
        public Author Author { get; private set; }


        [Required]
        public DateTime ReleaseDate { get; private set; }

        public string Description { get; private set; }
        
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public List<Category> CategoryList { get; private set; }
        public List<Review> ReviewList { get; private set; }
        public List<Rate> RateList { get; private set; }

        public Book() { }
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
            ReviewList = new List<Review>();
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
