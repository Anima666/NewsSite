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

        [Required(ErrorMessage = "Please input title post")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please input description post")]
        public string Description { get; set; }

        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

    }
}
