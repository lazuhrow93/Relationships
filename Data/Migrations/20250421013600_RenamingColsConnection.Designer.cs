﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(RelationshipDbContext))]
    [Migration("20250421013600_RenamingColsConnection")]
    partial class RenamingColsConnection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_character");

                    b.ToTable("character");
                });

            modelBuilder.Entity("Entities.Connection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int")
                        .HasColumnName("connection_type");

                    b.Property<int>("SourceCharacterId")
                        .HasColumnType("int")
                        .HasColumnName("source_character_id");

                    b.Property<int>("TargetCharacterId")
                        .HasColumnType("int")
                        .HasColumnName("target_character_id");

                    b.HasKey("Id")
                        .HasName("pk_connection");

                    b.HasIndex("SourceCharacterId")
                        .HasDatabaseName("ix_connection_source_character_id");

                    b.HasIndex("TargetCharacterId")
                        .HasDatabaseName("ix_connection_target_character_id");

                    b.ToTable("connection");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Entities.Connection", b =>
                {
                    b.HasOne("Entities.Character", "SourceCharacter")
                        .WithMany("SourceConnections")
                        .HasForeignKey("SourceCharacterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("fk_connection_source_character_id_character");

                    b.HasOne("Entities.Character", "TargetCharacter")
                        .WithMany("TargetConnections")
                        .HasForeignKey("TargetCharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_connection_target_character_id_character");

                    b.Navigation("SourceCharacter");

                    b.Navigation("TargetCharacter");
                });

            modelBuilder.Entity("Entities.Character", b =>
                {
                    b.Navigation("SourceConnections");

                    b.Navigation("TargetConnections");
                });
#pragma warning restore 612, 618
        }
    }
}
