using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitedPhone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 926, DateTimeKind.Utc).AddTicks(9582)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 929, DateTimeKind.Utc).AddTicks(4329)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 10, DateTimeKind.Utc).AddTicks(8410)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 21, DateTimeKind.Utc).AddTicks(8977)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ImageId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    FamilyName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OtherName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    MomoNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "Customer"),
                    LevelOfEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContactEmailAddress = table.Column<string>(name: "Contact_EmailAddress", type: "nvarchar(max)", nullable: true),
                    ContactPrimaryContact = table.Column<string>(name: "Contact_PrimaryContact", type: "varchar(15)", maxLength: 15, nullable: true),
                    ContactPrimaryContact2 = table.Column<string>(name: "Contact_PrimaryContact2", type: "nvarchar(max)", nullable: true),
                    ContactPrimaryContact3 = table.Column<string>(name: "Contact_PrimaryContact3", type: "nvarchar(max)", nullable: true),
                    AddressCountry = table.Column<int>(name: "Address_Country", type: "int", nullable: true),
                    AddressCity = table.Column<string>(name: "Address_City", type: "nvarchar(max)", nullable: true),
                    AddressTown = table.Column<string>(name: "Address_Town", type: "nvarchar(max)", nullable: true),
                    AddressStreetAddress = table.Column<string>(name: "Address_StreetAddress", type: "varchar(80)", maxLength: 80, nullable: true),
                    AddressStreetAddress2 = table.Column<string>(name: "Address_StreetAddress2", type: "nvarchar(max)", nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 25, DateTimeKind.Utc).AddTicks(9187)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Artisans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    BusinessName = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false),
                    Type = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false, defaultValue: "Individual"),
                    ECCN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 914, DateTimeKind.Utc).AddTicks(9497)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artisans", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Artisans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    InviterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    InviteeId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 943, DateTimeKind.Utc).AddTicks(1668)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectRequests", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ConnectRequests_Users_InviteeId",
                        column: x => x.InviteeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectRequests_Users_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    FirstParticipantId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    SecondParticipantId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 953, DateTimeKind.Utc).AddTicks(2373)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_FirstParticipantId",
                        column: x => x.FirstParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_SecondParticipantId",
                        column: x => x.SecondParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 918, DateTimeKind.Utc).AddTicks(9895)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    PublicId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 925, DateTimeKind.Utc).AddTicks(136)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LanguagesUsers",
                columns: table => new
                {
                    LanguagesId = table.Column<string>(type: "varchar(36)", nullable: false),
                    UsersId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagesUsers", x => new { x.LanguagesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LanguagesUsers_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagesUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 983, DateTimeKind.Utc).AddTicks(1702)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtisansServices",
                columns: table => new
                {
                    ArtisansId = table.Column<string>(type: "varchar(36)", nullable: false),
                    ServicesId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtisansServices", x => new { x.ArtisansId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ArtisansServices_Artisans_ArtisansId",
                        column: x => x.ArtisansId,
                        principalTable: "Artisans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtisansServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    SenderId = table.Column<string>(type: "varchar(36)", nullable: true),
                    ReceiverId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConversationId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 932, DateTimeKind.Utc).AddTicks(188)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ReferenceNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ArtisanId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsArtisanComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsRequestAccepted = table.Column<bool>(type: "bit", nullable: false),
                    ServiceId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Urgency = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Medium"),
                    Scope = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Maintenance"),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "Placed"),
                    PreferredStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredCompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkAddressCountry = table.Column<string>(name: "WorkAddress_Country", type: "nvarchar(max)", nullable: true, defaultValue: "Ghana"),
                    WorkAddressCity = table.Column<string>(name: "WorkAddress_City", type: "nvarchar(max)", nullable: true),
                    WorkAddressTown = table.Column<string>(name: "WorkAddress_Town", type: "nvarchar(max)", nullable: true),
                    WorkAddressStreetAddress = table.Column<string>(name: "WorkAddress_StreetAddress", type: "varchar(100)", maxLength: 100, nullable: true),
                    WorkAddressStreetAddress2 = table.Column<string>(name: "WorkAddress_StreetAddress2", type: "nvarchar(max)", nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 987, DateTimeKind.Utc).AddTicks(3397)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Orders_Artisans_ArtisanId",
                        column: x => x.ArtisanId,
                        principalTable: "Artisans",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ArtisanId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 8, DateTimeKind.Utc).AddTicks(5995)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Ratings_Artisans_ArtisanId",
                        column: x => x.ArtisanId,
                        principalTable: "Artisans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Disputes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OrderId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Details = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "Open"),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 46, 922, DateTimeKind.Utc).AddTicks(320)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disputes", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Disputes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disputes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OrderId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsForwarded = table.Column<bool>(type: "bit", nullable: false),
                    TransactionReference = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 5, DateTimeKind.Utc).AddTicks(1615)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ArtisanId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OrderId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 1, 6, 19, 39, 47, 15, DateTimeKind.Utc).AddTicks(2107)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Requests_Artisans_ArtisanId",
                        column: x => x.ArtisanId,
                        principalTable: "Artisans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Requests_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "Index_Artisan_BusinessName",
                table: "Artisans",
                column: "BusinessName");

            migrationBuilder.CreateIndex(
                name: "Index_Artisan_Type",
                table: "Artisans",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Artisans_RowId",
                table: "Artisans",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Artisans_UserId",
                table: "Artisans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtisansServices_ServicesId",
                table: "ArtisansServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RowId",
                table: "ChatMessages",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectRequests_InviteeId",
                table: "ConnectRequests",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectRequests_InviterId",
                table: "ConnectRequests",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectRequests_RowId",
                table: "ConnectRequests",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_FirstParticipantId",
                table: "Conversations",
                column: "FirstParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RowId",
                table: "Conversations",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_SecondParticipantId",
                table: "Conversations",
                column: "SecondParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RowId",
                table: "Customers",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_CustomerId",
                table: "Disputes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_OrderId",
                table: "Disputes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_RowId",
                table: "Disputes",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Status",
                table: "Disputes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RowId",
                table: "Images",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Index_Phone",
                table: "Invitations",
                column: "InvitedPhone");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_RowId",
                table: "Invitations",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "Index_Lang_Name",
                table: "Languages",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_RowId",
                table: "Languages",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesUsers_UsersId",
                table: "LanguagesUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RowId",
                table: "Notifications",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Id_IsArtisanComplete",
                table: "Orders",
                columns: new[] { "Id", "IsArtisanComplete" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ArtisanId",
                table: "Orders",
                column: "ArtisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReferenceNo",
                table: "Orders",
                column: "ReferenceNo");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RowId",
                table: "Orders",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_CusUserId",
                table: "Orders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_ArtisanUserId",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Status_RequestAc_UserId",
                table: "Orders",
                columns: new[] { "Status", "IsRequestAccepted" });

            migrationBuilder.CreateIndex(
                name: "IX_Artisan_Verified",
                table: "Payments",
                column: "IsVerified");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Verified",
                table: "Payments",
                columns: new[] { "OrderId", "IsVerified" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RowId",
                table: "Payments",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Reference",
                table: "Payments",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_TrxRef",
                table: "Payments",
                column: "TransactionReference");

            migrationBuilder.CreateIndex(
                name: "IX_ArtisanId",
                table: "Ratings",
                column: "ArtisanId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtisanId_CustomerId",
                table: "Ratings",
                columns: new[] { "ArtisanId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerId",
                table: "Ratings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RowId",
                table: "Ratings",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Code",
                table: "Registrations",
                column: "VerificationCode");

            migrationBuilder.CreateIndex(
                name: "IX_Code_Phone",
                table: "Registrations",
                columns: new[] { "Phone", "VerificationCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_Phone",
                table: "Registrations",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_RowId",
                table: "Registrations",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ArtisanId",
                table: "Requests",
                column: "ArtisanId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CustomerId",
                table: "Requests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_OrderId",
                table: "Requests",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RowId",
                table: "Requests",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Desc",
                table: "Services",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Name",
                table: "Services",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Services_RowId",
                table: "Services",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Type",
                table: "Users",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RowId",
                table: "Users",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtisansServices");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ConnectRequests");

            migrationBuilder.DropTable(
                name: "Disputes");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "LanguagesUsers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Artisans");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
