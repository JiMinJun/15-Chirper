using System;
using System.Collections.Generic;
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
        public int LikeCount { get; set; }
        public ICollection<string> LikedUsers { get; set; }
        public string LikedUsersAsString
        {
            get { return string.Join(",", LikedUsers); }
            set { LikedUsers = value.Split(',').ToList(); }
        }

        //relationship field
        public virtual Chirp Chirp { get; set; }
        public virtual ChirperUser User { get; set; }

        /*public Comment()
        {
            LikedUsers = new List<string>();
        }*/
    }
}