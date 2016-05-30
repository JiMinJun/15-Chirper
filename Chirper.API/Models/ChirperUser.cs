using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chirper.API.Models
{
    public class ChirperUser : IdentityUser
    {
        public virtual ICollection<Chirp> Chirps { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public ICollection<string> LikedChirps { get; set; }
        public ICollection<string> LikedComments { get; set; }

        public string LikedCommentsAsString
        {
            get { return string.Join(",", LikedComments); }
            set { LikedComments = value.Split(',').ToList(); }
        }
        public string LikedChirpsAsString
        {
            get { return string.Join(",", LikedChirps); }
            set { LikedChirps = value.Split(',').ToList(); }
        }

        public ChirperUser()
        {
            LikedChirps = new Collection<string>();
            LikedComments = new Collection<string>();
        }
    }
}