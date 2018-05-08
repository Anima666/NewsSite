using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;

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

        public ViewResult List(int page = 1)
        {
            PostListViewModel model = new PostListViewModel
            {
                Posts = repository.Posts.OrderBy(post => post.PostId).Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Posts.Count()
                }
            };
            return View(model);
        }
    }
}