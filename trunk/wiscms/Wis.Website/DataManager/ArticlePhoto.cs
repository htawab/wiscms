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
    /// 图片新闻信息
    /// </summary>
	[Serializable()]
    public class ArticlePhoto : Article
	{
		private int _ArticlePhotoId;
        /// <summary>
        /// 图片编号
        /// </summary>
		public int ArticlePhotoId
		{
			get { return _ArticlePhotoId; }
			set { _ArticlePhotoId = value; }
		}

		private Guid _ArticlePhotoGuid;
        /// <summary>
        /// 图片编号
        /// </summary>
        public Guid ArticlePhotoGuid
		{
			get { return _ArticlePhotoGuid; }
			set { _ArticlePhotoGuid = value; }
		}


		private string _SourcePath;
        /// <summary>
        /// 源图路径
        /// </summary>
		public string SourcePath
		{
			get { return _SourcePath; }
			set { _SourcePath = value; }
		}

		private string _ThumbnailPath;
        /// <summary>
        /// 缩略图路径
        /// </summary>
		public string ThumbnailPath
		{
			get { return _ThumbnailPath; }
			set { _ThumbnailPath = value; }
		}

		private Nullable<int> _PointX;
        /// <summary>
        /// X坐标
        /// </summary>
		public Nullable<int> PointX
		{
			get { return _PointX; }
			set { _PointX = value; }
		}

		private Nullable<int> _PointY;
        /// <summary>
        /// Y坐标
        /// </summary>
		public Nullable<int> PointY
		{
			get { return _PointY; }
			set { _PointY = value; }
		}

		private Nullable<bool> _Stretch;
        /// <summary>
        /// 拉伸
        /// </summary>
		public Nullable<bool> Stretch
		{
			get { return _Stretch; }
			set { _Stretch = value; }
		}

		private Nullable<bool> _Beveled;
        /// <summary>
        /// 斜角
        /// </summary>
		public Nullable<bool> Beveled
		{
			get { return _Beveled; }
			set { _Beveled = value; }
		}

		private string _CreatedBy;
        /// <summary>
        /// 创建人
        /// </summary>
		public string CreatedBy
		{
			get { return _CreatedBy; }
			set { _CreatedBy = value; }
		}

		private DateTime _CreationDate;
        /// <summary>
        /// 创建时间
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
        /// <param name="articlePhoto"></param>
        /// <param name="sourcePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="stretch"></param>
        /// <param name="beveled"></param>
        /// <param name="createdBy"></param>
        /// <param name="creationDate"></param>
        public ArticlePhoto(int articlePhotoId, Guid articlePhotoGuid, Guid articleGuid, string sourcePath, string thumbnailPath, Nullable<int> PointX, Nullable<int> PointY, Nullable<bool> Stretch, Nullable<bool> Beveled, string CreatedBy, DateTime CreationDate)
		{
			this.ArticlePhotoId = articlePhotoId;
			this.ArticlePhotoGuid = articlePhotoGuid;
            this.ArticleGuid = articleGuid;
			this.SourcePath = sourcePath;
			this.ThumbnailPath = thumbnailPath;
			this.PointX = PointX;
			this.PointY = PointY;
			this.Stretch = Stretch;
			this.Beveled = Beveled;
			this.CreatedBy = CreatedBy;
			this.CreationDate = CreationDate;
        }

#warning ArticlePhoto重载，加入Article对象
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            return "ArticlePhotoId = " + ArticlePhotoId.ToString() + ",ArticlePhotoGuid = " + ArticlePhotoGuid.ToString() + ", ArticleGuid=" + ArticleGuid.ToString() + ", SourcePath=" + SourcePath + ", ThumbnailPath=" + ThumbnailPath + ",PointX = " + PointX.ToString() + ",PointY = " + PointY.ToString() + ",Stretch = " + Stretch.ToString() + ",Beveled = " + Beveled.ToString() + ",CreatedBy = " + CreatedBy + ",CreationDate = " + CreationDate.ToString();
        }
#warning ToString() 加入Article对象

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
