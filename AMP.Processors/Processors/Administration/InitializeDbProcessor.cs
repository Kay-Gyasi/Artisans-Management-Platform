using AMP.Domain.Entities.BusinessManagement;
using AMP.Domain.Entities.UserManagement;

namespace AMP.Processors.Processors.Administration
{
    public static class InitIds
    {
        public static string KayAdmin => "d5ff4e1b-503d-428e-86f8-8395d81de8c3";
        public static string KayArtisan => "1f0a2ca4-f51e-4f4c-91bb-6a526b28de5a";
        public static string Woode => "ce770c59-321b-459f-8022-2d6c4c0b1e75";
        public static string Awate => "e6c36d19-f3ea-4c27-a08e-919cef6dc9d5";
        public static string Gloria => "91f9bc91-1d35-4299-b2d2-d35fcdddb294";
        public static string Abolo => "2193d840-32c0-40fd-a72b-18e3e17ea185";
        public static string Addae => "902b8d62-cb2b-46f8-91a9-cd1d10aedb76";
        public static string KayDeveloper => "ca9f1186-c4f8-43c6-9567-23f18974e0a7";
        public static string KaySuspended => "68c3f900-47cd-4fdc-8628-ad8d451b5258";
    }

    [Processor]
    public class InitializeDbProcessor : Processor
    {
        public InitializeDbProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) 
            : base(uow, mapper, cache)
        {
        }

        public async Task InitializeDatabase()
        {
            await InitializeServices();
            await InitializeLanguages();
            await InitializeUsers();
            await InitializeArtisans();
            await InitializeCustomers();
            await Uow.SaveChangesAsync();
        }

        private async Task InitializeServices()
        {
            var services = new List<Service>
            {
                Service.Create("Masonry"),
                Service.Create("Electrical Works"),
                Service.Create("Plumbing"),
                Service.Create("Mechanics"),
                Service.Create("Carpentry"),
                Service.Create("Painting"),
            };

            await Uow.Services.InsertAsync(services);
        }

        private async Task InitializeLanguages()
        {
            var languages = new List<Language>
            {
                Language.Create("English"),
                Language.Create("Fante"),
                Language.Create("Twi"),
                Language.Create("Ewe"),
                Language.Create("French"),
            };

            await Uow.Languages.InsertAsync(languages);
            await Uow.SaveChangesAsync();
        }

        private async Task InitializeUsers()
        {
            var password = Uow.Users.Register("pass");

            var users = new List<User>()
            {
                User.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Jeremiah")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Administrator)
                    .HasLevelOfEducation(LevelOfEducation.PhD)
                    .WithContact(Contact.Create("0557833216")
                        .WithEmailAddress("kofigyasidev@gmail.com"))
                    .WithAddress(Address.Create("Takoradi", "Anaji-I.Adu Rd."))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                        "Twi"
                    }))
                    .WithMomoNumber("0557833216")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.KayAdmin),
                User.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Jeremiah")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Artisan)
                    .HasLevelOfEducation(LevelOfEducation.PhD)
                    .WithContact(Contact.Create("0557833216")
                        .WithEmailAddress("kofigyasidev@gmail.com"))
                    .WithAddress(Address.Create("Takoradi", "Anaji-I.Adu Rd."))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                        "Twi"
                    }))
                    .WithMomoNumber("0557833216")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.KayArtisan),
                User.Create()
                    .WithFirstName("Samuel")
                    .WithFamilyName("Woode")
                    .WithOtherName("Aquaman")
                    .WithImageId(null)
                    .SetDisplayName()
                    .OfType(UserType.Artisan)
                    .HasLevelOfEducation(LevelOfEducation.Masters)
                    .WithContact(Contact.Create("0557511677")
                        .WithEmailAddress("nanakfobil@gmail.com"))
                    .WithAddress(Address.Create("Tarkwa", "Cyanide"))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                    }))
                    .WithMomoNumber("0556455344")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.Woode),
                User.Create()
                    .WithFirstName("Samuel")
                    .WithFamilyName("Awate")
                    .WithOtherName("Mumuni")
                    .WithImageId(null)
                    .SetDisplayName()
                    .OfType(UserType.Customer)
                    .HasLevelOfEducation(LevelOfEducation.Undergraduate)
                    .WithContact(Contact.Create("0556455344")
                        .WithEmailAddress("sammymumuni911@gmail.com"))
                    .WithAddress(Address.Create("Kumasi", "Sofoline"))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Twi",
                        "English",
                        "French",
                    }))
                    .WithMomoNumber("0557511677")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.Awate),
                User.Create()
                    .WithFirstName("Gloria")
                    .WithFamilyName("Mensah")
                    .WithOtherName("Reddington")
                    .WithImageId(null)
                    .SetDisplayName()
                    .OfType(UserType.Customer)
                    .HasLevelOfEducation(LevelOfEducation.PhD)
                    .WithContact(Contact.Create("0204377833")
                        .WithEmailAddress("gloriaredred@gmail.com"))
                    .WithAddress(Address.Create("Tarkwa", "Cyanide"))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Ewe",
                        "English",
                    }))
                    .WithMomoNumber("0204377833")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.Gloria),
                User.Create()
                    .WithFirstName("Emmanuel")
                    .WithFamilyName("Abolo")
                    .WithOtherName("Financial Abolo")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Customer)
                    .HasLevelOfEducation(LevelOfEducation.Shs)
                    .WithContact(Contact.Create("0545366277")
                        .WithEmailAddress("financialabolo889@gmail.com"))
                    .WithAddress(Address.Create("Tarkwa", "Aboso"))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                    }))
                    .WithMomoNumber("0545366277")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.Abolo),
                User.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Addae")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Artisan)
                    .HasLevelOfEducation(LevelOfEducation.Shs)
                    .WithContact(Contact.Create("0206744299")
                        .WithEmailAddress("addaengoah@gmail.com"))
                    .WithAddress(Address.Create("Obuasi", "Tech Junction"))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                    }))
                    .WithMomoNumber("0206744299")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.Addae),
                User.Create()
                    .WithFirstName("Kay")
                    .WithFamilyName("Gyasi")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Developer)
                    .HasLevelOfEducation(LevelOfEducation.PhD)
                    .WithContact(Contact.Create("0557833216")
                        .WithEmailAddress("kofigyasidev@gmail.com"))
                    .WithAddress(Address.Create("Takoradi", "Anaji-I.Adu Rd."))
                    .Speaks(await BuildLanguage(new List<string>
                    {
                        "Fante",
                        "English",
                        "Twi"
                    }))
                    .WithMomoNumber("0557833216")
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.KayDeveloper),
                User.Create()
                    .WithFirstName("Kay")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Suspended")
                    .SetDisplayName()
                    .WithImageId(null)
                    .OfType(UserType.Artisan)
                    .HasLevelOfEducation(LevelOfEducation.PhD)
                    .WithContact(Contact.Create("0557833216")
                        .WithEmailAddress("kofigyasidev@gmail.com"))
                    .WithAddress(Address.Create("Takoradi", "Anaji-I.Adu Rd."))
                    .Speaks(await BuildLanguage(new List<string>()
                    {
                        "Fante",
                        "English",
                        "Twi"
                    }))
                    .WithMomoNumber("0557833216")
                    .IsSuspendedd(true)
                    .HasPassword(password.Item1)
                    .HasPasswordKey(password.Item2)
                    .WithId(InitIds.KaySuspended),
            };

            await Uow.Users.InsertAsync(users);
            await Uow.SaveChangesAsync();
        }

        private async Task InitializeArtisans()
        {
            var builder = new StringBuilder();
            builder.Append("With over 6 years experience, ");
            builder.Append("QFace Group Ghana brings to you a wide variety of professional ");
            builder.Append("expertise that spans across various aspects of Information Technology. ");
            builder.Append("We build bespoke applications (Web, Desktop, Mobile) ");
            builder.Append("that is africa-tolerant and friendly to our african business ecosystem, ");
            builder.Append("making the solution exactly tailored to work in the way that fits ");
            builder.Append("into your work environment. Our main services include Software ");
            builder.Append("Development, Interactive Multimedia and Website Design.");

            var artisans = new List<Artisan>
            {
                Artisan.Create(InitIds.KayArtisan)
                    .WithBusinessName("Qface Group Ghana")
                    .WithDescription(builder.ToString())
                    .CreatedOn()
                    .IsVerifiedd(true)
                    .IsApprovedd(true)
                    .Offers(await BuildService(new List<string>
                        {
                            "Electrical Works"
                        })),
                Artisan.Create(InitIds.Woode)
                    .WithBusinessName("Aquaman Painting Works")
                    .WithDescription("")
                    .CreatedOn()
                    .IsVerifiedd(false)
                    .IsApprovedd(false)
                    .Offers(await BuildService(new List<string>
                    {
                        "Plumbing",
                        "Painting"
                    })),
                Artisan.Create(InitIds.Addae)
                    .WithBusinessName("Addae Uber Solutions")
                    .WithDescription("")
                    .CreatedOn()
                    .IsVerifiedd(true)
                    .IsApprovedd(true)
                    .Offers(await BuildService(new List<string>
                    {
                        "Carpentry",
                        "Masonry"
                    })),
            };

            await Uow.Artisans.InsertAsync(artisans);
        }

        private async Task InitializeCustomers()
        {
            var customers = new List<Customer>
            {
                Customer.Create(InitIds.Awate),
                Customer.Create(InitIds.Abolo),
                Customer.Create(InitIds.Gloria)
            };
            await Uow.Customers.InsertAsync(customers);
        }


        private async Task<List<Language>> BuildLanguage(List<string> languages)
        {
            return await Uow.Languages.BuildLanguages(languages);
        }

        private async Task<List<Service>> BuildService(List<string> services)
        {
            return await Uow.Services.BuildServices(services);
        }
    }
}