namespace AMP.Processors.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artisans, ArtisanDto>().ReverseMap();
            CreateMap<Artisans, ArtisanPageDto>().ReverseMap();
            CreateMap<PaginatedList<Artisans>, PaginatedList<ArtisanPageDto>>().ReverseMap();
            CreateMap<Artisans, ArtisanCommand>().ReverseMap();
            CreateMap<Customers, CustomerDto>().ReverseMap();
            CreateMap<Customers, CustomerPageDto>().ReverseMap();
            CreateMap<PaginatedList<Customers>, PaginatedList<CustomerPageDto>>().ReverseMap();
            CreateMap<Customers, CustomerCommand>().ReverseMap();
            CreateMap<Disputes, DisputeDto>().ReverseMap();
            CreateMap<Disputes, DisputePageDto>().ReverseMap();
            CreateMap<PaginatedList<Disputes>, PaginatedList<DisputePageDto>>().ReverseMap();
            CreateMap<Disputes, DisputeCommand>().ReverseMap();
            CreateMap<Orders, OrderDto>().ReverseMap();
            CreateMap<Orders, OrderPageDto>().ReverseMap();
            CreateMap<PaginatedList<Orders>, PaginatedList<OrderPageDto>>().ReverseMap();
            CreateMap<Orders, OrderCommand>().ReverseMap();
            CreateMap<Payments, PaymentDto>().ReverseMap();
            CreateMap<Payments, PaymentPageDto>().ReverseMap();
            CreateMap<PaginatedList<Payments>, PaginatedList<PaymentPageDto>>().ReverseMap();
            CreateMap<Payments, PaymentCommand>().ReverseMap();
            CreateMap<Ratings, RatingDto>().ReverseMap();
            CreateMap<Ratings, RatingPageDto>().ReverseMap();
            CreateMap<PaginatedList<Ratings>, PaginatedList<RatingPageDto>>().ReverseMap();
            CreateMap<Ratings, RatingCommand>().ReverseMap();
            CreateMap<Services, ServiceDto>().ReverseMap();
            CreateMap<Services, ServicePageDto>().ReverseMap();
            CreateMap<PaginatedList<Services>, PaginatedList<ServicePageDto>>().ReverseMap();
            CreateMap<Services, ServiceCommand>().ReverseMap();
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<Users, UserPageDto>().ReverseMap();
            CreateMap<PaginatedList<Users>, PaginatedList<UserPageDto>>().ReverseMap();
            CreateMap<Users, UserCommand>().ReverseMap();
            CreateMap<Languages, LanguagesDto>().ReverseMap();
            CreateMap<Languages, LanguagesPageDto>().ReverseMap();
            CreateMap<Address, AddressCommand>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressPageDto>().ReverseMap();
            CreateMap<Contact, ContactCommand>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactPageDto>().ReverseMap();
            CreateMap<Images, ImageDto>().ReverseMap();
            CreateMap<Images, ImagePageDto>().ReverseMap();
            CreateMap<Images, ImageCommand>().ReverseMap();
            CreateMap<Invitations, InvitationDto>().ReverseMap();

        }
    }
}