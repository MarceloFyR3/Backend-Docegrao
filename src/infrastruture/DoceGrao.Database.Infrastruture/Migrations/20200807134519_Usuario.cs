using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoceGrao.Database.Infrastructure.Migrations
{
    public partial class Usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "SaltKey",
                table: "Usuario",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IncorrectAttempts",
                table: "Usuario",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "interger",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "SaltKey",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<int>(
                name: "IncorrectAttempts",
                table: "Usuario",
                type: "interger",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
