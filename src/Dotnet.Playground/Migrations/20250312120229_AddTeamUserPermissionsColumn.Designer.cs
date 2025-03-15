﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Dotnet.Playground.DI.Data;
using Dotnet.Playground.Model.Authorization.Permissions;

#nullable disable

namespace Dotnet.Playground.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250312120229_AddTeamUserPermissionsColumn")]
    partial class AddTeamUserPermissionsColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dotnet.Playground.Model.Authentication.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Auth0UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auth0_user_id");

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Instant?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<Instant>("LastLogin")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login");

                    b.Property<Instant>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Auth0UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_users_auth0_user_id");

                    b.HasIndex("Email")
                        .HasDatabaseName("ix_users_email");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Authorization.Permissions.OrganizationPermission", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<int>("Permission")
                        .HasColumnType("integer")
                        .HasColumnName("permission");

                    b.HasKey("OrganizationId", "UserId", "Permission")
                        .HasName("pk_organization_permissions");

                    b.ToTable("organization_permissions", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Instant?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Instant>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_organizations");

                    b.ToTable("organizations", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Instant?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<Instant>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_organization_teams");

                    b.HasIndex("OrganizationId")
                        .HasDatabaseName("ix_organization_teams_organization_id");

                    b.ToTable("organization_teams", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationTeamUser", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<Guid>("OrganizationTeamId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_team_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<ICollection<Permission>>("Permissions")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("permissions");

                    b.HasKey("OrganizationId", "OrganizationTeamId", "UserId")
                        .HasName("pk_organization_team_users");

                    b.HasIndex("OrganizationTeamId")
                        .HasDatabaseName("ix_organization_team_users_organization_team_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_organization_team_users_user_id");

                    b.HasIndex("OrganizationId", "UserId")
                        .HasDatabaseName("ix_organization_team_users_organization_id_user_id");

                    b.ToTable("organization_team_users", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationUser", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("OrganizationId", "UserId")
                        .HasName("pk_organization_users");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_organization_users_user_id");

                    b.ToTable("organization_users", (string)null);
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Authorization.Permissions.OrganizationPermission", b =>
                {
                    b.HasOne("Dotnet.Playground.Model.Organizations.OrganizationUser", "OrganizationUser")
                        .WithMany("Permissions")
                        .HasForeignKey("OrganizationId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_permissions_organization_users_organization_id");

                    b.Navigation("OrganizationUser");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationTeam", b =>
                {
                    b.HasOne("Dotnet.Playground.Model.Organizations.Organization", "Organization")
                        .WithMany("Teams")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_teams_organizations_organization_id");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationTeamUser", b =>
                {
                    b.HasOne("Dotnet.Playground.Model.Organizations.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_team_users_organizations_organization_id");

                    b.HasOne("Dotnet.Playground.Model.Organizations.OrganizationTeam", "OrganizationTeam")
                        .WithMany("OrganizationTeamUsers")
                        .HasForeignKey("OrganizationTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_team_users_organization_teams_organization_tea");

                    b.HasOne("Dotnet.Playground.Model.Authentication.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_team_users_users_user_id");

                    b.HasOne("Dotnet.Playground.Model.Organizations.OrganizationUser", "OrganizationUser")
                        .WithMany("OrganizationTeamUsers")
                        .HasForeignKey("OrganizationId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_team_users_organization_users_organization_id_");

                    b.Navigation("Organization");

                    b.Navigation("OrganizationTeam");

                    b.Navigation("OrganizationUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationUser", b =>
                {
                    b.HasOne("Dotnet.Playground.Model.Organizations.Organization", "Organization")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_users_organizations_organization_id");

                    b.HasOne("Dotnet.Playground.Model.Authentication.User", "User")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_users_users_user_id");

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Authentication.User", b =>
                {
                    b.Navigation("OrganizationUsers");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.Organization", b =>
                {
                    b.Navigation("OrganizationUsers");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationTeam", b =>
                {
                    b.Navigation("OrganizationTeamUsers");
                });

            modelBuilder.Entity("Dotnet.Playground.Model.Organizations.OrganizationUser", b =>
                {
                    b.Navigation("OrganizationTeamUsers");

                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
