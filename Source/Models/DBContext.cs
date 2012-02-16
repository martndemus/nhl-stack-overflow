using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class NHLSODBContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Read> Read { get; set; }
        public DbSet<TagRegel> TagRegels { get; set; }
        public DbSet<User> Users { get; set; }
    }
}