using AMP.Domain.Entities.BusinessManagement;
using AMP.Domain.Entities.Messaging;
using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.PageDtos.Messaging;
using AMP.Processors.PageDtos.UserManagement;

namespace AMP.Processors.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Artisan, ArtisanDto>().ReverseMap();
            CreateMap<Artisan, ArtisanPageDto>().ReverseMap();
            CreateMap<PaginatedList<Artisan>, PaginatedList<ArtisanPageDto>>().ReverseMap();
            CreateMap<Artisan, ArtisanCommand>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerPageDto>().ReverseMap();
            CreateMap<PaginatedList<Customer>, PaginatedList<CustomerPageDto>>().ReverseMap();
            CreateMap<Customer, CustomerCommand>().ReverseMap();
            CreateMap<Dispute, DisputeDto>().ReverseMap();
            CreateMap<Dispute, DisputePageDto>().ReverseMap();
            CreateMap<PaginatedList<Dispute>, PaginatedList<DisputePageDto>>().ReverseMap();
            CreateMap<Dispute, DisputeCommand>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderPageDto>().ReverseMap();
            CreateMap<PaginatedList<Order>, PaginatedList<OrderPageDto>>().ReverseMap();
            CreateMap<Order, OrderCommand>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentPageDto>().ReverseMap();
            CreateMap<PaginatedList<Payment>, PaginatedList<PaymentPageDto>>().ReverseMap();
            CreateMap<Payment, PaymentCommand>().ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Rating, RatingPageDto>().ReverseMap();
            CreateMap<PaginatedList<Rating>, PaginatedList<RatingPageDto>>().ReverseMap();
            CreateMap<Rating, RatingCommand>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, ServicePageDto>().ReverseMap();
            CreateMap<PaginatedList<Service>, PaginatedList<ServicePageDto>>().ReverseMap();
            CreateMap<Service, ServiceCommand>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserPageDto>().ReverseMap();
            CreateMap<PaginatedList<User>, PaginatedList<UserPageDto>>().ReverseMap();
            CreateMap<ChatMessage, ChatMessageDto>().ReverseMap();
            CreateMap<ChatMessage, ChatMessagePageDto>().ReverseMap();
            CreateMap<PaginatedList<ChatMessage>, PaginatedList<ChatMessagePageDto>>().ReverseMap();
            CreateMap<Conversation, ConversationDto>().ReverseMap();
            CreateMap<Conversation, ConversationPageDto>().ReverseMap();
            CreateMap<PaginatedList<Conversation>, PaginatedList<ConversationPageDto>>().ReverseMap();
            CreateMap<ConnectRequest, ConnectRequestDto>().ReverseMap();
            CreateMap<ConnectRequest, ConnectRequestPageDto>().ReverseMap();
            CreateMap<PaginatedList<ConnectRequest>, PaginatedList<ConnectRequestPageDto>>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationPageDto>().ReverseMap();
            CreateMap<PaginatedList<Notification>, PaginatedList<NotificationPageDto>>().ReverseMap();
            CreateMap<User, UserCommand>().ReverseMap();
            CreateMap<Language, LanguagesDto>().ReverseMap();
            CreateMap<Language, LanguagesPageDto>().ReverseMap();
            CreateMap<Address, AddressCommand>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressPageDto>().ReverseMap();
            CreateMap<Contact, ContactCommand>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactPageDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<Image, ImagePageDto>().ReverseMap();
            CreateMap<Image, ImageCommand>().ReverseMap();
            CreateMap<Invitation, InvitationDto>().ReverseMap();

        }
    }
}