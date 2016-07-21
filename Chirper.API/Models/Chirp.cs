using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chirper.API.Models
{
    public class Chirp
    {
        //primary key
        public int ChirpId { get; set; }
        public string UserId { get; set; }

        //fields relevant to a chirp

        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        //Relationship fields
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ChirperUser User { get; set; }
        public virtual ICollection<ChirperUser> LikedUsers { get; set; }

    }

}