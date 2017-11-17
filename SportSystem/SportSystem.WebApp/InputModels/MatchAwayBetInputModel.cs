namespace SportSystem.WebApp.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using SportSystem.Common;
    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class MatchAwayBetInputModel : MatchBetInputModel, IMapTo<UserMatchBet>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = GlobalConstants.RequiredMessage)]
        [Range(GlobalConstants.MinimumBet, GlobalConstants.MaximumBet, ErrorMessage = "The {0} should be between {1} and {2}")]
        public decimal AwayBet { get; set; }
    }
}