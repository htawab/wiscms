using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Link
	{
		private int _LinkId;

		public int LinkId
		{
			get { return _LinkId; }
			set { _LinkId = value; }
		}

		private Guid _LinkGuid;

		public Guid LinkGuid
		{
			get { return _LinkGuid; }
			set { _LinkGuid = value; }
		}

		private string _LinkName;

		public string LinkName
		{
			get { return _LinkName; }
			set { _LinkName = value; }
		}

		private string _LinkUrl;

		public string LinkUrl
		{
			get { return _LinkUrl; }
			set { _LinkUrl = value; }
		}

		private string _Remark;

		public string Remark
		{
			get { return _Remark; }
			set { _Remark = value; }
		}

		private string _Logo;

		public string Logo
		{
			get { return _Logo; }
			set { _Logo = value; }
		}

		private int _Rank;

		public int Rank
		{
			get { return _Rank; }
			set { _Rank = value; }
		}

		private DateTime _DateCreated;

		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Link()
		{ }

		public Link(int LinkId,Guid LinkGuid,string LinkName,string LinkUrl,string Remark,string Logo,int Rank,DateTime DateCreated)
		{
			this.LinkId = LinkId;
			this.LinkGuid = LinkGuid;
			this.LinkName = LinkName;
			this.LinkUrl = LinkUrl;
			this.Remark = Remark;
			this.Logo = Logo;
			this.Rank = Rank;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "LinkId = " + LinkId.ToString() + ",LinkGuid = " + LinkGuid.ToString() + ",LinkName = " + LinkName + ",LinkUrl = " + LinkUrl + ",Remark = " + Remark + ",Logo = " + Logo + ",Rank = " + Rank.ToString() + ",DateCreated = " + DateCreated.ToString();
		}

		public class LinkIdComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public LinkIdComparer()
			{ }
			public LinkIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.LinkId.CompareTo(x.LinkId);
				}
				else
				{
					return x.LinkId.CompareTo(y.LinkId);
				}
			}
			#endregion
		}
		public class LinkGuidComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public LinkGuidComparer()
			{ }
			public LinkGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.LinkGuid.CompareTo(x.LinkGuid);
				}
				else
				{
					return x.LinkGuid.CompareTo(y.LinkGuid);
				}
			}
			#endregion
		}
		public class LinkNameComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public LinkNameComparer()
			{ }
			public LinkNameComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.LinkName.CompareTo(x.LinkName);
				}
				else
				{
					return x.LinkName.CompareTo(y.LinkName);
				}
			}
			#endregion
		}
		public class LinkUrlComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public LinkUrlComparer()
			{ }
			public LinkUrlComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.LinkUrl.CompareTo(x.LinkUrl);
				}
				else
				{
					return x.LinkUrl.CompareTo(y.LinkUrl);
				}
			}
			#endregion
		}
		public class RemarkComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public RemarkComparer()
			{ }
			public RemarkComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Remark.CompareTo(x.Remark);
				}
				else
				{
					return x.Remark.CompareTo(y.Remark);
				}
			}
			#endregion
		}
		public class LogoComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public LogoComparer()
			{ }
			public LogoComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Logo.CompareTo(x.Logo);
				}
				else
				{
					return x.Logo.CompareTo(y.Logo);
				}
			}
			#endregion
		}
		public class RankComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public RankComparer()
			{ }
			public RankComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Rank.CompareTo(x.Rank);
				}
				else
				{
					return x.Rank.CompareTo(y.Rank);
				}
			}
			#endregion
		}
		public class DateCreatedComparer : System.Collections.Generic.IComparer<Link>
		{
			public SorterMode SorterMode;
			public DateCreatedComparer()
			{ }
			public DateCreatedComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Link> Membres
			int System.Collections.Generic.IComparer<Link>.Compare(Link x, Link y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.DateCreated.CompareTo(x.DateCreated);
				}
				else
				{
					return x.DateCreated.CompareTo(y.DateCreated);
				}
			}
			#endregion
		}
	}
}
