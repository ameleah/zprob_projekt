using System;
using System.ComponentModel.DataAnnotations;

namespace bookreview.Models.BaseModels
{
    public class Rate
    {
        [Key]
        public int Id { get; private set; }
        public ApplicationUser User { get; private set; }

        public bool EntityType { get; private set; }

        public int Value { get; private set; }

        public Object Entity { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public Rate() { }
        public Rate(ApplicationUser user, bool entityType, int value, Object entity)
        {
            if (user == null) throw new Exception("Nie wybrano użytkownika");
            if (value <= 0 || value >10) throw new Exception("Ocena musi być z zakresu 1-10");
            if (entity == null) throw new Exception("Nie wybrano obiektu do oceny");

            User = user;
            EntityType = entityType;
            Value = value;
            Entity = entity;
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}
