using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
       // public virtual ICollection<TUserRole> Roles { get; }

    }
}
