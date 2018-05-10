using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;
using System.Diagnostics;

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
                .Where(p => tag == null || GetCountTags(tag, p) > 0)
                .OrderBy(post => post.PostId)
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

        private static int GetCountTags(string tag, Post p)
        {
            return p.PostTags.Count(a => a.Tag.Name == tag);
        }
    }
}