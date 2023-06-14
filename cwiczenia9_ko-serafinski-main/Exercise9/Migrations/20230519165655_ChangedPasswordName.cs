using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise8.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPasswordName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "HashedPassword");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 56, 55, 27, DateTimeKind.Local).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 56, 55, 27, DateTimeKind.Local).AddTicks(9300));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2023, 5, 19, 18, 56, 55, 27, DateTimeKind.Local).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(128), new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(144) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(148), new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(149) });

            migrationBuilder.UpdateData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 3,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(151), new DateTime(2023, 5, 19, 18, 56, 55, 29, DateTimeKind.Local).AddTicks(153) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Users",
                newName: "Password");

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
    }
}
