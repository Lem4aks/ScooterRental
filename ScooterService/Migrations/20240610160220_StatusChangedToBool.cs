using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScooterService.Migrations
{
    /// <inheritdoc />
    public partial class StatusChangedToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<bool>(
                name: "StatusTemp",
                table: "Scooters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            
            migrationBuilder.Sql("UPDATE \"Scooters\" SET \"StatusTemp\" = (CASE WHEN \"Status\" = 'available' THEN TRUE ELSE FALSE END)");

            
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Scooters");

           
            migrationBuilder.RenameColumn(
                name: "StatusTemp",
                table: "Scooters",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "StatusTemp",
                table: "Scooters",
                type: "text",
                nullable: false,
                defaultValue: "not available");

            
            migrationBuilder.Sql("UPDATE \"Scooters\" SET \"StatusTemp\" = (CASE WHEN \"Status\" = TRUE THEN 'available' ELSE 'not available' END)");

            
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Scooters");

            
            migrationBuilder.RenameColumn(
                name: "StatusTemp",
                table: "Scooters",
                newName: "Status");
        }
    }
}
