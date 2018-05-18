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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NewsSite.WebUi.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository repository;
        public int pageSize = 4;
        private UserManager<User> _userManager;
        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public PostController(IPostRepository repo, UserManager<User> userManager)
        {
            this.repository = repo;
            this._userManager = userManager;
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

        public async Task<IActionResult> ShowPost(int id = 1)
        {
            Post post = repository.Posts.Where(p => p.PostId == id).FirstOrDefault();

            User user = await _userManager.FindByIdAsync(post.UserID);
            if (user == null)
            {
                return NotFound();
            }
            var CurentUser = await GetCurrentUserAsync();
            string currentUserId = "";
            if (CurentUser!=null)
            currentUserId =  CurentUser.Id;


            PostViewModel model = new PostViewModel
            {
                Post = post,
                Comments = repository.Comments.Where(c => c.PostId == id),
                User = user,
                Users = _userManager.Users,
                CurrentUserId = currentUserId,
                // User = repository.Users.Where(u => u.Id == post.UserID).FirstOrDefault(),
                //User = user,
            };

            return View(model);
        }

        private static int GetCountTags(string tag, Post p)
        {
            return p.PostTags.Count(a => a.Tag.Name == tag);
        }

        [HttpPost]
        public ActionResult AddComment(int? parentId, int postId,string UserId, string Text)
        {

            repository.AddComment(parentId, postId, UserId, Text); //add validation

            return RedirectToAction("ShowPost");
        }


    }
}