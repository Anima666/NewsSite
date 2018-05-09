using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;

namespace NewsSite.WebUi.Views
{
    public class NavController : Controller
    {
        private IPostRepository repository;

        public NavController(IPostRepository repo)
        {
            this.repository = repo;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.Tags.Select(x=>x.Name);

            return PartialView(categories);
        }
    }
}