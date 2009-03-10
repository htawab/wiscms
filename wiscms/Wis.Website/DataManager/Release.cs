//------------------------------------------------------------------------------
// <copyright file="ReleaseRelation.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 
    /// </summary>
	[Serializable()]
	public class Release
	{
        /// <summary>
        /// 
        /// </summary>
        public Release() { }

		private int _ReleaseId;
        /// <summary>
        /// 发布编号
        /// </summary>
		public int ReleaseId
		{
			get { return _ReleaseId; }
			set { _ReleaseId = value; }
		}

		private Guid _ReleaseGuid;
        /// <summary>
        /// 发布编号
        /// </summary>
        public Guid ReleaseGuid
		{
			get { return _ReleaseGuid; }
			set { _ReleaseGuid = value; }
		}

        private string _Title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
		{
            get { return _Title; }
            set { _Title = value; }
		}

        public Template _Template;
        /// <summary>
        /// 模版信息
        /// </summary>
        public Template Template
        {
            get 
            {
                if (_Template == null)
                    _Template = new Template();

                return _Template;
            }
            set { _Template = value; }
        }

		private string _ReleasePath;
        /// <summary>
        /// 发布路径
        /// </summary>
		public string ReleasePath
		{
			get { return _ReleasePath; }
			set { _ReleasePath = value; }
		}

        private DateTime _DateReleased;
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime DateReleased
        {
            get { return _DateReleased; }
            set { _DateReleased = value; }
        }

        private Guid _ParentGuid;
        /// <summary>
        /// 父发布编号
        /// </summary>
        public Guid ParentGuid
        {
            get { return _ParentGuid; }
            set { _ParentGuid = value; }
        }

        private Nullable<int> _PageSize;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public Nullable<int> PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }


        private PagerStyle _PagerStyle;
        /// <summary>
        /// 分页样式
        /// </summary>
        public PagerStyle PagerStyle
        {
            get { return _PagerStyle; }
            set { _PagerStyle = value; }
        }

        /// <summary>
        /// 根据发布编号进行比较。
        /// </summary>
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
	}
}
