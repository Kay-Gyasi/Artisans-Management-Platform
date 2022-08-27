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
        public InitializeDbProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) 
            : base(uow, mapper, cache)
        {
        }

        public async Task InitializeDatabase()
        {
            //await _uow.InitializeDb.InitializeDatabase();
            await InitializeServices();
            await InitializeLanguages();
            await InitializeUsers();
            await InitializeArtisans();
            await InitializeCustomers();
            await _uow.SaveChangesAsync();
        }

        private async Task InitializeServices()
        {
            var services = new List<Services>
            {
                Services.Create("Masonry")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Electrical Works")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Plumbing")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Mechanics")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Carpentry")
                    .CreatedOn(DateTime.UtcNow),
                Services.Create("Painting")
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
            var password = _uow.Users.Register("pass");

            var users = new List<Users>()
            {
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),
                Users.Create()
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
                    .CreatedOn(DateTime.UtcNow),

            };

            await _uow.Users.InsertAsync(users);
            await _uow.SaveChangesAsync();
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
                            "Electrical Works"
                        })),
                Artisans.Create(2)
                    .WithBusinessName("Aquaman Painting Works")
                    .WithDescription("")
                    .CreatedOn(DateTime.UtcNow)
                    .IsVerifiedd(false)
                    .IsApprovedd(false)
                    .Offers(await BuildService(new List<string>
                    {
                        "Plumbing",
                        "Painting"
                    })),
                Artisans.Create(7)
                    .WithBusinessName("Addae Uber Solutions")
                    .WithDescription("")
                    .CreatedOn(DateTime.UtcNow)
                    .IsVerifiedd(true)
                    .IsApprovedd(true)
                    .Offers(await BuildService(new List<string>
                    {
                        "Carpentry",
                        "Masonry"
                    })),
            };

            await _uow.Artisans.InsertAsync(artisans);
        }

        private async Task InitializeCustomers()
        {
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