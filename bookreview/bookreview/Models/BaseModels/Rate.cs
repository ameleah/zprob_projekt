using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookreview.Models.BaseModels
{
    public class Rate
    {
        [Key]
        public int Id { get; private set; }

        public string User_Id { get; private set; }

        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; private set; }

        public bool EntityType { get; private set; }

        public int Value { get; private set; }

        public int? Book_Id { get; private set; }
        public int? Author_Id { get; private set; }
        
        [ForeignKey("Book_Id")]
        
        public virtual Book Book { get; private set; }

        [ForeignKey("Author_Id")]
        public virtual Author Author { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        [NotMapped]
        public int EntityId { get; private set; }

        public Rate() { }
        public Rate(ApplicationUser user, bool entityType, int value, Object entity)
        {
            Author a = new Author();
            Book b = new Book();
            if (user == null) throw new Exception("Nie wybrano użytkownika");
            if (value <= 0 || value >10) throw new Exception("Ocena musi być z zakresu 1-10");
            if (entity == null) throw new Exception("Nie wybrano obiektu do oceny");
            if ((!entityType && (entity is Author)) || (entityType && (entity is Book)))
            {
                User = user;
                EntityType = entityType;
                Value = value;

                if (entity is Author)
                {
                    Author = (Author)entity;
                    EntityId = Author.Id;
                }
                else
                {
                    Book = (Book)entity;
                    EntityId = Book.Id;
                }
                CreatedAt = UpdatedAt = DateTime.Now;
            } else
            {
                throw new Exception("Niepoprawny obiekt!");
            }
            
        }

    }
}
