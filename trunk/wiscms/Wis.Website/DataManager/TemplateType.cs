//------------------------------------------------------------------------------
// <copyright file="TemplateType.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 模版类型。
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// 单页。
        /// </summary>
        Page = 1,
        /// <summary>
        /// 不带分页的索引页
        /// </summary>
        Index = 2,
        /// <summary>
        /// 普通文章详细页。
        /// </summary>
        ArticleItem = 3,
        /// <summary>
        /// 普通文章列表页。
        /// </summary>
        ArticleList = 4,
        /// <summary>
        /// 普通文章索引页。
        /// </summary>
        ArticleIndex = 5,
        /// <summary>
        /// 图片文章详细页
        /// </summary>
        PhotoArticleItem = 6,
        /// <summary>
        /// 图片文章列表页
        /// </summary>
        PhotoArticleList = 7,
        /// <summary>
        /// 图片文章列表页
        /// </summary>
        PhotoArticleIndex = 8,
        /// <summary>
        /// 视频新闻详细页
        /// </summary>
        VideoArticleItem = 9,
        /// <summary>
        /// 视频新闻列表页
        /// </summary>
        VideoArticleList = 10,
        /// <summary>
        /// 视频新闻索引页
        /// </summary>
        VideoArticleIndex = 11,
        /// <summary>
        /// 专题列表页
        /// </summary>
        SpecialItem = 12,
        /// <summary>
        /// 专题列表页
        /// </summary>
        SpecialList = 13,
        /// <summary>
        /// 专题列表页
        /// </summary>
        SpecialIndex = 14
    }
}
