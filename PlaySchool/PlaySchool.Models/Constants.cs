using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySchool.Models
{
    public static class Constants
    {
        //ACCOUNT SECTION
        public const string ProfilePicturePrefix = @"/Resources/ProfilePictures/";
        public const string UsernameRegex = @"^[A-Za-z][A-Za-z0-9_]+$";
        public const string DefaultProfilePicture = "default.png";

        //GAME SECTION
        public const string GameNameRegex = @"^[A-Za-z][A-Za-z0-9_]+$";
        public const string JsGameUrlRegex = @"^.+\.js$";
        public const string JsGameUrlBBreaker = "~/Scripts/games/breakout.js";
        public const string PhaserJsFrameworkUrl = "~/Scripts/phaser.min.js";

        public const string SampleGameDescription =
            "Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.Sample Description desc mesc.";
        //GAME SECTION: PAGER (EXPLORE)
        public const int DefaultGamesPerPage = 9;
        public const int DefaultShowPages = 5;
        public const int WinPointsPerQuiz = 10;
    }
}