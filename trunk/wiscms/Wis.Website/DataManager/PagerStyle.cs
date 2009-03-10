//------------------------------------------------------------------------------
// <copyright file="PagerStyle.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 分页样式
    /// </summary>
    public enum PagerStyle
    {
        /// <summary>
        /// 无，对应的数据库记录为DBNull
        /// </summary>
        None = 0,
        /// <summary>
        /// 首页 上一页 下一页 尾页
        /// </summary>
        Tiny = 1,
        /// <summary>
        /// 第 1 页/共 5 页 首页 上一页 1 2 3 4 5 下一页 尾页
        /// </summary>
        Normal = 2
    }
}
