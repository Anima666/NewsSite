using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Components
{
    public class NavTagsViewComponent : ViewComponent
    {
        private IPostRepository repository;

        public NavTagsViewComponent (IPostRepository repo)
        {
            this.repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<string> categories = repository.Tags.Select(x => x.Name);

            return View(categories);
        }
    }
}
