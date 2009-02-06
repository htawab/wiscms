//------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.Pager
{/// <summary>
    /// 
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 初始化。
        /// </summary>
        public Entity()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pagerColumn"></param>
        /// <param name="pagerColumnSort"></param>
        /// <param name="columnList"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="searchCondition"></param>
        public Entity(System.String tableName, System.String pagerColumn, System.Boolean pagerColumnSort, System.String columnList, System.Int32 pageSize, System.Int32 pageIndex, System.String searchCondition)
        {
            TableName = tableName;	// --表名
            PagerColumn = pagerColumn;	//--按该列来进行分页
            PagerColumnSort = pagerColumnSort;	//         --排序,0-顺序,1-倒序
            ColumnList = columnList;//--要查询出的字段列表,*表示全部字段
            PageSize = pageSize;		//       --每页记录数
            PageIndex = pageIndex;  	//        --指定页
            SearchCondition = searchCondition;// --查询条件
        }


        private System.String _TableName = System.String.Empty;

        /// <summary>
        /// 表名。
        /// </summary>
        public System.String TableName
        {
            set { _TableName = value; }
            get { return _TableName; }
        }
        private System.String _PagerColumn = System.String.Empty;

        /// <summary>
        /// 按该列来进行分页。
        /// </summary>
        public System.String PagerColumn
        {
            set { _PagerColumn = value; }
            get { return _PagerColumn; }
        }
        private System.Boolean _PagerColumnSort = false;

        /// <summary>
        /// 排序。
        /// </summary>
        public System.Boolean PagerColumnSort
        {
            set { _PagerColumnSort = value; }
            get { return _PagerColumnSort; }
        }
        private System.String _ColumnList = System.String.Empty;

        /// <summary>
        /// 要查询出的字段列表。
        /// </summary>
        public System.String ColumnList
        {
            set { _ColumnList = value; }
            get { return _ColumnList; }
        }
        private System.Int32 _PageSize = System.Int32.MinValue;

        /// <summary>
        /// 每页记录数。
        /// </summary>
        public System.Int32 PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize; }
        }
        private System.Int32 _PageIndex = System.Int32.MinValue;

        /// <summary>
        /// 指定页。
        /// </summary>
        public System.Int32 PageIndex
        {
            set { _PageIndex = value; }
            get { return _PageIndex; }
        }
        private System.String _SearchCondition = System.String.Empty;

        /// <summary>
        /// 查询条件。
        /// </summary>
        public System.String SearchCondition
        {
            set { _SearchCondition = value; }
            get { return _SearchCondition; }
        }
    }
}