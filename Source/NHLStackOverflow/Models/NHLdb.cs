using System.Data.Entity;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class NHLdb : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Read> Read { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserMeta> UserMeta { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class NHLdbInitializer : DropCreateDatabaseAlways<NHLdb>
    {
        protected override void Seed(NHLdb context)
        {
            var users = new List<User>
            {
                new User { UserName = "Piet25", Password = "bbbbbbbbbbbbbbbbbbbbbbbbbbbb", Email = "test@testmail.com", Rank = 0 },
                new User { UserName = "Klaas538", Password = "aaaaaaaaaaaaaaaaaaaaaaaaaaaa", Email = "test@testmail.com", Rank = 0 },
                new User { UserName = "Kees1979", Password = "cccccccccccccccccccccccccccc", Email = "test@testmail.com", Rank = 0 }
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var usermeta = new List<UserMeta>
            {
                new UserMeta { UserId = 1 },
                new UserMeta { UserId = 2 },
                new UserMeta { UserId = 3 }
            };

            usermeta.ForEach(s => context.UserMeta.Add(s));
            context.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag { Name = "C#", Description = "KUTPROGRAMMMEEERRRTTAAALLL" },
                new Tag { Name = "JavaScript", Description = "Teh Awsum" }
            };

            tags.ForEach(s => context.Tags.Add(s) );
            context.SaveChanges();

        }
    }
}