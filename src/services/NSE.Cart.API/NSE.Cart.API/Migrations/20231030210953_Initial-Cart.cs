﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSE.Cart.API.Migrations
{
    public partial class InitialCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCart_CustomerCart_CartId",
                        column: x => x.CartId,
                        principalTable: "CustomerCart",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Cliente",
                table: "CustomerCart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCart_CartId",
                table: "ItemsCart",
                column: "CartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsCart");

            migrationBuilder.DropTable(
                name: "CustomerCart");
        }
    }
}
