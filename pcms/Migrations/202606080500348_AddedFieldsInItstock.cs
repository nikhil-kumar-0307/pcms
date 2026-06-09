namespace pcms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldsInItstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItStock", "SiNum", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItStock", "SiNum");
        }
    }
}
