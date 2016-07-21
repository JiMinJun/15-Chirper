using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chirper.API.Models
{
    public class Comment
    {
        //primary key
        public int CommentId { get; set; }
        public int ChirpId { get; set; }
        public string UserId { get; set; }

        //fields relevant to comment
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        //relationship field
        public virtual Chirp Chirp { get; set; }
        public virtual ChirperUser User { get; set; }
        public virtual ICollection<ChirperUser> LikedUsers { get; set; }

    }
}