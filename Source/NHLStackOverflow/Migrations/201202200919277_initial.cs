namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("Reads", new[] { "Question_QuestionID" });
            DropIndex("Reads", new[] { "User_UserID" });
            DropIndex("Messages", new[] { "User_UserID" });
            DropIndex("Messages", new[] { "Post_QuestionID" });
            DropIndex("Messages", new[] { "Receiver_UserID" });
            DropIndex("Messages", new[] { "Sender_UserID" });
            DropIndex("Favorites", new[] { "Post_QuestionID" });
            DropIndex("Favorites", new[] { "User_UserID" });
            DropIndex("QuestionTags", new[] { "Tag_TagID" });
            DropIndex("QuestionTags", new[] { "Question_QuestionID" });
            DropIndex("Questions", new[] { "User_UserID" });
            DropIndex("Comments", new[] { "Answer_AnswerID" });
            DropIndex("Comments", new[] { "Question_QuestionID" });
            DropIndex("Comments", new[] { "User_UserID" });
            DropIndex("Badges", new[] { "User_UserID" });
            DropIndex("Answers", new[] { "Question_QuestionID" });
            DropIndex("Answers", new[] { "User_UserID" });
            DropForeignKey("Reads", "Question_QuestionID", "Questions");
            DropForeignKey("Reads", "User_UserID", "Users");
            DropForeignKey("Messages", "User_UserID", "Users");
            DropForeignKey("Messages", "Post_QuestionID", "Questions");
            DropForeignKey("Messages", "Receiver_UserID", "Users");
            DropForeignKey("Messages", "Sender_UserID", "Users");
            DropForeignKey("Favorites", "Post_QuestionID", "Questions");
            DropForeignKey("Favorites", "User_UserID", "Users");
            DropForeignKey("QuestionTags", "Tag_TagID", "Tags");
            DropForeignKey("QuestionTags", "Question_QuestionID", "Questions");
            DropForeignKey("Questions", "User_UserID", "Users");
            DropForeignKey("Comments", "Answer_AnswerID", "Answers");
            DropForeignKey("Comments", "Question_QuestionID", "Questions");
            DropForeignKey("Comments", "User_UserID", "Users");
            DropForeignKey("Badges", "User_UserID", "Users");
            DropForeignKey("Answers", "Question_QuestionID", "Questions");
            DropForeignKey("Answers", "User_UserID", "Users");
            DropTable("Reads");
            DropTable("Messages");
            DropTable("Favorites");
            DropTable("Tags");
            DropTable("QuestionTags");
            DropTable("Questions");
            DropTable("Comments");
            DropTable("Badges");
            DropTable("Users");
            DropTable("Answers");

            CreateTable(
                "Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Votes = c.Int(nullable: false),
                        Flag = c.Int(nullable: false),
                        Created_At = c.String(nullable: false),
                        LastEdited = c.String(),
                        User_UserID = c.Int(),
                        Question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("Users", t => t.User_UserID)
                .ForeignKey("Questions", t => t.Question_QuestionID)
                .Index(t => t.User_UserID)
                .Index(t => t.Question_QuestionID);
            
            CreateTable(
                "Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Rank = c.Int(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Location = c.String(),
                        Website = c.String(),
                        Languages = c.String(),
                        Created_At = c.String(nullable: false),
                        LastEdited = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "Badges",
                c => new
                    {
                        BadgeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Created_At = c.String(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.BadgeID)
                .ForeignKey("Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Votes = c.Int(nullable: false),
                        Created_At = c.String(),
                        LastEdited = c.String(),
                        User_UserID = c.Int(),
                        Question_QuestionID = c.Int(),
                        Answer_AnswerID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("Users", t => t.User_UserID)
                .ForeignKey("Questions", t => t.Question_QuestionID)
                .ForeignKey("Answers", t => t.Answer_AnswerID)
                .Index(t => t.User_UserID)
                .Index(t => t.Question_QuestionID)
                .Index(t => t.Answer_AnswerID);
            
            CreateTable(
                "Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Votes = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Answered = c.Int(nullable: false),
                        Flag = c.Int(nullable: false),
                        Created_At = c.String(nullable: false),
                        LastEdited = c.String(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "QuestionTags",
                c => new
                    {
                        QuestionTagID = c.Int(nullable: false, identity: true),
                        Question_QuestionID = c.Int(),
                        Tag_TagID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionTagID)
                .ForeignKey("Questions", t => t.Question_QuestionID)
                .ForeignKey("Tags", t => t.Tag_TagID)
                .Index(t => t.Question_QuestionID)
                .Index(t => t.Tag_TagID);
            
            CreateTable(
                "Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Beschrijving = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "Favorites",
                c => new
                    {
                        FavoriteID = c.Int(nullable: false, identity: true),
                        Created_At = c.String(),
                        User_UserID = c.Int(),
                        Post_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.FavoriteID)
                .ForeignKey("Users", t => t.User_UserID)
                .ForeignKey("Questions", t => t.Post_QuestionID)
                .Index(t => t.User_UserID)
                .Index(t => t.Post_QuestionID);
            
            CreateTable(
                "Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Created_At = c.String(nullable: false),
                        LastEdited = c.String(),
                        Sender_UserID = c.Int(),
                        Receiver_UserID = c.Int(),
                        Post_QuestionID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("Users", t => t.Sender_UserID)
                .ForeignKey("Users", t => t.Receiver_UserID)
                .ForeignKey("Questions", t => t.Post_QuestionID)
                .ForeignKey("Users", t => t.User_UserID)
                .Index(t => t.Sender_UserID)
                .Index(t => t.Receiver_UserID)
                .Index(t => t.Post_QuestionID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "Reads",
                c => new
                    {
                        ReadID = c.Int(nullable: false, identity: true),
                        User_UserID = c.Int(),
                        Question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.ReadID)
                .ForeignKey("Users", t => t.User_UserID)
                .ForeignKey("Questions", t => t.Question_QuestionID)
                .Index(t => t.User_UserID)
                .Index(t => t.Question_QuestionID);
            
        }
        
        public override void Down()
        {
            DropIndex("Reads", new[] { "Question_QuestionID" });
            DropIndex("Reads", new[] { "User_UserID" });
            DropIndex("Messages", new[] { "User_UserID" });
            DropIndex("Messages", new[] { "Post_QuestionID" });
            DropIndex("Messages", new[] { "Receiver_UserID" });
            DropIndex("Messages", new[] { "Sender_UserID" });
            DropIndex("Favorites", new[] { "Post_QuestionID" });
            DropIndex("Favorites", new[] { "User_UserID" });
            DropIndex("QuestionTags", new[] { "Tag_TagID" });
            DropIndex("QuestionTags", new[] { "Question_QuestionID" });
            DropIndex("Questions", new[] { "User_UserID" });
            DropIndex("Comments", new[] { "Answer_AnswerID" });
            DropIndex("Comments", new[] { "Question_QuestionID" });
            DropIndex("Comments", new[] { "User_UserID" });
            DropIndex("Badges", new[] { "User_UserID" });
            DropIndex("Answers", new[] { "Question_QuestionID" });
            DropIndex("Answers", new[] { "User_UserID" });
            DropForeignKey("Reads", "Question_QuestionID", "Questions");
            DropForeignKey("Reads", "User_UserID", "Users");
            DropForeignKey("Messages", "User_UserID", "Users");
            DropForeignKey("Messages", "Post_QuestionID", "Questions");
            DropForeignKey("Messages", "Receiver_UserID", "Users");
            DropForeignKey("Messages", "Sender_UserID", "Users");
            DropForeignKey("Favorites", "Post_QuestionID", "Questions");
            DropForeignKey("Favorites", "User_UserID", "Users");
            DropForeignKey("QuestionTags", "Tag_TagID", "Tags");
            DropForeignKey("QuestionTags", "Question_QuestionID", "Questions");
            DropForeignKey("Questions", "User_UserID", "Users");
            DropForeignKey("Comments", "Answer_AnswerID", "Answers");
            DropForeignKey("Comments", "Question_QuestionID", "Questions");
            DropForeignKey("Comments", "User_UserID", "Users");
            DropForeignKey("Badges", "User_UserID", "Users");
            DropForeignKey("Answers", "Question_QuestionID", "Questions");
            DropForeignKey("Answers", "User_UserID", "Users");
            DropTable("Reads");
            DropTable("Messages");
            DropTable("Favorites");
            DropTable("Tags");
            DropTable("QuestionTags");
            DropTable("Questions");
            DropTable("Comments");
            DropTable("Badges");
            DropTable("Users");
            DropTable("Answers");
        }
    }
}
