namespace SoftUni.Blog.App.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Blog.Models;
    using Data.UnitOfWork;
    using AutoMapper;

    public class PostsController : BaseAdminController
    {
        public PostsController(ISoftUniBlogData data)
            : base(data)
        {
        }

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var posts = this.Data.Posts.All().Include(p => p.Author);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,AuthorId,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                this.Data.Posts.Add(post);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", post.AuthorId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", post.AuthorId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,AuthorId,Date")] Post post)
        {
            if (ModelState.IsValid)
            {
                var foundPost = this.Data.Posts.Find(post.Id);
                if (foundPost == null)
                {
                    return this.HttpNotFound();
                }

                foundPost = Mapper.Map<Post, Post>(post);
                this.Data.Posts.Update(foundPost);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", post.AuthorId);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = this.Data.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = this.Data.Posts.Find(id);
            this.Data.Posts.Remove(post);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
