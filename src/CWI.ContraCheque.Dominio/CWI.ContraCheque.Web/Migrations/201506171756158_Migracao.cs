namespace CWI.ContraCheque.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colaborador_Conta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Competencia = c.DateTime(nullable: false),
                        DataImportacao = c.DateTime(nullable: false),
                        Ocorrencia = c.String(),
                        CodigoConta = c.String(),
                        CodigoColaborador = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colaboradors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoColaborador = c.Long(nullable: false),
                        NomeColaborador = c.String(),
                        TipoSalario = c.String(),
                        CentroCusto = c.String(),
                        DataAdmissao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContaBancoColaboradorPagamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeBanco = c.String(),
                        Agencia = c.String(),
                        Conta = c.String(),
                        IdColaborador = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoConta = c.String(),
                        Descricao = c.String(),
                        Tipo = c.String(),
                        Total = c.Decimal(precision: 18, scale: 2),
                        Base = c.Decimal(precision: 18, scale: 2),
                        ValorReferencia = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CodigoColaborador = c.Long(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Contas");
            DropTable("dbo.ContaBancoColaboradorPagamentoes");
            DropTable("dbo.Colaboradors");
            DropTable("dbo.Colaborador_Conta");
        }
    }
}
