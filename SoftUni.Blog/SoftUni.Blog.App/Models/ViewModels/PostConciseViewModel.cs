namespace SoftUni.Blog.App.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PostConciseViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}