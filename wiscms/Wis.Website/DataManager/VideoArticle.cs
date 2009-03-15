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
    /// 视频新闻
    /// </summary>
	[Serializable()]
    public class VideoArticle
	{
		private int _VideoArticleId;
        /// <summary>
        /// 视频编号
        /// </summary>
		public int VideoArticleId
		{
			get { return _VideoArticleId; }
			set { _VideoArticleId = value; }
		}

		private Guid _VideoArticleGuid;
        /// <summary>
        /// 视频编号
        /// </summary>
        public Guid VideoArticleGuid
		{
			get { return _VideoArticleGuid; }
			set { _VideoArticleGuid = value; }
		}


        private Article _Article;
        /// <summary>
        /// 内容信息
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
        /// 视频路径
        /// </summary>
		public string VideoPath
		{
			get { return _VideoPath; }
			set { _VideoPath = value; }
		}


        private string _FlvVideoPath;
        /// <summary>
        /// Flv视频路径
        /// </summary>
        public string FlvVideoPath
        {
            get { return _FlvVideoPath; }
            set { _FlvVideoPath = value; }
        }


        private string _PreviewFramePath;
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PreviewFramePath
        {
            get { return _PreviewFramePath; }
            set { _PreviewFramePath = value; }
        }


        private Nullable<byte> _Star;
        /// <summary>
        /// 星级
        /// </summary>
		public Nullable<byte> Star
		{
            get { return _Star; }
            set { _Star = value; }
		}

        /// <summary>
        /// 
        /// </summary>
		public VideoArticle()
		{ }
#warning VideoArticle重载，加入Article对象

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    //
        //}
#warning ToString() 加入Article对象

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
	}
}
