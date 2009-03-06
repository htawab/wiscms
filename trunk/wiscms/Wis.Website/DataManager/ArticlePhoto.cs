//------------------------------------------------------------------------------
// <copyright file="ArticlePhoto.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// Í¼Æ¬ÐÂÎÅµÄÍ¼Æ¬ÐÅÏ¢
    /// </summary>
	[Serializable()]
    public class ArticlePhoto
	{
		private int _ArticlePhotoId;
        /// <summary>
        /// Í¼Æ¬±àºÅ
        /// </summary>
		public int ArticlePhotoId
		{
			get { return _ArticlePhotoId; }
			set { _ArticlePhotoId = value; }
		}

		private Guid _ArticlePhotoGuid;
        /// <summary>
        /// Í¼Æ¬±àºÅ
        /// </summary>
        public Guid ArticlePhotoGuid
		{
			get { return _ArticlePhotoGuid; }
			set { _ArticlePhotoGuid = value; }
		}

        private Article _Article;
        /// <summary>
        /// ÎÄÕÂÐÅÏ¢
        /// </summary>
        public Article Article
		{
            get { return _Article; }
            set { _Article = value; }
		}

		private string _SourcePath;
        /// <summary>
        /// 
        /// </summary>
		public string SourcePath
		{
			get { return _SourcePath; }
			set { _SourcePath = value; }
		}

		private string _ThumbnailPath;
        /// <summary>
        /// 
        /// </summary>
		public string ThumbnailPath
		{
			get { return _ThumbnailPath; }
			set { _ThumbnailPath = value; }
		}

		private Nullable<int> _PointX;
        /// <summary>
        /// 
        /// </summary>
		public Nullable<int> PointX
		{
			get { return _PointX; }
			set { _PointX = value; }
		}

		private Nullable<int> _PointY;
        /// <summary>
        /// 
        /// </summary>
		public Nullable<int> PointY
		{
			get { return _PointY; }
			set { _PointY = value; }
		}

		private Nullable<bool> _Stretch;
        /// <summary>
        /// 
        /// </summary>
		public Nullable<bool> Stretch
		{
			get { return _Stretch; }
			set { _Stretch = value; }
		}

		private Nullable<bool> _Beveled;
        /// <summary>
        /// 
        /// </summary>
		public Nullable<bool> Beveled
		{
			get { return _Beveled; }
			set { _Beveled = value; }
		}

		private string _CreatedBy;
        /// <summary>
        /// 
        /// </summary>
		public string CreatedBy
		{
			get { return _CreatedBy; }
			set { _CreatedBy = value; }
		}

		private DateTime _CreationDate;
        /// <summary>
        /// 
        /// </summary>
		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}

        /// <summary>
        /// 
        /// </summary>
		public ArticlePhoto()
		{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articlePhotoId"></param>
        /// <param name="articlePhotoGuid"></param>
        /// <param name="article"></param>
        /// <param name="sourcePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="stretch"></param>
        /// <param name="beveled"></param>
        /// <param name="createdBy"></param>
        /// <param name="creationDate"></param>
        public ArticlePhoto(int articlePhotoId, Guid articlePhotoGuid, Article article, string sourcePath, string thumbnailPath, Nullable<int> PointX, Nullable<int> PointY, Nullable<bool> Stretch, Nullable<bool> Beveled, string CreatedBy, DateTime CreationDate)
		{
			this.ArticlePhotoId = articlePhotoId;
			this.ArticlePhotoGuid = articlePhotoGuid;
            this.Article = article;
			this.SourcePath = sourcePath;
			this.ThumbnailPath = thumbnailPath;
			this.PointX = PointX;
			this.PointY = PointY;
			this.Stretch = Stretch;
			this.Beveled = Beveled;
			this.CreatedBy = CreatedBy;
			this.CreationDate = CreationDate;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            return "ArticlePhotoId = " + ArticlePhotoId.ToString() + ",ArticlePhotoGuid = " + ArticlePhotoGuid.ToString() + ",Article = " + Article.ToString() + ",SourcePath = " + SourcePath + ",ThumbnailPath = " + ThumbnailPath + ",PointX = " + PointX.ToString() + ",PointY = " + PointY.ToString() + ",Stretch = " + Stretch.ToString() + ",Beveled = " + Beveled.ToString() + ",CreatedBy = " + CreatedBy + ",CreationDate = " + CreationDate.ToString();
		}

        /// <summary>
        /// 
        /// </summary>
		public class ArticlePhotoIdComparer : System.Collections.Generic.IComparer<ArticlePhoto>
		{
			public SorterMode SorterMode;
			public ArticlePhotoIdComparer()
			{ }
			public ArticlePhotoIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<ArticlePhoto> Membres
			int System.Collections.Generic.IComparer<ArticlePhoto>.Compare(ArticlePhoto x, ArticlePhoto y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.ArticlePhotoId.CompareTo(x.ArticlePhotoId);
				}
				else
				{
					return x.ArticlePhotoId.CompareTo(y.ArticlePhotoId);
				}
			}
			#endregion
		}
		
        /// <summary>
        /// 
        /// </summary>
        public class CreatedByComparer : System.Collections.Generic.IComparer<ArticlePhoto>
		{
			public SorterMode SorterMode;
			public CreatedByComparer()
			{ }
			public CreatedByComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<ArticlePhoto> Membres
			int System.Collections.Generic.IComparer<ArticlePhoto>.Compare(ArticlePhoto x, ArticlePhoto y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CreatedBy.CompareTo(x.CreatedBy);
				}
				else
				{
					return x.CreatedBy.CompareTo(y.CreatedBy);
				}
			}
			#endregion
		}
		
        /// <summary>
        /// 
        /// </summary>
        public class CreationDateComparer : System.Collections.Generic.IComparer<ArticlePhoto>
		{
			public SorterMode SorterMode;
			public CreationDateComparer()
			{ }
			public CreationDateComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<ArticlePhoto> Membres
			int System.Collections.Generic.IComparer<ArticlePhoto>.Compare(ArticlePhoto x, ArticlePhoto y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CreationDate.CompareTo(x.CreationDate);
				}
				else
				{
					return x.CreationDate.CompareTo(y.CreationDate);
				}
			}
			#endregion
		}
	}
}
