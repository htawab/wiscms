//------------------------------------------------------------------------------
// <copyright file="Release.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Release
	{
		private int _ReleaseId;

		public int ReleaseId
		{
			get { return _ReleaseId; }
			set { _ReleaseId = value; }
		}

		private Guid _ReleaseGuid;

		public Guid ReleaseGuid
		{
			get { return _ReleaseGuid; }
			set { _ReleaseGuid = value; }
		}

		private Guid _CategoryGuid;

		public Guid CategoryGuid
		{
			get { return _CategoryGuid; }
			set { _CategoryGuid = value; }
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

		public Release()
		{ }

		public Release(int ReleaseId,Guid ReleaseGuid,Guid CategoryGuid,string TemplatePath,string ReleasePath)
		{
			this.ReleaseId = ReleaseId;
			this.ReleaseGuid = ReleaseGuid;
			this.CategoryGuid = CategoryGuid;
			this.TemplatePath = TemplatePath;
			this.ReleasePath = ReleasePath;
		}

		public override string ToString()
		{
			return "ReleaseId = " + ReleaseId.ToString() + ",ReleaseGuid = " + ReleaseGuid.ToString() + ",CategoryGuid = " + CategoryGuid.ToString() + ",TemplatePath = " + TemplatePath + ",ReleasePath = " + ReleasePath;
		}

		public class ReleaseIdComparer : System.Collections.Generic.IComparer<Release>
		{
			public SorterMode SorterMode;
			public ReleaseIdComparer()
			{ }
			public ReleaseIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Release> Membres
			int System.Collections.Generic.IComparer<Release>.Compare(Release x, Release y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ReleaseId.CompareTo(x.ReleaseId);
				}
				else
				{
					return x.ReleaseId.CompareTo(y.ReleaseId);
				}
			}
			#endregion
		}
		public class ReleaseGuidComparer : System.Collections.Generic.IComparer<Release>
		{
			public SorterMode SorterMode;
			public ReleaseGuidComparer()
			{ }
			public ReleaseGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Release> Membres
			int System.Collections.Generic.IComparer<Release>.Compare(Release x, Release y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ReleaseGuid.CompareTo(x.ReleaseGuid);
				}
				else
				{
					return x.ReleaseGuid.CompareTo(y.ReleaseGuid);
				}
			}
			#endregion
		}
		public class CategoryGuidComparer : System.Collections.Generic.IComparer<Release>
		{
			public SorterMode SorterMode;
			public CategoryGuidComparer()
			{ }
			public CategoryGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Release> Membres
			int System.Collections.Generic.IComparer<Release>.Compare(Release x, Release y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CategoryGuid.CompareTo(x.CategoryGuid);
				}
				else
				{
					return x.CategoryGuid.CompareTo(y.CategoryGuid);
				}
			}
			#endregion
		}
		public class TemplatePathComparer : System.Collections.Generic.IComparer<Release>
		{
			public SorterMode SorterMode;
			public TemplatePathComparer()
			{ }
			public TemplatePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Release> Membres
			int System.Collections.Generic.IComparer<Release>.Compare(Release x, Release y)
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
		public class ReleasePathComparer : System.Collections.Generic.IComparer<Release>
		{
			public SorterMode SorterMode;
			public ReleasePathComparer()
			{ }
			public ReleasePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Release> Membres
			int System.Collections.Generic.IComparer<Release>.Compare(Release x, Release y)
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
	}
}
