using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;

namespace Wis.Website.Web.Backend.Article
{
    public partial class ArticleUpdate : System.Web.UI.Page
    {
        Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Website.Setting.ConnectionString);
        string articleId;
        protected void Page_Load(object sender, EventArgs e)
        {
            articleId = Request["ArticleId"];
            if (!Page.IsPostBack)
            {
                this.Button1.Attributes.Add("onclick", "javascript:return checkNews();");
                if (!Wis.Toolkit.Validator.IsInt(articleId))
                {
                    Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                    return;
                }
                getdate();
            }
        }
        protected void getdate()
        {

            string commandtext = string.Format("select * from vw_Article where ArticleId ={0}", articleId);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
            dataProvider.Close();
            if (dt.Rows.Count < 1)
            {
                Response.Write("<script language='javascript'>alert ('编号不正确!');window.close();</script>");
                return;
            }
            System.Data.DataRow drow = dt.Rows[0];
            this.title.Value = drow["Title"].ToString();
            this.TitleColor.Value = drow["TitleColor"].ToString();
            this.SubTitle.Value = drow["SubTitle"].ToString();
            this.CategoryGuid.Value = drow["CategoryGuid"].ToString();
            this.CategoryId.Value = drow["CategoryId"].ToString();
            this.CategoryName.Value = drow["CategoryName"].ToString();
            this.TabloidPath.Value = drow["ImagePath"].ToString();
            this.MetaKeywords.Value = drow["MetaKeywords"].ToString();
            this.Summary.Value = drow["Summary"].ToString();
            this.ContentHtml.Value = drow["ContentHtml"].ToString();
            this.TemplatePath.Value = drow["TemplatePath"].ToString();
            this.ReleasePath.Value = drow["ReleasePath"].ToString();
            this.Author.Value = drow["Author"].ToString();
            this.Original.Value = drow["Original"].ToString();
            this.ArticleGuid.Value = drow["ArticleGuid"].ToString();
            this.MetaDesc.Value = drow["MetaDesc"].ToString();
            if (string.IsNullOrEmpty(drow["TitleColor"].ToString()))
                ViewState["TitleColor"] = "#4ED34E";
            else
                ViewState["TitleColor"] = drow["TitleColor"].ToString();
            daohang.InnerText = drow["CategoryName"].ToString();


            Wis.Website.DataManager.ArticleType articleType = (DataManager.ArticleType)System.Enum.Parse(typeof(DataManager.ArticleType), drow["ArticleType"].ToString(), true);

            if (articleType ==  Wis.Website.DataManager.ArticleType.Normal)
            {
                this.ArticleType0.Checked = true;
            }
            else if (articleType == Wis.Website.DataManager.ArticleType.Image)
            {
                this.TabloidPath.Value = drow["ImagePath"].ToString();
                this.ArticleType1.Checked = true;
                this.TabloidPathVideo.Value = "";
                this.divTabloidPath.Style.Value = "display:block;";
            }
            else if (articleType == Wis.Website.DataManager.ArticleType.Video)
            {
                this.ArticleType2.Checked = true;
                this.TabloidPathVideo.Value = drow["ImagePath"].ToString();
                this.TabloidPath.Value = "";
                this.divTabloidPathVideo.Style.Value = "display:block;"; 
            }
            else if (articleType == Wis.Website.DataManager.ArticleType.Soft)
            {
                //this.ArticleType2.Checked = true;
                //this.TabloidPathVideo.Value = drow["ImagePath"].ToString();
                //this.TabloidPath.Value = "";
                //this.divTabloidPathVideo.Style.Value = "display:block;";
            }
            getfile(drow["ArticleGuid"].ToString());
        }
        protected void getfile(string articleGuid)
        {
            string commandtext = string.Format("select * from [File] where SubmissionGuid ='{0}' order by Rank", articleGuid);
            dataProvider.Open();
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
            dataProvider.Close();
            if (dt.Rows.Count > 0)
            {
           //var tempstr = ''; 

                string html = "";
                html+="<table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='width: 60px;'><label> 附件列表：</label></td><td valign='top'>";
                foreach (System.Data.DataRow drow in dt.Rows)
                {
                    html += string.Format("<div id=files{0}>&nbsp;名称：<input name='OriginalFileName' type=text style='width:100px;' value='{1}' class=form id='OriginalFileName{0}' />&nbsp;附件地址：<input name='SaveAsFileName' type=text style='width:250px;'  value='{2}' class=form id='SaveAsFileName{0}' />", drow["FileId"].ToString(), drow["OriginalFileName"].ToString(),drow["SaveAsFileName"].ToString());
                    html += string.Format("&nbsp;<img src='../../Images/folder.gif' alt='选择已有附件' border='0' style='cursor:pointer;' onclick='selectFile(\"file\",document.form1.SaveAsFileName{0},280,500);document.form1.SaveAsFileName{0}.focus();' />&nbsp;<span onclick='selectFile(\"UploadFile\",document.form1.SaveAsFileName{0},165,500);document.form1.SaveAsFileName{0}.focus();' style='cursor:hand;color:Red;'>上传新附件</span>", drow["FileId"].ToString());
                    html += string.Format("&nbsp;排序：<input name='FileId'  type=text style='display: none;' value='{0}' class=form id='FileId{0}' /><input name='sort' type='text' onKeyPress='if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57))))return false;' id='sort{0}' value='{1}' style='width:50px;'  class='form' />&nbsp<a href='#' onclick='filedelete({0})' class='list_link'><img  src='../images/dels.gif' style='border:0px;vertical-align:middle;'/></a></div>", drow["FileId"].ToString(), drow["Rank"].ToString());
                }
                html+="</td><tr></table>";
                this.filetemp.InnerHtml = html;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!Wis.Toolkit.Validator.IsGuid(ArticleGuid.Value))
            {
                ViewState["javescript"] = string.Format("alert('编号不正确!');");
                return;
            }
            string tabloidPath = "";
            int articleType = 0;
            if (ArticleType1.Checked)
            {
                tabloidPath = this.TabloidPath.Value;
                articleType = 1;
            }
            else if (ArticleType2.Checked)
            {
                tabloidPath = this.TabloidPathVideo.Value;
                articleType = 2;
            }
            Guid articleGuid = new Guid(ArticleGuid.Value);
            string commandText = string.Format(@"update Article set CategoryGuid='{0}',ImagePath=N'{1}',MetaKeywords=N'{2}',MetaDesc=N'{3}'
,Title=N'{4}',TitleColor=N'{5}',SubTitle=N'{6}',Summary=N'{7}',ContentHtml=N'{8}',Author=N'{9}',Original=N'{10}',TemplatePath=N'{11}',ArticleType={13},ReleasePath=N'{14}' where ArticleId ={12} 
", CategoryGuid.Value, tabloidPath, MetaKeywords.Value.Replace("'", "\""), MetaDesc.Value.Replace("'", "\""), this.title.Value.Replace("'", "\""), TitleColor.Value, SubTitle.Value.Replace("'", "\""), Summary.Value.Replace("'", "\""), ContentHtml.Value.Replace("'", "\""),
 Author.Value.Replace("'", "\""), this.Original.Value.Replace("'", "\""), TemplatePath.Value, articleId, articleType, ReleasePath.Value);

            dataProvider.Open();
            dataProvider.BeginTransaction();
            try
            {
                dataProvider.ExecuteNonQuery(commandText);

                //添加附件
                string gFileURL = Request.Form["FileUrl"];
                string gFileOrderID = Request.Form["FileOrderID"];
                string gURLName = Request.Form["URLName"];
                if (!string.IsNullOrEmpty(gFileURL))
                {
                    string[] FileURL = gFileURL.Split(',');
                    string[] FileOrderID = gFileOrderID.Split(',');
                    string[] URLName = gURLName.Split(',');

                    for (int i = 0; i < FileURL.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(FileURL[i]))
                        {
                            long size = 0;
                            //判断当前路径所指向的是否为文件
                            if (File.Exists(Server.MapPath(FileURL[i])))
                            {
                                FileInfo fileInfo = new FileInfo(Server.MapPath(FileURL[i]));
                                size = fileInfo.Length;
                                if (string.IsNullOrEmpty(URLName[i]))
                                    URLName[i] = fileInfo.Name;
                            }
                            commandText = string.Format(@"insert into [File](FileGuid,SubmissionGuid,OriginalFileName,SaveAsFileName,Size,Rank) 
values ('{5}','{0}',N'{1}',N'{2}',{3},{4})", articleGuid, URLName[i].Replace("'", "\""), FileURL[i], size, FileOrderID[i], Guid.NewGuid());
                            dataProvider.ExecuteNonQuery(commandText);
                        }
                    }
                }
                //更新附件
                string saveAsFileName = Request.Form["SaveAsFileName"];
                string sort = Request.Form["sort"];
                string originalFileName = Request.Form["OriginalFileName"];
                string fileId = Request.Form["FileId"]; 
                if (!string.IsNullOrEmpty(saveAsFileName))
                {
                    string[] SaveAsFileName = saveAsFileName.Split(',');
                    string[] Sort = sort.Split(',');
                    string[] OriginalFileName = originalFileName.Split(',');
                    string[] FileId = fileId.Split(',');
                    for (int i = 0; i < SaveAsFileName.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(SaveAsFileName[i]))
                        {
                            long size = 0;
                            //判断当前路径所指向的是否为文件
                            if (File.Exists(Server.MapPath(SaveAsFileName[i])))
                            {
                                FileInfo fileInfo = new FileInfo(Server.MapPath(SaveAsFileName[i]));
                                size = fileInfo.Length;
                                if (string.IsNullOrEmpty(OriginalFileName[i]))
                                    OriginalFileName[i] = fileInfo.Name;
                            }
                            commandText = string.Format(@"update [File] set OriginalFileName =N'{0}',SaveAsFileName =N'{1}',Size ={2},Rank={3}
where FileId ={4}", OriginalFileName[i].Replace("'", "\""), SaveAsFileName[i], size, Sort[i], FileId[i]);
                            dataProvider.ExecuteNonQuery(commandText);
                        }
                    }
                }
                dataProvider.CommitTransaction();
                dataProvider.Close();
            }
            catch
            {
                ViewState["javescript"] = string.Format("alert('添加失败!');");
                if (dataProvider.HasTransaction) dataProvider.RollbackTransaction();
                if (!dataProvider.IsClosed) dataProvider.Close();
                return;
            }
            //生成静态页面
            //生成列表静态页面
            //Website.WriteHtml.Build(article);

            Wis.Website.Logger.LoggerInsert(Guid.NewGuid(), "修改新闻 " + this.title.Value.Replace("'", "\""), articleGuid, this.ContentHtml.Value.Replace("'", "\""));
            Response.Redirect("ArticleList.aspx?CategoryId=" + CategoryId.Value);
            return;
        }
    }
}
