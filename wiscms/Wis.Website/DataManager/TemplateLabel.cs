using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class TemplateLabel
	{
		private int _TemplateLabelId;

		public int TemplateLabelId
		{
			get { return _TemplateLabelId; }
			set { _TemplateLabelId = value; }
		}

		private Guid _TemplateLabelGuid;

		public Guid TemplateLabelGuid
		{
			get { return _TemplateLabelGuid; }
			set { _TemplateLabelGuid = value; }
		}

		private string _TemplateLabelName;

		public string TemplateLabelName
		{
			get { return _TemplateLabelName; }
			set { _TemplateLabelName = value; }
		}

		private string _TemplateLabelValue;

		public string TemplateLabelValue
		{
			get { return _TemplateLabelValue; }
			set { _TemplateLabelValue = value; }
		}

		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		private DateTime _DateCreated;

		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public TemplateLabel()
		{ }

		public TemplateLabel(int TemplateLabelId,Guid TemplateLabelGuid,string TemplateLabelName,string TemplateLabelValue,string Description,DateTime DateCreated)
		{
			this.TemplateLabelId = TemplateLabelId;
			this.TemplateLabelGuid = TemplateLabelGuid;
			this.TemplateLabelName = TemplateLabelName;
			this.TemplateLabelValue = TemplateLabelValue;
			this.Description = Description;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "TemplateLabelId = " + TemplateLabelId.ToString() + ",TemplateLabelGuid = " + TemplateLabelGuid.ToString() + ",TemplateLabelName = " + TemplateLabelName + ",TemplateLabelValue = " + TemplateLabelValue + ",Description = " + Description + ",DateCreated = " + DateCreated.ToString();
		}

		public class TemplateLabelIdComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public TemplateLabelIdComparer()
			{ }
			public TemplateLabelIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateLabelId.CompareTo(x.TemplateLabelId);
				}
				else
				{
					return x.TemplateLabelId.CompareTo(y.TemplateLabelId);
				}
			}
			#endregion
		}
		public class TemplateLabelGuidComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public TemplateLabelGuidComparer()
			{ }
			public TemplateLabelGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateLabelGuid.CompareTo(x.TemplateLabelGuid);
				}
				else
				{
					return x.TemplateLabelGuid.CompareTo(y.TemplateLabelGuid);
				}
			}
			#endregion
		}
		public class TemplateLabelNameComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public TemplateLabelNameComparer()
			{ }
			public TemplateLabelNameComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateLabelName.CompareTo(x.TemplateLabelName);
				}
				else
				{
					return x.TemplateLabelName.CompareTo(y.TemplateLabelName);
				}
			}
			#endregion
		}
		public class TemplateLabelValueComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public TemplateLabelValueComparer()
			{ }
			public TemplateLabelValueComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateLabelValue.CompareTo(x.TemplateLabelValue);
				}
				else
				{
					return x.TemplateLabelValue.CompareTo(y.TemplateLabelValue);
				}
			}
			#endregion
		}
		public class DescriptionComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public DescriptionComparer()
			{ }
			public DescriptionComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Description.CompareTo(x.Description);
				}
				else
				{
					return x.Description.CompareTo(y.Description);
				}
			}
			#endregion
		}
		public class DateCreatedComparer : System.Collections.Generic.IComparer<TemplateLabel>
		{
			public SorterMode SorterMode;
			public DateCreatedComparer()
			{ }
			public DateCreatedComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<TemplateLabel> Membres
			int System.Collections.Generic.IComparer<TemplateLabel>.Compare(TemplateLabel x, TemplateLabel y)
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
