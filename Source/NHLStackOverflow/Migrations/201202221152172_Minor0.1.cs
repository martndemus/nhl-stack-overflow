namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Minor01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("Tags", "Description", c => c.String(nullable: false, maxLength: 500));
            DropColumn("Tags", "Beschrijving");
        }
        
        public override void Down()
        {
            AddColumn("Tags", "Beschrijving", c => c.String(nullable: false, maxLength: 500));
            DropColumn("Tags", "Description");
        }
    }
}
