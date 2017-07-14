using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PlaySchool.Models.BMs.Game;
using PlaySchool.Models.EntityModels;
using PlaySchool.Models.VMs.Game;
using PlaySchool.Services.Interfaces;

namespace PlaySchool.Controllers
{
    [RoutePrefix("Game")]
    [Authorize(Roles = "Player,Admin")]
    public class GameController : Controller
    {
        protected IGameService service;
        public GameController(IGameService service)
        {
            this.service = service;
        }
        // GET: Game/Show/5
        [Route("Show/{id:int}")]
        public ActionResult Show(int id)
        {
            return View(this.service.GetShowGameViewModel(id));
        }
        //GET: Game, Game/Explore
        [Route("Explore/{page:int?}")]
        [Route("", Name = "defaultGameRoute")]
        public ActionResult Explore(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var exploreVm = this.service.GetExploreGameViewModel((int)page);
            return View(exploreVm);
        }
        // GET: Game/Quiz?id=5
        [Route("Quiz")]
        [HttpGet]
        public ActionResult Quiz()
        {
            int id = -1;
            try
            {
                id = int.Parse(Request.Params["id"]);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException("Invalid url!");
            }
            return Redirect("/game/quiz/"+id);
        }
        // GET: Game/Quiz/5
        [Route("Quiz/{gameId:int}")]
        [HttpGet]
        public ActionResult Quiz(int gameId)
        {
            var vm = this.service.GetQuizViewModel(gameId, User.Identity.GetUserId());
            return View(vm);
        }
        // POST: Game/Quiz/
        [Route("Quiz")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quiz([Bind(Include = "Questions,QuizId")]QuizBm bind)
        {
            string userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                ViewBag.InPost = true;
                ViewBag.AreCorrect = this.service.CheckQuiz(bind, userId);
            }
            
            return View(this.service.GetQuizViewModel(bind.QuizId, userId));
        }
    }
}
