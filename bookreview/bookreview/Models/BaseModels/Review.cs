using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookreview.Models;

namespace bookreview.Models.BaseModels
{
    public class Review
    {
        public ApplicationUser User { get; private set; }

        public bool EntityType { get; private set; }

        public Object Entity { get; private set; }

        public string Text { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }


        public Review(ApplicationUser user, bool entityType, Object entity, string text)
        {
            User = user;
            EntityType = entityType;
            Entity = entity;
            Text = text;
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}
