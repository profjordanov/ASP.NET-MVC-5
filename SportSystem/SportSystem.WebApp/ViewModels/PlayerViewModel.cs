namespace SportSystem.WebApp.ViewModels
{
    using System;

    using SportSystem.Common.Mappings;
    using SportSystem.Models;

    public class PlayerViewModel : IMapFrom<Player>
    {
        public string FullName { get; set; }

        public double Height { get; set; }

        public DateTime BirthDate { get; set; }
    }
}