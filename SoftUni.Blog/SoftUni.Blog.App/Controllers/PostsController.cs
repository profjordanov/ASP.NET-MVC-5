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
    public class PostsController : BaseController
    {
        public PostsController(ISoftUniBlogData data)
            : base(data)
        {
        }

        public ActionResult Details(int id)
        {
            var post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return this.HttpNotFound();
            }

            var postModel = Mapper.Map<Post,PostFullViewModel>(post);
            return View(postModel);
        }
    }
}