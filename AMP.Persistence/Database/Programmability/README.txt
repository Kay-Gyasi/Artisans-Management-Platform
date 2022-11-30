** HOW TO RUN PROGRAMMABILITY SCRIPTS **
1. Add scripts in migrations
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