namespace SportSystem.WebApp.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class CommentInputModel : IMapTo<Comment>
    {
        [Required]
        public string Content { get; set; }

        public int MatchId { get; set; }
    }
}
