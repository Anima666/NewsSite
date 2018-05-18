using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NewsSite.WebUi.HtmlHelpers
{
    public static class CommentsHelper
    {
        public static HtmlString CreateComments(this IHtmlHelper html, Comment comment, PostViewModel model)
        {
            var sb = new StringBuilder();

            if (model.Comments.Count() > 0)
            {        
                foreach (var item in model.Comments.Where(c => c.ParentId == comment.Id & c.Id != comment.Id))
                {
                    GetComment(html, model, sb, item);
                }
            }
            return new HtmlString(sb.ToString());
        }

        private static void GetComment(IHtmlHelper html, PostViewModel model, StringBuilder sb, Comment item)
        {
            string url = "http://placehold.it/50x50";
            User user = new User();
            user.UserName = "NoName";
            if (item.UserID != null)
            {              
                user = model.Users.Where(u => u.Id == item.UserID).First();
                url = user.UrlImage;
            }
       
            sb.AppendLine("<div class='media mt-4'>");
            sb.AppendFormat("<img class='d-flex mr-3 rounded-circle' src='{0}'>", url);
            sb.AppendLine("<div class='media-body'>");
            sb.AppendFormat("<h5 class='mt-0'>{0}</h5>", user.UserName);
            sb.AppendLine(item.Text);
            sb.AppendFormat("<a onclick='ShowCommentInput({0})'>Reply</a>", item.Id);

            GetCommentInput(sb, item, model);

            sb.AppendLine(html.CreateComments(item, model).ToString());

            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
        }

        private static void GetCommentInput(StringBuilder sb, Comment item, PostViewModel model)
        {
          
            sb.AppendFormat("<div class='card my-4' id='{0}' style='display: none;'>", item.Id);
            sb.AppendLine("<h5 class='card-header'>Leave a Comment:</h5>");
            sb.AppendLine("<div class='card-body'>");
            sb.AppendLine("<form action='/Post/AddComment' method='post'>");
            sb.AppendFormat("<input hidden ='true' name='PostId' value='{0}' />", item.PostId);
            sb.AppendFormat("<input hidden ='true' name='ParentId' value={0} />", item.Id);
            sb.AppendFormat("<input hidden ='true' name='UserId' value={0} />", model.CurrentUserId);
            sb.AppendLine("<div class='form-group'>");
            sb.AppendLine("<textarea name='Text' class='form-control' rows='2'></textarea>");
            sb.AppendLine("</div>");
            sb.AppendLine("<button type='submit' class='btn btn-primary'>Submit</button>");
            sb.AppendLine("</form>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
        }
    }
}

