using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Site6.Models;
using Site6;

namespace Site6.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        dbEntities db = new dbEntities();
        public IEnumerable<Site6.Models.Comment> Comments;
        public Site6.Models.Post mod;

        [AllowAnonymous]
        public ActionResult Index()
        {
            var FirstPost = db.Post.First();
            return View(FirstPost);
        }

        [AllowAnonymous]
        public ActionResult PostView(int Id)
        {
            ViewBag.Post = db.Post.Find(Id);
            ViewBag.Comments = Comments;
            var Comment = new Models.Comment();
            return View(Comment);
        }

        [AllowAnonymous]
        public ActionResult PostsView()
        {
            ViewBag.Posts = db.Post;
            var post = new Models.Post();
            return View(post);
        }

        [HttpPost]
        [Authorize(Users = "Admin")]
        public ActionResult AddPost(Models.Post post)
        {
            post.Date = DateTime.Now;
            db.Post.Add(post);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("PostsView");
        }
        [HttpGet]
        [Authorize(Users="Admin")]
        public ActionResult DeletePost(int Id)
        {
            db.Post.Remove(db.Post.Find(Id));
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("PostsView");
        }

        [HttpPost]
        public ActionResult AddComment(Models.Comment comment)
        {
            comment.Date = DateTime.Now;
            comment.UserName = User.Identity.Name;
            db.Comment.Add(comment);
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("PostView", new { Id = comment.PostId }); 
        }
        [HttpGet]
        public ActionResult DeleteComment(int Id)
        {
            int PostId = db.Comment.Find(Id).PostId;
            db.Comment.Remove(db.Comment.Find(Id));
            db.SaveChanges();
            db.Dispose();
            return RedirectToAction("PostView", new { Id = PostId }); 
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
