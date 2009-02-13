//------------------------------------------------------------------------------
// <copyright file="VerifyCodeHttpHandler.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Web;
using Wis.Toolkit.Drawings;

namespace Wis.Website.AjaxRequests
{
    /// <summary>
    /// 评论时的验证码校验
    /// </summary>
    public class CommentVerifyHttpHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public const string ConstCommentVerify = "Verify";

        public void ProcessRequest(HttpContext context)
        {
            TextToImage textToImage = new TextToImage();
            string randText = textToImage.CreateRandText();
            textToImage.Write(randText, context);

            // 保存验证码
            context.Session[ConstCommentVerify] = randText;
        }

        #endregion
    }
}
