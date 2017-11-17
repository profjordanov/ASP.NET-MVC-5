namespace SportSystem.WebApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class TeamDetailsViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public DateTime? Founded { get; set; }

        public string Website { get; set; }

        public IEnumerable<PlayerViewModel> Players { get; set; }

        public int Votes { get; set; }

        public bool UserHasVoted { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Team, TeamDetailsViewModel>()
                .ForMember(x => x.Votes, cnf => cnf.MapFrom(m => m.Votes.Any() ? m.Votes.Sum(v => v.Value) : 0));
        }
    }
}