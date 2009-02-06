//------------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data;

namespace Wis.Website.Page
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
        public Entity Load(int pageId)
        {
            if (DataAccess == null) DataAccess = CreateDataAccess();

            // *Add Cmd Parameter
            IDataParameter parameter;
            IDataParameter[] parameters = new IDataParameter[1];

            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageId";
            parameter.Value = pageId;
            parameters[0] = parameter;

            // *ExecuteScalar
            if (DataAccess.IsClosed) DataAccess.Open();
            object obj = DataAccess.ExecuteScalar(CommandType.StoredProcedure, "SP_Article_Exists", parameters);
            if (obj == null)
            {
                if (DataAccess.HasTransaction) DataAccess.RollbackTransaction();
                DataAccess.Close();
                return null; // 记录不存在，装载数据操作失败
            }

            Entity entity = new Entity();

            // *Add Cmd Parameter
            parameters = new IDataParameter[1];

            parameter = DataAccess.CreateDataParameter("int");
            parameter.ParameterName = "@PageId";
            parameter.Value = pageId;
            parameters[0] = parameter;

            // *ExecuteReader
            if (DataAccess.IsClosed) DataAccess.Open();
            //IDataReader dataReader 
            System.Data.DataSet ds = DataAccess.ExecuteDataset(CommandType.StoredProcedure, "SP_Article_Load", parameters);
            System.Data.DataTable dt = ds.Tables[0];
            foreach (System.Data.DataRow dataReader in dt.Rows)
            {
                entity.PageId = (System.Int32)dataReader["PageId"];
                entity.PageGuid = (System.Guid)dataReader["PageGuid"];
                entity.CategoryId = (System.Int32)dataReader["CategoryId"];
              
                if (!dataReader["MetaKeywords"].Equals(System.DBNull.Value))
                    entity.MetaKeywords = dataReader["MetaKeywords"].ToString();
                if (!dataReader["MetaDesc"].Equals(System.DBNull.Value))
                    entity.MetaDesc = dataReader["MetaDesc"].ToString();
                if (!dataReader["Title"].Equals(System.DBNull.Value))
                    entity.Title = dataReader["Title"].ToString();
                if (!dataReader["ContentHtml"].Equals(System.DBNull.Value))
                    entity.ContentHtml = dataReader["ContentHtml"].ToString();
                if (!dataReader["TemplatePath"].Equals(System.DBNull.Value))
                    entity.TemplatePath = dataReader["TemplatePath"].ToString();
                if (!dataReader["ReleasePath"].Equals(System.DBNull.Value))
                    entity.ReleasePath = dataReader["ReleasePath"].ToString();
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