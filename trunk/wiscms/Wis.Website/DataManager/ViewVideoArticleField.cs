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
    /// ViewVideoArticleField 视图的字段
    /// </summary>
    public class ViewVideoArticleField
    {
        // 分类信息

        /// <summary>
        /// 分类编号。
        /// </summary>
        public const string CategoryId = "CategoryId";
        /// <summary>
        /// 分类编号。
        /// </summary>
        public const string CategoryGuid = "CategoryGuid";
        /// <summary>
        /// 分类名称。
        /// </summary>
        public const string CategoryName = "CategoryName";
        /// <summary>
        /// 父分类编号。
        /// </summary>
        public const string ParentGuid = "ParentGuid";
        /// <summary>
        /// 父分类名称。
        /// </summary>
        public const string ParentCategoryName = "ParentCategoryName";
        /// <summary>
        /// 分类排序等级。
        /// </summary>
        public const string CategoryRank = "CategoryRank";
        /// <summary>
        /// 分类下的记录数。
        /// </summary>
        public const string RecordCount = "RecordCount";
        
        /// <summary>
        /// 内容类型
        /// </summary>
        public const string ArticleType = "ArticleType";
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public const string ThumbnailWidth = "ThumbnailWidth";
        /// <summary>
        /// 缩略图高度
        /// </summary>
        public const string ThumbnailHeight = "ThumbnailHeight";

        // 内容信息

        /// <summary>
        /// 文章编号
        /// </summary>
        public const string ArticleId = "ArticleId";
        /// <summary>
        /// 文章编号
        /// </summary>
        public const string ArticleGuid = "ArticleGuid";
        /// <summary>
        /// 
        /// </summary>
        public const string MetaKeywords = "MetaKeywords";
        /// <summary>
        /// 
        /// </summary>
        public const string MetaDesc = "MetaDesc";
        /// <summary>
        /// 
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// 
        /// </summary>
        public const string TitleColor = "TitleColor";
        /// <summary>
        /// 
        /// </summary>
        public const string SubTitle = "SubTitle";
        /// <summary>
        /// 
        /// </summary>
        public const string Summary = "Summary";
        /// <summary>
        /// 
        /// </summary>
        public const string ContentHtml = "ContentHtml";
        /// <summary>
        /// 
        /// </summary>
        public const string Editor = "Editor";
        /// <summary>
        /// 
        /// </summary>
        public const string Author = "Author";
        /// <summary>
        /// 
        /// </summary>
        public const string Original = "Original";
        /// <summary>
        /// 文章等级，完成置顶功能
        /// </summary>
        public const string ArticleRank = "ArticleRank";
        /// <summary>
        /// 
        /// </summary>
        public const string Hits = "Hits";
        /// <summary>
        /// 
        /// </summary>
        public const string Comments = "Comments";
        /// <summary>
        /// 
        /// </summary>
        public const string Votes = "Votes";
        /// <summary>
        /// 创建日期
        /// </summary>
        public const string DateCreated = "DateCreated";

        // 视频信息

        /// <summary>
        /// 视频编号。
        /// </summary>
        public const string VideoArticleId = "VideoArticleId";
        /// <summary>
        /// 视频编号。
        /// </summary>
        public const string VideoArticleGuid = "VideoArticleGuid";
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
        /// 视频星级。
        /// </summary>
        public const string Star = "Star";
    }
}
