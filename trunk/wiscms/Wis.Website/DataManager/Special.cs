using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Special
	{
		private int _SpecialId;

		public int SpecialId
		{
			get { return _SpecialId; }
			set { _SpecialId = value; }
		}

		private Guid _SpecialGuid;

		public Guid SpecialGuid
		{
			get { return _SpecialGuid; }
			set { _SpecialGuid = value; }
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

		private string _ImagePath;

		public string ImagePath
		{
			get { return _ImagePath; }
			set { _ImagePath = value; }
		}

		private Nullable<int> _ImageWidth;

		public Nullable<int> ImageWidth
		{
			get { return _ImageWidth; }
			set { _ImageWidth = value; }
		}

		private Nullable<int> _ImageHeight;

		public Nullable<int> ImageHeight
		{
			get { return _ImageHeight; }
			set { _ImageHeight = value; }
		}

		private int _Hits;

		public int Hits
		{
			get { return _Hits; }
			set { _Hits = value; }
		}

		private int _Comments;

		public int Comments
		{
			get { return _Comments; }
			set { _Comments = value; }
		}

		public Special()
		{ }

		public Special(int SpecialId,Guid SpecialGuid,string Title,string ContentHtml,string TemplatePath,string ReleasePath,string ImagePath,Nullable<int> ImageWidth,Nullable<int> ImageHeight,int Hits,int Comments)
		{
			this.SpecialId = SpecialId;
			this.SpecialGuid = SpecialGuid;
			this.Title = Title;
			this.ContentHtml = ContentHtml;
			this.TemplatePath = TemplatePath;
			this.ReleasePath = ReleasePath;
			this.ImagePath = ImagePath;
			this.ImageWidth = ImageWidth;
			this.ImageHeight = ImageHeight;
			this.Hits = Hits;
			this.Comments = Comments;
		}

		public override string ToString()
		{
			return "SpecialId = " + SpecialId.ToString() + ",SpecialGuid = " + SpecialGuid.ToString() + ",Title = " + Title + ",ContentHtml = " + ContentHtml + ",TemplatePath = " + TemplatePath + ",ReleasePath = " + ReleasePath + ",ImagePath = " + ImagePath + ",ImageWidth = " + ImageWidth.ToString() + ",ImageHeight = " + ImageHeight.ToString() + ",Hits = " + Hits.ToString() + ",Comments = " + Comments.ToString();
		}

		public class SpecialIdComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public SpecialIdComparer()
			{ }
			public SpecialIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SpecialId.CompareTo(x.SpecialId);
				}
				else
				{
					return x.SpecialId.CompareTo(y.SpecialId);
				}
			}
			#endregion
		}
		public class SpecialGuidComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public SpecialGuidComparer()
			{ }
			public SpecialGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SpecialGuid.CompareTo(x.SpecialGuid);
				}
				else
				{
					return x.SpecialGuid.CompareTo(y.SpecialGuid);
				}
			}
			#endregion
		}
		public class TitleComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
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
		public class ContentHtmlComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public ContentHtmlComparer()
			{ }
			public ContentHtmlComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
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
		public class TemplatePathComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public TemplatePathComparer()
			{ }
			public TemplatePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
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
		public class ReleasePathComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public ReleasePathComparer()
			{ }
			public ReleasePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
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
		public class ImagePathComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public ImagePathComparer()
			{ }
			public ImagePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ImagePath.CompareTo(x.ImagePath);
				}
				else
				{
					return x.ImagePath.CompareTo(y.ImagePath);
				}
			}
			#endregion
		}
		public class HitsComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public HitsComparer()
			{ }
			public HitsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
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
		public class CommentsComparer : System.Collections.Generic.IComparer<Special>
		{
			public SorterMode SorterMode;
			public CommentsComparer()
			{ }
			public CommentsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Special> Membres
			int System.Collections.Generic.IComparer<Special>.Compare(Special x, Special y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Comments.CompareTo(x.Comments);
				}
				else
				{
					return x.Comments.CompareTo(y.Comments);
				}
			}
			#endregion
		}
	}
}
