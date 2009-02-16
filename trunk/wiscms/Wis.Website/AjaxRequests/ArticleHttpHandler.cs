using System;
using System.Web;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Collections.Generic;
namespace Wis.Website.AjaxRequests
{
    class ArticleHttpHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string command = (context.Request["command"] == null) ? string.Empty : context.Request["command"];
            if (command == "ArticleList")
                this.ArticleList(context);
            else if (command == "Count")
                this.Count(context);
            else if (command == "DleteFile")
                this.DleteFile(context);
            else if (command == "ArticleDel")
                this.ArticleDel(context);
            else if (command == "ArticleDelAll")
                this.ArticleDelAll(context);
            else if (command == "CategoryList")
                this.CategoryList(context);
            else if (command == "PageCount")
                this.PageCount(context);
            else if (command == "PageList")
                this.PageList(context);
            else if (command == "PageDel")
                this.PageDel(context);
            else if (command == "CategoryHtml")
                CategoryHtml(context);

        }
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="context"></param>
        public void ArticleList(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            string CategoryId = context.Request["CategoryId"];
            string keysword = context.Request["keysword"];
            string type = context.Request["type"];

            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            string searchCondition = "";

            if (!string.IsNullOrEmpty(CategoryId))
            {
                searchCondition = string.Format(@"( CategoryId = {0})", CategoryId); //  or CategoryId in (select CategoryId  from Category where ParentGuid = {0})
                if (!string.IsNullOrEmpty(keysword))
                    searchCondition += string.Format("and {0} like '%{1}%' ", type, keysword.Replace("'", ""));
            }
            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "View_Article";
            entity.ColumnList = "*";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "ArticleId";
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
                    sb.Append(dataReader["ArticleId"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["CategoryId"].ToString());
                    sb.Append(";"); //   
                    sb.Append(dataReader["CategoryName"].ToString());
                    sb.Append(";"); //  
                    sb.Append(dataReader["ImagePath"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["TitleColor"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["Author"].ToString());
                    sb.Append(";"); // 
                    sb.Append(Wis.Toolkit.Utility.StringUtility.TruncateString(dataReader["Title"].ToString(), 28));
                    sb.Append(";"); // 
                    sb.Append(System.Convert.ToDateTime(dataReader["DateCreated"]).ToString("yyyy-mm-dd"));
                    sb.Append(";"); // 
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }
        /// <summary>
        /// 新闻记录数。

        /// </summary>
        /// <param name="context"></param>
        public void Count(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string CategoryId = context.Request["CategoryId"];
            string keysword = context.Request["keysword"];
            string type = context.Request["type"];
            string searchCondition = "";
            if (!string.IsNullOrEmpty(CategoryId))
            {
                //  or CategoryId in (select CategoryId  from Category where  ParentGuid = {0})
                searchCondition = string.Format(@" where ( CategoryId = {0})", CategoryId);
                if (!string.IsNullOrEmpty(keysword))
                    searchCondition += string.Format("and {0} like '%{1}%' ", type, keysword.Replace("'", ""));
            }

            string commandText = string.Format("select count(ArticleId) from View_Article {0}", searchCondition);
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
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
        public void ArticleDel(HttpContext context)
        {
            string ArticleId = context.Request["ArticleId"];
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            string commandText = string.Format("select * from Article where ArticleId= {0}", ArticleId);
            System.Data.DataTable dt  =dataProvider.ExecuteDataset(commandText).Tables[0];
            if (dt.Rows.Count < 1)
            {
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            //删除新闻html页面
             WriteHtml.DelHtml(dt.Rows[0]["ReleasePath"].ToString());
            dataProvider.BeginTransaction();
            try
            {
                commandText = string.Format("delete from [File] where SubmissionGuid in(select ArticleGuid from Article where ArticleId ={0} )", ArticleId);
                //删除附件
                dataProvider.ExecuteNonQuery(commandText);
                //删除新闻
                commandText = string.Format("delete from Article where ArticleId= {0}", ArticleId);
                dataProvider.ExecuteNonQuery(commandText);
                //更新html
                dataProvider.CommitTransaction();
                dataProvider.Close();
                //WriteHtml.GetCategoryListhtml((int)dt.Rows[0]["CategoryId"]);
                Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "删除新闻 " + dt.Rows[0]["Title"].ToString(), Guid.Empty, dt.Rows[0]["ContentHtml"].ToString());
                context.Response.Write(1);
                return;
            }
            catch
            {
                if (dataProvider.HasTransaction) dataProvider.RollbackTransaction();
                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
            }
       

        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="context"></param>
        public void ArticleDelAll(HttpContext context)
        {
            string articleId = context.Request["ArticleId"];
            string[] ArticleId = articleId.Split(',');
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            dataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < ArticleId.Length; i++)
                {
                    if (Wis.Toolkit.Validator.IsInt(ArticleId[i]))
                    {
                        string commandText = string.Format("delete from Files where SubmissionGuid in(select ArticleGuid from Article where ArticleId ={0} )", ArticleId[i]);
                        dataProvider.ExecuteNonQuery(commandText);
                        commandText = string.Format("delete from Article where ArticleId= {0}", ArticleId[i]);
                        dataProvider.ExecuteNonQuery(commandText);
                    }
                }
                dataProvider.CommitTransaction();
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            catch
            {
                if (dataProvider.HasTransaction) dataProvider.RollbackTransaction();
                if (!dataProvider.IsClosed) dataProvider.Close();
                context.Response.Write(0);
                return;
            }
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <param name="context"></param>
        public void CategoryList(HttpContext context)
        {
            Guid parentGuid;
            string parentId = context.Request["ParentId"];
            string commandText;
            string stlyeclass = "";
            if (string.IsNullOrEmpty(parentId))
                parentId = "0";
            if (parentId == "0")
            {
                parentGuid = Guid.Empty;
                commandText = string.Format("select * from Category where ParentGuid = '{0}'", Guid.Empty);

            }
            else
            {
                commandText = string.Format("select * from Category where ParentGuid in (select CategoryGuid from Category where CategoryId ={0}) ", parentId);
                stlyeclass = "&nbsp;&nbsp;&nbsp;";
            }
                // TODO: 这个方法在哪里的Ajax用到了？
            string liststr = "";
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
        
            
                
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandText).Tables[0];
            foreach (System.Data.DataRow drow in dt.Rows)
            {
                commandText = string.Format("select count(CategoryId) from Category where ParentGuid = '{0}'", drow["CategoryGuid"].ToString());
                int o = (int)dataProvider.ExecuteScalar(commandText);
                if (o > 0)
                {
                    liststr += "<div class=\"Parent\" id=\"Category" + drow["CategoryId"] + "\">" + stlyeclass + "<img src=\"../images/ico1.gif\"  style=\"cursor:hand;\" width=\"11\" height=\"11\" alt=\"点击展开子栏目\"  border=\"0\"  onClick=\"javascript:SwitchImg(this,'" + drow["CategoryId"] + "');\" />&nbsp;<a  href=\"ArticleList.aspx?CategoryId=" + drow["CategoryId"] + "\" onClick=\"javascript:Change('" + drow["CategoryId"] + "');\"  target=\"main\" >" + drow["CategoryName"] + "</a></div><div id=\"Parent" + drow["CategoryId"] + "\"  HasSub=\"True\" style=\"height:100%;display:none;\"></div>";
                }
                else
                {
                    liststr += "<div class=\"Parent\" id=\"Category" + drow["CategoryId"] + "\">" + stlyeclass + "<img src=\"../images/ico2.gif\" width=\"11\" height=\"11\" alt=\"没有子栏目\"  border=\"0\"  />&nbsp;<a  href=\"ArticleList.aspx?CategoryId=" + drow["CategoryId"] + "\" onClick=\"javascript:Change('" + drow["CategoryId"] + "');\" target=\"main\" >" + drow["CategoryName"] + "</a></div>";
                }
            }

            //// 读取单页面列表

            //if (parentGuid == "0")
            //{
            //    commandText = string.Format("select * from Page");
            //    System.Data.DataTable pageDt = dataProvider.ExecuteDataset(commandText).Tables[0];
            //    if (pageDt.Rows.Count > 0)
            //    {
            //        liststr += "<div class=\"Parent\" id=\"CategoryPage0\"><img src=\"../images/ico1.gif\"  style=\"cursor:hand;\" width=\"11\" height=\"11\" alt=\"点击展开子栏目\"  border=\"0\"  onClick=\"javascript:SwitchImgPage(this);\" />&nbsp;<a  href=\"PageList.aspx\" onClick=\"javascript:Change('Page0');\"  target=\"main\" >单页面</a></div>";
            //        liststr += "<div id=\"Page\"  HasSub=\"True\" style=\"height:100%;display:none;\">";//</div>
            //        foreach (System.Data.DataRow pageDrow in pageDt.Rows)
            //        {
            //            liststr += "<div class=\"Parent\" id=\"CategoryPage" + pageDrow["PageId"] + "\">&nbsp;&nbsp;&nbsp;<img src=\"../images/ico2.gif\" width=\"11\" height=\"11\" alt=\"没有子栏目\"  border=\"0\"  />&nbsp;<a  href=\"PageUpdate.aspx?PageId=" + pageDrow["PageId"] + "\" onClick=\"javascript:Change('Page" + pageDrow["PageId"] + "');\" target=\"main\" >" + pageDrow["Title"] + "</a></div>";
            //        }
            //        liststr += "</div>";
            //    }
            //    else
            //    {
            //        liststr += "<div class=\"Parent\" id=\"CategoryPage0\"><img src=\"../images/ico2.gif\" width=\"11\" height=\"11\" alt=\"没有子栏目\"  border=\"0\"  />&nbsp;<a  href=\"PageList.aspx\" onClick=\"javascript:Change('Page0');\" target=\"main\" >单页面</a></div>";
            //    }
            //}
            dataProvider.Close();
            context.Response.Write(liststr);
            return;
        }
        /// <summary>
        /// 单页记录数

        /// </summary>
        /// <param name="context"></param>
        public void PageCount(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string commandText = string.Format("select count(PageId) from Page");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
        }
        /// <summary>
        /// 单页列表
        /// </summary>
        /// <param name="context"></param>
        public void PageList(HttpContext context)
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
            entity.TableName = "Page";
            entity.ColumnList = "*";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "PageId";
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
                    sb.Append(dataReader["PageId"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["CategoryId"].ToString());
                    sb.Append(";"); // 
                    sb.Append(CategoryName(dataReader["CategoryId"].ToString()));
                    sb.Append(";"); //  
                    sb.Append(dataReader["Enable"].ToString());
                    sb.Append(";"); // 
                    sb.Append(Wis.Toolkit.Utility.StringUtility.TruncateString(dataReader["Title"].ToString(), 25));
                    sb.Append(";"); // 
                    sb.Append(System.Convert.ToDateTime(dataReader["DateCreated"]).ToString("yyyy-mm-dd"));
                    sb.Append(";"); // 
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }
        /// <summary>
        /// 删除单页面

        /// </summary>
        /// <param name="context"></param>
        public void PageDel(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string PageId = context.Request["PageId"];
            string commandText = string.Format("delete from Page where PageId ={0}", PageId);
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
                dataProvider.Close();
                context.Response.Write(0);
                return;
            }
        }


        public void CategoryHtml(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            int categoryId;
            // 不能一次性生成所有栏目的新闻，如果量大的话，会引起超时的问题，要全部生成一遍，最好是后台任务
            string requestCategoryId = context.Request["CategoryId"];
            if(!int.TryParse(requestCategoryId, out categoryId))
            {
                context.Response.Write(0);
                return;
            }

            List<Wis.Website.DataManager.Article> articles = null;
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            //??articles = articleManager.GetArticlesByCategoryName(categoryId);
            int index = 0;
            foreach (Wis.Website.DataManager.Article article in articles)
            {
                // 生成单个页面静态页面

                if (WriteHtml.NewsHtml(article))
                    index++;
            }
            // 生成栏目静态页面

            WriteHtml.GetCategoryListhtml(categoryId);
            context.Response.Write(string.Format("共生成了 {1} 个静态页面", index));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string CategoryName(string categoryId)
        {
            if (!Wis.Toolkit.Validator.IsInt(categoryId))
                return "";
            string commandText = string.Format("select CategoryName from Category where CategoryId ={0}", categoryId);
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
            dataProvider.Open();
            object o = dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            if (o == null)
                return "";
            return o.ToString();
        }
        public void Exists(HttpContext context)
        {
            if (context == null) return;
        }

        #endregion
    }
}
