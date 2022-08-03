using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors.Administration
{
    [Processor]
    public class InitializeDbProcessor : ProcessorBase
    {
        public InitializeDbProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task InitializeDatabase()
        {
            await _uow.InitializeDb.InitializeDatabase();
            await InitializeServices();
            await InitializeLanguages();
            await InitializeUsers();
            await InitializeArtisans();
            await InitializeCustomers();
            await _uow.SaveChangesAsync();
            await InitializeOrders();

            await _uow.SaveChangesAsync();
        }

        
        private async Task InitializeServices()
        {
            var count = await _uow.Services.CountAsync();
            if (count > 0) return;

            var services = new List<Services>
            {
                Services.Create("Bricklayer")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Electrician")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Millwright")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Boilermaker")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Plumber")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Mechanic including Automotive Mechanic")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Diesel Mechanic")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Carpenter")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Welder")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Rigger")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Fitter and Turner")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Mechanical Fitter")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Pipe Fitter")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Painter")
                    .CreatedOn(DateTime.UtcNow)
            };

            await _uow.Services.InsertAsync(services);
        }

        private async Task InitializeLanguages()
        {
            var languages = new List<Languages>
            {
                Languages.Create("English"),
                Languages.Create("Fante"),
                Languages.Create("Twi"),
                Languages.Create("Ewe"),
                Languages.Create("French"),
            };

            await _uow.Languages.InsertAsync(languages);
            await _uow.SaveChangesAsync();
        }

        private async Task InitializeUsers()
        {
            var count = await _uow.Users.CountAsync();
            if (count > 0) return;
            var password = _uow.Users.Register("pass");

            var users = new List<Users>()
            {
                Users.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Jeremiah")
                    .SetDisplayName()
                    .WithImageUrl("https://avatars.githubusercontent.com/u/69327748?v=4")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Jeremiah")
                    .SetDisplayName()
                    .WithImageUrl("https://avatars.githubusercontent.com/u/69327748?v=4")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Samuel")
                    .WithFamilyName("Woode")
                    .WithOtherName("Aquaman")
                    .SetDisplayName()
                    .WithImageUrl("")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Samuel")
                    .WithFamilyName("Awate")
                    .WithOtherName("Mumuni")
                    .SetDisplayName()
                    .WithImageUrl("")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Gloria")
                    .WithFamilyName("Mensah")
                    .WithOtherName("Reddington")
                    .SetDisplayName()
                    .WithImageUrl("")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Emmanuel")
                    .WithFamilyName("Abolo")
                    .WithOtherName("Financial Abolo")
                    .SetDisplayName()
                    .WithImageUrl("")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Kofi")
                    .WithFamilyName("Addae")
                    .SetDisplayName()
                    .WithImageUrl("")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Kay")
                    .WithFamilyName("Gyasi")
                    .SetDisplayName()
                    .WithImageUrl("https://avatars.githubusercontent.com/u/69327748?v=4")
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
                    .WithFirstName("Kay")
                    .WithFamilyName("Gyasi")
                    .WithOtherName("Suspended")
                    .SetDisplayName()
                    .WithImageUrl("https://avatars.githubusercontent.com/u/69327748?v=4")
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
                    .CreatedOn(DateTime.UtcNow),

            };

            await _uow.Users.InsertAsync(users);
            await _uow.SaveChangesAsync();
        }

        private async Task InitializeArtisans()
        {
            var count = await _uow.Artisans.CountAsync();
            if (count > 0) return;

            var builder = new StringBuilder();
            builder.Append("With over 6 years experience, ");
            builder.Append("QFace Group Ghana brings to you a wide variety of professional ");
            builder.Append("expertise that spans across various aspects of Information Technology. ");
            builder.Append("We build bespoke applications (Web, Desktop, Mobile) ");
            builder.Append("that is africa-tolerant and friendly to our african business ecosystem, ");
            builder.Append("making the solution exactly tailored to work in the way that fits ");
            builder.Append("into your work environment. Our main services include Software ");
            builder.Append("Development, Interactive Multimedia and Website Design.");

            var artisans = new List<Artisans>
            {
                Artisans.Create(3)
                    .WithBusinessName("Qface Group Ghana")
                    .WithDescription(builder.ToString())
                    .CreatedOn(DateTime.UtcNow)
                    .IsVerifiedd(true)
                    .IsApprovedd(true)
                    .Offers(await BuildService(new List<string>
                        {
                            "Electrician"
                        })),
                Artisans.Create(2)
                    .WithBusinessName("Aquaman Painting Works")
                    .WithDescription("")
                    .CreatedOn(DateTime.UtcNow)
                    .IsVerifiedd(false)
                    .IsApprovedd(false)
                    .Offers(await BuildService(new List<string>
                    {
                        "Plumber",
                        "Painter"
                    })),
                Artisans.Create(7)
                    .WithBusinessName("Addae Uber Solutions")
                    .WithDescription("")
                    .CreatedOn(DateTime.UtcNow)
                    .IsVerifiedd(true)
                    .IsApprovedd(true)
                    .Offers(await BuildService(new List<string>
                    {
                        "Carpenter",
                        "Bricklayer"
                    })),
            };

            await _uow.Artisans.InsertAsync(artisans);
        }

        private async Task InitializeCustomers()
        {
            var count = await _uow.Customers.CountAsync();
            if (count > 0) return;

            var customers = new List<Customers>
            {
                Customers.Create(1)
                    .CreatedOn(DateTime.UtcNow),
                Customers.Create(5)
                    .CreatedOn(DateTime.UtcNow),
                Customers.Create(6)
                    .CreatedOn(DateTime.UtcNow),
            };
            await _uow.Customers.InsertAsync(customers);
        }

        private async Task InitializeOrders()
        {
            var orders = new List<Orders>
            {
                Orders.Create(1, 1)
                    .ForArtisanWithId(2)
                    .WithDescription("I need a painter to work on my house")
                    .WithUrgency(Urgency.High)
                    .WithScope(ScopeOfWork.New)
                    .WithPreferredDate(DateTime.Today + TimeSpan.FromDays(7))
                    .WithWorkAddress(Address.Create("Tarkwa", "Brahabebome, Kaspa Hostel"))
                    .CreatedOn(DateTime.UtcNow),
                Orders.Create(2, 1)
                    .WithDescription("Painter needed for some maintenance work")
                    .WithUrgency(Urgency.High)
                    .WithScope(ScopeOfWork.Maintenance)
                    .WithPreferredDate(DateTime.Today + TimeSpan.FromDays(7))
                    .WithWorkAddress(Address.Create("Tarkwa", "Brahabebome, Hilda Hostel"))
                    .CreatedOn(DateTime.UtcNow),
                Orders.Create(1, 9)
                    //.ForArtisanWithId(2)
                    .WithDescription("Installation of a kitchen sink")
                    .WithUrgency(Urgency.High)
                    .WithScope(ScopeOfWork.New)
                    .WithPreferredDate(DateTime.Today + TimeSpan.FromDays(7))
                    .WithWorkAddress(Address.Create("Tarkwa", "UMaT"))
                    .CreatedOn(DateTime.UtcNow),
                Orders.Create(1, 12)
                    //.ForArtisanWithId(2)
                    .WithDescription("Fixing kitchen sink")
                    .WithUrgency(Urgency.Low)
                    .WithScope(ScopeOfWork.Maintenance)
                    .WithPreferredDate(DateTime.Today + TimeSpan.FromDays(4))
                    .WithWorkAddress(Address.Create("Takoradi", "Anaji-Estate"))
                    .CreatedOn(DateTime.UtcNow),
                Orders.Create(1, 6)
                    .ForArtisanWithId(1)
                    .WithDescription("Carpenter needed to fix some furniture")
                    .WithUrgency(Urgency.Medium)
                    .WithPreferredDate(DateTime.Today + TimeSpan.FromDays(4))
                    .WithWorkAddress(Address.Create("Tarkwa", "Cyanide"))
                    .CreatedOn(DateTime.UtcNow),
                Orders.Create(1, 1)
                    .ForArtisanWithId(2)
                    .WithDescription("I need a painter to work on my house (completed)")
                    .WithUrgency(Urgency.High)
                    .WithScope(ScopeOfWork.New)
                    .WithPreferredDate(DateTime.Today)
                    .WithWorkAddress(Address.Create("Tarkwa", "Brahabebome, Kaspa Hostel"))
                    .CreatedOn(DateTime.UtcNow)
                    .IsCompleted(true)
                    .WithStatus(OrderStatus.Completed),
            };

            await _uow.Orders.InsertAsync(orders);
        }


        private async Task<List<Languages>> BuildLanguage(List<string> languages)
        {
            return await _uow.Languages.BuildLanguages(languages);
        }

        private async Task<List<Services>> BuildService(List<string> services)
        {
            return await _uow.Services.BuildServices(services);
        }
    }
}