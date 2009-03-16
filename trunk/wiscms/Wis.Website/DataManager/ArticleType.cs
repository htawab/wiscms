//------------------------------------------------------------------------------
// <copyright file="ArticleType.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 内容类型。
    /// </summary>
    public enum ArticleType
    {
        /// <summary>
        /// 普通新闻。
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 图片新闻。
        /// </summary>
        Photo = 2,
        /// <summary>
        /// 视频新闻。
        /// </summary>
        Video = 3,
        /// <summary>
        /// 软件。
        /// </summary>
        Soft = 4,
        /// <summary>
        /// 链接。
        /// </summary>
        Link = 5
    }
}