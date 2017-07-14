using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PlaySchool.Models.BMs.Game;
using PlaySchool.Models.VMs.Game;

namespace PlaySchool.Services.Interfaces
{
    public interface IGameService
    {
        bool CheckQuiz(QuizBm bind,string userId);
        QuizViewModel GetQuizViewModel(int quizId, string applicationUserId);
        QuizViewModel GetQuizViewModelByGameId(int gameId,string applicationUserId);
        ShowGameViewModel GetShowGameViewModel(int gameId);
        ExploreGameViewModel GetExploreGameViewModel(int page);
    }
}
