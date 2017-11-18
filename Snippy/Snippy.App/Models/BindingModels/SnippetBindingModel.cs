namespace Snippy.App.Models.BindingModels
{
    public class SnippetBindingModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int LanguageId { get; set; }

        public string Labels { get; set; }
    }
}