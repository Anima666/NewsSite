using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        public int PostId { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 20)]
        [Required(ErrorMessage = "Please input title post")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 160)]
        [Required(ErrorMessage = "Please input description post")]  
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Rating { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime DateChanged { get; set; }

        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

        // public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UserID { get; set; }
        public virtual User User { get; set; }

        public string Path { get; set; }



    }
}
