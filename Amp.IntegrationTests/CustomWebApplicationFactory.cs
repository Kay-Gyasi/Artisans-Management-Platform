namespace Amp.IntegrationTests;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
{
    private IServiceScope _scope;
    private AmpDbContext _db;

    public IUnitOfWork UnitOfWork { get; set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, conf) =>
        {
            conf.Sources.Clear();
            conf.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"));
            conf.AddEnvironmentVariables();

            conf.Build();
        });
        // builder.ConfigureTestServices(services =>
        // {
        //     // We can further customize our application setup here.
        // });
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AmpDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<AmpDbContext>(options =>
            {
                options.UseSqlServer("Data Source=YOGA-X1;Integrated Security=True;Initial Catalog=AmpTestDb;");
            });

            var sp = services.BuildServiceProvider();

            _scope = sp.CreateScope();
             var scopedServices = _scope.ServiceProvider;
            _db = scopedServices.GetRequiredService<AmpDbContext>();
            UnitOfWork = scopedServices.GetRequiredService<IUnitOfWork>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            _db.Database.EnsureDeleted();
            _db.Database.Migrate();

            try
            {
                var sql =
                    "USE [AmpTestDb]\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'38c7bda5-42c4-4e3b-9ab3-a652b9d978ce', N'Mechanics', N'', CAST(N'2022-09-03T09:24:10.7100221' AS DateTime2), CAST(N'2022-09-03T09:24:10.7100220' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'6079bd37-b82d-4f3b-864d-2ee0ed7fd404', N'Electrical Works', N'', CAST(N'2022-09-03T09:24:10.7100208' AS DateTime2), CAST(N'2022-09-03T09:24:10.7100206' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'78d65de4-a3fa-4291-bf1a-f223b2be411d', N'Masonry', N'', CAST(N'2022-09-03T09:24:10.7098600' AS DateTime2), CAST(N'2022-09-03T09:24:10.7097143' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'83a1cba2-3069-43b7-9e07-0d7dd8a495c5', N'Plumbing', N'', CAST(N'2022-09-03T09:24:10.7100216' AS DateTime2), CAST(N'2022-09-03T09:24:10.7100215' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'a1771119-146c-4f4e-b4c8-093d81301174', N'Painting', N'', CAST(N'2022-09-03T09:24:10.7100260' AS DateTime2), CAST(N'2022-09-03T09:24:10.7100259' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Services] ([Id], [Name], [Description], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'a6b1bf41-f6c7-477f-a7cb-1c6c179a2c1e', N'Carpentry', N'', CAST(N'2022-09-03T09:24:10.7100226' AS DateTime2), CAST(N'2022-09-03T09:24:10.7100225' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Users] ([Id], [ImageId], [FirstName], [FamilyName], [OtherName], [DisplayName], [MomoNumber], [IsSuspended], [IsVerified], [IsRemoved], [Type], [LevelOfEducation], [Password], [PasswordKey], [Contact_EmailAddress], [Contact_PrimaryContact], [Contact_PrimaryContact2], [Contact_PrimaryContact3], [Address_Country], [Address_City], [Address_Town], [Address_StreetAddress], [Address_StreetAddress2], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'02a0e32f-0f78-4eea-984a-96f25ab2e8ed', NULL, N'Kofi', N'Gyasi', NULL, N'Kofi Gyasi', NULL, 0, 1, 0, N'Artisan', N'0', 0xF1B56939BC946C7F207152769AD3D9892021628EC0235A4E1889364F03A38EE74D78EFBB39843C3DDC6C829E4DB47D33F2232516712F106E6C023653027D9386, 0x093D14CDC2E2C518765706B74BFB7B3DB28A11EB8BFDF8185E611616EA0DA47B005A3E7F6B2E9EF9674E630F595F5EFEA4C5E7C42C925C1CADCBBB4F006016CE55CB6DC88E2F10326715F6698DEDC0E4822C444CB6F1B2BA5E9ECA91A7498A5E88058A659AF6DC30F4BAB06CC43FC9C94FF357539638BCE1B39FF84906B081BB, N'', N'0557833216', N'', N'', 0, NULL, NULL, NULL, NULL, CAST(N'2022-10-13T03:02:28.9169308' AS DateTime2), CAST(N'2022-10-11T23:46:23.9430768' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Users] ([Id], [ImageId], [FirstName], [FamilyName], [OtherName], [DisplayName], [MomoNumber], [IsSuspended], [IsVerified], [IsRemoved], [Type], [LevelOfEducation], [Password], [PasswordKey], [Contact_EmailAddress], [Contact_PrimaryContact], [Contact_PrimaryContact2], [Contact_PrimaryContact3], [Address_Country], [Address_City], [Address_Town], [Address_StreetAddress], [Address_StreetAddress2], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'29c15927-485c-4e7f-a9e3-2fb299b2f1a6', NULL, N'Unverified Kofi', N'Gyasi', NULL, N'Unverified Kofi Gyasi', NULL, 0, 0, 0, N'Artisan', N'0', 0xF1B56939BC946C7F207152769AD3D9892021628EC0235A4E1889364F03A38EE74D78EFBB39843C3DDC6C829E4DB47D33F2232516712F106E6C023653027D9386, 0x093D14CDC2E2C518765706B74BFB7B3DB28A11EB8BFDF8185E611616EA0DA47B005A3E7F6B2E9EF9674E630F595F5EFEA4C5E7C42C925C1CADCBBB4F006016CE55CB6DC88E2F10326715F6698DEDC0E4822C444CB6F1B2BA5E9ECA91A7498A5E88058A659AF6DC30F4BAB06CC43FC9C94FF357539638BCE1B39FF84906B081BB, N'', N'0554860725', N'', N'', 0, NULL, NULL, NULL, NULL, CAST(N'2022-10-13T03:02:28.9169308' AS DateTime2), CAST(N'2022-10-11T23:46:23.9430768' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Users] ([Id], [ImageId], [FirstName], [FamilyName], [OtherName], [DisplayName], [MomoNumber], [IsSuspended], [IsVerified], [IsRemoved], [Type], [LevelOfEducation], [Password], [PasswordKey], [Contact_EmailAddress], [Contact_PrimaryContact], [Contact_PrimaryContact2], [Contact_PrimaryContact3], [Address_Country], [Address_City], [Address_Town], [Address_StreetAddress], [Address_StreetAddress2], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'56e2970a-492e-46da-bb26-eded8cec9b28', NULL, N'Kay', N'Gyasi', NULL, N'Kay Gyasi', NULL, 0, 1, 0, N'Customer', N'0', 0x6BDC8C974D2C1ED8C1BED64210927E95E8325B37332E6C27C61317B7CAABDDD3535A9B93BB77B27D560019D48C9F59320F73E5CA79C328178AB328AD8D8A152A, 0xD8475E572CEED74EF3D7B978C97736ABF327C49138EB54DE338C68F335049B5C9231BC016882DEF89CE7BD64AD8D24C7745559E6790A721C7B07742827A25492452D991168DFEA83BB4B7BB26507F089CC84076FF215D0686AD33DA321FEBD8DB5AD3F82DC8F79E9203B18308C32CF9B8F25149B9DC25927DDB8C067D2E159E2, N'', N'0207733247', N'', N'', 0, NULL, NULL, NULL, NULL, CAST(N'2022-10-12T23:43:11.0200352' AS DateTime2), CAST(N'2022-10-11T23:46:23.9430768' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Artisans] ([Id], [UserId], [BusinessName], [Type], [ECCN], [Description], [IsVerified], [IsApproved], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'f49af2d5-db77-4645-8865-b6e11637d551', N'02a0e32f-0f78-4eea-984a-96f25ab2e8ed', N'Qface Group', N'Company', NULL, N'', 0, 0, CAST(N'2022-10-13T03:02:29.1998381' AS DateTime2), CAST(N'2022-10-11T23:46:23.9158544' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[ArtisansServices] ([ArtisansId], [ServicesId]) VALUES (N'f49af2d5-db77-4645-8865-b6e11637d551', N'38c7bda5-42c4-4e3b-9ab3-a652b9d978ce')\nGO\nINSERT [dbo].[Customers] ([Id], [UserId], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'22f9ddf1-c16a-4311-b3cc-33143b6f2f03', N'56e2970a-492e-46da-bb26-eded8cec9b28', CAST(N'2022-10-12T23:43:11.8242302' AS DateTime2), CAST(N'2022-10-11T23:46:23.9167157' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Languages] ([Id], [Name], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'7e4d6736-2f50-4616-9df4-46696dccc178', N'Twi', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-09-03T09:24:12.5269318' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Languages] ([Id], [Name], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'a2e93409-9528-4edd-a839-bc13b2f5591e', N'French', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-09-03T09:24:12.5269326' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Languages] ([Id], [Name], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'bbb508fa-bbd0-4879-8212-9ae2eaeadee0', N'English', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-09-03T09:24:12.5268744' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Languages] ([Id], [Name], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'c5d9d69d-b469-42bb-a584-091bc1ab5ea1', N'Ewe', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-09-03T09:24:12.5269322' AS DateTime2), N'Normal')\nGO\nINSERT [dbo].[Languages] ([Id], [Name], [DateCreated], [DateModified], [EntityStatus]) VALUES (N'ea29f141-7b61-4f63-b6e0-b37e146d1cad', N'Fante', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2022-09-03T09:24:12.5269314' AS DateTime2), N'Normal')\nGO\n"
                        .Replace("\n", "");
                _db.Database.ExecuteSqlRaw(sql.Replace("GO", " "));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database. Error: {Message}", ex.Message);
            }
        });
    }
    
    public async Task AuthenticateAsync(HttpClient testClient, UserType type)
    {
        testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
            await GetJwtAsync(testClient, type));
    }

    private static async Task<string> GetJwtAsync(HttpClient testClient, UserType type)
    {
        return type switch
        {
            UserType.Artisan => await GetTokenForArtisanTestClient(testClient),
            UserType.Customer => await GetTokenForCustomerTestClient(testClient),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static async Task<string> GetTokenForArtisanTestClient(HttpClient testClient)
    {
        var request = await testClient.PostAsJsonAsync<SigninCommand>("api/v1/user/login", new SigninCommand
        {
            Phone = "0557833216",
            Password = "pass"
        }, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<SigninResponse>())?.Token ?? "";
    }
    
    private static async Task<string> GetTokenForCustomerTestClient(HttpClient testClient)
    {
        var request = await testClient.PostAsJsonAsync("api/v1/user/login", new SigninCommand
        {
            Phone = "0207733247",
            Password = "pass"
        }, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<SigninResponse>())?.Token ?? "";
    }

    protected override void Dispose(bool disposing)
    {
        _scope?.Dispose();
        _db?.Dispose();
    }
}