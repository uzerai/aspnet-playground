﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet.PlaygroundMigrations
{
    /// <inheritdoc />
    public partial class AlterAreaModelMaintainerOrganizationOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_areas_organizations_maintainer_organization_id",
                table: "areas");

            migrationBuilder.AlterColumn<Guid>(
                name: "maintainer_organization_id",
                table: "areas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_areas_organizations_maintainer_organization_id",
                table: "areas",
                column: "maintainer_organization_id",
                principalTable: "organizations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_areas_organizations_maintainer_organization_id",
                table: "areas");

            migrationBuilder.AlterColumn<Guid>(
                name: "maintainer_organization_id",
                table: "areas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_areas_organizations_maintainer_organization_id",
                table: "areas",
                column: "maintainer_organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
