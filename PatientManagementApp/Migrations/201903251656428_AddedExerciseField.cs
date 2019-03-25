namespace PatientManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExerciseField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "AdditionalInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "AdditionalInformation");
        }
    }
}
