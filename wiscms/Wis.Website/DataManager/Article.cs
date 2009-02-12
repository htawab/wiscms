//------------------------------------------------------------------------------
// <copyright file="Article.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Article
	{
		private int _ArticleId;

		public int ArticleId
		{
			get { return _ArticleId; }
			set { _ArticleId = value; }
		}

		private Guid _ArticleGuid;

		public Guid ArticleGuid
		{
			get { return _ArticleGuid; }
			set { _ArticleGuid = value; }
		}

        private Category _Category;
        /// <summary>
        /// ∑÷¿‡°£
        /// </summary>
        public Category Category
		{
            get 
            {
                if (_Category == null)
                    _Category = new Category();

                return _Category;
            }
            set { _Category = value; }
		}

        private ArticleType _ArticleType;

        public ArticleType ArticleType
		{
			get { return _ArticleType; }
			set { _ArticleType = value; }
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

		private string _TitleColor;

		public string TitleColor
		{
			get { return _TitleColor; }
			set { _TitleColor = value; }
		}

		private string _SubTitle;

		public string SubTitle
		{
			get { return _SubTitle; }
			set { _SubTitle = value; }
		}

		private string _Summary;

		public string Summary
		{
			get { return _Summary; }
			set { _Summary = value; }
		}

		private string _ContentHtml;

		public string ContentHtml
		{
			get { return _ContentHtml; }
			set { _ContentHtml = value; }
		}

		private Nullable<Guid> _Editor;

		public Nullable<Guid> Editor
		{
			get { return _Editor; }
			set { _Editor = value; }
		}

		private string _Author;

		public string Author
		{
			get { return _Author; }
			set { _Author = value; }
		}

		private string _Original;

		public string Original
		{
			get { return _Original; }
			set { _Original = value; }
		}

		private int _Rank;

		public int Rank
		{
			get { return _Rank; }
			set { _Rank = value; }
		}

		private Nullable<Guid> _SpecialGuid;

		public Nullable<Guid> SpecialGuid
		{
			get { return _SpecialGuid; }
			set { _SpecialGuid = value; }
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

		private int _Comments;

		public int Comments
		{
			get { return _Comments; }
			set { _Comments = value; }
		}

		private int _Votes;

		public int Votes
		{
			get { return _Votes; }
			set { _Votes = value; }
		}

		private DateTime _DateCreated;

		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Article()
		{ }

        public Article(int ArticleId, Guid ArticleGuid, Category category, ArticleType articleType, string ImagePath, Nullable<int> ImageWidth, Nullable<int> ImageHeight, string MetaKeywords, string MetaDesc, string Title, string TitleColor, string SubTitle, string Summary, string ContentHtml, Nullable<Guid> Editor, string Author, string Original, int Rank, Nullable<Guid> SpecialGuid, string TemplatePath, string ReleasePath, int Hits, int Comments, int Votes, DateTime DateCreated)
		{
			this.ArticleId = ArticleId;
			this.ArticleGuid = ArticleGuid;
            this.Category = category;
			this.ArticleType = articleType;
			this.ImagePath = ImagePath;
			this.ImageWidth = ImageWidth;
			this.ImageHeight = ImageHeight;
			this.MetaKeywords = MetaKeywords;
			this.MetaDesc = MetaDesc;
			this.Title = Title;
			this.TitleColor = TitleColor;
			this.SubTitle = SubTitle;
			this.Summary = Summary;
			this.ContentHtml = ContentHtml;
			this.Editor = Editor;
			this.Author = Author;
			this.Original = Original;
			this.Rank = Rank;
			this.SpecialGuid = SpecialGuid;
			this.TemplatePath = TemplatePath;
			this.ReleasePath = ReleasePath;
			this.Hits = Hits;
			this.Comments = Comments;
			this.Votes = Votes;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
            return "ArticleId = " + ArticleId.ToString() + ",ArticleGuid = " + ArticleGuid.ToString() + ",CategoryGuid = " + Category.CategoryGuid.ToString() + ",CategoryId = " + Category.CategoryId.ToString() + ",CategoryName = " + Category.CategoryName.ToString() + ",ArticleType = " + ArticleType.ToString() + ",ImagePath = " + ImagePath + ",ImageWidth = " + ImageWidth.ToString() + ",ImageHeight = " + ImageHeight.ToString() + ",MetaKeywords = " + MetaKeywords + ",MetaDesc = " + MetaDesc + ",Title = " + Title + ",TitleColor = " + TitleColor + ",SubTitle = " + SubTitle + ",Summary = " + Summary + ",ContentHtml = " + ContentHtml + ",Editor = " + Editor.ToString() + ",Author = " + Author + ",Original = " + Original + ",Rank = " + Rank.ToString() + ",SpecialGuid = " + SpecialGuid.ToString() + ",TemplatePath = " + TemplatePath + ",ReleasePath = " + ReleasePath + ",Hits = " + Hits.ToString() + ",Comments = " + Comments.ToString() + ",Votes = " + Votes.ToString() + ",DateCreated = " + DateCreated.ToString();
		}

		public class ArticleIdComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ArticleIdComparer()
			{ }
			public ArticleIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ArticleId.CompareTo(x.ArticleId);
				}
				else
				{
					return x.ArticleId.CompareTo(y.ArticleId);
				}
			}
			#endregion
		}
		public class ArticleGuidComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ArticleGuidComparer()
			{ }
			public ArticleGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ArticleGuid.CompareTo(x.ArticleGuid);
				}
				else
				{
					return x.ArticleGuid.CompareTo(y.ArticleGuid);
				}
			}
			#endregion
		}

		public class ArticleTypeComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ArticleTypeComparer()
			{ }
			public ArticleTypeComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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

		public class ImagePathComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ImagePathComparer()
			{ }
			public ImagePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class MetaKeywordsComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public MetaKeywordsComparer()
			{ }
			public MetaKeywordsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class MetaDescComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public MetaDescComparer()
			{ }
			public MetaDescComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class TitleComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class TitleColorComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public TitleColorComparer()
			{ }
			public TitleColorComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.TitleColor.CompareTo(x.TitleColor);
				}
				else
				{
					return x.TitleColor.CompareTo(y.TitleColor);
				}
			}
			#endregion
		}
		public class SubTitleComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public SubTitleComparer()
			{ }
			public SubTitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SubTitle.CompareTo(x.SubTitle);
				}
				else
				{
					return x.SubTitle.CompareTo(y.SubTitle);
				}
			}
			#endregion
		}
		public class SummaryComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public SummaryComparer()
			{ }
			public SummaryComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Summary.CompareTo(x.Summary);
				}
				else
				{
					return x.Summary.CompareTo(y.Summary);
				}
			}
			#endregion
		}
		public class ContentHtmlComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ContentHtmlComparer()
			{ }
			public ContentHtmlComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class AuthorComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public AuthorComparer()
			{ }
			public AuthorComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Author.CompareTo(x.Author);
				}
				else
				{
					return x.Author.CompareTo(y.Author);
				}
			}
			#endregion
		}
		public class OriginalComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public OriginalComparer()
			{ }
			public OriginalComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class RankComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public RankComparer()
			{ }
			public RankComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class TemplatePathComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public TemplatePathComparer()
			{ }
			public TemplatePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class ReleasePathComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public ReleasePathComparer()
			{ }
			public ReleasePathComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class HitsComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public HitsComparer()
			{ }
			public HitsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class CommentsComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public CommentsComparer()
			{ }
			public CommentsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
		public class VotesComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public VotesComparer()
			{ }
			public VotesComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Votes.CompareTo(x.Votes);
				}
				else
				{
					return x.Votes.CompareTo(y.Votes);
				}
			}
			#endregion
		}
		public class DateCreatedComparer : System.Collections.Generic.IComparer<Article>
		{
			public SorterMode SorterMode;
			public DateCreatedComparer()
			{ }
			public DateCreatedComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Article> Membres
			int System.Collections.Generic.IComparer<Article>.Compare(Article x, Article y)
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
