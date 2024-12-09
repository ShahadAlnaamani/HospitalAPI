using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalAPI.Migrations
{
    /// <inheritdoc />
    public partial class EditedBookingCOlumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clinics_ClinicCID",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClinicCID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClinicCID",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                columns: new[] { "Date", "SlotNumber", "CID" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CID",
                table: "Bookings",
                column: "CID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clinics_CID",
                table: "Bookings",
                column: "CID",
                principalTable: "Clinics",
                principalColumn: "CID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Clinics_CID",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_CID",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ClinicCID",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                columns: new[] { "Date", "SlotNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClinicCID",
                table: "Bookings",
                column: "ClinicCID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Clinics_ClinicCID",
                table: "Bookings",
                column: "ClinicCID",
                principalTable: "Clinics",
                principalColumn: "CID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
