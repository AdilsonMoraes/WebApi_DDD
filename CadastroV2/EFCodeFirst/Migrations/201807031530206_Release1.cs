namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Release1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funcionario",
                c => new
                    {
                        funcionarioid = c.Int(nullable: false, identity: true),
                        registro = c.String(),
                        nome = c.String(),
                        telefone = c.String(),
                    })
                .PrimaryKey(t => t.funcionarioid);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        usuarioid = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        senha = c.String(),
                        funcionarioid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usuarioid)
                .ForeignKey("dbo.Funcionario", t => t.funcionarioid, cascadeDelete: true)
                .Index(t => t.funcionarioid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "funcionarioid", "dbo.Funcionario");
            DropIndex("dbo.Usuario", new[] { "funcionarioid" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Funcionario");
        }
    }
}
