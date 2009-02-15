using System;
using System.Web;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Wis.Website.AjaxRequests
{
    public class MeetingHttpHandler : IHttpHandler
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            string command = (context.Request["command"] == null) ? string.Empty : context.Request["command"];
            if (command == "MeetingList")
                this.MeetingList(context);
            else if (command == "Count")
                this.Count(context);
            else if (command == "MeetingInfo")
                this.MeetingInfo(context);
            else if (command == "MeetingListIndex")
                this.MeetingListIndex(context);
            else if (command == "CommentsLoad")
                this.CommentsLoad(context);
            else if (command == "CommentsInsert")
                this.CommentsInsert(context);
        }
        public void MeetingList(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "wd_75";
            entity.ColumnList = @"txtZhuChi ,txtEndDate,txtEndTime ,txtRenShu ,
txtTime ,txtAddress ,txtDate ,txtTitle ,
txtshenqingren ,deptName , eformsn";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "eformsn";
            entity.PageSize = 19;
            entity.PagerColumnSort = true;
            entity.SearchCondition = string.Format("toPublic=1");
            Wis.Website.Pager.Manager manager = new Wis.Website.Pager.Manager();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                System.Data.DataSet ds = manager.QueryMeeting(entity);
                foreach (System.Data.DataRow dataReader in ds.Tables[0].Rows)
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader["eformsn"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["txtZhuChi"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["txtEndDate"].ToString() + " " + dataReader["txtEndTime"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["txtRenShu"].ToString());
                    sb.Append(";"); // 

                    sb.Append(dataReader["txtDate"].ToString() + " " +dataReader["txtTime"].ToString());
                    sb.Append(";"); // 

                    sb.Append(dataReader["txtAddress"].ToString());
                    sb.Append(";"); // 

                    sb.Append(Wis.Toolkit.Utility.StringUtility.TruncateString(dataReader["txtTitle"].ToString(), 16));
                    sb.Append(";"); // 
                    sb.Append(dataReader["txtshenqingren"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["deptName"].ToString());
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
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void MeetingListIndex(HttpContext context)
        {
            if (context == null) return;
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

            string pageIndex = context.Request["PageIndex"];
            if (!Wis.Toolkit.Validator.IsInt(pageIndex))
                pageIndex = "1";
            //select txtZhuChi as 主持人,txtEndDate as 会议结束日期,txtRenShu as 参会人数,txtTime as 会议时间,txtAddress as 会议地点,txtDate as 会议日期,txtTitle as 会议名称,txtshenqingren as 申请人,deptName as 预定部门, eformsn from wd_75 where toPublic=1    order by eformsn Desc
            Wis.Website.Pager.Entity entity = new Wis.Website.Pager.Entity();
            entity.TableName = "wd_75";
            entity.ColumnList = @"txtZhuChi ,txtEndDate,txtEndTime ,txtRenShu ,
txtTime ,txtAddress ,txtDate ,txtTitle ,
txtshenqingren ,deptName , eformsn";
            entity.PageIndex = System.Convert.ToInt32(pageIndex);
            entity.PagerColumn = "eformsn";
            entity.PageSize = 7;
            entity.PagerColumnSort = true;
            entity.SearchCondition = string.Format("toPublic=1");
            Wis.Website.Pager.Manager manager = new Wis.Website.Pager.Manager();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                System.Data.DataSet ds = manager.QueryMeeting(entity);
                foreach (System.Data.DataRow dataReader in ds.Tables[0].Rows)
                {
                    xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dataReader["eformsn"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["txtZhuChi"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dataReader["txtEndDate"].ToString() + " " + dataReader["txtEndTime"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["txtRenShu"].ToString());
                    sb.Append(";"); // 

                    sb.Append(dataReader["txtDate"].ToString() + " " + dataReader["txtTime"].ToString());
                    sb.Append(";"); // 

                    sb.Append(dataReader["txtAddress"].ToString());
                    sb.Append(";"); // 

                    sb.Append(Wis.Toolkit.Utility.StringUtility.TruncateString(dataReader["txtTitle"].ToString(), 10));
                    sb.Append(";"); // 
                    sb.Append(dataReader["txtshenqingren"].ToString());
                    sb.Append(";"); // 
                    sb.Append(dataReader["deptName"].ToString());
                    sb.Append(";"); // 
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                }
                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());
            }
        }
        public void MeetingInfo(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
           string eformsn = context.Request["eformsn"];
            if(!Wis.Toolkit.Validator.IsInt(eformsn))
            {
              context.Response.Write(0);
                return;
            }
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.MeetingConnectionString);
            dataProvider.Open();
            string commandText = string.Format("update wd_75 set Hits = Hits + 1 where  eformsn ={0}", eformsn);
            dataProvider.ExecuteNonQuery(commandText);
             commandText = string.Format("select * from wd_75 where eformsn ={0}", eformsn);
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandText).Tables[0];
            dataProvider.Close();
            if (dt.Rows.Count < 1)
            {
                context.Response.Write(0);
                return;
            }
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                  xtw.WriteStartElement("A");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(dt.Rows[0]["eformsn"].ToString());
                    sb.Append(";"); //                     
                    sb.Append(dt.Rows[0]["txtZhuChi"].ToString().Replace(";", ","));//主持人
                    sb.Append(";"); //                     
                    sb.Append(dt.Rows[0]["txtEndDate"].ToString() + " " + dt.Rows[0]["txtEndTime"].ToString());//会议结束日期
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtRenShu"].ToString().Replace(";", ","));//参会人数
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtDate"].ToString() + " " + dt.Rows[0]["txtTime"].ToString());//会议开始日期
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtAddress"].ToString().Replace(";", ","));//会议地点
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtTitle"].ToString().Replace(";", ","));//会议名称
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtshenqingren"].ToString().Replace(";", ","));//申请人
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["deptName"].ToString().Replace(";", ","));//预定部门
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["txtYuHuiRY"].ToString().Replace(";", ","));//与会人员
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["RichTextBox1"].ToString().Replace(";", ","));//内容
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["Hits"].ToString());//浏览次数
                    sb.Append(";"); // 
                    sb.Append(dt.Rows[0]["Comments"].ToString());//评论条数
                    sb.Append(";"); // 

                    
                    xtw.WriteAttributeString("V", sb.ToString());
                    xtw.WriteEndElement();// ActiveMatch -> A
                     //commandText = string.Format("select * from webflowAttach where eformsn ={0} and eformID =75", eformsn);
                     //System.Data.DataTable dt1 = dataProvider.ExecuteDataset(commandText).Tables[0];
                     //foreach(System.Data.DataRow drow in dt1.Rows)
                     //{ 
                     //    xtw.WriteStartElement("F");
                     //    System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

                     //    xtw.WriteAttributeString("V", sb1.ToString());
                     //    xtw.WriteEndElement();// ActiveMatch -> A
                     //}

                xtw.WriteEndElement();// Live -> L
                //xtw.WriteEndDocument();
                context.Response.Write(sw.ToString());

            }
        }
        public void Count(HttpContext context)
        {
            if (context == null)
            {
                context.Response.Write(0);
                return;
            }
            string commandText = string.Format("select count(eformsn) from wd_75 where toPublic=1");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.MeetingConnectionString);
            dataProvider.Open();
            int count = (int)dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            context.Response.Write(count);
        }
        /// <summary>
        /// 读取评论
        /// </summary>
        /// <param name="context"></param>
        public void CommentsLoad(HttpContext context)
        {
            string eformsn = context.Request["eformsn"];
            if (!Wis.Toolkit.Validator.IsInt(eformsn))
            {
                context.Response.Write(0);
                return;
            }
            context.Response.ContentType = "text/xml";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider();
            dataProvider.ConnectionString = Wis.Website.Setting.MeetingConnectionString;
            dataProvider.Open();
            string commandText = string.Format("select meetingGuid from wd_75 where eformsn ={0}", eformsn);
            object o = dataProvider.ExecuteScalar(commandText);
            dataProvider.Close();
            if (o == null)
            {
                context.Response.Write(0);
                return;
            }
            dataProvider.ConnectionString = Setting.ConnectionString;
            dataProvider.Open();
            using (StringWriter sw = new StringWriter())
            {
                XmlTextWriter xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                // xtw.WriteStartDocument();
                xtw.WriteStartElement("L"); // League -> L
                commandText = string.Format("select * from Comment where ObjectGuid ='{0}' order by CommentId", o);
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
        /// <param name="context"></param>
        public void CommentsInsert(HttpContext context)
        {
            string eformsn = context.Request["eformsn"];
            if (!Wis.Toolkit.Validator.IsInt(eformsn))
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
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider();
            dataProvider.ConnectionString = Wis.Website.Setting.MeetingConnectionString;
            dataProvider.Open();
            string commandText = string.Format("select meetingGuid from wd_75 where eformsn ={0}", eformsn);
            object o = dataProvider.ExecuteScalar(commandText);
            if (o == null)
            {
                dataProvider.Close();
                context.Response.Write(0);
                return;
            }
            commandText = string.Format("update wd_75 set Comments = Comments + 1 where eformsn ={0}", eformsn);
            dataProvider.ExecuteNonQuery(commandText);
            dataProvider.Close();

            dataProvider.ConnectionString = Setting.ConnectionString;
            dataProvider.Open();
            try
            {

                commandText = string.Format("insert into Comment(ObjectGuid,Title,Commentator,ContentHtml,Original) values('{0}',N'{1}',N'{2}',N'{3}',N'{4}')", o, Title.Replace("'", "\""), Commentator, ContentHtml.Replace("'", "\""), Wis.Toolkit.RequestManager.GetClientIP());
                dataProvider.ExecuteNonQuery(commandText);
                dataProvider.Close();
                context.Response.Write(1);
                return;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Open();
                context.Response.Write(0);
                return;
            }
        }
      
        public void Exists(HttpContext context)
        {
            if (context == null) return;
        }

        #endregion
    }
}
