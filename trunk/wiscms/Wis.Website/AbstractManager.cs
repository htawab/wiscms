//------------------------------------------------------------------------------
// <copyright file="AbstractManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Configuration;
namespace Wis.Website
{
    /// <summary>
    /// AbstractManager ��ժҪ˵����
    /// </summary>
    public abstract class AbstractManager
    {
        /// <summary>
        /// ���ݿ���ʽӿڡ�
        /// </summary>
        private Wis.Toolkit.DataAccess.IDataAccess dataAccess;

        /// <summary>
        /// ���ݿ���ʽӿڡ�
        /// </summary>
        public Wis.Toolkit.DataAccess.IDataAccess DataAccess
        {
            get { return dataAccess; }
            set { dataAccess = value; }
        }

        public Wis.Toolkit.DataAccess.IDataAccess CreateDataAccess()
        {
            if (dataAccess == null)
            {
                dataAccess = Wis.Toolkit.DataAccess.DataAccessFactory.CreateDataAccess(Website.Setting.ConnectionString, "SqlServer");
            }

            return dataAccess;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Wis.Toolkit.DataAccess.IDataAccess MeetingCreateDataAccess()
        {
            if (dataAccess == null)
            {

                dataAccess = Wis.Toolkit.DataAccess.DataAccessFactory.CreateDataAccess(Website.Setting.MeetingConnectionString, "SqlServer");
            }

            return dataAccess;
        }
    }
}
