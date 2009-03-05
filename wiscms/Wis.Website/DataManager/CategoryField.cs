using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 分类。
    /// </summary>
    public class CategoryField
    {
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
        /// 排序等级。
        /// </summary>
        public const string Rank = "Rank";
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
    }
}
