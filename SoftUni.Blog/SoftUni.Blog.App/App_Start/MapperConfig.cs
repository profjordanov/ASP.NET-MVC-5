using AutoMapper;
using SoftUni.Blog.App.Models.ViewModels;
using SoftUni.Blog.Models;

namespace SoftUni.Blog.App.App_Start
{
    public static class MapperConfig
    {

        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Post, PostConciseViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
            Mapper.CreateMap<Post, PostFullViewModel>()
                .ForMember(model => model.Author, config => config.MapFrom(post => post.Author.UserName));
        }
    }
}