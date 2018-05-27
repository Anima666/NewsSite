using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;

namespace NewsSite.Views.Manage
{
    public static class ManageNavPages
    {
      
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string UserArticles => "UserArticles";
        public static string AllUsers => "AllUsers";

        public static string ChangePassword => "ChangePassword";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string IndexListPost(ViewContext viewContext) => PageNavClass(viewContext, UserArticles);
        public static string IndexAllUsers(ViewContext viewContext) => PageNavClass(viewContext, AllUsers);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "nav-link active" : "nav-link";
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
