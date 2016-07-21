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
        public virtual ICollection<Comment> LikedComments { get; set; }
        public virtual ICollection<Chirp> LikedChirps { get; set; }
    }
}