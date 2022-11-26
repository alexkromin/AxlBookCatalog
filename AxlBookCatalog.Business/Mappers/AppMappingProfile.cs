using AutoMapper;
using AxlBookCatalog.Business.Models.Book;
using AxlBookCatalog.Domain;

namespace AxlBookCatalog.Business.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<AddBookRequest, Book>();
            CreateMap<EditBookRequest, Book>();
        }
    }
}
