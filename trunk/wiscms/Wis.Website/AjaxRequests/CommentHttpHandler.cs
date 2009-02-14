//------------------------------------------------------------------------------
// <copyright file="AbstractManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Web;
using Wis.Toolkit;
using Wis.Website.DataManager;

namespace Wis.Website.AjaxRequests
{
    public class CommentHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // 1 验证验证码
            if (context.Session[CommentVerifyHttpHandler.ConstCommentVerify] == null)
            {
                return;
            }
            string commentVerify1 = context.Session[CommentVerifyHttpHandler.ConstCommentVerify].ToString();
            string commentVerify2 = context.Request[CommentVerifyHttpHandler.ConstCommentVerify];
            if (string.IsNullOrEmpty(commentVerify1) || string.IsNullOrEmpty(commentVerify2))
            {
                return;
            }
            if (commentVerify1.ToUpper() != commentVerify2.ToUpper())
            {
                return;
            }

            // 验证评论数据，两者不能同时为空
            string title = RequestManager.Request("Title");
            string contentHtml = RequestManager.Request("ContentHtml");
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(contentHtml))
            {
                return;
            }

            // 2 验证评论数据
            // 获取文章的编号
            if (context.Request.UrlReferrer == null)
            {
                return;
            }

            string rawUrl = context.Request.UrlReferrer.AbsoluteUri;
            int charIndex = rawUrl.LastIndexOf('/');
            if (charIndex == -1)
            {
                return;
            }

            // http://localhost:3419/Web/2/2-13/38.htm
            rawUrl = rawUrl.Substring(charIndex + 1); // 38.htm
            charIndex = rawUrl.IndexOf('.');
            if (charIndex == -1)
            {
                return;
            }
            string requestArticleId = rawUrl.Substring(0, charIndex); // RequestManager.Request("ArticleId");

            // 文章编号是否为数字
            int articleId;
            if (int.TryParse(requestArticleId, out articleId) == false)
            {
                // 提示失败
                return;
            }

            // 文章是否存在
            ArticleManager articleManager = new ArticleManager();
            Article article = articleManager.GetArticle(articleId);
            if (string.IsNullOrEmpty(article.Title))
            {
                // 提示失败
                return;
            }

            // 构造评论实体类
            Comment comment = new Comment();

            // 获得实体类
            // TODO:如果前台支持用户注册和登录，获取用户的昵称(唯一不重复)
            comment.Commentator = string.Empty;

            comment.CommentGuid = Guid.NewGuid();
            //comment.CommentId
            comment.ContentHtml = contentHtml;
            comment.DateCreated = DateTime.Now;
            comment.IPAddress = RequestManager.GetClientIP();
            comment.Original = string.Empty;
            comment.SubmissionGuid = article.ArticleGuid;
            comment.Title = title;

            CommentManager commentManager = new CommentManager();
            commentManager.AddNew(comment);

            // TODO:事务处理
            // 更新Article的评论数
            articleManager.UpdateArticleComments(article.ArticleGuid);

            // 清空评论验证码
            context.Session[CommentVerifyHttpHandler.ConstCommentVerify] = null;

            // 输出评论数
            //context.Response.Write((article.Comments + 1).ToString());
            // 重新生成 Article
            ReleaseManager releaseManager = new ReleaseManager();
            releaseManager.ReleaseArticle(article);
        }

        #endregion
    }
}
