using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;

namespace NewsSite.WebUi.Controllers
{
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
            Post Post = repository.Posts
                .FirstOrDefault(p => p.PostId == PostId);
            return View(Post);
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
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                repository.SavePost(post);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", post.Title);
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