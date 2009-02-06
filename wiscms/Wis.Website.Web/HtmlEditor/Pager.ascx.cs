using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Wis.Website.Web.Admin.UserControls
{
    public partial class Pager : System.Web.UI.UserControl
    {
        protected const string PagerInfoFormat = "共有{0}页,当前第{1}页";
        #region Properties
        private int mPageSize;

        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get {
                if (mPageSize < 3)
                    mPageSize = 10;
                return mPageSize; }
            set { mPageSize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RecordCount
        {
            get { return int.Parse(hRecordCount.Value); }
            set { hRecordCount.Value = value.ToString(); }
        }

        public int CurrentPageIndex
        {
            get { return int.Parse(currentpage.Value); }
            set { currentpage.Value = value.ToString(); }
        }

        private int mPageCount;
        public int PageCount
        {
            get {
                mPageCount = RecordCount / PageSize;
                if (RecordCount % PageSize != 0)
                {
                    mPageCount += 1;
                }
                
                return mPageCount; }
        }
	

        #endregion
        public event EventHandler PageIndexChange; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                this.firstpage.Text = "首页";
                this.prepage.Text = "上一页";
                this.nextpage.Text = "下一页";
                this.lastpage.Text = "尾页";

            }
           // this.pagerSummary.InnerHtml = string.Format(pagerSummary, PageCount, CurrentPage);
            
        }
        protected void Page_PreRender(object sender, EventArgs e)
        { 
        //Do render
            SetValue();
           
        }

        protected void Firstpage_Click(object sender, EventArgs e)
        {
            this.CurrentPageIndex = 1;
            PageIndexChange(this,null );
        }

        protected void Prepage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = CurrentPageIndex - 1;
            PageIndexChange(this,null);
        }

        protected void Nextpage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex += 1;
            PageIndexChange(this, null);
        }

        protected void Lastpage_Click(object sender, EventArgs e)
        {
            CurrentPageIndex = PageCount;
            PageIndexChange(this, null);
        }

        private void SetValue()
        {
            this.pagerSummary.InnerHtml = string.Format(PagerInfoFormat, PageCount, CurrentPageIndex);
            if (PageCount > 1)
            {
                if (CurrentPageIndex == 1)
                { 
                this.firstpage.Enabled = false;
                this.prepage.Enabled = false;
                this.nextpage.Enabled = true;
                this.lastpage.Enabled = true;
                }
                else if (1 < CurrentPageIndex && CurrentPageIndex < PageCount)
                {
                    this.firstpage.Enabled = true;
                    this.prepage.Enabled = true;
                    this.nextpage.Enabled = true;
                    this.lastpage.Enabled = true;
                }
                else if (CurrentPageIndex == PageCount)
                {
                    this.firstpage.Enabled = true ;
                    this.prepage.Enabled = true;
                    this.nextpage.Enabled = false;
                    this.lastpage.Enabled = false;
                }
            }
            else
            {
                this.Visible = false;
            }

        }

    }
}