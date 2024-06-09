using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RentalService.Migrations
{
    /// <inheritdoc />
    public partial class ChangedScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_ClientEntity_ClientId1",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_ScooterEntity_ScooterId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "ClientEntity");

            migrationBuilder.DropTable(
                name: "ScooterEntity");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ClientId1",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_ScooterId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId1",
                table: "Sessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ClientEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    userName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScooterEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScooterEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClientId1",
                table: "Sessions",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ScooterId",
                table: "Sessions",
                column: "ScooterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_ClientEntity_ClientId1",
                table: "Sessions",
                column: "ClientId1",
                principalTable: "ClientEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_ScooterEntity_ScooterId",
                table: "Sessions",
                column: "ScooterId",
                principalTable: "ScooterEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
