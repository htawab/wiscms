using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Comment
	{
		private int _CommentId;

		public int CommentId
		{
			get { return _CommentId; }
			set { _CommentId = value; }
		}

		private Nullable<Guid> _CommentGuid;

		public Nullable<Guid> CommentGuid
		{
			get { return _CommentGuid; }
			set { _CommentGuid = value; }
		}

		private Guid _SubmissionGuid;

		public Guid SubmissionGuid
		{
			get { return _SubmissionGuid; }
			set { _SubmissionGuid = value; }
		}

		private string _Commentator;

		public string Commentator
		{
			get { return _Commentator; }
			set { _Commentator = value; }
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

		private string _Original;

		public string Original
		{
			get { return _Original; }
			set { _Original = value; }
		}

		private string _IPAddress;

		public string IPAddress
		{
			get { return _IPAddress; }
			set { _IPAddress = value; }
		}

		private Nullable<DateTime> _DateCreated;

		public Nullable<DateTime> DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Comment()
		{ }

		public Comment(int CommentId,Nullable<Guid> CommentGuid,Guid SubmissionGuid,string Commentator,string Title,string ContentHtml,string Original,string IPAddress,Nullable<DateTime> DateCreated)
		{
			this.CommentId = CommentId;
			this.CommentGuid = CommentGuid;
			this.SubmissionGuid = SubmissionGuid;
			this.Commentator = Commentator;
			this.Title = Title;
			this.ContentHtml = ContentHtml;
			this.Original = Original;
			this.IPAddress = IPAddress;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "CommentId = " + CommentId.ToString() + ",CommentGuid = " + CommentGuid.ToString() + ",SubmissionGuid = " + SubmissionGuid.ToString() + ",Commentator = " + Commentator + ",Title = " + Title + ",ContentHtml = " + ContentHtml + ",Original = " + Original + ",IPAddress = " + IPAddress + ",DateCreated = " + DateCreated.ToString();
		}

		public class CommentIdComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public CommentIdComparer()
			{ }
			public CommentIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CommentId.CompareTo(x.CommentId);
				}
				else
				{
					return x.CommentId.CompareTo(y.CommentId);
				}
			}
			#endregion
		}
		public class SubmissionGuidComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public SubmissionGuidComparer()
			{ }
			public SubmissionGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SubmissionGuid.CompareTo(x.SubmissionGuid);
				}
				else
				{
					return x.SubmissionGuid.CompareTo(y.SubmissionGuid);
				}
			}
			#endregion
		}
		public class CommentatorComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public CommentatorComparer()
			{ }
			public CommentatorComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Commentator.CompareTo(x.Commentator);
				}
				else
				{
					return x.Commentator.CompareTo(y.Commentator);
				}
			}
			#endregion
		}
		public class TitleComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
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
		public class ContentHtmlComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public ContentHtmlComparer()
			{ }
			public ContentHtmlComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
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
		public class OriginalComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public OriginalComparer()
			{ }
			public OriginalComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Original.CompareTo(x.Original);
				}
				else
				{
					return x.Original.CompareTo(y.Original);
				}
			}
			#endregion
		}
		public class IPAddressComparer : System.Collections.Generic.IComparer<Comment>
		{
			public SorterMode SorterMode;
			public IPAddressComparer()
			{ }
			public IPAddressComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Comment> Membres
			int System.Collections.Generic.IComparer<Comment>.Compare(Comment x, Comment y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.IPAddress.CompareTo(x.IPAddress);
				}
				else
				{
					return x.IPAddress.CompareTo(y.IPAddress);
				}
			}
			#endregion
		}
	}
}
