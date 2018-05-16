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
        public static HtmlString CreateCommets(this IHtmlHelper html, Comment comment, PostViewModel model)
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
            sb.AppendLine("<div class='media mt-4'>");
            sb.AppendLine("<img class='d-flex mr-3 rounded-circle' src='http://placehold.it/50x50'>");
            sb.AppendLine("<div class='media-body'>");
            sb.AppendFormat("<h5 class='mt-0'>{0}</h5>", "Commenter Name");
            sb.AppendLine(item.Text);
            sb.AppendFormat("<a onclick='ShowCommentInput({0})'>Reply</a>", item.Id);

            GetCommentInput(sb, item);

            sb.AppendLine(html.CreateList(item, model).ToString());

            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
        }

        private static void GetCommentInput(StringBuilder sb, Comment item)
        {
            sb.AppendFormat("<div class='card my-4' id='{0}' style='display: none;'>", item.Id);
            sb.AppendLine("<h5 class='card-header'>Leave a Comment:</h5>");
            sb.AppendLine("<div class='card-body'>");
            sb.AppendLine("<form action='/Post/AddComment' method='post'>");
            sb.AppendFormat("<input hidden ='true' name='PostId' value='{0}' />", item.PostId);
            sb.AppendFormat("<input hidden ='true' name='ParentId' value={0} />", item.Id);
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

