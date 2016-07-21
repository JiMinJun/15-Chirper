using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Chirper.API.Infrastructure;
using Chirper.API.Models;
using System;

namespace Chirper.API.Controllers
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private readonly DataContext db = new DataContext();

        // GET: api/Comments
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            var originalComment = db.Comments.Find(id);
            string username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            //if user has already like the Comment do not increment LikeCount
            if (comment.LikeCount != originalComment.LikeCount)
            {
                if(!originalComment.LikedUsers.Contains(user))
                {
                    originalComment.LikeCount++;
                    originalComment.LikedUsers.Add(user);
                    user.LikedComments.Add(originalComment);
                }
            
            }
            if (comment.Text != originalComment.Text)
            {
                originalComment.Text = comment.Text;
            }

            db.Entry(originalComment).State = EntityState.Modified;
            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            string username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == username);

            if (user.Id == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                comment.UserId = user.Id;
            }

            comment.CreatedDate = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.CommentId == id) > 0;
        }
    }
}