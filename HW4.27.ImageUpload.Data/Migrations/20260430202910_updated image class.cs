using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW4._27.ImageUpload.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedimageclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedByIds",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "LikedByIds",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
