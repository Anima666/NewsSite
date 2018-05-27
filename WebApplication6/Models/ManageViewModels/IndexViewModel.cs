using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }

        public string StatusMessage { get; set; }

        public string Image { get; set; }

        public int Likes { get; set; }
        public int Articles { get; set; }
        public int MinimalToshow { get; set; }
    }
}
