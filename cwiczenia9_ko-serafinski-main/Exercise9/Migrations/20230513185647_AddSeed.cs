using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exercise8.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "jan@kowalski.com", "Jan", "Kowalski" },
                    { 2, "adam@nowak.com", "Adam", "Nowak" },
                    { 3, "tomasz@serafinski.com", "Tomasz", "Serafinski" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[] { 1, "Lek na ból", "Ibuprofen", "Przeciwbólowy" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1079), "Janusz", "Januszewski" },
                    { 2, new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1114), "Alina", "Alinowska" },
                    { 3, new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1117), "Tomasz", "Tomaszewski" }
                });

            migrationBuilder.InsertData(
                table: "Prescription",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3344), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3356), 1, 1 },
                    { 2, new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3360), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3362), 2, 2 },
                    { 3, new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3364), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3365), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicament",
                columns: new[] { "IdMedicament", "IdPrescription", "Details", "Dose" },
                values: new object[,]
                {
                    { 1, 1, "2x dziennie", 200 },
                    { 1, 2, "Brać doraźnie", null },
                    { 1, 3, "Max. 4 razy dziennie!", 400 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PrescriptionMedicament",
                keyColumns: new[] { "IdMedicament", "IdPrescription" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3);
        }
    }
}
