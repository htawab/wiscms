//------------------------------------------------------------------------------
// <copyright file="VideoArticleField.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// VideoArticle 表的字段
    /// </summary>
    public class VideoArticleField
    {
        /// <summary>
        /// 视频编号。
        /// </summary>
        public const string VideoArticleId = "VideoArticleId";
        /// <summary>
        /// 视频编号。
        /// </summary>
        public const string VideoArticleGuid = "VideoArticleGuid";
        /// <summary>
        /// 文章编号。
        /// </summary>
        public const string ArticleGuid = "ArticleGuid";
        /// <summary>
        /// 视频路径。
        /// </summary>
        public const string VideoPath = "VideoPath";
        /// <summary>
        /// Flv视频路径。
        /// </summary>
        public const string FlvVideoPath = "FlvVideoPath";
        /// <summary>
        /// 缩略图路径。
        /// </summary>
        public const string PreviewFramePath = "PreviewFramePath";
        /// <summary>
        /// 星级。
        /// </summary>
        public const string Star = "Star";
    }
}
