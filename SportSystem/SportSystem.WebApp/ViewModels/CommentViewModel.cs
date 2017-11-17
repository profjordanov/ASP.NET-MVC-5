namespace SportSystem.WebApp.ViewModels
{
    using System;

    using AutoMapper;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Username { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.Username, m => m.MapFrom(y => y.User.UserName));
        }
    }
}
