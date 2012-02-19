namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Drop_All : DbMigration
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
        }
        
        public override void Down()
        {
        }
    }
}
