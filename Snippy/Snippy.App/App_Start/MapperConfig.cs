using AutoMapper;
using Snippy.App.Models.BindingModels;
using Snippy.App.Models.ViewModels;
using Snippy.Models;
using System;
using System.Linq;

namespace Snippy.App.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Snippet, ConciseSnippetViewModel>();
            Mapper.CreateMap<Snippet, SnippetDetailsViewModel>()
                .ForMember(vm => vm.Author, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(vm => vm.Comments, opt => opt.MapFrom(s => s.Comments.OrderByDescending(c => c.CreationTime)));
            Mapper.CreateMap<SnippetBindingModel, Snippet>()
                .ForMember(s => s.Language, opt => opt.MapFrom(bm => new Language() { Id = bm.LanguageId }))
                .ForMember(
                s => s.Labels,
                opt => opt.MapFrom(bm => bm.Labels.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(label => new Label() { Text = label })));
            Mapper.CreateMap<Snippet, SnippetBindingModel>()
                .ForMember(vm => vm.LanguageId, opt => opt.MapFrom(s => s.Language.Id))
                .ForMember(vm => vm.Labels, opt => opt.MapFrom(s => string.Join(";", s.Labels.Select(l => l.Text))));
            Mapper.CreateMap<Label, ConciseLabelViewModel>();
            Mapper.CreateMap<Label, LabelViewModel>()
                .ForMember(vm => vm.SnippetsCount, opt => opt.MapFrom(l => l.Snippets.Count));
            Mapper.CreateMap<Comment, ConciseCommentViewModel>()
                .ForMember(vm => vm.Author, opt => opt.MapFrom(c => c.Author.UserName));
            Mapper.CreateMap<Comment, CommentViewModel>()
                .ForMember(vm => vm.Author, opt => opt.MapFrom(c => c.Author.UserName))
                .ForMember(vm => vm.SnippetId, opt => opt.MapFrom(c => c.Snippet.Id))
                .ForMember(vm => vm.SnippetTitle, opt => opt.MapFrom(c => c.Snippet.Title));
            Mapper.CreateMap<Language, LanguageViewModel>();
        }
    }
}