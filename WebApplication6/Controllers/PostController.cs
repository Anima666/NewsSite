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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace NewsSite.WebUi.Controllers
{
    public class PostController : Controller
    {

        public int pageSize = 4;
        private IPostRepository repository;
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("List");
        }

        [HttpPost]
        public ViewResult Search(string text = "", int page = 1) //add comment
        {
            IEnumerable<Post> posts = repository.Posts.Where(p => p.Title.ToUpper().Contains(text.ToUpper()));

            if (posts.Count() == 0)
            {
                posts = repository.Posts.Where(p => p.Text.ToUpper().Contains(text.ToUpper()));
            }

            PostListViewModel model = new PostListViewModel
            {
                Posts = posts
             .OrderByDescending(post => post.DateChanged)
             .Skip((page - 1) * pageSize)
             .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = posts.Count(),

                },
            };
            return View("List", model);
        }

        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Post> posts = repository.Posts
                .Where(p => category == null || category == p.Category.Name)
                .OrderByDescending(post => post.DateChanged);


            PostListViewModel model = new PostListViewModel
            {
                Posts = posts.
                Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = posts.Count()

                },
                CurrentCategory = category
            };
            return View(model);
        }

        public async Task<IActionResult> ShowPost(int id = 1)
        {
            Post post = repository.Posts.Where(p => p.PostId == id).FirstOrDefault();
            User user=null;
            if (post != null)
            {
                user = await _userManager.FindByIdAsync(post.UserID);

                if (user == null)
                {
                    return NotFound();
                }
            }

            var CurentUser = await GetCurrentUserAsync();
            string currentUserId = "";

            if (CurentUser != null)
                currentUserId = CurentUser.Id;

            PostViewModel model = new PostViewModel
            {
                Post = post,
                Comments = repository.Comments.Where(c => c.PostId == id),
                User = user,
                Users = _userManager.Users,
                CurrentUserId = currentUserId,
            };

            return View(model);
        }

        private static int GetCountTags(string tag, Post p)
        {
            return p.PostTags.Count(a => a.Tag.Name == tag);
        }

        [HttpPost]
        public ActionResult AddComment(int? parentId, int postId, string UserId, string Text)
        {

            repository.AddComment(parentId, postId, UserId, Text); //add validation

            return RedirectToAction("ShowPost");
        }


    }
}