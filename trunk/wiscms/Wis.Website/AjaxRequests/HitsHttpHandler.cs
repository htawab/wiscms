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
    public class HitsHttpHandler : IHttpHandler
    {

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // TODO:同一IP在指定的时间内浏览网页不做统计
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

            // 更新Article的浏览数
            articleManager.UpdateArticleHits(article.ArticleGuid);

            // 输出浏览数
            context.Response.Write((article.Hits + 1).ToString());
        }

        #endregion
    }
}
