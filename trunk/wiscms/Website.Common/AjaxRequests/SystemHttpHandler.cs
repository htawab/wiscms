using System;
using System.Web;
using System.Globalization;
using System.IO;
using System.Xml;
namespace Wis.Website.AjaxRequests
{
    class SystemHttpHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string command = (context.Request["command"] == null) ? string.Empty : context.Request["command"];
            if (command == "TagsList")
                this.TagsList(context);
            else if (command == "TagsCount")
                this.TagsCount(context);
            else if (command == "CategoryCount")
                this.CategoryCount(context);
            else if (command == "TagDel")
                this.TagDel(context);
            else if (command == "CategoryDel")
                this.CategoryDel(context);
            else if (command == "CategoryList")
                this.CategoryList(context);
            else if (command == "LinkCount")
                this.LinkCount(context);
            else if (command == "LinkList")
                this.LinkList(context);
            else if (command == "LinkDel")
                this.LinkDel(context);
            else if (command == "linkhtml")
                this.linkhtml(context);
        }

        public void linkhtml(HttpContext context)
        {
            if (WriteHtml.linkhtml())
                context.Response.Write(1);
            else
                context.Response.Write(0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void LinkCount(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string commandText = string.Format("select count(LinkId) from Link");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void LinkList(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");


            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            string searchCondition = "1=1";

            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "Link";
            entity.ColumnList = "*";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "LinkId";
            entity.PageSize = 20;
            entity.PagerColumnSort = true;
            entity.SearchCondition = searchCondition;
            Wis.Website.Pager.Manager manager = new Wis.Website.Pager.Manager();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                System.Data.DataSet ds = manager.Query(entity);
                foreach (System.Data.DataRow dataReader in ds.Tables[0].Rows)
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader["LinkId"].ToString());
                    sb.Append(","); //                     
                    sb.Append(dataReader["LinkName"].ToString());
                    sb.Append(","); //   
                    sb.Append(dataReader["LinkUrl"].ToString());
                    sb.Append(","); //  
                    sb.Append(dataReader["Logo"].ToString());
                    sb.Append(","); //
                    sb.Append(dataReader["Sequence"].ToString());
                    sb.Append(","); //
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void LinkDel(HttpContext context)
        {
            string linkId = context.Request["LinkId"];
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {

                string commandText = string.Format("delete from Link where LinkId ={0}", linkId);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                WriteHtml.linkhtml();

                context.Response.Write(1);
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void TagsList(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");


            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            string searchCondition = "1=1";

            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "TemplateLabel";
            entity.ColumnList = "*";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "TemplateLabelId";
            entity.PageSize = 20;
            entity.PagerColumnSort = true;
            entity.SearchCondition = searchCondition;
            Wis.Website.Pager.Manager manager = new Wis.Website.Pager.Manager();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                System.Data.DataSet ds = manager.Query(entity);
                foreach (System.Data.DataRow dataReader in ds.Tables[0].Rows)
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader["TemplateLabelId"].ToString());
                    sb.Append(":"); //                     
                    sb.Append("$Tag_" + dataReader["TemplateLabelName"].ToString() + "$");
                    sb.Append(":"); //   
                    sb.Append(dataReader["Description"].ToString());
                    sb.Append(":"); //  
                 
                    sb.Append(System.Convert.ToDateTime(dataReader["DateCreated"].ToString()).ToShortDateString());
                    sb.Append(":"); // 
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void TagsCount(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }

            string commandText = string.Format("select count(TemplateLabelId) from TemplateLabel");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void CategoryCount(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }

            string commandText = string.Format("select count(CategoryId) from Category");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void CategoryList(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");


            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            string searchCondition = "1=1";

            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "Category";
            entity.ColumnList = "*";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "CategoryId";
            entity.PageSize = 20;
            entity.PagerColumnSort = false;
            entity.SearchCondition = searchCondition;
            Wis.Website.Pager.Manager manager = new Wis.Website.Pager.Manager();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                System.Data.DataSet ds = manager.Query(entity);
                foreach (System.Data.DataRow dataReader in ds.Tables[0].Rows)
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader[DataManager.CategoryField.CategoryId].ToString());
                    sb.Append(":"); //                     
                    sb.Append(dataReader[DataManager.CategoryField.CategoryName].ToString());
                    sb.Append(":"); //   
                    sb.Append(dataReader[DataManager.CategoryField.ParentGuid].ToString());
                    sb.Append(":"); //  
                    sb.Append(ParentName((Guid)dataReader[DataManager.CategoryField.ParentGuid]));
                    sb.Append(":"); // 
                    sb.Append(dataReader[DataManager.CategoryField.Rank].ToString());
                    sb.Append(":"); //  
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }

        public string ParentName(Guid ParentGuid)
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format("select CategoryName from Category where CategoryGuid ={0}", ParentGuid);
                object o =  dataProvider.ExecuteScalar(commandText);
                dataProvider.Close();
                if (o == null)
                    return "";
                return o.ToString();
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                return "";
            }
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="context"></param>
        public void DleteFile(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string FileId = context.Request["FileId"];
            string commandText = string.Format("delete from [File] where FileId ={0}", FileId);
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            catch
            {
                                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
            }
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="context"></param>
        public void TagDel(HttpContext context)
        {
            string tagId = context.Request["TagId"];
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format("delete from TemplateLabel where TemplateLabelId ={0}", tagId);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();

                context.Response.Write(1);
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
            }
        }

        public void CategoryDel(HttpContext context)
        {
            string categoryId = context.Request["CategoryId"];
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            try
            {
                string commandText = string.Format("delete from Category where CategoryId ={0}", categoryId);
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
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
