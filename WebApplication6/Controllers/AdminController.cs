using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;

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
    }
}