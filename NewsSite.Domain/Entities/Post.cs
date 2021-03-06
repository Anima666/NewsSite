﻿using Microsoft.AspNetCore.Mvc;
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

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 15)]
        [Required(ErrorMessage = "Please input title post")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "PasswordLength", MinimumLength = 100)]
        [Required(ErrorMessage = "Please input description post")]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [HiddenInput(DisplayValue = false)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please input text post")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ValueRating { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime DateChanged { get; set; }

        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

        [Display(Name = "Categories")]
        public Category Category { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Path { get; set; }

        public ICollection<Rating> Rating { get;  } = new List<Rating>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();



    }
}
