using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Page
	{
		private int _PageId;

		public int PageId
		{
			get { return _PageId; }
			set { _PageId = value; }
		}

		private Guid _PageGuid;

		public Guid PageGuid
		{
			get { return _PageGuid; }
			set { _PageGuid = value; }
		}

		private string _MetaKeywords;

		public string MetaKeywords
		{
			get { return _MetaKeywords; }
			set { _MetaKeywords = value; }
		}

		private string _MetaDesc;

		public string MetaDesc
		{
			get { return _MetaDesc; }
			set { _MetaDesc = value; }
		}

		private string _Title;

		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}

		private string _ContentHtml;

		public string ContentHtml
		{
			get { return _ContentHtml; }
			set { _ContentHtml = value; }
		}

		private string _TemplatePath;

		public string TemplatePath
		{
			get { return _TemplatePath; }
			set { _TemplatePath = value; }
		}

		private string _ReleasePath;

		public string ReleasePath
		{
			get { return _ReleasePath; }
			set { _ReleasePath = value; }
		}

		private int _Hits;

		public int Hits
		{
			get { return _Hits; }
			set { _Hits = value; }
		}

		private Nullable<DateTime> _DateCreated;

		public Nullable<DateTime> DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Page()
		{ }

		public Page(int PageId,Guid PageGuid,string MetaKeywords,string MetaDesc,string Title,string ContentHtml,string TemplatePath,string ReleasePath,int Hits,Nullable<DateTime> DateCreated)
		{
			this.PageId = PageId;
			this.PageGuid = PageGuid;
			this.MetaKeywords = MetaKeywords;
			this.MetaDesc = MetaDesc;
			this.Title = Title;
			this.ContentHtml = ContentHtml;
			this.TemplatePath = TemplatePath;
			this.ReleasePath = ReleasePath;
			this.Hits = Hits;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "PageId = " + PageId.ToString() + ",PageGuid = " + PageGuid.ToString() + ",MetaKeywords = " + MetaKeywords + ",MetaDesc = " + MetaDesc + ",Title = " + Title + ",ContentHtml = " + ContentHtml + ",TemplatePath = " + TemplatePath + ",ReleasePath = " + ReleasePath + ",Hits = " + Hits.ToString() + ",DateCreated = " + DateCreated.ToString();
		}

		public class PageIdComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public PageIdComparer()
			{ }
			public PageIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.PageId.CompareTo(x.PageId);
				}
				else
				{
					return x.PageId.CompareTo(y.PageId);
				}
			}
			#endregion
		}
		public class PageGuidComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public PageGuidComparer()
			{ }
			public PageGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.PageGuid.CompareTo(x.PageGuid);
				}
				else
				{
					return x.PageGuid.CompareTo(y.PageGuid);
				}
			}
			#endregion
		}
		public class MetaKeywordsComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public MetaKeywordsComparer()
			{ }
			public MetaKeywordsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.MetaKeywords.CompareTo(x.MetaKeywords);
				}
				else
				{
					return x.MetaKeywords.CompareTo(y.MetaKeywords);
				}
			}
			#endregion
		}
		public class MetaDescComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public MetaDescComparer()
			{ }
			public MetaDescComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.MetaDesc.CompareTo(x.MetaDesc);
				}
				else
				{
					return x.MetaDesc.CompareTo(y.MetaDesc);
				}
			}
			#endregion
		}
		public class TitleComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Title.CompareTo(x.Title);
				}
				else
				{
					return x.Title.CompareTo(y.Title);
				}
			}
			#endregion
		}
		public class ContentHtmlComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public ContentHtmlComparer()
			{ }
			public ContentHtmlComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ContentHtml.CompareTo(x.ContentHtml);
				}
				else
				{
					return x.ContentHtml.CompareTo(y.ContentHtml);
				}
			}
			#endregion
		}
		public class TemplatePathComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public TemplatePathComparer()
			{ }
			public TemplatePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplatePath.CompareTo(x.TemplatePath);
				}
				else
				{
					return x.TemplatePath.CompareTo(y.TemplatePath);
				}
			}
			#endregion
		}
		public class ReleasePathComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public ReleasePathComparer()
			{ }
			public ReleasePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ReleasePath.CompareTo(x.ReleasePath);
				}
				else
				{
					return x.ReleasePath.CompareTo(y.ReleasePath);
				}
			}
			#endregion
		}
		public class HitsComparer : System.Collections.Generic.IComparer<Page>
		{
			public SorterMode SorterMode;
			public HitsComparer()
			{ }
			public HitsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Page> Membres
			int System.Collections.Generic.IComparer<Page>.Compare(Page x, Page y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Hits.CompareTo(x.Hits);
				}
				else
				{
					return x.Hits.CompareTo(y.Hits);
				}
			}
			#endregion
		}
	}
}
