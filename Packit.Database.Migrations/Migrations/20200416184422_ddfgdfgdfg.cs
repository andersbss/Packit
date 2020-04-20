﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Packit.Database.Migrations.Migrations
{
    public partial class ddfgdfgdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "ghhh",
                table: "Items",
                newName: "df");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "df",
                table: "Items",
                newName: "ghhh");

            migrationBuilder.AddForeignKey(
                name: "FK_Backpacks_Users_UserId",
                table: "Backpacks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Backpacks_BackpackId",
                table: "BackpackTrip",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BackpackTrip_Trips_TripId",
                table: "BackpackTrip",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Backpacks_BackpackId",
                table: "ItemBackpack",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBackpack_Items_ItemId",
                table: "ItemBackpack",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedBackpacks_Backpacks_BackpackId",
                table: "SharedBackpacks",
                column: "BackpackId",
                principalTable: "Backpacks",
                principalColumn: "BackpackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}