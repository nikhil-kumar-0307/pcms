namespace pcms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptId = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 100),
                        DeptCode = c.String(nullable: false, maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeptId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
