namespace SportSystem.WebApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class MatchDetailsViewModel : IMapFrom<Match>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public int AwayTeamId { get; set; }

        public string AwayTeamName { get; set; }

        public DateTime Start { get; set; }

        public decimal AwayBets { get; set; }

        public decimal HomeBets { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Match, MatchDetailsViewModel>()
                .ForMember(x => x.HomeBets, c => c.MapFrom(m => m.UserMatchBets.Any() ? m.UserMatchBets.Sum(y => y.HomeBet) : 0));
            configuration.CreateMap<Match, MatchDetailsViewModel>()
                .ForMember(x => x.AwayBets, c => c.MapFrom(m => m.UserMatchBets.Any() ? m.UserMatchBets.Sum(y => y.AwayBet) : 0));
        }
    }
}