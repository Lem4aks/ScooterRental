using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScooterService.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Scooters",
                columns: new[] { "Id", "Model", "SessionIds", "Status" },
                values: new object[,]
                {
                    { new Guid("145e70f3-8a2d-4abe-a762-0aa84724e82d"), "UrbanGlide", new List<Guid>(), true },
                    { new Guid("1c9d8f60-4bdb-4255-837d-b97e38c0899f"), "XR-1000", new List<Guid>(), true },
                    { new Guid("3bd6bdf5-98cc-4853-ba91-3bcf09023603"), "XR-1000", new List<Guid>(), false },
                    { new Guid("421546c9-25bd-4806-a0cc-27f723e52a17"), "SpeedMaster", new List<Guid>(), true },
                    { new Guid("4edb2562-cde9-43ee-a527-4cf3802c26d0"), "UrbanGlide", new List<Guid>(), false },
                    { new Guid("6052fd98-d98b-4a2f-9bc8-b90b1e134603"), "UrbanGlide", new List<Guid>(), true },
                    { new Guid("94db0ba1-6414-427d-9595-14a1d99a966a"), "TurboScoot", new List<Guid>(), true },
                    { new Guid("a1c96566-82d5-41ee-a407-db13b6b03dc2"), "SpeedMaster", new List<Guid>(), false },
                    { new Guid("be003688-c1ca-4eb0-a22e-542f445df9c2"), "UrbanGlide", new List<Guid>(), true },
                    { new Guid("c9581c77-8f22-4388-ba55-12a087a3ec09"), "TurboScoot", new List<Guid>(), false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("145e70f3-8a2d-4abe-a762-0aa84724e82d"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("1c9d8f60-4bdb-4255-837d-b97e38c0899f"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("3bd6bdf5-98cc-4853-ba91-3bcf09023603"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("421546c9-25bd-4806-a0cc-27f723e52a17"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("4edb2562-cde9-43ee-a527-4cf3802c26d0"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("6052fd98-d98b-4a2f-9bc8-b90b1e134603"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("94db0ba1-6414-427d-9595-14a1d99a966a"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("a1c96566-82d5-41ee-a407-db13b6b03dc2"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("be003688-c1ca-4eb0-a22e-542f445df9c2"));

            migrationBuilder.DeleteData(
                table: "Scooters",
                keyColumn: "Id",
                keyValue: new Guid("c9581c77-8f22-4388-ba55-12a087a3ec09"));
        }
    }
}
