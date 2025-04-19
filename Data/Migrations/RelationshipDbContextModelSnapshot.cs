﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(RelationshipDbContext))]
    partial class RelationshipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("CharacterOneId")
                        .HasColumnType("int")
                        .HasColumnName("character_one_id");

                    b.Property<int>("CharacterTwoId")
                        .HasColumnType("int")
                        .HasColumnName("character_two_id");

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int")
                        .HasColumnName("connection_type");

                    b.HasKey("Id")
                        .HasName("pk_connection");

                    b.HasIndex("CharacterOneId")
                        .HasDatabaseName("ix_connection_character_one_id");

                    b.HasIndex("CharacterTwoId")
                        .HasDatabaseName("ix_connection_character_two_id");

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
                    b.HasOne("Entities.Character", "CharacterOne")
                        .WithMany("CharacterConnectionsOne")
                        .HasForeignKey("CharacterOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_connection_character_one_id_character");

                    b.HasOne("Entities.Character", "CharacterTwo")
                        .WithMany("CharacterConnectionsTwo")
                        .HasForeignKey("CharacterTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_connection_character_two_id_character");

                    b.Navigation("CharacterOne");

                    b.Navigation("CharacterTwo");
                });

            modelBuilder.Entity("Entities.Character", b =>
                {
                    b.Navigation("CharacterConnectionsOne");

                    b.Navigation("CharacterConnectionsTwo");
                });
#pragma warning restore 612, 618
        }
    }
}
