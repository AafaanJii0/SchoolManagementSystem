using AutoMapper;
using IMobi.School.DomainModal.v1;
using IMobi.School.DomainModal.v1.Book;
using IMobi.School.ServiceModal.v1.AppUser;
using IMobi.School.ServiceModal.v1.Book;

namespace IMobi.School.API.AutoMapper
{
    internal class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<BookSM, BookDM>().ReverseMap();
            CreateMap<AuthorSM, AuthorDM>().ReverseMap();
            CreateMap<ClientUserSM, ClientUserDM>().ReverseMap();
        }
    }
}
