//------------------------------------------------------------------------------
// <copyright file="VideoArticle.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// ��Ƶ����
    /// </summary>
	[Serializable()]
    public class VideoArticle
	{
		private int _VideoArticleId;
        /// <summary>
        /// ��Ƶ���
        /// </summary>
		public int VideoArticleId
		{
			get { return _VideoArticleId; }
			set { _VideoArticleId = value; }
		}

		private Guid _VideoArticleGuid;
        /// <summary>
        /// ��Ƶ���
        /// </summary>
        public Guid VideoArticleGuid
		{
			get { return _VideoArticleGuid; }
			set { _VideoArticleGuid = value; }
		}


        private Article _Article;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Article Article
        {
            get 
            {
                if (_Article == null)
                    _Article = new Article();

                return _Article;
            }
            set { _Article = value; }
        }


		private string _VideoPath;
        /// <summary>
        /// ��Ƶ·��
        /// </summary>
		public string VideoPath
		{
			get { return _VideoPath; }
			set { _VideoPath = value; }
		}

        private Int64 _Size;
        /// <summary>
        /// �ļ��ߴ�
        /// </summary>
		public Int64 Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

        private Nullable<byte> _Rank;
        /// <summary>
        /// �Ǽ�
        /// </summary>
		public Nullable<byte> Rank
		{
			get { return _Rank; }
			set { _Rank = value; }
		}

		private string _SourceImagePath;
        /// <summary>
        /// Դͼ·��
        /// </summary>
		public string SourceImagePath
		{
			get { return _SourceImagePath; }
			set { _SourceImagePath = value; }
		}

		private string _ThumbnailPath;
        /// <summary>
        /// ����ͼ·��
        /// </summary>
		public string ThumbnailPath
		{
			get { return _ThumbnailPath; }
			set { _ThumbnailPath = value; }
		}

		private Nullable<int> _PointX;
        /// <summary>
        /// X����
        /// </summary>
		public Nullable<int> PointX
		{
			get { return _PointX; }
			set { _PointX = value; }
		}

		private Nullable<int> _PointY;
        /// <summary>
        /// Y����
        /// </summary>
		public Nullable<int> PointY
		{
			get { return _PointY; }
			set { _PointY = value; }
		}

		private Nullable<bool> _Stretch;
        /// <summary>
        /// ����
        /// </summary>
		public Nullable<bool> Stretch
		{
			get { return _Stretch; }
			set { _Stretch = value; }
		}

		private Nullable<bool> _Beveled;
        /// <summary>
        /// б��
        /// </summary>
		public Nullable<bool> Beveled
		{
			get { return _Beveled; }
			set { _Beveled = value; }
		}

		private string _CreatedBy;
        /// <summary>
        /// ������
        /// </summary>
		public string CreatedBy
		{
			get { return _CreatedBy; }
			set { _CreatedBy = value; }
		}

		private DateTime _CreationDate;
        /// <summary>
        /// ����ʱ��
        /// </summary>
		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}

        /// <summary>
        /// 
        /// </summary>
		public VideoArticle()
		{ }
#warning VideoArticle���أ�����Article����

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    //
        //}
#warning ToString() ����Article����

        /// <summary>
        /// 
        /// </summary>
		public class VideoArticleIdComparer : System.Collections.Generic.IComparer<VideoArticle>
		{
			public SorterMode SorterMode;
			public VideoArticleIdComparer()
			{ }
            public VideoArticleIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
            }
            #region IComparer<VideoArticle> Membres
            int System.Collections.Generic.IComparer<VideoArticle>.Compare(VideoArticle x, VideoArticle y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
                    return y.VideoArticleId.CompareTo(x.VideoArticleId);
				}
				else
				{
                    return x.VideoArticleId.CompareTo(y.VideoArticleId);
				}
			}
			#endregion
		}
		
        /// <summary>
        /// 
        /// </summary>
        public class CreatedByComparer : System.Collections.Generic.IComparer<VideoArticle>
		{
			public SorterMode SorterMode;
			public CreatedByComparer()
			{ }
			public CreatedByComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
            }
            #region IComparer<VideoArticle> Membres
            int System.Collections.Generic.IComparer<VideoArticle>.Compare(VideoArticle x, VideoArticle y)
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
        public class CreationDateComparer : System.Collections.Generic.IComparer<VideoArticle>
		{
			public SorterMode SorterMode;
			public CreationDateComparer()
			{ }
			public CreationDateComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
            }
            #region IComparer<VideoArticle> Membres
            int System.Collections.Generic.IComparer<VideoArticle>.Compare(VideoArticle x, VideoArticle y)
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
