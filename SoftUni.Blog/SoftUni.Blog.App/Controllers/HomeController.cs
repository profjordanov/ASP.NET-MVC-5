using AutoMapper;
using SoftUni.Blog.App.Models.ViewModels;
using SoftUni.Blog.Data.UnitOfWork;
using SoftUni.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftUni.Blog.App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISoftUniBlogData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var posts = this.Data.Posts.All()
                .OrderByDescending(p => p.Date);
            var postModels = Mapper.Map<IEnumerable<Post>, IEnumerable<PostConciseViewModel>>(posts);
            return this.View(postModels);
        }
    }
}