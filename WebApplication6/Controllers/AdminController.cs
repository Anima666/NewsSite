using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;

namespace NewsSite.WebUi.Controllers
{
   // [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IPostRepository repository;

        public AdminController(IPostRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Posts);
        }

        public ViewResult Create()
        {
            return View("Edit", new Post());
        }
        
        public ViewResult Edit(int PostId)
        {
            var model = new EditPostViewModel
            {
                Post = repository.Posts
                .FirstOrDefault(p => p.PostId == PostId),

                Categories = repository.Categories,

                Tags = repository.Tags.OrderBy(tag => tag.Name)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int PostId)
        {
            Post deletedGame = repository.DeletePost(PostId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Post \"{0}\" была удалена",
                    deletedGame.Title);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Post post, List<Tag> tags)
        {
            if (ModelState.IsValid)
            {
             
                repository.SavePost(post, tags);

                TempData["message"] = string.Format("Изменения в посту \"{0}\" были сохранены", post.Title);
                return RedirectToAction("Index");

            }
            else
            {
                // Что-то не так со значениями данных
                return View(post);
            }
        }
    }
}