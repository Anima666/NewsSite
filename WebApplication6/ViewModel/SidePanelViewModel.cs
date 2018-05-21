using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.ViewModel
{
    public class SidePanelViewModel
    {
        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
