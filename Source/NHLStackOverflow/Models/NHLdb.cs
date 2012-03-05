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
                new Tag { Name = "JavaScript", Description = "Teh Awsum" },
                new Tag { Name = "Objective-C", Description = "Apple rules"},
                new Tag { Name = "C++", Description = "plusser de plus"},
                new Tag { Name = "Python", Description = "ssshhhaagasiash"},
                new Tag { Name = "Ruby", Description = "Bling!"},
                new Tag { Name = "Smalltalk", Description = "blablablablablabla"},
                new Tag { Name = "ASP.NET", Description = "asp dot et net"},
                new Tag { Name = "PHP", Description = "peeehaaaapeeee"},
                new Tag { Name = "HTML", Description = "haaaateeeeeemel"},
                new Tag { Name = "CSS", Description = "ceeeeses"},
                new Tag { Name = "F#", Description = "wherearedene"},
                new Tag { Name = "Visual Basic", Description = "Gay Sick"},
                new Tag { Name = "Delphi", Description = "dolfijnflipper"},
                new Tag { Name = "ActionScript", Description = "flasherdeflash"},
            };

            tags.ForEach(s => context.Tags.Add(s) );
            context.SaveChanges();

            var read = new List<Read>
            {
                new Read { UserId = 1, Question = 1},
                new Read { UserId = 2, Question = 2},
                new Read { UserId = 3, Question = 3}
            };

            read.ForEach(s => context.Read.Add(s));
            context.SaveChanges();

            var questiontag = new List<QuestionTag>
            {
                new QuestionTag { QuestionId = 1, TagId = 1},
                new QuestionTag { QuestionId = 2, TagId = 2},
                new QuestionTag { QuestionId = 3, TagId = 3}
            };

            questiontag.ForEach(s => context.QuestionTags.Add(s));
            context.SaveChanges();

            var question = new List<Question>
            {
                new Question { UserId = 1 },
                new Question { UserId = 2 },
                new Question { UserId = 3 }
            };

            question.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var option = new List<Option>
            {
                new Option { Name = "pietpaal", Value = "een"},
                new Option { Name = "henksen", Value = "twee"},
                new Option { Name = "jantjes", Value = "drie"}
            };

            option.ForEach(s => context.Options.Add(s));
            context.SaveChanges();

            var message = new List<Message>
            {
                new Message { SenderId = 1, ReceiverId = 1, QuestionId = 1},
                new Message { SenderId = 2, ReceiverId = 2, QuestionId = 2},
                new Message { SenderId = 3, ReceiverId = 3, QuestionId = 3}
            };

            message.ForEach(s => context.Messages.Add(s));
            context.SaveChanges();

            var favorite = new List<Favorite>
            {
                new Favorite { UserId = 1, QuestionId = 1},
                new Favorite { UserId = 2, QuestionId = 2},
                new Favorite { UserId = 3, QuestionId = 3}
            };

            favorite.ForEach(s => context.Favorites.Add(s));
            context.SaveChanges();

            var comment = new List<Comment>
            {
                new Comment { UserId = 1, QuestionId = 1, AnswerId = 1},
                new Comment { UserId = 2, QuestionId = 2, AnswerId = 2},
                new Comment { UserId = 3, QuestionId = 3, AnswerId = 3}
            };

            comment.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();

            var badge = new List<Badge>
            {
                new Badge { UserId = 1 },
                new Badge { UserId = 2 },
                new Badge { UserId = 3 }
            };

            badge.ForEach(s => context.Badges.Add(s));
            context.SaveChanges();

            var answer = new List<Answer>
            {
                new Answer { UserId = 1, QuestionId = 1},
                new Answer { UserId = 2, QuestionId = 2},
                new Answer { UserId = 3, QuestionId = 3}
            };

            answer.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();
        }
    }
}