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
     
            if (item.UserId != null)
            {
                User user = model.Users.Where(u => u.Id == item.UserId).First();
                string url = user.UrlImage;

                sb.AppendFormat("<div class='card card-inner'>" +
               "<div class='card-sub'>" +
               "<div class='card-body'>" +
               "<div class='row'>" +
               "<div class='сol-sm-2 col-lg-2'>" +
               "<img src = '{5}' class='fluid-profile' />" +
               "<p class='text-secondary text-center'>{0}</p></div>" +
               "<div class='col-sm-10 col-lg-10'>" +
               "<p>" +
               "<a class='float-left'><strong>{1}</strong></a>" +
               "<span class='float-right'>" + GetIconLike(item, model) +
               "</span>" +
               "</p>" +
               "<div class='clearfix'></div>" +
               "<p>{2}</p>" +
               "<p>" +
               "<a class='float-right btn-sm btn btn-outline-primary ml-2' onclick='ShowCommentInput({4})'> <i class='fa fa-reply'></i> Reply</a>" +

               "</p>" +
               "</div>" +
               "</div>" + GetCommentInput(item, model) + html.CreateComments(item, model).ToString() +
               "</div>" +
               "</div>" +
               "</div>", item.PostedTime.ToShortTimeString(), user.UserName, item.Text, item.LikeCount, item.Id, url);
            }

           

        }

        private static string GetCommentInput(Comment item, PostViewModel model)
        {
            var stringBuilder = new StringBuilder();
            if (model.CurrentUserId != "")
            {
                stringBuilder.AppendFormat("<div class='card my-4' id='{0}' style='display: none' >", item.Id);
                // sb.AppendLine("<h5 class='card-header'>Leave a Comment:</h5>");
                stringBuilder.AppendLine("<div class='card-body'>");
                stringBuilder.AppendLine("<form action='/Post/AddComment' method='post'>");
                stringBuilder.AppendFormat("<input type = 'hidden' name='PostId' value='{0}' />", item.PostId);
                stringBuilder.AppendFormat("<input hidden = 'true' name='ParentId' value={0} />", item.Id);
                stringBuilder.AppendFormat("<input hidden = 'true' name='UserId' value={0} />", model.CurrentUserId);
                stringBuilder.AppendLine("<div class='form-group'>");
                stringBuilder.AppendLine("<textarea name='Text' class='form-control' rows='2'></textarea>");
                stringBuilder.AppendLine("</div>");
                stringBuilder.AppendLine("<button type='submit' class='btn btn-primary'>Submit</button>");
                stringBuilder.AppendLine("</form>");
                stringBuilder.AppendLine("</div>");
                stringBuilder.AppendLine("</div>");
            }

            return stringBuilder.ToString();
        }
        private static string GetIconLike(Comment item, PostViewModel model)
        {
            var stringBuilder = new StringBuilder();
            if (model.CurrentUserId == "")
            {
                stringBuilder.AppendFormat("<i class='fa fa-heart-o'></i><span>{0}</span>", item.LikeCount);

            }
            else
            {
                if (model.Likes.Where(x => x.UserId == model.CurrentUserId & x.CommentId == item.Id).FirstOrDefault() == null)
                {
                    stringBuilder.AppendFormat("<a class='like' id='like-{0}' onclick='like({0})'>", item.Id);
                    stringBuilder.AppendLine("<i class='fa fa-heart-o'></i>");
                    stringBuilder.AppendFormat("<span>{0}</span>", item.LikeCount);
                    stringBuilder.AppendLine("</a>");
                }
                else
                {
                    stringBuilder.AppendLine("<i class='fa fa-heart'></i>");
                    stringBuilder.AppendFormat("<span>{0}</span>", item.LikeCount);
                }
            }
            return stringBuilder.ToString();
        }
    }
}

