﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Site6.Models;
using Site6;

using PagedList;

namespace Site6.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        dbEntities db = new dbEntities();
        public class myclass{
            public IEnumerable<Site6.Models.Post> PostList;
            public PagedList.IPagedList<Site6.Models.Post> PostPagedList;
            public Models.Post PostModel;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            IEnumerable<Site6.Models.Post> Posts = db.Post;
            var LastPost = Posts.Last();
            return View(LastPost);
        }

        [AllowAnonymous]
        public ActionResult PostView(int Id)
        {
            ViewBag.Post = db.Post.Find(Id);
            ViewBag.Comments = db.Comment;
            var Comment = new Models.Comment();
            return View(Comment);
        }

        [AllowAnonymous]
        public ActionResult PostsView(int? page)
        {
            int pagenum = page ?? 1;
            int size = 10;
            var model = db.Post.ToList();

            myclass cl = new myclass();

            cl.PostModel = new Models.Post();
            cl.PostPagedList = db.Post.OrderByDescending(s => s.Date).ToPagedList(pagenum, size);
            cl.PostList = db.Post;

            
            return View(cl);
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
