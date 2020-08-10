using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoceGrao.Database.Infrastructure.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGrupo = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: true),
                    Login = table.Column<string>(type: "varchar(30)", unicode: false, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", nullable: true),
                    SaltKey = table.Column<byte[]>(nullable: true),
                    CPFCNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    DocumentoDateIssue = table.Column<DateTime>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    Block = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    DateBlock = table.Column<DateTime>(type: "datetime", nullable: true),
                    IncorrectAttempts = table.Column<int>(type: "int", nullable: true),
                    DateRegister = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    UrlImg = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
