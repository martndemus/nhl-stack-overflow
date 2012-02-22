namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Answers", "User_UserID", "Users");
            DropForeignKey("Answers", "Question_QuestionID", "Questions");
            DropForeignKey("Badges", "User_UserID", "Users");
            DropForeignKey("Comments", "User_UserID", "Users");
            DropForeignKey("Questions", "User_UserID", "Users");
            DropForeignKey("QuestionTags", "Question_QuestionID", "Questions");
            DropForeignKey("QuestionTags", "Tag_TagID", "Tags");
            DropForeignKey("Favorites", "User_UserID", "Users");
            DropForeignKey("Favorites", "Post_QuestionID", "Questions");
            DropForeignKey("Messages", "Sender_UserID", "Users");
            DropForeignKey("Messages", "Receiver_UserID", "Users");
            DropForeignKey("Reads", "User_UserID", "Users");
            DropForeignKey("Reads", "Question_QuestionID", "Questions");
            DropIndex("Answers", new[] { "User_UserID" });
            DropIndex("Answers", new[] { "Question_QuestionID" });
            DropIndex("Badges", new[] { "User_UserID" });
            DropIndex("Comments", new[] { "User_UserID" });
            DropIndex("Questions", new[] { "User_UserID" });
            DropIndex("QuestionTags", new[] { "Question_QuestionID" });
            DropIndex("QuestionTags", new[] { "Tag_TagID" });
            DropIndex("Favorites", new[] { "User_UserID" });
            DropIndex("Favorites", new[] { "Post_QuestionID" });
            DropIndex("Messages", new[] { "Sender_UserID" });
            DropIndex("Messages", new[] { "Receiver_UserID" });
            DropIndex("Reads", new[] { "User_UserID" });
            DropIndex("Reads", new[] { "Question_QuestionID" });
            CreateTable(
                "UserMetas",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        AantalQuestions = c.Int(nullable: false),
                        AantalBestAnswers = c.Int(nullable: false),
                        TotalVotes = c.Int(nullable: false),
                        AantalAnswers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "Options",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        OptionID = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            AddColumn("Tags", "Name", c => c.String(nullable: false));
            AddColumn("Tags", "UserMeta_UserID", c => c.Int());
            AlterColumn("Answers", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("Answers", "Question_QuestionID", c => c.Int(nullable: false));
            AlterColumn("Users", "UserID", c => c.Int(nullable: false));
            AlterColumn("Badges", "Name", c => c.String(nullable: false));
            AlterColumn("Badges", "Created_At", c => c.String(nullable: false));
            AlterColumn("Badges", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("Comments", "Content", c => c.String(nullable: false));
            AlterColumn("Comments", "Created_At", c => c.String(nullable: false));
            AlterColumn("Comments", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("Questions", "Title", c => c.String(nullable: false, maxLength: 140));
            AlterColumn("Questions", "Content", c => c.String(nullable: false));
            AlterColumn("Questions", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("QuestionTags", "Question_QuestionID", c => c.Int(nullable: false));
            AlterColumn("QuestionTags", "Tag_TagID", c => c.Int(nullable: false));
            AlterColumn("Tags", "Beschrijving", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("Favorites", "Created_At", c => c.String(nullable: false));
            AlterColumn("Favorites", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("Favorites", "Post_QuestionID", c => c.Int(nullable: false));
            AlterColumn("Messages", "Title", c => c.String(nullable: false, maxLength: 140));
            AlterColumn("Messages", "Content", c => c.String(nullable: false));
            AlterColumn("Messages", "Sender_UserID", c => c.Int(nullable: false));
            AlterColumn("Messages", "Receiver_UserID", c => c.Int(nullable: false));
            AlterColumn("Reads", "User_UserID", c => c.Int(nullable: false));
            AlterColumn("Reads", "Question_QuestionID", c => c.Int(nullable: false));
            AddForeignKey("Answers", "User_UserID", "Users", "UserID", cascadeDelete: true);
            AddForeignKey("Answers", "Question_QuestionID", "Questions", "QuestionID", cascadeDelete: true);
            AddForeignKey("Users", "UserID", "UserMetas", "UserID");
            AddForeignKey("Tags", "UserMeta_UserID", "UserMetas", "UserID");
            AddForeignKey("QuestionTags", "Question_QuestionID", "Questions", "QuestionID", cascadeDelete: true);
            AddForeignKey("QuestionTags", "Tag_TagID", "Tags", "TagID", cascadeDelete: true);
            AddForeignKey("Questions", "User_UserID", "Users", "UserID", cascadeDelete: false);
            AddForeignKey("Comments", "User_UserID", "Users", "UserID", cascadeDelete: true);
            AddForeignKey("Badges", "User_UserID", "Users", "UserID", cascadeDelete: true);
            AddForeignKey("Favorites", "User_UserID", "Users", "UserID", cascadeDelete: true);
            AddForeignKey("Favorites", "Post_QuestionID", "Questions", "QuestionID", cascadeDelete: true);
            AddForeignKey("Messages", "Sender_UserID", "Users", "UserID", cascadeDelete: false);
            AddForeignKey("Messages", "Receiver_UserID", "Users", "UserID", cascadeDelete: false);
            AddForeignKey("Reads", "User_UserID", "Users", "UserID", cascadeDelete: true);
            AddForeignKey("Reads", "Question_QuestionID", "Questions", "QuestionID", cascadeDelete: true);
            CreateIndex("Answers", "User_UserID");
            CreateIndex("Answers", "Question_QuestionID");
            CreateIndex("Users", "UserID");
            CreateIndex("Tags", "UserMeta_UserID");
            CreateIndex("QuestionTags", "Question_QuestionID");
            CreateIndex("QuestionTags", "Tag_TagID");
            CreateIndex("Questions", "User_UserID");
            CreateIndex("Comments", "User_UserID");
            CreateIndex("Badges", "User_UserID");
            CreateIndex("Favorites", "User_UserID");
            CreateIndex("Favorites", "Post_QuestionID");
            CreateIndex("Messages", "Sender_UserID");
            CreateIndex("Messages", "Receiver_UserID");
            CreateIndex("Reads", "User_UserID");
            CreateIndex("Reads", "Question_QuestionID");
        }
        
        public override void Down()
        {
            DropIndex("Reads", new[] { "Question_QuestionID" });
            DropIndex("Reads", new[] { "User_UserID" });
            DropIndex("Messages", new[] { "Receiver_UserID" });
            DropIndex("Messages", new[] { "Sender_UserID" });
            DropIndex("Favorites", new[] { "Post_QuestionID" });
            DropIndex("Favorites", new[] { "User_UserID" });
            DropIndex("Badges", new[] { "User_UserID" });
            DropIndex("Comments", new[] { "User_UserID" });
            DropIndex("Questions", new[] { "User_UserID" });
            DropIndex("QuestionTags", new[] { "Tag_TagID" });
            DropIndex("QuestionTags", new[] { "Question_QuestionID" });
            DropIndex("Tags", new[] { "UserMeta_UserID" });
            DropIndex("Users", new[] { "UserID" });
            DropIndex("Answers", new[] { "Question_QuestionID" });
            DropIndex("Answers", new[] { "User_UserID" });
            DropForeignKey("Reads", "Question_QuestionID", "Questions");
            DropForeignKey("Reads", "User_UserID", "Users");
            DropForeignKey("Messages", "Receiver_UserID", "Users");
            DropForeignKey("Messages", "Sender_UserID", "Users");
            DropForeignKey("Favorites", "Post_QuestionID", "Questions");
            DropForeignKey("Favorites", "User_UserID", "Users");
            DropForeignKey("Badges", "User_UserID", "Users");
            DropForeignKey("Comments", "User_UserID", "Users");
            DropForeignKey("Questions", "User_UserID", "Users");
            DropForeignKey("QuestionTags", "Tag_TagID", "Tags");
            DropForeignKey("QuestionTags", "Question_QuestionID", "Questions");
            DropForeignKey("Tags", "UserMeta_UserID", "UserMetas");
            DropForeignKey("Users", "UserID", "UserMetas");
            DropForeignKey("Answers", "Question_QuestionID", "Questions");
            DropForeignKey("Answers", "User_UserID", "Users");
            AlterColumn("Reads", "Question_QuestionID", c => c.Int());
            AlterColumn("Reads", "User_UserID", c => c.Int());
            AlterColumn("Messages", "Receiver_UserID", c => c.Int());
            AlterColumn("Messages", "Sender_UserID", c => c.Int());
            AlterColumn("Messages", "Content", c => c.String());
            AlterColumn("Messages", "Title", c => c.String());
            AlterColumn("Favorites", "Post_QuestionID", c => c.Int());
            AlterColumn("Favorites", "User_UserID", c => c.Int());
            AlterColumn("Favorites", "Created_At", c => c.String());
            AlterColumn("Tags", "Beschrijving", c => c.String());
            AlterColumn("QuestionTags", "Tag_TagID", c => c.Int());
            AlterColumn("QuestionTags", "Question_QuestionID", c => c.Int());
            AlterColumn("Questions", "User_UserID", c => c.Int());
            AlterColumn("Questions", "Content", c => c.String());
            AlterColumn("Questions", "Title", c => c.String());
            AlterColumn("Comments", "User_UserID", c => c.Int());
            AlterColumn("Comments", "Created_At", c => c.String());
            AlterColumn("Comments", "Content", c => c.String());
            AlterColumn("Badges", "User_UserID", c => c.Int());
            AlterColumn("Badges", "Created_At", c => c.String());
            AlterColumn("Badges", "Name", c => c.String());
            AlterColumn("Users", "UserID", c => c.Int(nullable: false, identity: true));
            AlterColumn("Answers", "Question_QuestionID", c => c.Int());
            AlterColumn("Answers", "User_UserID", c => c.Int());
            DropColumn("Tags", "UserMeta_UserID");
            DropColumn("Tags", "Name");
            DropTable("Options");
            DropTable("UserMetas");
            CreateIndex("Reads", "Question_QuestionID");
            CreateIndex("Reads", "User_UserID");
            CreateIndex("Messages", "Receiver_UserID");
            CreateIndex("Messages", "Sender_UserID");
            CreateIndex("Favorites", "Post_QuestionID");
            CreateIndex("Favorites", "User_UserID");
            CreateIndex("QuestionTags", "Tag_TagID");
            CreateIndex("QuestionTags", "Question_QuestionID");
            CreateIndex("Questions", "User_UserID");
            CreateIndex("Comments", "User_UserID");
            CreateIndex("Badges", "User_UserID");
            CreateIndex("Answers", "Question_QuestionID");
            CreateIndex("Answers", "User_UserID");
            AddForeignKey("Reads", "Question_QuestionID", "Questions", "QuestionID");
            AddForeignKey("Reads", "User_UserID", "Users", "UserID");
            AddForeignKey("Messages", "Receiver_UserID", "Users", "UserID");
            AddForeignKey("Messages", "Sender_UserID", "Users", "UserID");
            AddForeignKey("Favorites", "Post_QuestionID", "Questions", "QuestionID");
            AddForeignKey("Favorites", "User_UserID", "Users", "UserID");
            AddForeignKey("QuestionTags", "Tag_TagID", "Tags", "TagID");
            AddForeignKey("QuestionTags", "Question_QuestionID", "Questions", "QuestionID");
            AddForeignKey("Questions", "User_UserID", "Users", "UserID");
            AddForeignKey("Comments", "User_UserID", "Users", "UserID");
            AddForeignKey("Badges", "User_UserID", "Users", "UserID");
            AddForeignKey("Answers", "Question_QuestionID", "Questions", "QuestionID");
            AddForeignKey("Answers", "User_UserID", "Users", "UserID");
        }
    }
}