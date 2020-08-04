using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Service.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_MakeId",
                table: "VehicleModels");

            migrationBuilder.AlterColumn<int>(
                name: "MakeId",
                table: "VehicleModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_MakeId",
                table: "VehicleModels",
                column: "MakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleMakes_MakeId",
                table: "VehicleModels");

            migrationBuilder.AlterColumn<int>(
                name: "MakeId",
                table: "VehicleModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleMakes_MakeId",
                table: "VehicleModels",
                column: "MakeId",
                principalTable: "VehicleMakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
