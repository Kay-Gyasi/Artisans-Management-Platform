-- create procedure [dbo].[GetUserForAuthorizationByPhone]
-- @phone nvarchar(15)
-- as
-- Begin
--     set nocount on;
--     select [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, [Images].ImageUrl,
--            [Type], Contact_EmailAddress, Address_StreetAddress
--     from dbo.[Users] LEFT JOIN Images on Users.ImageId = Images.Id
--     where Contact_PrimaryContact = @phone and IsVerified = 1 and Users.EntityStatus = 'Normal' and IsSuspended = 0
-- end


-- create procedure [dbo].[GetUserInfoForRefreshToken]
-- @id nvarchar(40)
-- as
-- Begin
--     set nocount on;
--     select [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, [Images].ImageUrl,
--            [Type], Contact_EmailAddress, Address_StreetAddress
--     from dbo.[Users] LEFT JOIN Images on Users.ImageId = Images.Id
--     where Users.Id = @id and IsVerified = 1 and Users.EntityStatus = 'Normal' and IsSuspended = 0
-- end

