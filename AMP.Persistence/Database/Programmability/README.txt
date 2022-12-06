** HOW TO RUN PROGRAMMABILITY SCRIPTS **
1. Add scripts in migrations

    ** STORED PROCEDURES **
    var getUserForAuthorizationByPhoneSP = @"CREATE PROCEDURE [dbo].[GetUserForAuthorizationByPhone]
        @phone nvarchar(15)
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, [Images].ImageUrl,
                   [Type], Contact_EmailAddress, Address_StreetAddress
            FROM dbo.[Users] LEFT JOIN Images ON Users.ImageId = Images.Id
            WHERE Contact_PrimaryContact = @phone AND IsVerified = 1 AND Users.EntityStatus = 'Normal' AND IsSuspended = 0
        END";
    migrationBuilder.Sql(getUserForAuthorizationByPhoneSP);
    
    var getUserInfoForRefreshTokenSP = @"CREATE PROCEDURE [dbo].[GetUserInfoForRefreshToken]
        @id nvarchar(40)
        AS
        BEGIN
            SET NOCOUNT ON;
            SELECT [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, [Images].ImageUrl,
                   [Type], Contact_EmailAddress, Address_StreetAddress
            FROM dbo.[Users] LEFT JOIN Images ON Users.ImageId = Images.Id
            WHERE Users.Id = @id AND IsVerified = 1 AND Users.EntityStatus = 'Normal' AND IsSuspended = 0
        END";
    migrationBuilder.Sql(getUserInfoForRefreshTokenSP);
    
    var primaryContactIXOnUsers = @"CREATE INDEX IX_PrimaryContact ON Users (Contact_PrimaryContact)";
    migrationBuilder.Sql(primaryContactIXOnUsers);
    
    ** TRIGGERS **
    var tables = new List<string>()
    {
        "Artisans",
        "Customers",
        "Disputes",
        "Images",
        "Invitations",
        "Languages",
        "Orders",
        "Payments",
        "Ratings",
        "Registrations",
        "Requests",
        "Services",
        "Users"
    };

    foreach (var table in tables)
    {
        var builder = new StringBuilder();
        builder.Append($"CREATE TRIGGER updateLastModified{table} {Environment.NewLine}");
        builder.Append($"ON [dbo].[{table}] {Environment.NewLine}");
        builder.Append($"AFTER INSERT, UPDATE {Environment.NewLine} AS {Environment.NewLine}");
        builder.Append($"BEGIN {Environment.NewLine} DECLARE @rowId AS int; {Environment.NewLine}");
        builder.Append($"BEGIN {Environment.NewLine}");
        builder.Append($"IF EXISTS (SELECT 0 FROM DELETED) {Environment.NewLine}");
        builder.Append($"BEGIN SELECT @rowId = RowId from DELETED {Environment.NewLine}");
        builder.Append($"UPDATE [dbo].[Artisans] {Environment.NewLine}");
        builder.Append($"SET DateModified = GETDATE() {Environment.NewLine} WHERE RowId = @rowId; END ELSE {Environment.NewLine}");
        builder.Append($"BEGIN {Environment.NewLine} SELECT @rowId = RowId from INSERTED; {Environment.NewLine}");
        builder.Append($"UPDATE [dbo].[Artisans] {Environment.NewLine}");
        builder.Append($"SET DateModified = GETDATE() {Environment.NewLine} WHERE RowId = @rowId; {Environment.NewLine} END {Environment.NewLine}");
        builder.Append($"END {Environment.NewLine} END");
        migrationBuilder.Sql(builder.ToString());
    }