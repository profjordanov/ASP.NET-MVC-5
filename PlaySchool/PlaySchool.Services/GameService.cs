using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlaySchool.Data;
using PlaySchool.Models;
using PlaySchool.Models.BMs.Game;
using PlaySchool.Models.EntityModels;
using PlaySchool.Models.VMs.Game;
using PlaySchool.Services.Interfaces;

namespace PlaySchool.Services
{
    public class GameService : Service,IGameService
    {
        public GameService(PlaySchoolContext context) : base(context)
        {
        }

        public ShowGameViewModel GetShowGameViewModel(int gameId)
        {
            Game game = Context.Games.Include("Creator").FirstOrDefault(g=>g.GameId == gameId);
            if (game == null)
            {
                throw new ArgumentNullException("No game with this id");
            }
            game.NumberPlayed++;
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Exception b = e;
                throw;
            }
            
           var vm = Mapper.Map<ShowGameViewModel>(game);
            vm.Quiz = Context.Quizzes.FirstOrDefault(q => q.Game.GameId == gameId);
            return vm;
        }

        public ExploreGameViewModel GetExploreGameViewModel(int page)
        {
            var games =
                Context.Games.Include("Creator").OrderByDescending(g=>g.NumberPlayed)
                    .Skip((page - 1) * Constants.DefaultGamesPerPage)
                    .Take(Constants.DefaultGamesPerPage).ToList();
            List<SmallGameViewModel> smallVm = new List<SmallGameViewModel>();
            foreach (var game in games)
            {
                smallVm.Add(Mapper.Map<SmallGameViewModel>(game));
            }
            ExploreGameViewModel exploreVm = new ExploreGameViewModel();
            exploreVm.SmallGameViewModel = smallVm;
            exploreVm.CurrentPage = page;
            int count = Context.Games.Count();
            count = (count % Constants.DefaultGamesPerPage == 0 ? 0 : 1) + count/Constants.DefaultGamesPerPage;
            exploreVm.TotalPages = count;
            return exploreVm;
        }

        public QuizViewModel GetQuizViewModelByGameId(int gameId,string applicationUserId)
        {
            if (Context.Quizzes.Any(q => q.Game.GameId == gameId))
            {
                Quiz quiz =
                    this.Context.Quizzes.Where(q => q.Game.GameId == gameId).Include(q => q.PlayersPlayed)
                        .Include(q => q.Questions)
                        .FirstOrDefault();
                IList<QuestionViewModel> questionsVM = new List<QuestionViewModel>();
                foreach (var question in quiz.Questions)
                {
                    questionsVM.Add(new QuestionViewModel() {Answer = "",QuestionTitle = question.QuestionTitle,QuestionTip = question.QuestionTip});
                }
                QuizViewModel qvm = new QuizViewModel();
                qvm.Questions = questionsVM;
                qvm.QuizId = quiz.QuizId;
                qvm.IsPlayed = quiz.PlayersPlayed.Any(p => p.AppUser.Id == applicationUserId);
                return qvm;
            }
            throw new ArgumentNullException("Quiz for this game doesn't exist!");
        }

        public bool CheckQuiz(QuizBm bind,string userId)
        {
            Quiz quiz = Context.Quizzes.Find(bind.QuizId);
            if (quiz == null)
            {
                throw new ArgumentNullException("No such quiz!");
            }
            try
            {
                Player player = this.Context.Players.FirstOrDefault(p => p.AppUser.Id == userId);
                player.Points += Constants.WinPointsPerQuiz;
                this.Context.Quizzes.Find(bind.QuizId).PlayersPlayed.Add(player);
                for (int i = 0; i < quiz.Questions.Count; i++)
                {
                    if (bind.Questions[i].Answer != quiz.Questions[i].Answer)
                    {
                        Context.SaveChanges();
                        return false;
                    }
                }

                
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong!");
            }
            return true;
        }

        public QuizViewModel GetQuizViewModel(int quizId, string applicationUserId)
        {
            if (Context.Quizzes.Any(q => q.QuizId==quizId))
            {
                Quiz quiz =
                    this.Context.Quizzes.Where(q => q.QuizId == quizId).Include(q => q.PlayersPlayed)
                        .Include(q => q.Questions)
                        .FirstOrDefault();
                IList<QuestionViewModel> questionsVM = new List<QuestionViewModel>();
                foreach (var question in quiz.Questions)
                {
                    questionsVM.Add(new QuestionViewModel() { Answer = "", QuestionTitle = question.QuestionTitle, QuestionTip = question.QuestionTip });
                }
                QuizViewModel qvm = new QuizViewModel();
                qvm.Questions = questionsVM;
                qvm.QuizId = quiz.QuizId;
                qvm.IsPlayed = quiz.PlayersPlayed.Any(p => p.AppUser.Id == applicationUserId);
                return qvm;
            }
            throw new ArgumentNullException("Quiz for this game doesn't exist!");
        }
    }
}
