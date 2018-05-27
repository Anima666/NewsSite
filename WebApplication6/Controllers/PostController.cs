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
                Ratings = repository.Ratings
            };
            return View("List", model);
        }

        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Post> posts = repository.Posts
                .Where(p => category == null || category == p.Category.Name)
                .OrderByDescending(post => post.Rating);

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
                CurrentCategory = category,

                Ratings = repository.Ratings
                
            };
            return View(model);
        }

        public async Task<IActionResult> ShowPost(int id = 1)
        {
            Post post = repository.Posts.Where(p => p.PostId == id).FirstOrDefault();

            if (post == null)
                return NotFound();

            User user = null;
            if (post != null)
            {
                user = await _userManager.FindByIdAsync(post.UserId);

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
                Likes = repository.Likes,
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
            if (Text != null)
            {
                repository.AddComment(parentId, postId, UserId, Text); 
            }
            else
            {
                TempData["Required field"] = "Required field";
               
            }

            return RedirectToAction("ShowPost", new { id = postId});
        }
        [HttpPost]
        public async Task<JsonResult> SetRating(int id, int value)
        {
            if (User.Identity.IsAuthenticated)
            {
                Post post = repository.Posts.Where(x => x.PostId == id).First();

                var curentUser = await GetCurrentUserAsync();

                repository.SetRating(id, post, curentUser, value);

                return Json(post.Rating.ToString());

            }

            return Json(null);
        }

        [HttpPost]
        public async Task<JsonResult> LikeThis(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Comment comment = repository.Comments.Where(x => x.Id == id).First();

                var curentUser = await GetCurrentUserAsync();
                repository.SetLike(id, comment, curentUser);

                return Json(comment.LikeCount.ToString());
            }

            return Json(null);
        }

       
    }
}