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


        private string _FlvVideoPath;
        /// <summary>
        /// Flv��Ƶ·��
        /// </summary>
        public string FlvVideoPath
        {
            get { return _FlvVideoPath; }
            set { _FlvVideoPath = value; }
        }


        private string _PreviewFramePath;
        /// <summary>
        /// ����ͼ·��
        /// </summary>
        public string PreviewFramePath
        {
            get { return _PreviewFramePath; }
            set { _PreviewFramePath = value; }
        }


        private Nullable<byte> _Star;
        /// <summary>
        /// �Ǽ�
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
	}
}
