using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Template
	{
		private int _TemplateId;

		public int TemplateId
		{
			get { return _TemplateId; }
			set { _TemplateId = value; }
		}

		private Guid _TemplateGuid;

		public Guid TemplateGuid
		{
			get { return _TemplateGuid; }
			set { _TemplateGuid = value; }
		}

		private string _Title;

		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}

		private string _TemplatePath;

		public string TemplatePath
		{
			get { return _TemplatePath; }
			set { _TemplatePath = value; }
		}

		private SByte _TemplateType;

		public SByte TemplateType
		{
			get { return _TemplateType; }
			set { _TemplateType = value; }
		}

		private SByte _ArticleType;

		public SByte ArticleType
		{
			get { return _ArticleType; }
			set { _ArticleType = value; }
		}

		public Template()
		{ }

		public Template(int TemplateId,Guid TemplateGuid,string Title,string TemplatePath,SByte TemplateType,SByte ArticleType)
		{
			this.TemplateId = TemplateId;
			this.TemplateGuid = TemplateGuid;
			this.Title = Title;
			this.TemplatePath = TemplatePath;
			this.TemplateType = TemplateType;
			this.ArticleType = ArticleType;
		}

		public override string ToString()
		{
			return "TemplateId = " + TemplateId.ToString() + ",TemplateGuid = " + TemplateGuid.ToString() + ",Title = " + Title + ",TemplatePath = " + TemplatePath + ",TemplateType = " + TemplateType.ToString() + ",ArticleType = " + ArticleType.ToString();
		}

		public class TemplateIdComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public TemplateIdComparer()
			{ }
			public TemplateIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateId.CompareTo(x.TemplateId);
				}
				else
				{
					return x.TemplateId.CompareTo(y.TemplateId);
				}
			}
			#endregion
		}
		public class TemplateGuidComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public TemplateGuidComparer()
			{ }
			public TemplateGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateGuid.CompareTo(x.TemplateGuid);
				}
				else
				{
					return x.TemplateGuid.CompareTo(y.TemplateGuid);
				}
			}
			#endregion
		}
		public class TitleComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
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
		public class TemplatePathComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public TemplatePathComparer()
			{ }
			public TemplatePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
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
		public class TemplateTypeComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public TemplateTypeComparer()
			{ }
			public TemplateTypeComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TemplateType.CompareTo(x.TemplateType);
				}
				else
				{
					return x.TemplateType.CompareTo(y.TemplateType);
				}
			}
			#endregion
		}
		public class ArticleTypeComparer : System.Collections.Generic.IComparer<Template>
		{
			public SorterMode SorterMode;
			public ArticleTypeComparer()
			{ }
			public ArticleTypeComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Template> Membres
			int System.Collections.Generic.IComparer<Template>.Compare(Template x, Template y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ArticleType.CompareTo(x.ArticleType);
				}
				else
				{
					return x.ArticleType.CompareTo(y.ArticleType);
				}
			}
			#endregion
		}
	}
}
