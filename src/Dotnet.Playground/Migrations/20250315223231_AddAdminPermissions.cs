﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Dotnet.Playground.Model.Authorization.Permissions;

#nullable disable

namespace Dotnet.PlaygroundMigrations
{
    /// <inheritdoc />
    public partial class AddAdminPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ICollection<Permission>>(
                name: "admin_permissions",
                table: "users",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "admin_permissions",
                table: "users");
        }
    }
}
