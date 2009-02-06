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
        /// <param name="searchCondition">��ѯ������</param>
        /// <param name="orderList">�����б�</param>
        /// <param name="pageSize">ҳ��С��</param>
        /// <param name="pageIndex">��ǰҳ�롣</param>
        /// <returns>���� DataSet ���ݼ���</returns>
        public DataSet Query(Entity entity)
        {
            if (DataAccess == null) DataAccess = CreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[7];

            // ��ѯ����
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TableName";
            parameter.Value = entity.TableName;
            parameters[0] = parameter;

            // �����б�
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@PagerColumn";
            parameter.Value =entity.PagerColumn;
            parameters[1] = parameter;

            // ҳ��С
            parameter = DataAccess.CreateDataParameter("bit");
            parameter.ParameterName = "@PagerColumnSort";
            parameter.Value = entity.PagerColumnSort;
            parameters[2] = parameter;

            // ҳ��С
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@ColumnList";
            parameter.Value = entity.ColumnList;
            parameters[3] = parameter;

            // ��ǰҳ��
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageSize";
            parameter.Value = entity.PageSize;
            parameters[4] = parameter;
            // ��ǰҳ��
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageIndex";
            parameter.Value = entity.PageIndex;
            parameters[5] = parameter;

            // ��ǰҳ��
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
        /// <param name="searchCondition">��ѯ������</param>
        /// <param name="orderList">�����б�</param>
        /// <param name="pageSize">ҳ��С��</param>
        /// <param name="pageIndex">��ǰҳ�롣</param>
        /// <returns>���� DataSet ���ݼ���</returns>
        public DataSet QueryMeeting(Entity entity)
        {
            if (DataAccess == null) DataAccess = MeetingCreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[7];

            // ��ѯ����
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TableName";
            parameter.Value = entity.TableName;
            parameters[0] = parameter;

            // �����б�
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@PagerColumn";
            parameter.Value = entity.PagerColumn;
            parameters[1] = parameter;

            // ҳ��С
            parameter = DataAccess.CreateDataParameter("bit");
            parameter.ParameterName = "@PagerColumnSort";
            parameter.Value = entity.PagerColumnSort;
            parameters[2] = parameter;

            // ҳ��С
            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@ColumnList";
            parameter.Value = entity.ColumnList;
            parameters[3] = parameter;

            // ��ǰҳ��
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageSize";
            parameter.Value = entity.PageSize;
            parameters[4] = parameter;
            // ��ǰҳ��
            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageIndex";
            parameter.Value = entity.PageIndex;
            parameters[5] = parameter;

            // ��ǰҳ��
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