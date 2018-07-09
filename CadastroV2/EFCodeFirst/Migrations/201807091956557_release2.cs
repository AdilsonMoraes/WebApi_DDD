namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class release2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionario", "status", c => c.Int(nullable: false));
            AddColumn("dbo.Usuario", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "status");
            DropColumn("dbo.Funcionario", "status");
        }
    }
}
