using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;
using System.Diagnostics;
using NewsSite.Domain.Concrete;

namespace NewsSite.WebUi.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository repository;
        public int pageSize = 4;

        public PostController(IPostRepository repo)
        {
            this.repository = repo;          
        }

        public ViewResult List(string tag, int page = 1)
        {
            PostListViewModel model = new PostListViewModel
            {
                Posts = repository.Posts
                .Where(p => tag == null || tag == p.Category.Name)
                .OrderByDescending(post => post.DateChanged)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),



                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = tag == null ?
                    repository.Posts.Count() :
                    repository.Posts.Where(p => GetCountTags(tag, p) > 0).Count()
                },
                CurrentTag = tag
            };
            return View(model);
        }

        public ViewResult ShowPost(int id = 1)
        {
            PostViewModel model = new PostViewModel
            {
                Post = repository.Posts.Where(post => post.PostId == id).FirstOrDefault(),
                Comments = repository.Comments.Where(c => c.PostId == id)
            };

            return View(model);
        }

        private static int GetCountTags(string tag, Post p)
        {
            return p.PostTags.Count(a => a.Tag.Name == tag);
        }

        [HttpPost]
        public ActionResult AddComment(int? parentId, int postId, string Text)
        {

            repository.AddComment(parentId,postId,Text); //add validation
 
            return RedirectToAction("ShowPost");
        }


    }
}