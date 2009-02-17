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
        /// 模版路径。
        /// </summary>
        public const string TemplatePath = "TemplatePath";
        /// <summary>
        /// 发布路径。
        /// </summary>
        public const string ReleasePath = "ReleasePath";
    }
}
