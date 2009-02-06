using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Original
	{
		private int _OriginalId;

		public int OriginalId
		{
			get { return _OriginalId; }
			set { _OriginalId = value; }
		}

		private Guid _OriginalGuid;

		public Guid OriginalGuid
		{
			get { return _OriginalGuid; }
			set { _OriginalGuid = value; }
		}

		private string _OriginalText;

		public string OriginalText
		{
			get { return _OriginalText; }
			set { _OriginalText = value; }
		}

		private string _HyperLink;

		public string HyperLink
		{
			get { return _HyperLink; }
			set { _HyperLink = value; }
		}

		private string _ToolTip;

		public string ToolTip
		{
			get { return _ToolTip; }
			set { _ToolTip = value; }
		}

		public Original()
		{ }

		public Original(int OriginalId,Guid OriginalGuid,string OriginalText,string HyperLink,string ToolTip)
		{
			this.OriginalId = OriginalId;
			this.OriginalGuid = OriginalGuid;
			this.OriginalText = OriginalText;
			this.HyperLink = HyperLink;
			this.ToolTip = ToolTip;
		}

		public override string ToString()
		{
			return "OriginalId = " + OriginalId.ToString() + ",OriginalGuid = " + OriginalGuid.ToString() + ",OriginalText = " + OriginalText + ",HyperLink = " + HyperLink + ",ToolTip = " + ToolTip;
		}

		public class OriginalIdComparer : System.Collections.Generic.IComparer<Original>
		{
			public SorterMode SorterMode;
			public OriginalIdComparer()
			{ }
			public OriginalIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Original> Membres
			int System.Collections.Generic.IComparer<Original>.Compare(Original x, Original y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.OriginalId.CompareTo(x.OriginalId);
				}
				else
				{
					return x.OriginalId.CompareTo(y.OriginalId);
				}
			}
			#endregion
		}
		public class OriginalGuidComparer : System.Collections.Generic.IComparer<Original>
		{
			public SorterMode SorterMode;
			public OriginalGuidComparer()
			{ }
			public OriginalGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Original> Membres
			int System.Collections.Generic.IComparer<Original>.Compare(Original x, Original y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.OriginalGuid.CompareTo(x.OriginalGuid);
				}
				else
				{
					return x.OriginalGuid.CompareTo(y.OriginalGuid);
				}
			}
			#endregion
		}
		public class OriginalTextComparer : System.Collections.Generic.IComparer<Original>
		{
			public SorterMode SorterMode;
			public OriginalTextComparer()
			{ }
			public OriginalTextComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Original> Membres
			int System.Collections.Generic.IComparer<Original>.Compare(Original x, Original y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.OriginalText.CompareTo(x.OriginalText);
				}
				else
				{
					return x.OriginalText.CompareTo(y.OriginalText);
				}
			}
			#endregion
		}
		public class HyperLinkComparer : System.Collections.Generic.IComparer<Original>
		{
			public SorterMode SorterMode;
			public HyperLinkComparer()
			{ }
			public HyperLinkComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Original> Membres
			int System.Collections.Generic.IComparer<Original>.Compare(Original x, Original y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.HyperLink.CompareTo(x.HyperLink);
				}
				else
				{
					return x.HyperLink.CompareTo(y.HyperLink);
				}
			}
			#endregion
		}
		public class ToolTipComparer : System.Collections.Generic.IComparer<Original>
		{
			public SorterMode SorterMode;
			public ToolTipComparer()
			{ }
			public ToolTipComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Original> Membres
			int System.Collections.Generic.IComparer<Original>.Compare(Original x, Original y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ToolTip.CompareTo(x.ToolTip);
				}
				else
				{
					return x.ToolTip.CompareTo(y.ToolTip);
				}
			}
			#endregion
		}
	}
}
