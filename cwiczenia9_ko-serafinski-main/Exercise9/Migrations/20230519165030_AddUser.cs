using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise8.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 50, 30, 211, DateTimeKind.Local).AddTicks(9703));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 50, 30, 211, DateTimeKind.Local).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 50, 30, 211, DateTimeKind.Local).AddTicks(9746));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(443), new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(456) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(460), new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(462) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 3,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(464), new DateTime(2023, 5, 19, 18, 50, 30, 213, DateTimeKind.Local).AddTicks(465) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 22, 7, 27, 512, DateTimeKind.Local).AddTicks(6799));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 22, 7, 27, 512, DateTimeKind.Local).AddTicks(6842));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 22, 7, 27, 512, DateTimeKind.Local).AddTicks(6844));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7792), new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7804) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7808), new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7809) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 3,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7811), new DateTime(2023, 5, 13, 22, 7, 27, 513, DateTimeKind.Local).AddTicks(7812) });
        }
    }
}
