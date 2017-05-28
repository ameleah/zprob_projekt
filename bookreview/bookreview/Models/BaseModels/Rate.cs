﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookreview.Models.BaseModels
{
    public class Rate
    {
        public ApplicationUser User { get; private set; }

        public bool EntityType { get; private set; }

        public int Value { get; private set; }

        public Object Entity { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }


        public Rate(ApplicationUser user, bool entityType, int value, Object entity)
        {
            User = user;
            EntityType = entityType;
            Value = value;
            Entity = entity;
            CreatedAt = UpdatedAt = DateTime.Now;
        }

    }
}