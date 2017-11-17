using AutoMapper;
using StreamPowered.App.Models.ViewModels;
using StreamPowered.Models;
using System.Linq;

namespace StreamPowered.App.App_Start
{
    public static class MapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<Game, ConciseGameViewModel>()
                .ForMember(vm => vm.CoverImageUrl, opt => opt.MapFrom(g => g.ImageUrls.FirstOrDefault().Url))
                .ForMember(vm => vm.Rating, opt => opt.MapFrom(g => g.Ratings.Any() ? g.Ratings.Average(r => r.Value) : 0));
            Mapper.CreateMap<Review, ReviewViewModel>()
                .ForMember(vm => vm.Author, opt => opt.MapFrom(r => r.Author.UserName))
                .ForMember(vm => vm.GameId, opt => opt.MapFrom(r => r.Game.Id))
                .ForMember(vm => vm.GameTitle, opt => opt.MapFrom(r => r.Game.Title));
            Mapper.CreateMap<Game, GameDetailsViewModel>()
                .ForMember(vm => vm.GenreId, opt => opt.MapFrom(g => g.Genre.Id))
                .ForMember(vm => vm.GenreName, opt => opt.MapFrom(g => g.Genre.Name))
                .ForMember(vm => vm.Rating, opt => opt.MapFrom(g => g.Ratings.Any() ? g.Ratings.Average(r => r.Value) : 0))
                .ForMember(vm => vm.ImageUrls, opt => opt.MapFrom(g => g.ImageUrls.Select(u => u.Url)));
            Mapper.CreateMap<Review, ConciseReviewViewModel>()
                .ForMember(vm => vm.Author, opt => opt.MapFrom(r => r.Author.UserName));
            Mapper.CreateMap<Rating, RatingViewModel>();
        }
    }
}