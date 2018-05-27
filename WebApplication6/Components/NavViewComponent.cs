using Microsoft.AspNetCore.Mvc;
using NewsSite.Domain.Abstract;
using NewsSite.WebUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Components
{
    public class NavTagsViewComponent : ViewComponent
    {
        private IPostRepository repository;
        private int countArticle = 4;

        public NavTagsViewComponent(IPostRepository repo)
        {
            this.repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SidePanelViewModel model = new SidePanelViewModel
            {
                Categories = repository.Categories.OrderByDescending(n => n.Name.Length).Select(x => x.Name),
                Posts = repository.Posts.OrderByDescending(p => p.DateChanged).Take(countArticle),
            };

            return View(model);
        }
    }
}
