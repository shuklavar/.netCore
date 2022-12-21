using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCDHProject.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Custid", "Balance", "City", "Name", "Status" },
                values: new object[,]
                {
                    { 101, 50000.00m, "Delhi", "Sai", true },
                    { 102, 50000.00m, "Mumbai", "Pratik", true },
                    { 103, 50000.00m, "Bangalore", "Mohan", true },
                    { 104, 50000.00m, "Kolkatta", "Mrunal", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Custid",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Custid",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Custid",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Custid",
                keyValue: 104);
        }
    }
}
