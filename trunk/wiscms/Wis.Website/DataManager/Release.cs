//------------------------------------------------------------------------------
// <copyright file="Release.cs" company="Everwis">
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="releaseId"></param>
        /// <param name="releaseGuid"></param>
        /// <param name="title"></param>
        /// <param name="templatePath"></param>
        /// <param name="releasePath"></param>
        /// <param name="dateReleased"></param>
        /// <param name="parentGuid"></param>
        public Release(int releaseId, Guid releaseGuid, string title, string templatePath, string releasePath, DateTime dateReleased, Guid parentGuid)
        {
            this.ReleaseId = releaseId;
            this.ReleaseGuid = releaseGuid;
            this.Title = title;
            this.TemplatePath = templatePath;
            this.ReleasePath = releasePath;
            this.DateReleased = dateReleased;
            this.ParentGuid = parentGuid;
        }

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

		private string _TemplatePath;
        /// <summary>
        /// 模版路径
        /// </summary>
		public string TemplatePath
		{
			get { return _TemplatePath; }
			set { _TemplatePath = value; }
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public override string ToString()
		{
            return string.Format("ReleaseId={0}, ReleaseGuid={1}, Title={2}, TemplatePath={3}, ReleasePath={4}, DateReleased={5}, ParentGuid={6}",
                this.ReleaseId, this.ReleaseGuid, this.Title, this.TemplatePath, this.ReleasePath, this.DateReleased, this.ParentGuid);
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
