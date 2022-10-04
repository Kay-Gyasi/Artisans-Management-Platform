IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Languages] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Languages] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Services] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Services] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [ImageId] int NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [FamilyName] nvarchar(max) NOT NULL,
        [OtherName] nvarchar(max) NULL,
        [DisplayName] nvarchar(max) NULL,
        [MomoNumber] nvarchar(max) NULL,
        [IsSuspended] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsRemoved] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Type] nvarchar(max) NOT NULL DEFAULT N'Customer',
        [LevelOfEducation] nvarchar(max) NOT NULL,
        [Password] varbinary(max) NULL,
        [PasswordKey] varbinary(max) NULL,
        [Contact_EmailAddress] nvarchar(max) NULL,
        [Contact_PrimaryContact] nvarchar(max) NULL,
        [Contact_PrimaryContact2] nvarchar(max) NULL,
        [Contact_PrimaryContact3] nvarchar(max) NULL,
        [Address_Country] int NULL,
        [Address_City] nvarchar(max) NULL,
        [Address_Town] nvarchar(max) NULL,
        [Address_StreetAddress] nvarchar(max) NULL,
        [Address_StreetAddress2] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Artisans] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [BusinessName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsVerified] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsApproved] bit NOT NULL DEFAULT CAST(0 AS bit),
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Artisans] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Artisans_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Customers] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Customers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Images] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NULL,
        [PublicId] nvarchar(max) NOT NULL,
        [ImageUrl] nvarchar(max) NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Images_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [LanguagesUsers] (
        [LanguagesId] int NOT NULL,
        [UsersId] int NOT NULL,
        CONSTRAINT [PK_LanguagesUsers] PRIMARY KEY ([LanguagesId], [UsersId]),
        CONSTRAINT [FK_LanguagesUsers_Languages_LanguagesId] FOREIGN KEY ([LanguagesId]) REFERENCES [Languages] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LanguagesUsers_Users_UsersId] FOREIGN KEY ([UsersId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [ArtisansServices] (
        [ArtisansId] int NOT NULL,
        [ServicesId] int NOT NULL,
        CONSTRAINT [PK_ArtisansServices] PRIMARY KEY ([ArtisansId], [ServicesId]),
        CONSTRAINT [FK_ArtisansServices_Artisans_ArtisansId] FOREIGN KEY ([ArtisansId]) REFERENCES [Artisans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ArtisansServices_Services_ServicesId] FOREIGN KEY ([ServicesId]) REFERENCES [Services] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [ArtisanId] int NULL,
        [IsComplete] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsRequestAccepted] bit NOT NULL,
        [ServiceId] int NOT NULL,
        [PaymentId] int NULL,
        [Description] nvarchar(max) NOT NULL,
        [Cost] decimal(18,2) NOT NULL,
        [Urgency] nvarchar(max) NOT NULL DEFAULT N'Medium',
        [Scope] nvarchar(max) NOT NULL DEFAULT N'Maintenance',
        [Status] nvarchar(max) NOT NULL DEFAULT N'Placed',
        [PreferredDate] datetime2 NOT NULL,
        [WorkAddress_Country] nvarchar(max) NULL DEFAULT N'Ghana',
        [WorkAddress_City] nvarchar(max) NULL,
        [WorkAddress_Town] nvarchar(max) NULL,
        [WorkAddress_StreetAddress] nvarchar(max) NULL,
        [WorkAddress_StreetAddress2] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Artisans_ArtisanId] FOREIGN KEY ([ArtisanId]) REFERENCES [Artisans] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Orders_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Ratings] (
        [Id] int NOT NULL IDENTITY,
        [ArtisanId] int NOT NULL,
        [CustomerId] int NOT NULL,
        [Votes] int NOT NULL DEFAULT 0,
        [Description] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Ratings] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Ratings_Artisans_ArtisanId] FOREIGN KEY ([ArtisanId]) REFERENCES [Artisans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Ratings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Disputes] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [OrderId] int NOT NULL,
        [Details] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL DEFAULT N'Open',
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Disputes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Disputes_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Disputes_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Payments] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [OrderId] int NOT NULL,
        [AmountPaid] decimal(18,2) NOT NULL DEFAULT 0.0,
        [Status] nvarchar(max) NOT NULL DEFAULT N'NotSent',
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Payments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Payments_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE TABLE [Requests] (
        [Id] int NOT NULL IDENTITY,
        [CustomerId] int NOT NULL,
        [ArtisanId] int NOT NULL,
        [OrderId] int NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateModified] datetime2 NOT NULL,
        [EntityStatus] nvarchar(max) NOT NULL DEFAULT N'Normal',
        CONSTRAINT [PK_Requests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Requests_Artisans_ArtisanId] FOREIGN KEY ([ArtisanId]) REFERENCES [Artisans] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Requests_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]),
        CONSTRAINT [FK_Requests_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Artisans_UserId] ON [Artisans] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_ArtisansServices_ServicesId] ON [ArtisansServices] ([ServicesId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Customers_UserId] ON [Customers] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Disputes_CustomerId] ON [Disputes] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Disputes_OrderId] ON [Disputes] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Images_UserId] ON [Images] ([UserId]) WHERE [UserId] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_LanguagesUsers_UsersId] ON [LanguagesUsers] ([UsersId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Orders_ArtisanId] ON [Orders] ([ArtisanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Orders_ServiceId] ON [Orders] ([ServiceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Payments_CustomerId] ON [Payments] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE UNIQUE INDEX [IX_Payments_OrderId] ON [Payments] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Ratings_ArtisanId] ON [Ratings] ([ArtisanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Ratings_CustomerId] ON [Ratings] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Requests_ArtisanId] ON [Requests] ([ArtisanId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Requests_CustomerId] ON [Requests] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    CREATE INDEX [IX_Requests_OrderId] ON [Requests] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220812175201_InitialSetup')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220812175201_InitialSetup', N'6.0.1');
END;
GO

COMMIT;
GO

