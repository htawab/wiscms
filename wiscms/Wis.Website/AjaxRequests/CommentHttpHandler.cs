﻿//------------------------------------------------------------------------------
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
    public class CommentHttpHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // 验证评论数据

            // 获取文章的编号
            string requestArticleId = RequestManager.Request("ArticleId");

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
            comment.ContentHtml = RequestManager.Request("ContentHtml");
            comment.DateCreated = DateTime.Now;
            comment.IPAddress = RequestManager.GetClientIP();
            comment.Original = string.Empty;
            comment.SubmissionGuid = article.ArticleGuid;
            comment.Title = RequestManager.Request("Title");

            CommentManager commentManager = new CommentManager();
            commentManager.AddNew(comment);

            // TODO:事务处理
            // 更新Article的评论数
            articleManager.UpdateArticleComments(article.ArticleGuid);

            // 输出评论数
            context.Response.Write((article.Comments + 1).ToString());
        }

        #endregion
    }
}
