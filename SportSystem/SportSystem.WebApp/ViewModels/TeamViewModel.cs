namespace SportSystem.WebApp.ViewModels
{
    using System.Linq;

    using AutoMapper;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class TeamViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public int Votes { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamViewModel>()
                .ForMember(x => x.Votes, cnf => cnf.MapFrom(m => m.Votes.Any() ? m.Votes.Sum(v => v.Value) : 0));
        }
    }
}