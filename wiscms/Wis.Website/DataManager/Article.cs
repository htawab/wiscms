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

        public Article(int ArticleId, Guid ArticleGuid, Category category, string MetaKeywords, string MetaDesc, string Title, string TitleColor, string SubTitle, string Summary, string ContentHtml, Nullable<Guid> Editor, string Author, string Original, int Rank, int Hits, int Comments, int Votes, DateTime DateCreated)
		{
			this.ArticleId = ArticleId;
			this.ArticleGuid = ArticleGuid;
            this.Category = category;
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
			this.Hits = Hits;
			this.Comments = Comments;
			this.Votes = Votes;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
            return "ArticleId = " + ArticleId.ToString() + ",ArticleGuid = " + ArticleGuid.ToString() + ",CategoryGuid = " + Category.CategoryGuid.ToString() + ",CategoryId = " + Category.CategoryId.ToString() + ",CategoryName = " + Category.CategoryName.ToString() + ",ArticleType = " + Category.ArticleType.ToString() + ", ThumbnailWidth = " + Category.ThumbnailWidth.ToString() + ",ThumbnailHeight = " + Category.ThumbnailHeight.ToString() + ", RecordCount = " + Category.RecordCount.ToString() + ", MetaKeywords = " + MetaKeywords + ",MetaDesc = " + MetaDesc + ",Title = " + Title + ",TitleColor = " + TitleColor + ",SubTitle = " + SubTitle + ",Summary = " + Summary + ",ContentHtml = " + ContentHtml + ",Editor = " + Editor.ToString() + ",Author = " + Author + ",Original = " + Original + ",Rank = " + Rank.ToString() + ",Hits = " + Hits.ToString() + ",Comments = " + Comments.ToString() + ",Votes = " + Votes.ToString() + ",DateCreated = " + DateCreated.ToString();
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
