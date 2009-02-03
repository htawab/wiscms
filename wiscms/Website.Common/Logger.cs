using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Website
{
    public class Logger
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="title"></param>
        /// <param name="objectGuid"></param>
        /// <param name="Message"></param>
        public static void LoggerInsert(Guid userGuid, string title, Guid objectGuid, string Message)
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format(@"insert into Log(UserGuid,Title,SubmissionGuid,Message) values
('{0}',N'{1}','{2}',N'{3}')", userGuid, title, objectGuid, Message);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void dsfs()
        { 
        
        }

    }
}
