namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Users", "Password", c => c.String(nullable: false, maxLength: 28));
        }
        
        public override void Down()
        {
            AlterColumn("Users", "Password", c => c.String(nullable: false));
        }
    }
}
