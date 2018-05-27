using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NewsSite.WebUi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Authorize(Roles = "admin, writer")]
    public class AdminController : Controller
    {
        private IPostRepository repository;
        private IHostingEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public AdminController(IPostRepository repo, UserManager<User> userManager, IHostingEnvironment appEnvironment)
        {
            repository = repo;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }
        [Authorize(Roles = "admin")]
        public ViewResult Index()
        {
            return View(repository.Posts);
        }

        public ViewResult Create()
        {
            EditPostViewModel model = new EditPostViewModel
            {
                Post = new Post(),
                Categories = repository.Categories.ToList(),
                Tags = repository.Tags.ToList()

            };
            return View("Edit", model);
        }
      //  [Authorize(Roles = "writer")]
        public ViewResult Edit(int postId)
        {

            var model = new EditPostViewModel
            {
                Post = repository.Posts
                .FirstOrDefault(p => p.PostId == postId),

                Categories = repository.Categories,

                Tags = repository.Tags.OrderBy(tag => tag.Name)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int PostId)
        {
            Post deletedPost = repository.DeletePost(PostId);
            if (deletedPost != null)
            {
                TempData["message"] = string.Format("Post \"{0}\" была удалена",
                    deletedPost.Title);
            }

            return RedirectToAction("Index");
        }

     //   [Authorize(Roles = "writer")]
        [HttpPost]
        public async Task<ActionResult> Edit(Post post, List<Tag> tags, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                await SavingFile(post, uploadedFile);

                var userId = _userManager.GetUserId(HttpContext.User);

                repository.SavePost(post, tags, userId);

                TempData["message"] = string.Format("Изменения в посту \"{0}\" были сохранены", post.Title);


                return RedirectToAction("List","Post");
            }
            else
            {
               // Post post2 = repository.Posts.First(p => p.PostId == post.PostId);

                EditPostViewModel model = GetEditPostViewModel(post);
                // Что-то не так со значениями данных
                return View(model);
            }
        }

        private async Task SavingFile(Post post, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                post.Path = path;
            }
        }

        private EditPostViewModel GetEditPostViewModel(Post post)
        {
            return new EditPostViewModel
            {
                Post = post,

                Categories = repository.Categories,

                Tags = repository.Tags.OrderBy(tag => tag.Name)
            };
        }
    }
}


