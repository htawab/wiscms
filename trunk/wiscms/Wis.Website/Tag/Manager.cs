//------------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data;

namespace Wis.Website.Tag
{
    /// <summary>
    /// 
    /// </summary>
    public class Manager : AbstractManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchGuid"></param>
        /// <returns></returns>
        public Entity Load(string tagName)
        {
            if (DataAccess == null) DataAccess = CreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[1];

            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TagName";
            parameter.Value = tagName;
            parameters[0] = parameter;

            // *ExecuteScalar
            if (DataAccess.IsClosed) DataAccess.Open();
            object obj = DataAccess.ExecuteScalar(CommandType.StoredProcedure, "SP_Tag_Exists", parameters);
            if (obj == null)
            {
                if (DataAccess.HasTransaction) DataAccess.RollbackTransaction();
                DataAccess.Close();
                return null; // 记录不存在，装载数据操作失败
            }

            Entity entity = new Entity();

            // *Add Cmd Parameter
            parameters = new IDataParameter[1];

            parameter = DataAccess.CreateDataParameter("nvarchar");
            parameter.ParameterName = "@TagName";
            parameter.Value = tagName;
            parameters[0] = parameter;

            // *ExecuteReader
            if (DataAccess.IsClosed) DataAccess.Open();
            //IDataReader dataReader 
            System.Data.DataSet ds = DataAccess.ExecuteDataset(CommandType.StoredProcedure, "SP_Tag_Load", parameters);
            System.Data.DataTable dt = ds.Tables[0];
            foreach (System.Data.DataRow dataReader in dt.Rows)
            {
                entity.TagId = (System.Int32)dataReader["TagId"];
                entity.TagGuid = (System.Guid)dataReader["TagGuid"];
                if (!dataReader["ObjectGuid"].Equals(System.DBNull.Value))
                entity.ObjectGuid = (System.Guid)dataReader["ObjectGuid"];
                if (!dataReader["TagName"].Equals(System.DBNull.Value))
                    entity.TagName = dataReader["TagName"].ToString();
                if (!dataReader["Description"].Equals(System.DBNull.Value))
                    entity.Description = dataReader["Description"].ToString();
                if (!dataReader["ContentHtml"].Equals(System.DBNull.Value))
                    entity.ContentHtml = dataReader["ContentHtml"].ToString();
                if (!dataReader["DateCreated"].Equals(System.DBNull.Value))
                    entity.DateCreated = (System.DateTime)dataReader["DateCreated"];
            }
            //dataReader.Close();

            // *Close Connection
            if (!DataAccess.HasTransaction) DataAccess.Close();

            return entity;
        }


    }
}