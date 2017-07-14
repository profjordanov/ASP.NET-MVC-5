using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;

namespace PlaySchool.Models.EntityModels
{
    public class Player
    {
        public Player()
        {
            this.Friends = new List<Player>();
            this.PlayersRequestedFriendship = new List<Player>();
        }
        public int PlayerId { get; set; }
        public int Points { get; set; }
        [StringLength(15)]
        public string FirstName { get; set; }
        [StringLength(15)]
        public string LastName { get; set; }
        public bool HaveProfilePicture { get; set; }
        [NotMapped]
        public string ProfilePictureName
        {
            get { return this.AppUser.UserName + "_profile_picture.jpg"; }
        }
        public virtual ApplicationUser AppUser { get; set; }
        [Column("PlayerId")]
        public virtual IList<Quiz> QuizzesTaken { get; set; }
        public virtual IList<Player> Friends { get; set; }
        public virtual IList<Player> PlayersRequestedFriendship { get; set; }
    }
}
