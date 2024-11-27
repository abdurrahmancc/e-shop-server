using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_shop_server.Migrations
{
    /// <inheritdoc />
    public partial class initalupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    _id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    RegularPrice = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ReviewQuantity = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    SubCategory = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Review = table.Column<string>(type: "text", nullable: false),
                    Img = table.Column<List<string>>(type: "text[]", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Badge = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    ShortFeatures = table.Column<List<string>>(type: "text[]", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x._id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
