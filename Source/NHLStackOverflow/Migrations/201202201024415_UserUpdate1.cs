namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "LastOnline", c => c.String());
            AlterColumn("Users", "UserName", c => c.String(nullable: false));
            AlterColumn("Users", "Password", c => c.String(nullable: false));
            AlterColumn("Users", "Email", c => c.String(nullable: false));
            DropColumn("Users", "LastEdited");
        }
        
        public override void Down()
        {
            AddColumn("Users", "LastEdited", c => c.String());
            AlterColumn("Users", "Email", c => c.String());
            AlterColumn("Users", "Password", c => c.String());
            AlterColumn("Users", "UserName", c => c.String());
            DropColumn("Users", "LastOnline");
        }
    }
}
