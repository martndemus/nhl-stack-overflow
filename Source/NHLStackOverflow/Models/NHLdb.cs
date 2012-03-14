using System.Data.Entity;
using System.Collections.Generic;
using NHLStackOverflow.Classes;
using System;

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
                new User { UserName = "Piet25", Password = PasswordHasher.Hash("test"), Email = "test1@testmail.com", Rank = 3, Activated = 1 }, // admin allowed to view all!
                new User { UserName = "Klaas538", Password = PasswordHasher.Hash("admin"), Email = "test2@testmail.com", Rank = 0, Activated = 1 },
                new User { UserName = "Kees1979", Password = PasswordHasher.Hash("abc"), Email = "test3@testmail.com", Rank = 0, Activated = 1, PassLost = "BCLob11SgwNtcanBG3ZmayFehZfpBz67rIrNuPNO8xTHU4JJHZD1EKpSfXDnouNJeyK572UGaBpaSau+xjRfSw" } // only person 
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
                new Tag { Name = "C#", Description = "KUTPROGRAMMMEEERRRTTAAALLL", Count = 100 },
                new Tag { Name = "JavaScript", Description = "Teh Awsum123", Count = 1337 },
                new Tag { Name = "Objective-C", Description = "Apple rules12342134", Count = 10},
                new Tag { Name = "C++", Description = "plusser de plus12341234", Count = 9001},
                new Tag { Name = "Python", Description = "ssshhhaagasiash12341234", Count = 3},
                new Tag { Name = "Ruby", Description = "Bling!13412341234", Count = 40},
                new Tag { Name = "Smalltalk", Description = "blablablablablabla112341235werfqwef", Count = 20},
                new Tag { Name = "ASP.NET", Description = "asp dot et netasdfasdfasdf", Count = 666},
                new Tag { Name = "PHP", Description = "peeehaaaapeeeeadfasdf", Count = 222},
                new Tag { Name = "HTML", Description = "haaaateeeeeemelasdfasdf", Count = 901},
                new Tag { Name = "CSS", Description = "ceeeeseasdfasdfs", Count = 230},
                new Tag { Name = "F#", Description = "wherearedeneaadf", Count = 777},
                new Tag { Name = "Visual Basic", Description = "Gay Sickadfasdf", Count = 1},
                new Tag { Name = "Delphi", Description = "dolfijnflipperadfasdf", Count = -5},
                new Tag { Name = "ActionScript", Description = "flasherdeflashasdfasdfa", Count = -9000},
            };

            tags.ForEach(s => context.Tags.Add(s) );
            context.SaveChanges();

            var read = new List<Read>
            {
                new Read { UserId = 1, QuestionId = 1},
                new Read { UserId = 2, QuestionId = 2},
                new Read { UserId = 3, QuestionId = 3}
            };

            read.ForEach(s => context.Read.Add(s));
            context.SaveChanges();

            var questiontag = new List<QuestionTag>
            {
                new QuestionTag { QuestionId = 1, TagId = 1},
                new QuestionTag { QuestionId = 1, TagId = 4},
                new QuestionTag { QuestionId = 1, TagId = 5},
                new QuestionTag { QuestionId = 1, TagId = 7},
                new QuestionTag { QuestionId = 2, TagId = 2},
                new QuestionTag { QuestionId = 2, TagId = 3},
                new QuestionTag { QuestionId = 2, TagId = 8},
                new QuestionTag { QuestionId = 2, TagId = 10},
                new QuestionTag { QuestionId = 3, TagId = 6},
                new QuestionTag { QuestionId = 3, TagId = 9},
                new QuestionTag { QuestionId = 3, TagId = 11},
                new QuestionTag { QuestionId = 3, TagId = 12}
            };

            questiontag.ForEach(s => context.QuestionTags.Add(s));
            context.SaveChanges();

            var question = new List<Question>
            {
                new Question { UserId = 1, Title = "Hello world!1", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 2, 14, 8, 5, 10)) },
                new Question { UserId = 2, Title = "Hello world!2", Content = "2Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2004, 2, 13, 8, 5, 10)) },
                new Question { UserId = 3, Title = "Hello world!3", Content = "3Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(1996, 1, 10, 8, 5, 10)) },
                new Question { UserId = 3, Title = "Hello world!4", Content = "This is a flagged question! 3Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(1950, 1, 10, 8, 5, 10)), Flag = 1}
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
                new Message { SenderId = 1, ReceiverId = 1, Title = "1Hello world! Test title.", Content = "1Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."},
                new Message { SenderId = 2, ReceiverId = 2, QuestionId = 2, Title = "2Hello world! Test title.", Content = "2Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."},
                new Message { SenderId = 3, ReceiverId = 3, QuestionId = 3, Title = "3Hello world! Test title.", Content = "3Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."}
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
                new Comment { UserId = 1, QuestionId = 1, Content = "1Hello this is a comment on a question asked by a noob. The awnser is simple. Don't program pls :D", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2010, 2, 14, 8, 5, 10))},
                new Comment { UserId = 2, QuestionId = 2, Content = "2Hello this is a comment on a question asked by a noob. The awnser is simple. Don't program pls :D", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2009, 2, 14, 8, 5, 10))},
                new Comment { UserId = 3, QuestionId = 3, Content = "3Hello this is a comment on a question asked by a noob. The awnser is simple. Don't program pls :D", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2011, 2, 14, 8, 5, 10))},
                new Comment { UserId = 3, QuestionId = 1, Content = "4Hello this is a comment on a question asked by a noob. The awnser is simple. Don't program pls :D", Flag = 1, Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 14, 12, 5, 10))}

            };

            comment.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();

            var badge = new List<Badge>
            {
                new Badge { UserId = 1, Name = "test1" },
                new Badge { UserId = 2, Name = "test2" },
                new Badge { UserId = 3, Name = "test3" }
            };

            badge.ForEach(s => context.Badges.Add(s));
            context.SaveChanges();

            var answer = new List<Answer>
            {
                new Answer { UserId = 1, QuestionId = 1, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai."},
                new Answer { UserId = 2, QuestionId = 2, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai."},
                new Answer { UserId = 2, QuestionId = 2, Content = "Evil! Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai.", Flag = 1},
                new Answer { UserId = 3, QuestionId = 3, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai."}
            };

            answer.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();
        }
    }
}