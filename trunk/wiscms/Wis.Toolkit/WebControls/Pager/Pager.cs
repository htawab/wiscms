//------------------------------------------------------------------------------
// <copyright file="Pager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web.UI;

[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.Pager.Pager.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.Pager.Pager.css", "text/css")]

namespace Wis.Toolkit.WebControls
{

    public class Pager : Control, INamingContainer
    {
        /// <summary>
        /// Complied with UI concept: page never starts from 0.
        /// </summary>
        private const string PageControlIdPrefix = "pager";

        private string pagerId = string.Empty;
        /// <summary>
        /// pager client id.
        /// </summary>
        protected string PagerId
        {
            get
            {
                if (string.IsNullOrEmpty(pagerId))
                    pagerId = string.Concat(PageControlIdPrefix, this.ClientID);

                return pagerId;
            }
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            string registeredKey = "PagerScript";
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(registeredKey))
            {
//#if DEBUG
                // this.GetType().Namespace
                System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream("Wis.Toolkit.WebControls.Pager.Pager.js");
                System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
                string script = string.Format("<script type='text/javascript'>\n{0}\n</script>\n", sr.ReadToEnd());

#warning TODO:ɾ��template���滻$Ϊ����$�����
                script = script.Replace("$", "$$");

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), registeredKey, script);
//#else
//                this.Page.ClientScript.RegisterClientScriptInclude(
//                         this.GetType(), "Pager",
//                         Page.ClientScript.GetWebResourceUrl(this.GetType(),
//                         "Wis.Toolkit.WebControls.Pager.Pager.js"));
//#endif
            }

            registeredKey = "PagerCss";
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered(registeredKey))
            {
                System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream("Wis.Toolkit.WebControls.Pager.Pager.css");
                System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), registeredKey,
                        @"<style type=""text/css"">
                        " + sr.ReadToEnd() + @"
                        </style>");
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
/*
Example
----------------------
var pg = new Pager('pg');
pg.pageCount = 12; //������ҳ��(��Ҫ)
pg.argName = 'p';  //���������(��ѡ,ȱʡΪpage)
pg.printHtml();    //��ʾ

Supported in Internet Explorer, Mozilla Firefox
*/
            string html = string.Format(@"<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>
	<tbody>
    <tr>
		<td align='right'>
			<script language='JavaScript' type='text/javascript'>
				<!--
					var {0} = new Pager('{0}');
					{0}.PageSize = {1};
					{0}.RecordCount = {2};
					{0}.ArgumentName = '{3}';
					{0}.CreateHtml({4});
				//-->
			</script>
		</td>
    </tr>
    </tbody>
</table>", this.PagerId, this.PageSize, this.RecordCount, this.ArgumentName, this.PagerMode);
            output.Write(html);
        }

        /// <summary>
        /// ��ǰҳ����
        /// </summary>
        public int PageIndex
        {
            get
            {
                int pageIndex;
                if (int.TryParse(Page.Request["PageIndex"], out pageIndex) == false)
                    pageIndex = 1;

                if (pageIndex < 1) pageIndex = 1;
                if (pageIndex > this.PageCount) pageIndex = this.PageCount;

                return pageIndex;
            }
        }

        private string argumentName;
        /// <summary>
        /// ÿҳ��ʾ��¼����
        /// </summary>
        public string ArgumentName
        {
            get 
            {
                if (string.IsNullOrEmpty(argumentName))
                    argumentName = "PageIndex";

                return argumentName;
            }
            set { argumentName = value;}
        }


        private int pagerMode = 1;
        /// <summary>
        /// ��ҳģʽ��
        /// </summary>
        public int PagerMode
        {
            get { return pagerMode; }
            set
            {   // 0-5
                if (value >= 0 && value <= 5)
                    pagerMode = value;
            }
        }


        private int pageSize = 10;
        /// <summary>
        /// ÿҳ��ʾ��¼����
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value > 0)
                    pageSize = value;
            }
        }

        /// <summary>
        /// ��ҳ����
        /// </summary>
        public int PageCount
        {
            get { return RecordCount / PageSize + ((RecordCount % PageSize == 0) ? 0 : 1); }
        }

        private int recordCount;
        /// <summary>
        /// �ܼ�¼����
        /// </summary>
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }
    }
}
