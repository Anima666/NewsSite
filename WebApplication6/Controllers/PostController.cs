using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;

namespace NewsSite.WebUi.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository repository;
        public PostController(IPostRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Posts);
        }
    }
}