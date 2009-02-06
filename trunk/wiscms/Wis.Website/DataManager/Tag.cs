using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Tag
	{
		private int _TagId;

		public int TagId
		{
			get { return _TagId; }
			set { _TagId = value; }
		}

		private Guid _TagGuid;

		public Guid TagGuid
		{
			get { return _TagGuid; }
			set { _TagGuid = value; }
		}

		private Guid _SubmissionGuid;

		public Guid SubmissionGuid
		{
			get { return _SubmissionGuid; }
			set { _SubmissionGuid = value; }
		}

		private string _TagName;

		public string TagName
		{
			get { return _TagName; }
			set { _TagName = value; }
		}

		private int _Hits;

		public int Hits
		{
			get { return _Hits; }
			set { _Hits = value; }
		}

		public Tag()
		{ }

		public Tag(int TagId,Guid TagGuid,Guid SubmissionGuid,string TagName,int Hits)
		{
			this.TagId = TagId;
			this.TagGuid = TagGuid;
			this.SubmissionGuid = SubmissionGuid;
			this.TagName = TagName;
			this.Hits = Hits;
		}

		public override string ToString()
		{
			return "TagId = " + TagId.ToString() + ",TagGuid = " + TagGuid.ToString() + ",SubmissionGuid = " + SubmissionGuid.ToString() + ",TagName = " + TagName + ",Hits = " + Hits.ToString();
		}

		public class TagIdComparer : System.Collections.Generic.IComparer<Tag>
		{
			public SorterMode SorterMode;
			public TagIdComparer()
			{ }
			public TagIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Tag> Membres
			int System.Collections.Generic.IComparer<Tag>.Compare(Tag x, Tag y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TagId.CompareTo(x.TagId);
				}
				else
				{
					return x.TagId.CompareTo(y.TagId);
				}
			}
			#endregion
		}
		public class TagGuidComparer : System.Collections.Generic.IComparer<Tag>
		{
			public SorterMode SorterMode;
			public TagGuidComparer()
			{ }
			public TagGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Tag> Membres
			int System.Collections.Generic.IComparer<Tag>.Compare(Tag x, Tag y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TagGuid.CompareTo(x.TagGuid);
				}
				else
				{
					return x.TagGuid.CompareTo(y.TagGuid);
				}
			}
			#endregion
		}
		public class SubmissionGuidComparer : System.Collections.Generic.IComparer<Tag>
		{
			public SorterMode SorterMode;
			public SubmissionGuidComparer()
			{ }
			public SubmissionGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Tag> Membres
			int System.Collections.Generic.IComparer<Tag>.Compare(Tag x, Tag y)
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
		public class TagNameComparer : System.Collections.Generic.IComparer<Tag>
		{
			public SorterMode SorterMode;
			public TagNameComparer()
			{ }
			public TagNameComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Tag> Membres
			int System.Collections.Generic.IComparer<Tag>.Compare(Tag x, Tag y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TagName.CompareTo(x.TagName);
				}
				else
				{
					return x.TagName.CompareTo(y.TagName);
				}
			}
			#endregion
		}
		public class HitsComparer : System.Collections.Generic.IComparer<Tag>
		{
			public SorterMode SorterMode;
			public HitsComparer()
			{ }
			public HitsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Tag> Membres
			int System.Collections.Generic.IComparer<Tag>.Compare(Tag x, Tag y)
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
