using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise8.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicament_Prescription_IdPrescription",
                table: "PrescriptionMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient",
                table: "Patient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "PrescriptionMedicament",
                newName: "PrescriptionMedicaments");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "Patient",
                newName: "Patients");

            migrationBuilder.RenameTable(
                name: "Medicament",
                newName: "Medicaments");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionMedicament_IdPrescription",
                table: "PrescriptionMedicaments",
                newName: "IX_PrescriptionMedicaments_IdPrescription");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IdPatient",
                table: "Prescriptions",
                newName: "IX_Prescriptions_IdPatient");

            migrationBuilder.RenameIndex(
                name: "IX_Prescription_IdDoctor",
                table: "Prescriptions",
                newName: "IX_Prescriptions_IdDoctor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicaments",
                table: "PrescriptionMedicaments",
                columns: new[] { "IdMedicament", "IdPrescription" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicaments",
                table: "Medicaments",
                column: "IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "IdDoctor");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicaments_Medicaments_IdMedicament",
                table: "PrescriptionMedicaments",
                column: "IdMedicament",
                principalTable: "Medicaments",
                principalColumn: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicaments_Prescriptions_IdPrescription",
                table: "PrescriptionMedicaments",
                column: "IdPrescription",
                principalTable: "Prescriptions",
                principalColumn: "IdPrescription");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_IdDoctor",
                table: "Prescriptions",
                column: "IdDoctor",
                principalTable: "Doctors",
                principalColumn: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions",
                column: "IdPatient",
                principalTable: "Patients",
                principalColumn: "IdPatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicaments_Medicaments_IdMedicament",
                table: "PrescriptionMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicaments_Prescriptions_IdPrescription",
                table: "PrescriptionMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_IdDoctor",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_IdPatient",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicaments",
                table: "PrescriptionMedicaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicaments",
                table: "Medicaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameTable(
                name: "PrescriptionMedicaments",
                newName: "PrescriptionMedicament");

            migrationBuilder.RenameTable(
                name: "Patients",
                newName: "Patient");

            migrationBuilder.RenameTable(
                name: "Medicaments",
                newName: "Medicament");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_IdPatient",
                table: "Prescription",
                newName: "IX_Prescription_IdPatient");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_IdDoctor",
                table: "Prescription",
                newName: "IX_Prescription_IdDoctor");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionMedicaments_IdPrescription",
                table: "PrescriptionMedicament",
                newName: "IX_PrescriptionMedicament_IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "IdPrescription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicament",
                table: "PrescriptionMedicament",
                columns: new[] { "IdMedicament", "IdPrescription" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient",
                table: "Patient",
                column: "IdPatient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicament",
                table: "Medicament",
                column: "IdMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "IdDoctor");

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1079));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1114));

            migrationBuilder.UpdateData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2023, 5, 13, 20, 56, 47, 86, DateTimeKind.Local).AddTicks(1117));

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 1,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3344), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3356) });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 2,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3360), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3362) });

            migrationBuilder.UpdateData(
                table: "Prescription",
                keyColumn: "IdPrescription",
                keyValue: 3,
                columns: new[] { "Date", "DueDate" },
                values: new object[] { new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3364), new DateTime(2023, 5, 13, 20, 56, 47, 87, DateTimeKind.Local).AddTicks(3365) });

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Doctor_IdDoctor",
                table: "Prescription",
                column: "IdDoctor",
                principalTable: "Doctor",
                principalColumn: "IdDoctor");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescription_Patient_IdPatient",
                table: "Prescription",
                column: "IdPatient",
                principalTable: "Patient",
                principalColumn: "IdPatient");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Medicament_IdMedicament",
                table: "PrescriptionMedicament",
                column: "IdMedicament",
                principalTable: "Medicament",
                principalColumn: "IdMedicament");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicament_Prescription_IdPrescription",
                table: "PrescriptionMedicament",
                column: "IdPrescription",
                principalTable: "Prescription",
                principalColumn: "IdPrescription");
        }
    }
}
