//------------------------------------------------------------------------------
// <copyright file="MiniPager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Web.UI;
using System.Text;
using System.ComponentModel;

namespace Wis.Toolkit.WebControls
{
    [DefaultProperty("UrlPattern")]
    [Designer(typeof(MiniPagerControlDesigner))]
    public class MiniPager : Control, INamingContainer
    {
        protected override void OnPreRender(System.EventArgs e)
        {
            // 注册分页样式
            string registeredKey = "MiniPagerCss";
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(registeredKey))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), registeredKey,
                        string.Format(@"<style type=""text/css"">
#{0} 
{{
	color:#222;
	font-family:宋体;
	font-size:12px;
    line-height:22px;
    text-align:right;
}}
#{0} a 	
{{
	border:1px solid #b6d0e9;
	text-decoration:none;
	padding:0 6px;
	color:#886db4;
    display:inline-block;
    height:22px;
    background:#fafafa;
    margin-left:2px;
}}
#{0} a:hover	
{{
	border:1px solid #b6d0e9;
	text-decoration:underline;
	color:#fff;
	background:#c48c4b;
}}
#{0} a.pageLeft:hover	
{{
	border:1px solid #9aafe5;
	text-decoration:none;
	padding:0 6px;
	color:#886db4;
    display:inline-block;
    height:22px;
    background:#fafafa;
    margin-left:2px;
}}

#{0} a.currentPager
{{
	border:1px solid #5999d4;
	text-decoration:none;
	background:#7eb8ee;
    color:#fff;
    cursor:default;
font-weight:bold;
}}
#{0} a:hover.currentPager
{{
	border:1px solid #fcc;
	text-decoration:none;
	background:#f0f0f0;
    color:#f00;
    cursor:default;
}}
#{0} a.noLink
{{
	border:1px solid #c2c2c2;
	text-decoration:none;
    background:#fff;
    color:#999;
    cursor:default;
}}
#{0} a.noLink:hover
{{
	border:1px solid #c2c2c2;
	text-decoration:none;
    background:#fff;
    color:#999;
    cursor:default;
	
}}
                        </style>", this.ClientID));
            }
        }

        private string urlPattern;
        public string UrlPattern
        {
            get { return urlPattern; }
            set 
            {
                if (value.IndexOf("{0}") == -1)
                {
                    throw new ArgumentException("UrlPattern无效，必须包含 {0} 这个分页参数");
                }
                urlPattern = value;
            }
        }

        private int pageIndex = 1;
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        private int pageSize = 20;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        protected override void Render(HtmlTextWriter output)
        {
            int pageCount = (RecordCount % PageSize == 0) ? (RecordCount / PageSize): ((RecordCount / PageSize) + 1);

            // 第2页 / 共2页 首页 上一页 [1] [2] 下一页 尾页
            if (PageIndex < 1) return; // 当前页数必须>0
            if (pageCount <= 1) return;
            int prevPage = PageIndex - 1;
            int nextPage = PageIndex + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div id='{0}'>", this.ClientID));
            sb.Append(string.Format("<a class='pageLeft'>共{0}条记录&nbsp;第{1}页/共{2}页</a>", RecordCount, PageIndex, pageCount));

            if (prevPage < 1)
            {
                sb.Append("<a class='noLink'>首页</a>");
                sb.Append("<a class='noLink'>上一页</a>");
            }
            else
            {
                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, 1));
                sb.Append("'>首页</a>");

                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, prevPage));
                sb.Append("'>上一页</a>");
            }

            int startPage;
            if (PageIndex % 10 == 0)
                startPage = PageIndex - 9;
            else
                startPage = PageIndex - PageIndex % 10 + 1;

            if (startPage > 10)
            {
                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, (startPage - 1)));
                sb.Append("' title='前10页'>...</a>");
            }

            for (int index = startPage; index < startPage + 10; index++)
            {
                if (index > pageCount) break;
                if (index == PageIndex)
                {
                    sb.Append(string.Format("<a title='第{0}页' class='currentPager'>[{0}]</a>", index));
                }
                else
                {
                    sb.Append("<a href='");
                    sb.Append(string.Format(UrlPattern, index));
                    sb.Append(string.Format("' title='第{0}页'>[{0}]</a>", index));
                }
            }
            if (pageCount >= startPage + 10)
            {
                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, (startPage + 10)));
                sb.Append(string.Format("' title='下{0}页'>...</a>", (startPage + 10)));
            }
            if (nextPage > pageCount)
            {
                sb.Append("<a class='noLink'>下一页0</a>");
                sb.Append("<a class='noLink'>尾页</a>");
            }
            else
            {
                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, nextPage));
                sb.Append("'>下一页</a>");

                sb.Append("<a href='");
                sb.Append(string.Format(UrlPattern, pageCount));
                sb.Append("'>尾页</a>");
            }

            sb.Append("</div>");

            // 输出 Html 脚本
            output.Write(sb.ToString());
        }
    }
}
