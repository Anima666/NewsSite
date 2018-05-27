using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

using System.Text;

namespace NewsSite.Domain.Entities
{
    public class User:IdentityUser
    {
        public string UrlImage { get; set; }
        public int Likes { get; set; }
        public int MinimalToShow { get; set; }
    }
}
