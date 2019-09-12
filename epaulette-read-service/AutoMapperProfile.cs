
using AutoMapper;
using Src = epaulette_data.epaulette_database_model;
using Dest = epaulette_read_service.ViewModel;

namespace epaulette_read_service
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Src.Author, Dest.Author>();
      CreateMap<Src.EpauletteContent, Dest.EpauletteContent>();
      CreateMap<Src.ExternalHost, Dest.ExternalHost>();
      CreateMap<Src.PostContent, Dest.PostContent>();
      CreateMap<Src.Post, Dest.Post>();
      CreateMap<Src.PostTag, Dest.PostTag>();
      CreateMap<Src.PostType, Dest.PostType>();
      CreateMap<Src.Tag, Dest.Tag>();
    }
  }
}