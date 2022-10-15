﻿// <auto-generated />
using System;
using AMP.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AMP.Persistence.Migrations
{
    [DbContext(typeof(AmpDbContext))]
    [Migration("20221013152241_Addinvitations")]
    partial class Addinvitations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AMP.Domain.Entities.Artisans", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(357));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ECCN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)")
                        .HasDefaultValue("Individual");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Artisans", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Customers", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(9548));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Disputes", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 791, DateTimeKind.Utc).AddTicks(5433));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Open");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Disputes", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Images", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 792, DateTimeKind.Utc).AddTicks(7878));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Invitations", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(1963));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("InvitedPhone")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Invitations", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Languages", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(4748));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Languages", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ArtisanId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(8026));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<bool>("IsArtisanComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsComplete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsRequestAccepted")
                        .HasColumnType("bit");

                    b.Property<decimal>("PaymentMade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PreferredCompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PreferredStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferenceNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Maintenance");

                    b.Property<string>("ServiceId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Placed");

                    b.Property<string>("Urgency")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Medium");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Payments", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<decimal>("AmountPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<string>("CustomersId")
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 802, DateTimeKind.Utc).AddTicks(3523));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<bool>("IsForwarded")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TransactionReference")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CustomersId");

                    b.HasIndex("OrderId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Ratings", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ArtisanId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 803, DateTimeKind.Utc).AddTicks(5913));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<int>("Votes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Ratings", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Registrations", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 804, DateTimeKind.Utc).AddTicks(1200));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Registrations", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Requests", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ArtisanId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 805, DateTimeKind.Utc).AddTicks(1088));

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Requests", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Services", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(179));

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Services", (string)null);
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(3346));

                    b.Property<string>("DisplayName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EntityStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasDefaultValue("Normal");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ImageId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSuspended")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LevelOfEducation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MomoNumber")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("OtherName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordKey")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Customer");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ArtisansServices", b =>
                {
                    b.Property<string>("ArtisansId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ServicesId")
                        .HasColumnType("varchar(36)");

                    b.HasKey("ArtisansId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("ArtisansServices");
                });

            modelBuilder.Entity("LanguagesUsers", b =>
                {
                    b.Property<string>("LanguagesId")
                        .HasColumnType("varchar(36)");

                    b.Property<string>("UsersId")
                        .HasColumnType("varchar(36)");

                    b.HasKey("LanguagesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LanguagesUsers");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Artisans", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Users", "User")
                        .WithMany("Artisans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Customers", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Users", "User")
                        .WithMany("Customers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Disputes", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Disputes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithMany("Disputes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Images", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Users", "User")
                        .WithOne("Image")
                        .HasForeignKey("AMP.Domain.Entities.Images", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany("Orders")
                        .HasForeignKey("ArtisanId");

                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Services", "Service")
                        .WithMany("Orders")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("AMP.Domain.ValueObjects.Address", "WorkAddress", b1 =>
                        {
                            b1.Property<string>("OrdersId")
                                .HasColumnType("varchar(36)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("Ghana");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("StreetAddress2")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Town")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrdersId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrdersId");
                        });

                    b.Navigation("Artisan");

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("WorkAddress");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Payments", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Customers", null)
                        .WithMany("Payments")
                        .HasForeignKey("CustomersId");

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithMany("Payments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Ratings", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany("Ratings")
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Ratings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artisan");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Requests", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany("Requests")
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Requests")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithMany("Requests")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artisan");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.OwnsOne("AMP.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<string>("UsersId")
                                .HasColumnType("varchar(36)");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Country")
                                .HasColumnType("int");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasMaxLength(80)
                                .HasColumnType("varchar(80)");

                            b1.Property<string>("StreetAddress2")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Town")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsersId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UsersId");
                        });

                    b.OwnsOne("AMP.Domain.ValueObjects.Contact", "Contact", b1 =>
                        {
                            b1.Property<string>("UsersId")
                                .HasColumnType("varchar(36)");

                            b1.Property<string>("EmailAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PrimaryContact")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("varchar(15)");

                            b1.Property<string>("PrimaryContact2")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PrimaryContact3")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("UsersId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UsersId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("ArtisansServices", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", null)
                        .WithMany()
                        .HasForeignKey("ArtisansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Services", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LanguagesUsers", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Languages", null)
                        .WithMany()
                        .HasForeignKey("LanguagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Users", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AMP.Domain.Entities.Artisans", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Ratings");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Customers", b =>
                {
                    b.Navigation("Disputes");

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("Ratings");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
                    b.Navigation("Disputes");

                    b.Navigation("Payments");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Services", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.Navigation("Artisans");

                    b.Navigation("Customers");

                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
