﻿// <auto-generated />
using System;
using AMP.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Migrations
{
    [DbContext(typeof(AmpDbContext))]
    partial class AmpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AMP.Domain.Entities.Artisans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 491, DateTimeKind.Utc).AddTicks(2124));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Artisans");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Customers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 506, DateTimeKind.Utc).AddTicks(9574));

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Disputes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtisanId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 509, DateTimeKind.Utc).AddTicks(362));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Open");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Disputes");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 514, DateTimeKind.Utc).AddTicks(8378));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PreferredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

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

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Payments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmountPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 599, DateTimeKind.Utc).AddTicks(7365));

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("NotSent");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Proposals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtisanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 606, DateTimeKind.Utc).AddTicks(8015));

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("OrderId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Ratings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtisanId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 609, DateTimeKind.Utc).AddTicks(1862));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<int>("Votes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Schedules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArtisanId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 611, DateTimeKind.Utc).AddTicks(1259));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArtisanId");

                    b.HasIndex("OrderId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Services", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 612, DateTimeKind.Utc).AddTicks(5616));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 6, 27, 23, 0, 53, 615, DateTimeKind.Utc).AddTicks(7454));

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityStatus")
                        .HasColumnType("int");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsSuspended")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LevelOfEducation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MomoNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Customer");

                    b.Property<string>("UserNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArtisansServices", b =>
                {
                    b.Property<int>("ArtisansId")
                        .HasColumnType("int");

                    b.Property<int>("ServicesId")
                        .HasColumnType("int");

                    b.HasKey("ArtisansId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("ArtisansServices");
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
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany("Disputes")
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Disputes")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artisan");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
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
                            b1.Property<int>("OrdersId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("Ghana");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAddress2")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Town")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrdersId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrdersId");
                        });

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("WorkAddress");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Payments", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Customers", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithOne("Payment")
                        .HasForeignKey("AMP.Domain.Entities.Payments", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Proposals", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany("Proposals")
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithMany("Proposals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artisan");

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

            modelBuilder.Entity("AMP.Domain.Entities.Schedules", b =>
                {
                    b.HasOne("AMP.Domain.Entities.Artisans", "Artisan")
                        .WithMany()
                        .HasForeignKey("ArtisanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AMP.Domain.Entities.Orders", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artisan");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.OwnsOne("AMP.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("UsersId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

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
                            b1.Property<int>("UsersId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("EmailAddress")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PrimaryContact")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("AMP.Domain.Entities.Artisans", b =>
                {
                    b.Navigation("Disputes");

                    b.Navigation("Proposals");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Customers", b =>
                {
                    b.Navigation("Disputes");

                    b.Navigation("Orders");

                    b.Navigation("Payments");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Orders", b =>
                {
                    b.Navigation("Payment");

                    b.Navigation("Proposals");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Services", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AMP.Domain.Entities.Users", b =>
                {
                    b.Navigation("Artisans");

                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
