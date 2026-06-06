namespace pcms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(),
                        PasswordHash = c.String(),
                        Role = c.String(nullable: false, maxLength: 20),
                        LastLoginAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LotMaster",
                c => new
                    {
                        LotNo = c.String(nullable: false, maxLength: 50),
                        EquipType = c.String(nullable: false, maxLength: 100),
                        EquipSubType = c.String(maxLength: 100),
                        BrandName = c.String(maxLength: 100),
                        ModelName = c.String(maxLength: 200),
                        Quantity = c.Int(),
                        WarrantyClause = c.String(maxLength: 50),
                        WarrantyStartDate = c.DateTime(),
                        WarrantyEndDate = c.DateTime(),
                        AmcVendor = c.String(maxLength: 200),
                        AmcStartDate = c.DateTime(),
                        AmcEndDate = c.DateTime(),
                        PoVendor = c.String(maxLength: 200),
                        PoNo = c.String(maxLength: 100),
                        PoDate = c.DateTime(),
                        Spec1 = c.String(maxLength: 500),
                        Spec2 = c.String(maxLength: 500),
                        Spec3 = c.String(maxLength: 500),
                        Spec4 = c.String(maxLength: 500),
                        Remarks = c.String(maxLength: 1000),
                        CapitalisationDate = c.DateTime(),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.LotNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LotMaster");
            DropTable("dbo.Employees");
        }
    }
}
