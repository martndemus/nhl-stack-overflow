// <auto-generated />
namespace NHLStackOverflow.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class Drop_All : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201202192009486_Drop_All"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return "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"; }
        }
    }
}
