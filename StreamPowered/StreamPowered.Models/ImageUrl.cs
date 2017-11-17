namespace StreamPowered.Models
{
    public class ImageUrl
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public virtual Game Game { get; set; }
    }
}