using System;
using System.Web;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Wis.Website.AjaxRequests
{
    class CommentHttpHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string command = (context.Request["command"] == null) ? string.Empty : context.Request["command"];
            if (command == "Hits")
                this.Hits(context);
            else if (command == "Comments")
                this.Comments(context);
            else if (command == "CommentsInsert")
                this.CommentsInsert(context);
            else if (command == "CommentsLoad")
                this.CommentsLoad(context);
            //else if (command == "CategoryDel")
            //    this.CategoryDel(context);
            //else if (command == "CategoryList")
            //    this.CategoryList(context);
        }

        public void Hits(HttpContext context)
        {
            string articleId = context.Request["ArticleId"];
            if(!Wis.Toolkit.Validator.IsInt(articleId))
            {
             context.Response.Write(0);
                return;
            }
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format("select ArticleId from Article where ArticleId ={0}", articleId);
                object o = dataProvider.ExecuteScalar(commandText);
                if (o == null)
                {
                    context.Response.Write(0);
                    dataProvider.Close();
                    return;
                }
                commandText = string.Format("update Article set Hits = Hits + 1 where ArticleId ={0}", articleId);
                dataProvider.ExecuteNonQuery(commandText);
                commandText = string.Format("select Hits from Article where ArticleId={0}", articleId);
                o = dataProvider.ExecuteScalar(commandText);
                dataProvider.Close();
                context.Response.Write(o.ToString());
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Open();
                context.Response.Write(0);
                return;
            }
        }
        public void Comments(HttpContext context)
        {
            string articleId = context.Request["ArticleId"];
            if (!Wis.Toolkit.Validator.IsInt(articleId))
            {
                context.Response.Write(0);
                return;
            }
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format("select ArticleId from Article where ArticleId ={0}", articleId);
                object o = dataProvider.ExecuteScalar(commandText);
                if (o == null)
                {
                    context.Response.Write(0);
                    dataProvider.Close();
                    return;
                }
                commandText = string.Format("select Comments from Article where ArticleId={0}", articleId);
                o = dataProvider.ExecuteScalar(commandText);
                dataProvider.Close();
                context.Response.Write(o.ToString());
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Open();
                context.Response.Write(0);
                return;
            }
        }

        public void CommentsInsert(HttpContext context)
        {
            string articleId = context.Request["ArticleId"];
            if (!Wis.Toolkit.Validator.IsInt(articleId))
            {
                context.Response.Write(0);
                return;
            }
            string Title = context.Request["Title"];
            string ContentHtml = context.Request["ContentHtml"];
            if (string.IsNullOrEmpty(ContentHtml))
            {
                context.Response.Write(0);
                return;
            }
            string Commentator = "";
            //获取用户姓名
            if (string.IsNullOrEmpty(Commentator))
                Commentator = "匿名用户";
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            string commandText = string.Format("select ArticleGuid from Article where ArticleId ={0}", articleId);
            object o = dataProvider.ExecuteScalar(commandText);
            if (o == null)
            {
                context.Response.Write(0);
                dataProvider.Close();
                return;
            }
            dataProvider.BeginTransaction();
            try
            {

                commandText = string.Format("insert into Comment(ObjectGuid,Title,Commentator,ContentHtml,Original) values('{0}',N'{1}',N'{2}',N'{3}',N'{4}')", o, Title.Replace("'", "\""), Commentator, ContentHtml.Replace("'", "\""), Wis.Toolkit.RequestManager.GetClientIP());
                dataProvider.ExecuteNonQuery(commandText);
                commandText = string.Format("update Article set Comments = Comments + 1 where ArticleId ={0}", articleId);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.CommitTransaction();
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            catch
            {
                if (dataProvider.HasTransaction) dataProvider.RollbackTransaction();
                if (!dataProvider.IsClosed) dataProvider.Open();
                context.Response.Write(0);
                return;
            }
        }
      
        /// <summary>
        /// 读取评论
        /// </summary>
        /// <param name="context"></param>
        public void CommentsLoad(HttpContext context)
        {
            string articleId = context.Request["ArticleId"];
            if (!Wis.Toolkit.Validator.IsInt(articleId))
            {
                context.Response.Write(0);
                return;
            }
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
                dataProvider.Open();
                string commandText = string.Format("select * from Comment where ObjectGuid in(select ArticleGuid from Article where ArticleId ={0}) order by CommentId", articleId);
                System.Data.IDataReader dataReader = dataProvider.ExecuteReader(commandText);
                while (dataReader.Read())
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader["Title"].ToString());
                    sb.Append(","); //                     
                    sb.Append(dataReader["Commentator"].ToString());
                    sb.Append(","); //   
                    sb.Append(dataReader["ContentHtml"].ToString());
                    sb.Append(","); //  
                    sb.Append(dataReader["Original"].ToString());
                    sb.Append(","); // 
                    sb.Append(System.Convert.ToDateTime(dataReader["DateCreated"]).ToString("yyyy-mm-dd HH:mm"));
                    sb.Append(","); // 
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                dataReader.Close();
                dataProvider.Close();
                xtw.WriteEndElement();// Live -> L
                context.Response.Write(sw.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public void Exists(HttpContext context)
        {
            if (context == null) return;
        }

        #endregion
    }
}
