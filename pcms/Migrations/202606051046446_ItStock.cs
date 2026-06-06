namespace pcms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItStock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LotNo = c.String(nullable: false, maxLength: 50),
                        ENo = c.Int(),
                        EmpNo = c.String(maxLength: 20),
                        EmpName = c.String(maxLength: 200),
                        Dept = c.String(maxLength: 20),
                        DeptName = c.String(maxLength: 200),
                        RecEmpNo = c.String(maxLength: 20),
                        RecEmpName = c.String(maxLength: 200),
                        RecDept = c.String(maxLength: 20),
                        RecDeptName = c.String(maxLength: 200),
                        IssueDt = c.DateTime(),
                        TransferInOutDt = c.DateTime(),
                        Grade = c.String(maxLength: 10),
                        CurrLoc = c.String(maxLength: 200),
                        PrevLoc = c.String(maxLength: 200),
                        StType = c.String(maxLength: 10),
                        PoolTag = c.String(maxLength: 1),
                        AssetCode = c.String(maxLength: 100),
                        PvCode = c.String(maxLength: 100),
                        McNum = c.String(maxLength: 100),
                        PTag = c.String(maxLength: 50),
                        IpNo = c.String(maxLength: 100),
                        MacAddress = c.String(maxLength: 50),
                        PhoneNo = c.String(maxLength: 20),
                        PhyVerifierCode = c.String(maxLength: 20),
                        Remarks = c.String(maxLength: 1000),
                        CreatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LotMaster", t => t.LotNo, cascadeDelete: true)
                .Index(t => t.LotNo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItStock", "LotNo", "dbo.LotMaster");
            DropIndex("dbo.ItStock", new[] { "LotNo" });
            DropTable("dbo.ItStock");
        }
    }
}
