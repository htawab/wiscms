//------------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data;

namespace Wis.Website.Pager
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager : AbstractManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCondition">查询条件。</param>
        /// <param name="orderList">排序列表。</param>
        /// <param name="pageSize">页大小。</param>
        /// <param name="pageIndex">当前页码。</param>
        /// <returns>返回 DataSet 数据集。</returns>
        public DataSet Query(Entity entity)
        {
            if (DataAccess == null) DataAccess = CreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[7];

            // 查询条件
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TableName";
            parameter.Value = entity.TableName;
            parameters[0] = parameter;

            // 排序列表
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@PagerColumn";
            parameter.Value =entity.PagerColumn;
            parameters[1] = parameter;

            // 页大小
            parameter = DataAccess.CreateDataParameter("bit");
            parameter.ParameterName = "@PagerColumnSort";
            parameter.Value = entity.PagerColumnSort;
            parameters[2] = parameter;

            // 页大小
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@ColumnList";
            parameter.Value = entity.ColumnList;
            parameters[3] = parameter;

            // 当前页码
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageSize";
            parameter.Value = entity.PageSize;
            parameters[4] = parameter;
            // 当前页码
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageIndex";
            parameter.Value = entity.PageIndex;
            parameters[5] = parameter;

            // 当前页码
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@SearchCondition";
            parameter.Value = entity.SearchCondition;
            parameters[6] = parameter;



            if (DataAccess.IsClosed) DataAccess.Open();
            System.Data.DataSet ds = DataAccess.ExecuteDataset(CommandType.StoredProcedure, "SP_Pager", parameters);
            DataAccess.Close();
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchCondition">查询条件。</param>
        /// <param name="orderList">排序列表。</param>
        /// <param name="pageSize">页大小。</param>
        /// <param name="pageIndex">当前页码。</param>
        /// <returns>返回 DataSet 数据集。</returns>
        public DataSet QueryMeeting(Entity entity)
        {
            if (DataAccess == null) DataAccess = MeetingCreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[7];

            // 查询条件
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TableName";
            parameter.Value = entity.TableName;
            parameters[0] = parameter;

            // 排序列表
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@PagerColumn";
            parameter.Value = entity.PagerColumn;
            parameters[1] = parameter;

            // 页大小
            parameter = DataAccess.CreateDataParameter("bit");
            parameter.ParameterName = "@PagerColumnSort";
            parameter.Value = entity.PagerColumnSort;
            parameters[2] = parameter;

            // 页大小
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@ColumnList";
            parameter.Value = entity.ColumnList;
            parameters[3] = parameter;

            // 当前页码
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageSize";
            parameter.Value = entity.PageSize;
            parameters[4] = parameter;
            // 当前页码
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageIndex";
            parameter.Value = entity.PageIndex;
            parameters[5] = parameter;

            // 当前页码
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@SearchCondition";
            parameter.Value = entity.SearchCondition;
            parameters[6] = parameter;



            if (DataAccess.IsClosed) DataAccess.Open();
            System.Data.DataSet ds = DataAccess.ExecuteDataset(CommandType.StoredProcedure, "SP_Pager", parameters);
            DataAccess.Close();
            return ds;
        }
    }
}