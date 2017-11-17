namespace SportSystem.Models
{
    using System;

    public class Player
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public double Height { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
