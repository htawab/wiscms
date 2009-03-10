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
        /// �������
        /// </summary>
		public int ReleaseId
		{
			get { return _ReleaseId; }
			set { _ReleaseId = value; }
		}

		private Guid _ReleaseGuid;
        /// <summary>
        /// �������
        /// </summary>
        public Guid ReleaseGuid
		{
			get { return _ReleaseGuid; }
			set { _ReleaseGuid = value; }
		}

        private string _Title;
        /// <summary>
        /// ����
        /// </summary>
        public string Title
		{
            get { return _Title; }
            set { _Title = value; }
		}

        public Template _Template;
        /// <summary>
        /// ģ����Ϣ
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
        /// ����·��
        /// </summary>
		public string ReleasePath
		{
			get { return _ReleasePath; }
			set { _ReleasePath = value; }
		}

        private DateTime _DateReleased;
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime DateReleased
        {
            get { return _DateReleased; }
            set { _DateReleased = value; }
        }

        private Guid _ParentGuid;
        /// <summary>
        /// ���������
        /// </summary>
        public Guid ParentGuid
        {
            get { return _ParentGuid; }
            set { _ParentGuid = value; }
        }

        private Nullable<int> _PageSize;
        /// <summary>
        /// ÿҳ��¼��
        /// </summary>
        public Nullable<int> PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }


        private PagerStyle _PagerStyle;
        /// <summary>
        /// ��ҳ��ʽ
        /// </summary>
        public PagerStyle PagerStyle
        {
            get { return _PagerStyle; }
            set { _PagerStyle = value; }
        }

        /// <summary>
        /// ���ݷ�����Ž��бȽϡ�
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
