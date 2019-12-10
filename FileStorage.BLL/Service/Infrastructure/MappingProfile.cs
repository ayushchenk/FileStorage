using AutoMapper;
using FileStorage.BLL.Model;
using FileStorage.DAL.Model;

namespace FileStorage.BLL.Service.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Folder, FolderDTO>().ReverseMap();

            CreateMap<File, FileDTO>()
                .ForMember("FirstName", options => options.MapFrom(file => file.User.FirstName))
                .ForMember("LastName", options => options.MapFrom(file => file.User.LastName))
                .ForMember("Email", options => options.MapFrom(file => file.User.Email))
                .ForMember("CategoryName", options => options.MapFrom(file => file.Category.CategoryName))
                .ForMember("FolderName", options => options.MapFrom(file => file.Folder.FolderName));
            CreateMap<FileDTO, File>();
        }
    }
}
