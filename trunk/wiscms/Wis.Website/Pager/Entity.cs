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
        /// ��ʼ����
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
            TableName = tableName;	// --����
            PagerColumn = pagerColumn;	//--�����������з�ҳ
            PagerColumnSort = pagerColumnSort;	//         --����,0-˳��,1-����
            ColumnList = columnList;//--Ҫ��ѯ�����ֶ��б�,*��ʾȫ���ֶ�
            PageSize = pageSize;		//       --ÿҳ��¼��
            PageIndex = pageIndex;  	//        --ָ��ҳ
            SearchCondition = searchCondition;// --��ѯ����
        }


        private System.String _TableName = System.String.Empty;

        /// <summary>
        /// ������
        /// </summary>
        public System.String TableName
        {
            set { _TableName = value; }
            get { return _TableName; }
        }
        private System.String _PagerColumn = System.String.Empty;

        /// <summary>
        /// �����������з�ҳ��
        /// </summary>
        public System.String PagerColumn
        {
            set { _PagerColumn = value; }
            get { return _PagerColumn; }
        }
        private System.Boolean _PagerColumnSort = false;

        /// <summary>
        /// ����
        /// </summary>
        public System.Boolean PagerColumnSort
        {
            set { _PagerColumnSort = value; }
            get { return _PagerColumnSort; }
        }
        private System.String _ColumnList = System.String.Empty;

        /// <summary>
        /// Ҫ��ѯ�����ֶ��б�
        /// </summary>
        public System.String ColumnList
        {
            set { _ColumnList = value; }
            get { return _ColumnList; }
        }
        private System.Int32 _PageSize = System.Int32.MinValue;

        /// <summary>
        /// ÿҳ��¼����
        /// </summary>
        public System.Int32 PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize; }
        }
        private System.Int32 _PageIndex = System.Int32.MinValue;

        /// <summary>
        /// ָ��ҳ��
        /// </summary>
        public System.Int32 PageIndex
        {
            set { _PageIndex = value; }
            get { return _PageIndex; }
        }
        private System.String _SearchCondition = System.String.Empty;

        /// <summary>
        /// ��ѯ������
        /// </summary>
        public System.String SearchCondition
        {
            set { _SearchCondition = value; }
            get { return _SearchCondition; }
        }
    }
}