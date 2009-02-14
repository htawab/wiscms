using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Wis.Website
{
    public class WriteHtml
    {
        //public static Wis.Website.DataManager.Article articleEntity = new Wis.Website.DataManager.Article();
        public static Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
        public static Tag.Entity tagEntity = new Wis.Website.Tag.Entity();
        public static Tag.Manager tagManager = new Wis.Website.Tag.Manager();
        public static Label.Entity labelEntity = new Wis.Website.Label.Entity();
        public static string tempcontent = "";
        /// <summary>
        /// 写HTML文件
        /// </summary>
        /// <param name="Content">内容</param>
        /// <param name="FilePath">物理路径</param>
        public static void WriteHtmls(string Content, string FilePath)
        {
            string getContent = Content;
            //string getajaxJS = "<script language=\"javascript\" type=\"text/javascript\" src=\"" + CommonData.SiteDomain + "/configuration/js/Prototype.js\"></script>\r\n";
            //getajaxJS += "<script language=\"javascript\" type=\"text/javascript\" src=\"" + CommonData.SiteDomain + "/configuration/js/jspublic.js\"></script>\r\n";

            //// 开启CNZZ整合
            //getajaxJS += "<script src='http://s140.cnzz.com/stat.php?id=1063592&web_id=1063592&show=pic' language='JavaScript' charset='gb2312'></script>";

            //// 开启 51.la 的流量统计
            //getajaxJS += "<script language='javascript' type='text/javascript' src='http://js.users.51.la/2156800.js'></script><noscript><a href='http://www.51.la/?2156800' target='_blank'><img alt='&#x6211;&#x8981;&#x5566;&#x514D;&#x8D39;&#x7EDF;&#x8BA1;' src='http://img.users.51.la/2156800.asp' style='border:none'/></a></noscript>";
            //if (Wiscms.Common.Public.readparamConfig("Open", "Cnzz") == "11")
            //{
            //    getajaxJS += "<script src='http://pw.cnzz.com/c.php?id=" + Wiscms.Common.Public.readparamConfig("SiteID", "Cnzz") + "' " +
            //                "language='JavaScript' charset='gb2312'></script>\r\n";
            //}

            // 从配置文件中读取
            //string byCreat = "<!--常智内容管理系统创建于 " + DateTime.Now + "-->\r\n";
            try
            {   
                FilePath = FilePath.Replace("/","\\");
                string Dir = FilePath.Substring(0, FilePath.LastIndexOf("\\"));
                if (!Directory.Exists(Dir))
                    Directory.CreateDirectory(Dir);
                using (StreamWriter sw = new StreamWriter(FilePath, false))
                {
                    //替换
                    sw.Write(getContent);
                    sw.Dispose();
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// 删除HTML页面
        /// </summary>
        /// <param name="FilePath"></param>
        public static void DelHtml(string FilePath)
        {
            FilePath = FilePath.Replace("/", "\\");
            FilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + FilePath;
            if (File.Exists(FilePath))
            {
                try
                {
                    File.Delete(FilePath);
                }
                catch
                { }
            }
            return;
        }
        /// <summary>
        /// 读取HTML文件内容
        /// </summary>
        /// <param name="Path">物理路径</param>
        /// <returns></returns>
        public static string ReadHtml(string Path)
        {
            Path = Path.Replace("/", "\\");
            Path = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + Path;
            if (File.Exists( Path))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Path))
                    {
                        tempcontent = sr.ReadToEnd();
                    }
                }
                catch
                { }
            }
            else
            {
                tempcontent = "模板不存在!";
            }
            return tempcontent;
        }

        /// <summary> 
        /// 转换新闻页面的标签
        /// </summary>
        public static void ReplaceNewsLabels(Wis.Website.DataManager.Article article)
        {
            //转换新闻页面的标签
            string pattern = @"\$#_[^\$]+\$";
            tempcontent = ReadHtml(article.TemplatePath);
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection m = reg.Matches(tempcontent);
            for(int i = 0 ;i< m.Count;i++)
            {
                string myLabelName = m[i].Value;
                myLabelName = myLabelName.Replace("$", "").Replace("#_", "");
                myLabelName = GetArticle(myLabelName, article);
                tempcontent = tempcontent.Replace(m[i].Value, myLabelName);
            }
            ReplacelistLabels();
        }
        /// <summary>
        /// 转换列表页面标签
        /// </summary>
        public static void ReplacelistLabels()
        {
            string pattern = @"\$Tag_[^\$]+\$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection m = reg.Matches(tempcontent);
            for (int i = 0; i < m.Count; i++)
            {
                string myLabelName = m[i].Value;
                myLabelName = myLabelName.Replace("$", "").Replace("Tag_", "");
                myLabelName = getTags(myLabelName);
                tempcontent = tempcontent.Replace(m[i].Value, myLabelName);
            }
        }
        /// <summary>
        /// 解析列表标签
        /// </summary>
        /// <param name="myLabelName"></param>
        /// <returns></returns>
        public static string getTags(string myLabelName)
        {
            tagEntity = tagManager.Load(myLabelName);
            if (tagEntity == null)
                return "";
            string pattern = @"\[tpl:[^\]]+\]";
            Regex reg = new Regex(pattern, RegexOptions.Compiled);
            MatchCollection m = reg.Matches(tagEntity.ContentHtml);
            if (m.Count == 0)
                return "";
            string contentHtml = m[0].Value;
            contentHtml = contentHtml.Replace("[tpl:", "").Replace("]", "");
            string[] contentHtmls = contentHtml.Split(';');
            labelEntity = new Wis.Website.Label.Entity();
            for (int k = 0; k < contentHtmls.Length; k++)
            {
                if (contentHtmls[k].IndexOf("CommandText") > -1)
                {
                    contentHtmls[k] = contentHtmls[k].Replace("CommandText=", "").Replace("{","").Replace("}","");
                     labelEntity.Load("CommandText", contentHtmls[k]);
                }
                else
                {
                    string[] labels = contentHtmls[k].Split('=');
                     labelEntity.Load(labels[0], labels[1]);
                }
            }
            //读取数据库数据
            return Listhtml(tagEntity.ContentHtml.Replace(m[0].Value,"").Replace("[/tpl]",""));
        }
        /// <summary>
        ///  列表数据HTML
        /// </summary>
        /// <returns></returns>
        public static string Listhtml(string dateHtml)
        {
            string html = "";
            System.Data.DataTable dt = ArticleDt();
            if (dt == null)
                return "";
            //分页 装载到数组中 JS读取数组
            if (labelEntity.IsPage)
            {
                html += "<SCRIPT language=javascript>\r\n";
                html += "var " + tagEntity.TagName + "webData = [";
                string pattern = @"\$[^\$]+\$";
                Regex reg = new Regex(pattern, RegexOptions.Compiled);
                MatchCollection m = reg.Matches(tagEntity.ContentHtml);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "{";
                    for (int k = 0; k < m.Count; k++)
                    {
                        string columns = m[k].Value.Replace("$", "");
                        string columnsValue = "";
                        try
                        {
                            columnsValue = dt.Rows[i][columns].ToString();
                            if (columns == "DateCreated")
                                columnsValue = System.Convert.ToDateTime(columnsValue).ToShortDateString();
                            dateHtml = dateHtml.Replace(m[k].Value, "'+" + tagEntity.TagName + "webData[i]." + columns + "+'");
                        }
                        catch { }
                        html += "\"" + columns + "\":\"" + columnsValue.Replace('"', '\'') + "\"";
                        if (k < m.Count - 1)
                            html += ",";
                    }
                    html += "}\r\n";
                    if (i < dt.Rows.Count - 1)
                        html += ",";
                }
                html += "]\r\n";
                html += "var " + tagEntity.TagName + "CurPage =" + labelEntity.CurPage + ";\r\n ";//当前记录数
                html += "var " + tagEntity.TagName + "vNum =" + tagEntity.TagName + "webData.length;\r\n";//全部数目
                html += "var " + tagEntity.TagName + "showData =" + tagEntity.TagName + "webData;\r\n";
                html += "var " + tagEntity.TagName + "pageListNum = " + labelEntity.PageSize + ";\r\n";//每页显示记录数
                html += "var " + tagEntity.TagName + "pageCount = " + tagEntity.TagName + "vNum/" + tagEntity.TagName + "pageListNum + ((" + tagEntity.TagName + "vNum%" + tagEntity.TagName + "pageListNum == 0)? 0:1);\r\n";  //总记录数    

                html += "if (isNaN(parseInt(" + tagEntity.TagName + "pageCount))) " + tagEntity.TagName + "pageCount = 1;\r\n";
                html += "if (" + tagEntity.TagName + "pageCount < 1) " + tagEntity.TagName + "pageCount = 1;\r\n";
                html += "  " + tagEntity.TagName + "pageCount = parseInt(" + tagEntity.TagName + "pageCount);\r\n";
                html += "var " + tagEntity.TagName + "draw = function(page){\r\n";
                html += "     if (page <= 0 || page > " + tagEntity.TagName + "pageCount)\r\n";
                html += "         page = 1;\r\n";
                html += "    " + tagEntity.TagName + "CurPage = page;\r\n";
                html += " var tvBegin = (" + tagEntity.TagName + "CurPage-1) * " + tagEntity.TagName + "pageListNum;\r\n";
                html += "var html = '';\r\n";
                html += "var tempListNum = tvBegin+" + tagEntity.TagName + "pageListNum;\r\n";
                html += "if (tempListNum > " + tagEntity.TagName + "vNum)\r\n";
                html += "tempListNum = " + tagEntity.TagName + "vNum;\r\n";
                html += "var k =0 ;\r\n";
                html += "for(var i = tvBegin; i < tempListNum; i++){\r\n";
                html += " html += '" + dateHtml + "';\r\n";
                html += " k++;\r\n";
                html += "}\r\n";
                html += " if (k <   " + tagEntity.TagName + "pageListNum) {\r\n";
                html += "for (var i = 0; i < " + tagEntity.TagName + "pageListNum - k; i++)\r\n";
                html += "   html += '<li> </li>';\r\n";
                html += " }\r\n";

                html += "document.getElementById('" + tagEntity.TagName + "pageinfo').innerHTML = html;\r\n";
                html += "document.getElementById('" + tagEntity.TagName + "page').innerHTML = " + tagEntity.TagName + "getPageHtml();\r\n";
                html += "}\r\n";
                html += tagEntity.TagName + "draw(1);\r\n";
                //总页数;

                ///输出分页html
                html += "function " + tagEntity.TagName + "getPageHtml(){\r\n";
                html += "var pageIndex =" + tagEntity.TagName + "CurPage;\r\n";
                html += " var pageCount =" + tagEntity.TagName + "pageCount;\r\n";
                html += " var RecordCount =" + tagEntity.TagName + "vNum;\r\n";
                html += "var html = '',prevPage = pageIndex - 1, nextPage = pageIndex + 1;;\r\n";
                html += "if (pageCount <= 1)\r\n";
                html += "{\r\n";
                html += "     return \"\";\r\n";
                html += "}\r\n";
                html += " html += '<span><span class=\"Pager\">共' + RecordCount + '条记录&nbsp;第' + pageIndex + '页/共' + pageCount + '页</span>';\r\n";
                html += "  if (prevPage < 1) {\r\n";
                html += "} else {\r\n";
                html += "  html += '<a href=\"javascript:" + tagEntity.TagName + "draw(1)\" title=\"首页\" class=\"Pager\">首页</a>';\r\n";
                html += " html += '<a href=\"javascript:" + tagEntity.TagName + "draw('+prevPage+')\" title=\"上一页\" class=\"Pager\">上一页</a>';\r\n";
                html += "  }\r\n";
                html += " if (pageIndex % 10 == 0) {\r\n";
                html += "    var startPage = pageIndex - 9;\r\n";
                html += "   } else {\r\n";
                html += "      var startPage = pageIndex - pageIndex % 10 + 1;\r\n";
                html += "   }\r\n";
                html += "   if (startPage > 10) html += '<a href=\"javascript:" + tagEntity.TagName + "draw('+(startPage - 1)+')\" title=\"前10页\" class=\"Pager\">...</a>';\r\n";
                html += "   for (var i = startPage; i < startPage + 10; i++) {\r\n";
                html += "  if (i > pageCount) break;\r\n";
                html += "  if (i == pageIndex) {\r\n";
                html += "       html += '<span title=\"第' + i + '页\" class=\"Current\">' + i + '</span>';\r\n";
                html += "   } else {\r\n";
                html += "        html += '<a href=\"javascript:" + tagEntity.TagName + "draw('+i+')\" title=\"第' + i + '页\" class=\"Pager\">' + i + '</a>';\r\n";
                html += "    }\r\n";
                html += " }\r\n";
                html += " if (pageCount >= startPage + 10) html += '<a href=\"javascript:" + tagEntity.TagName + "draw('+(startPage +10)+')\" title=\"下10页\" class=\"Pager\">...</a>';\r\n";
                html += " if (nextPage > pageCount) {\r\n";
                html += " } else {\r\n";
                html += "     html += '<a href=\"javascript:" + tagEntity.TagName + "draw('+nextPage+')\" title=\"下一页\" class=\"Pager\">下一页</a>';\r\n";
                html += "      html += '<a href= \"javascript:" + tagEntity.TagName + "draw('+pageCount+')\" title=\"尾页\" class=\"Pager\">尾页</a>';\r\n";
                html += "  }\r\n";
                html += "  html += '</span>';\r\n";
                html += "return html;}\r\n";
                html += "</SCRIPT>";


            }
            //未分页直接些到HTML中区
            else
            {
                if (labelEntity.Type.ToLower() == "flash")
                {
                    html += "<SCRIPT language=javascript>\r\n";
                    string pics = "";
                    string mylinks = "";
                    string texts = "";
                    string texts2 = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        pics += dt.Rows[i]["ImagePath"].ToString();
                        mylinks += dt.Rows[i]["ReleasePath"].ToString();
                        texts += dt.Rows[i]["Summary"].ToString();
                        texts2 += dt.Rows[i]["Title"].ToString();
                        if (i < dt.Rows.Count - 1)
                        {
                            pics += "|"; mylinks += "|"; texts += "|"; texts2 += "|";
                        }
                    }
                    html += "var pics ='" + pics + "'\r\n";
                    html += "var mylinks ='" + mylinks + "'\r\n";
                    html += "var texts ='" + texts + "'\r\n";
                    html += "var texts2 ='" + texts2 + "'\r\n";
                    html += "var eduFlash2 = new eduFlash(\"../flash/vv_new.swf\",\"eduFlashID01\",\"662\",\"180\",\"7\",\"#ffffff\");\r\n";
                    html += "    eduFlash2.addParam(\"quality\", \"high\");\r\n";
                    html += "         eduFlash2.addParam(\"wmode\", \"opaque\");\r\n";
                    html += "    eduFlash2.addParam(\"salign\", \"t\");	\r\n";
                    html += "    eduFlash2.addVariable(\"p\",pics);\r\n";
                    html += "    eduFlash2.addVariable(\"l\",mylinks);\r\n";
                    html += "    eduFlash2.addVariable(\"icon\",texts);\r\n";
                    html += "    eduFlash2.addVariable(\"icon_2\",texts2);\r\n";
                    html += "    eduFlash2.write(\"" + tagEntity.TagName + "flash\");\r\n";
                    html += "            </script>\r\n";
                }
                else if (labelEntity.Type.ToLower() == "video")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string pattern = @"\$[^\$]+\$";
                        Regex reg = new Regex(pattern, RegexOptions.Compiled);
                        MatchCollection m = reg.Matches(tagEntity.ContentHtml);
                        string columnstml = dateHtml;
                        for (int k = 0; k < m.Count; k++)
                        {
                            string columns = m[k].Value.Replace("$", "");
                            string columnsValue = "";
                            try
                            {
                                columnsValue = dt.Rows[i][columns].ToString();
                                if (columns == "Title")
                                {
                                    if (labelEntity.TruncateNumber != 0)
                                        columnsValue = Wis.Toolkit.Utility.StringUtility.NTruncateString(columnsValue, labelEntity.TruncateNumber);
                                }
                                if (columns == "TabloidPath")
                                {
                                    ///插入播放器代码；
                                    columnsValue = videoHtml(columnsValue);
                                }
                                columnstml = columnstml.Replace(m[k].Value, columnsValue);
                            }
                            catch { }
                        }
                        html += columnstml;
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string pattern = @"\$[^\$]+\$";
                        Regex reg = new Regex(pattern, RegexOptions.Compiled);
                        MatchCollection m = reg.Matches(tagEntity.ContentHtml);
                        string columnstml = dateHtml;
                        for (int k = 0; k < m.Count; k++)
                        {
                            string columns = m[k].Value.Replace("$", "");
                            string columnsValue = "";
                            try
                            {
                                columnsValue = dt.Rows[i][columns].ToString();
                                if (columns == "DateCreated")
                                    columnsValue = System.Convert.ToDateTime(columnsValue).ToShortDateString();
                                if (columns == "Title")
                                {
                                    if (labelEntity.TruncateNumber != 0)
                                        columnsValue = Wis.Toolkit.Utility.StringUtility.NTruncateString(columnsValue, labelEntity.TruncateNumber);
                                }
                                if (columns == "Summary")
                                {
                                    if (labelEntity.SummaryNumber != 0)
                                        columnsValue = Wis.Toolkit.Utility.StringUtility.TruncateString(columnsValue, labelEntity.SummaryNumber);
                                }
                                columnstml = columnstml.Replace(m[k].Value, columnsValue);
                            }
                            catch { }

                        }
                        html += columnstml;
                    }
                }
            }
            return html;
        }
        /// <summary>
        /// 插入播放器代码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string videoHtml(string gvalur)
        {
            string content = "<img src=\"/images/video.jpg\" width=\"300\"  height =\"190\"/>";
            if (gvalur != null && gvalur != "")
            {
                if (gvalur.IndexOf(".") > -1)
                {
                    string extension = System.IO.Path.GetExtension(gvalur);
                    //int fileExstarpostion = gvalur.LastIndexOf(".");
                    //string fileExName = gvalur.Substring(fileExstarpostion,(gvalur.Length-1));
                    switch (extension.ToLower())
                    {
                        case ".jpg":
                        case ".gif":
                        case ".bmp":
                        case ".ico":
                        case ".png":
                        case ".jpeg":
                        case ".tif":
                            content = "<img src=\"" + gvalur + "\" onerror='Javascript:this.src=\"/images/video.jpg\"'  width=\"300\"  height =\"190\" />";
                            break;
                        case ".rar":
                        case ".doc":
                        case ".zip":
                        case ".txt":
                            return content;
                        case ".swf":
                            content = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" id=\"NSPlay\"  width=\"300\"  height =\"190\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\">";
                            content += "<param name=\"movie\" value=\"" + gvalur + "\" />";
                            content += "<param name=\"quality\" value=\"high\" />";
                            content += "<embed src=\"" + gvalur + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"></embed>";
                            content += "</object>";
                            break;
                        case ".flv":
                            content = "<object id=\"NSPlay\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"   width=\"300\"  height =\"190\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" width=\"500\" height=\"400\">" +
                            "<param name=\"movie\" value=\" \"/>" +
                            "<param name=\"quality\" value=\"high\"/>" +
                            "<param name=\"allowFullScreen\" value=\"true\" />" +
                            "<param name=\"FlashVars\" value=\"vcastr_file=" + gvalur + "\" />" +
                            "<embed src=\" \" FlashVars=\"vcastr_file=" + gvalur + "\" allowFullScreen=\"true\"  quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"500\" height=\"400\"></embed>" +
                            "</object>";
                            break;
                        //                        case ".avi":     
                        //                            content='<embed width="' + widthid + '"  height ="' + heightid + '" border="0" showdisplay="1" showcontrols="1" autostart="' + AutoStart + '" '+     
                        //                            ' autorewind="0" playcount="0"moviewindowheight="' + heightid + '" moviewindowwidth="' + widthid + '" filename="/' + gvalur + '" src="' + gvalur + '">' +     
                        //                            '</embed>'     
                        //                            break;     
                        case ".wmv":
                            content = "<object id=\"NSPlay\" width=\"300\"  height =\"190\"  classid=\"CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,5,715\" standby=\"Loading Microsoft Windows Media Player components...\" type=\"application/x-oleobject\" hspace=\"5\">\r\n" +
                            "<param name=\"AutoRewind\" value=1/>\r\n" +
                            "<param name=\"FileName\" value=\"" + gvalur + "\"/>\r\n" +
                            "<param name=\"ShowControls\" value=\"1\"/>\r\n" +
                            "<param name=\"ShowPositionControls\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowAudioControls\" value=\"1\"/>\r\n" +
                            "<param name=\"ShowTracker\" value=\"-1\"/>\r\n" +
                            "<param name=\"ShowDisplay\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowStatusBar\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowGotoBar\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowCaptioning\" value=\"0\"/>\r\n" +
                            "<param name=\"AutoStart\" value=\"-1\"/>\r\n" +
                            "<param name=\"AudioStream\" value=\"-1\"/>\r\n" +
                            "<param name=\"WindowlessVideo\" value=\"0\"/>\r\n" +
                            "<param name=\"EnablePositionControls\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnableFullScreenControls\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnableTracker\" value=\"-1\"/>\r\n" +
                            "<param name=\"Volume\" value=\"-2500\"/>\r\n" +
                            "<param name=\"AnimationAtStart\" value=\"0\"/>\r\n" +
                            "<param name=\"TransparentAtStart\" value=\"0\"/>\r\n" +
                            "<param name=\"AllowChangeDisplaySize\" value=\"0\"/>\r\n" +
                            "<param name=\"AllowScan\" value=\"0\"/>\r\n" +
                            "<param name=\"EnableContextMenu\" value=\"-1\"/>\r\n" +
                            "<param name=\"ClickToPlay\" value=\"0\"/>\r\n" +
                            "</object>";
                            break;
                        case ".mpg":
                            content = "<object classid=\"clsid:05589FA1-C356-11CE-BF01-00AA0055595A\"  id=\"NSPlay\" width=\"300\"  height =\"190\"  >\r\n" +
                            "<param name=\"Appearance\" value=\"0\"/>\r\n" +
                            "<param name=\"AutoStart\" value=\"-1\"/>\r\n" +
                            "<param name=\"AllowChangeDisplayMode\" value=\"-1\"/>\r\n" +
                            "<param name=\"AllowHideDisplay\" value=\"0\"/>\r\n" +
                            "<param name=\"AllowHideControls\" value=\"-1\"/>\r\n" +
                            "<param name=\"AutoRewind\" value=\"-1\"/>\r\n" +
                            "<param name=\"Balance\" value=\"0\"/>\r\n" +
                            "<param name=\"CurrentPosition\" value=\"0\"/>\r\n" +
                            "<param name=\"DisplayBackColor\" value=\"0\"/>\r\n" +
                            "<param name=\"DisplayForeColor\" value=\"16777215\"/>\r\n" +
                            "<param name=\"DisplayMode\" value=\"0\"/>\r\n" +
                            "<param name=\"Enabled\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnableContextMenu\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnablePositionControls\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnableSelectionControls\" value=\"0\"/>\r\n" +
                            "<param name=\"EnableTracker\" value=\"-1\"/>\r\n" +
                            "<param name=\"Filename\" value=\"" + gvalur + "\" valuetype=\"ref\"/>\r\n" +
                            "<param name=\"FullScreenMode\" value=\"0\"/>\r\n" +
                            "<param name=\"MovieWindowSize\" value=\"0\"/>\r\n" +
                            "<param name=\"PlayCount\" value=\"1\"/>\r\n" +
                            "<param name=\"Rate\" value=\"1\"/>\r\n" +
                            "<param name=\"SelectionStart\" value=\"-1\"/>\r\n" +
                            "<param name=\"SelectionEnd\" value=\"-1\"/>\r\n" +
                            "<param name=\"ShowControls\" value=\"-1\"/>\r\n" +
                            "<param name=\"ShowDisplay\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowPositionControls\" value=\"0\"/>\r\n" +
                            "<param name=\"ShowTracker\" value=\"-1\"/>\r\n" +
                            "<param name=\"Volume\" value=\"-480\"/>\r\n" +
                            "</object>";
                            break;
                        case ".wma":
                            content = "<object classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\"  width=\"300\"  height =\"190\" id=\"NSPlay\" > \r\n" +
                            "<param name=\"Filename\" value=\"" + gvalur + "\"/>\r\n" +
                            "<param name=\"PlayCount\" value=\"1\"/> \r\n" +
                            "<param name=\"AutoStart\" value=\"-1\"/>\r\n" +
                            "<param name=\"ClickToPlay\" value=\"1\"/>\r\n" +
                            "<param name=\"DisplaySize\" value=\"0\"/>\r\n" +
                            "<param name=\"EnableFullScreen Controls\" value=\"1\"/>\r\n" +
                            "<param name=\"ShowAudio Controls\" value=\"-1\"/>\r\n" +
                            "<param name=\"EnableContext Menu\" value=\"-1\"/>\r\n" +
                            "<param name=\"ShowDisplay\" value=\"0\"/>\r\n" +
                            "</object>";
                            break;
                        case ".rmvb":
                            //                            content = '<p align="' + textalign + '"><object classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" name="player"   width="' + widthid + '"  height ="' + heightid + '" id="player">\r\n' +
                            //                           '<param name="_ExtentX" value="11298"/> \r\n' +
                            //                           '<param name="_ExtentY" value="7938"/> \r\n' + 
                            //                            '<param name="AUTOSTART" value="' + AutoStart + '"/> \r\n' +
                            //                            '<param name="SHUFFLE" value="0"/> \r\n' +
                            //                            '<param name="PREFETCH" value="0"/> \r\n' +
                            //                            '<param name="NOLABELS" value="-1"/> \r\n' +
                            //                            '<param name="SRC" value="' + gvalur + '"/> \r\n' +
                            //                            '<param name="CONTROLS" value="ImageWindow,StatusBar,ControlPanel"/> \r\n' +
                            //                            '<param name="CONSOLE" value="clip1"/> \r\n' +
                            //                            '<param name="LOOP" value="true"/> \r\n' +
                            //                            '<param name="NUMLOOP" value="0"/> \r\n' +
                            //                            '<param name="CENTER" value="0"/> \r\n' +
                            //                            '<param name="MAINTAINASPECT" value="0"/> \r\n' +
                            //                            '<param name="BACKGROUNDCOLOR" value="#000000"/> \r\n' +
                            //                            '</object> '
                            //                            
                            content = "<object  classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" width=\"300\"  height =\"170\" id=\"NSPlay\"> \r\n" +
                            "<param name=\"_ExtentX\" value=\"11298\"/> \r\n" +
                            "<param name=\"_ExtentY\" value=\"7938\"/> \r\n" +
                            "<param name=\"AUTOSTART\" value=\"-1\"/> \r\n" +
                            "<param name=\"SHUFFLE\" value=\"0\"/> \r\n" +
                            "<param name=\"PREFETCH\" value=\"0\"/> \r\n" +
                            "<param name=\"NOLABELS\" value=\"-1\"/> \r\n" +
                            "<param name=\"SRC\" value=\"" + gvalur + "\";/> \r\n" +
                            "<param name=\"CONTROLS\" value=\"Imagewindow\"/> \r\n" +
                            "<param name=\"CONSOLE\" value=\"clip1\"/> \r\n" +
                            "<param name=\"LOOP\" value=\"0\"/> \r\n" +
                            "<param name=\"NUMLOOP\" value=\"0\"/> \r\n" +
                            "<param name=\"CENTER\" value=\"0\"/> \r\n" +
                            "<param name=\"MAINTAINASPECT\" value=\"0\"/> \r\n" +
                            "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\"/> \r\n" +
                            "</object>\r\n" +
                            "<object id=\"NSPlay\" classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" width=300 height=30> \r\n" +
                            "<param name=\"_ExtentX\" value=\"11298\"/> \r\n" +
                            "<param name=\"_ExtentY\" value=\"794\"/> \r\n" +
                            "<param name=\"AUTOSTART\" value=\"-1\"/> \r\n" +
                            "<param name=\"SHUFFLE\" value=\"0\"/> \r\n" +
                            "<param name=\"PREFETCH\" value=\"0\"/> \r\n" +
                            "<param name=\"NOLABELS\" value=\"-1\"/> \r\n" +
                            "<param name=\"SRC\" value=\"" + gvalur + "\";/> \r\n" +
                            "<param name=\"CONTROLS\" value=\"ControlPanel\"/> \r\n" +
                            "<param name=\"CONSOLE\" value=\"clip1\"/> \r\n" +
                            "<param name=\"LOOP\" value=\"0\"/> \r\n" +
                            "<param name=\"NUMLOOP\" value=\"0\"/> \r\n" +
                            "<param name=\"CENTER\" value=\"0\"/> \r\n" +
                            "<param name=\"MAINTAINASPECT\" value=\"0\"/> \r\n" +
                            "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\"/> \r\n" +
                             "</object> ";
                            break;
                        case ".rm":
                            content = "<object CLASSID=\"clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA\" id=\"NSPlay\"  width=\"300\"  height =\"190\" >\r\n" +
                            "<param name=\"_ExtentX\" value=\"22304\"/>\r\n" +
                            "<param  name=\"_ExtentY\" value=\"14288\"/>\r\n" +
                            "<param name=\"AUTOSTART\" value=\"-1\"/> \r\n" +
                            "<param name=\"SHUFFLE\" value=\"0\"/> \r\n" +
                            "<param name=\"PREFETCH\" value=\"0\"/> \r\n" +
                            "<param name=\"NOLABELS\" value=\"-1\"/> \r\n" +
                            "<param name=\"SRC\" value=\"" + gvalur + "\"/> \r\n" +
                            "<param name=\"CONTROLS\" value=\"ImageWindow,StatusBar,ControlPanel\"> \r\n" +
                            "<param name=\"CONSOLE\" value=\"clip1\"/> \r\n" +
                            "<param name=\"LOOP\" value=\"true\"/> \r\n" +
                            "<param name=\"NUMLOOP\" value=\"0\"/> \r\n" +
                            "<param name=\"CENTER\" value=\"0\"/> \r\n" +
                            "<param name=\"MAINTAINASPECT\" value=\"0\"/> \r\n" +
                            "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\"/> \r\n" +
                            "</object>";
                            break;
                        default:
                            content = "<OBJECT ID=\"NSPlay\" CLASSID=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\"  width=\"300\"  height =\"190\">\r\n" +
                            "<param name=\"_ExtentX\" value=\"9313\"/>\r\n" +
                            "<param name=\"_ExtentY\" value=\"7620\"/>\r\n" +
                            "<param name=\"AUTOSTART\" value=\"-1\"/>\r\n" +
                            "<param name=\"SHUFFLE\" value=\"-1\"/>\r\n" +
                            "<param name=\"PREFETCH\" value=\"-1\"/>\r\n" +
                            "<param name=\"NOLABELS\" value=\"-1\"/>\r\n" +
                            "<param name=\"SRC\" value=\"" + gvalur + "\"/>\r\n" +
                            "<param name=\"CONTROLS\" value=\"ImageWindow,StatusBar,ControlPanel\"/>\r\n" +
                            "<param name=\"CONSOLE\" value=\"Clip1\"/>\r\n" +
                            "<param name=\"LOOP\" value=\"-1\"/>\r\n" +
                            "<param name=\"NUMLOOP\" value=\"-1\"/>\r\n" +
                            "<param name=\"CENTER\" value=\"-1\"/>\r\n" +
                            "<param name=\"MAINTAINASPECT\" value=\"0\"/>\r\n" +
                            "<param name=\"BACKGROUNDCOLOR\" value=\"#000000\"/>\r\n" +
                            "</OBJECT>";
                            break;
                    }
                }
            }
            return content;
        }
        /// <summary>
        /// 读取列表数据
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable ArticleDt()
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
            dataProvider.Open();
            try
            {
              System.Data.DataTable dt =  dataProvider.ExecuteDataset(labelEntity.CommandText).Tables[0];
                dataProvider.Close();
                return dt;
            }
            catch
            {
                if(!dataProvider.IsClosed) dataProvider.Close();
                return null;
            }
        }

        /// <summary>
        /// 新闻页面数据
        /// </summary>
        /// <param name="myLabelName"></param>
        /// <returns></returns>
        public static string GetArticle(string myLabelName, Wis.Website.DataManager.Article article)
        {
            switch (myLabelName)
            { 
                case "ArticleId":
                    myLabelName = article.ArticleId.ToString();
                    break;
                case "ArticleGuid":
                    myLabelName = article.ArticleGuid.ToString();
                    break;
                case "CategoryId":
                    myLabelName = article.Category.CategoryId.ToString();
                    break;
                case "CategoryName":
                    myLabelName = article.Category.CategoryName;
                    break;
                case "TabloidPath":
                    myLabelName = article.ImagePath;
                    break;
                case "MetaKeywords":
                    myLabelName = article.MetaKeywords.ToString();
                    break;
                case "MetaDesc":
                    myLabelName = article.MetaDesc.ToString();
                    break;
                case "Title":
                    myLabelName = article.Title.ToString();
                    break;
                case "TitleColor":
                    myLabelName = article.TitleColor.ToString();
                    break;
                case "SubTitle":
                    myLabelName = article.SubTitle.ToString();
                    break;
                case "Summary":
                    myLabelName = article.Summary.ToString();
                    break;
                case "ContentHtml":
                    myLabelName = article.ContentHtml.ToString();
                    break;
                case "Author":
                    myLabelName = article.Author.ToString();
                    break;
                case "Original":
                    myLabelName = article.Original.ToString();
                    break;
                case "TemplatePath":
                    myLabelName = article.TemplatePath.ToString();
                    break;
                case "ReleasePath":
                    myLabelName = article.ReleasePath.ToString();
                    break;
                case "Hits":
                    myLabelName = article.Hits.ToString();
                    break;
                case "Comments":
                    myLabelName = article.Comments.ToString();
                    break;
                case "Votes":
                    myLabelName = article.Votes.ToString();
                    break;
                case "ArticleType":
                    myLabelName = article.ArticleType.ToString();
                    break;
                case "DateCreated":
                    myLabelName = article.DateCreated.ToShortDateString();
                    break;
                case "Files":
                    myLabelName = FileHtml(article);
                    break;
                default:
                    myLabelName = "";
                    break;
            }
            return myLabelName;
        }
        /// <summary>
        /// 获取附件HTML
        /// </summary>
        /// <returns></returns>
        public static string FileHtml(Wis.Website.DataManager.Article article)
        {
            if (article.ArticleGuid == null)
                return "";

            Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();
            List<Wis.Website.DataManager.File> files = Wis.Website.DataManager.FileManager.GetFilesBySubmissionGuid(article.ArticleGuid);
            int index = 1;
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (Wis.Website.DataManager.File file in files)
            {
                sb.Append("<p class=\"contenta\">附件：<a href=\"" + file.SaveAsFileName + "\" target=\"_blank\" title=\"点击下载附件\">" + file.OriginalFileName + "</a></p>");
                if (index == files.Count)
                {
                    sb.Append("<br/>");
                }
                index++;
            }

            return sb.ToString();
        }


        /// <summary>
        /// 生成新闻页面HTML
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool RedTemplate(Wis.Website.DataManager.Article article)
        {
            ReplaceNewsLabels(article);
            WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + article.ReleasePath + "\\" + article.ArticleId.ToString() + ".htm");
            GetListhtml(article);
            return true;
        }

        public static void Build(Wis.Website.DataManager.Article article)
        {
            ReplaceNewsLabels(article);
            WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + article.ReleasePath + "\\" + article.ArticleId.ToString() + ".htm");
            GetListhtml(article);
        }


        /// <summary>
        /// 生成单个新闻页面HTML
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool NewsHtml(Wis.Website.DataManager.Article article)
        {
            ReplaceNewsLabels(article);
            WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + article.ReleasePath + "\\" + article.ArticleId.ToString() + ".htm");
            return true;
        }


        /// <summary>
        /// 根据新闻编号生成列表html
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static void GetListhtml(Wis.Website.DataManager.Article article)
        {
            tempcontent = ReadHtml(article.Category.TemplatePath);
            ReplacelistLabels();
            WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + article.Category.ReleasePath + "\\" + article.Category.CategoryId.ToString() + ".htm");
            ///生成关联页面
            GetTemplateHtml(article.Category.CategoryId);
        }


        /// <summary>
        /// 根据栏目编号生成列表html
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public static bool GetCategoryListhtml(int categoryId)
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
            dataProvider.Open();
            string CommandText = string.Format("select * from Category where CategoryId = {0}", categoryId);
            try
            {
                System.Data.DataTable dt = dataProvider.ExecuteDataset(CommandText).Tables[0];
                dataProvider.Close();
                tempcontent = ReadHtml(dt.Rows[0]["TemplatePath"].ToString());
                ReplacelistLabels();
                WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + dt.Rows[0]["ReleasePath"].ToString());

                ///生成关联页面
                GetTemplateHtml((int)dt.Rows[0]["CategoryId"]);
                return true;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                return false;
            }

        }
        /// <summary>
        /// 生成关联页面
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static bool GetTemplateHtml(int categoryId)
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
            dataProvider.Open();
            string CommandText = string.Format("select * from Template where CategoryId ={0}", categoryId);
            try
            {
                System.Data.DataTable dt = dataProvider.ExecuteDataset(CommandText).Tables[0];
                dataProvider.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tempcontent = ReadHtml(dt.Rows[i]["TemplatePath"].ToString());
                    ReplacelistLabels();
                    WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + dt.Rows[i]["ReleasePath"].ToString());
                }
                return true;
            }
            catch
            {
                if (!dataProvider.IsClosed) dataProvider.Close();
                return false;
             
            }

        }

        public static bool linkhtml()
        {
            Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
            dataProvider.Open();
            string commandtext = string.Format("select * from Category  where CategoryId = 37");
            System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];

            tempcontent = ReadHtml(dt.Rows[0]["TemplatePath"].ToString());
            ReplacelistLabels();
            WriteHtmls(tempcontent, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + dt.Rows[0]["ReleasePath"].ToString());
            return true;
        }
        ///// <summary>
        ///// 友情连接生成
        ///// </summary>
        ///// <param name="linkguid"></param>
        //public static bool linkhtml(Guid linkguid)
        //{
        //    Wis.Toolkit.DataProvider dataProvider = new Wis.Toolkit.DataProvider(Setting.ConnectionString);
        //    dataProvider.Open();
        //    try
        //    {
        //        string commandtext = string.Format("select CategoryId from Category  where CategoryGuid in(select CategoryGuid from Link where Linkguid = '{0}')", linkguid);
        //        System.Data.DataTable dt = dataProvider.ExecuteDataset(commandtext).Tables[0];
        //        foreach (System.Data.DataRow drow in dt.Rows)
        //        {
        //            GetCategoryListhtml((int)drow[0]);
        //            commandtext = string.Format("select ArticleId from Article  where CategoryId = {0}", (int)drow[0]);
        //            System.Data.DataTable dt1 = dataProvider.ExecuteDataset(commandtext).Tables[0];
        //            foreach (System.Data.DataRow drow1 in dt1.Rows)
        //            {
        //                NewsHtml((int)drow1[0]);
        //            }
        //        }
        //        dataProvider.Close();
        //        return true;
        //    }
        //    catch
        //    {
        //        if (!dataProvider.IsClosed) dataProvider.Close();
        //        return false;
        //    }
        //}
    }
}
