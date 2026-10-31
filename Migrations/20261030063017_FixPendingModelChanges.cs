using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebsiteApp.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Guests_GuestId1",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeId1",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_GuestId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RoomTypeId1",
                table: "Bookings");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "RoomTypeId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "GuestCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "GuestId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RoomTypeId1",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Bookings",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(254)",
                oldMaxLength: 254);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ConfirmationId",
                table: "Bookings",
                column: "ConfirmationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_ConfirmationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ConfirmationId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Bookings",
                newName: "CreatedAtUtc");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RoomTypes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "Rooms",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Guests",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Guests",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationCode",
                table: "Bookings",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GuestCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GuestId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "Description", "IsActive", "MaxCapacity", "Name", "WeekdayPrice", "WeekendPrice" },
                values: new object[,]
                {
                    { 1, "Comfortable room for everyday stays.", true, 2, "Standard Room", 2500m, 3000m },
                    { 2, "Spacious room with upgraded amenities.", true, 3, "Deluxe Room", 4000m, 4800m },
                    { 3, "Premium suite for a relaxing stay.", true, 4, "Deluxe Suite", 6500m, 7800m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "IsClean", "IsOutOfOrder", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, true, false, "101", 1 },
                    { 2, true, false, "102", 1 },
                    { 3, true, false, "201", 2 },
                    { 4, true, false, "202", 2 },
                    { 5, true, false, "301", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_GuestId1",
                table: "Bookings",
                column: "GuestId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId1",
                table: "Bookings",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomTypeId1",
                table: "Bookings",
                column: "RoomTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Guests_GuestId1",
                table: "Bookings",
                column: "GuestId1",
                principalTable: "Guests",
                principalColumn: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RoomTypes_RoomTypeId1",
                table: "Bookings",
                column: "RoomTypeId1",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId1",
                table: "Bookings",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }
    }
}
