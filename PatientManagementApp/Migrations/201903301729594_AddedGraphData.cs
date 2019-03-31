namespace PatientManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGraphData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GraphDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatinetId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        xValue = c.DateTime(nullable: false),
                        yValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CorrectMeasure = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GraphDatas");
        }
    }
}
