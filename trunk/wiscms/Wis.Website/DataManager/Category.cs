//------------------------------------------------------------------------------
// <copyright file="Category.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Website.DataManager
{
    [Serializable()]
    public class Category
    {
        private int _CategoryId;
        /// <summary>
        /// ������
        /// </summary>
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        private Guid _CategoryGuid;
        /// <summary>
        /// ������
        /// </summary>
        public Guid CategoryGuid
        {
            get { return _CategoryGuid; }
            set { _CategoryGuid = value; }
        }

        private byte _ArticleType;
        /// <summary>
        /// ��������
        /// 1 ��ͨ����
        /// 2 ͼƬ����
        /// 3 ��Ƶ����
        /// 4 ���
        /// </summary>
        public byte ArticleType
        {
            get { return _ArticleType; }
            set { _ArticleType = value; }
        }

        private Nullable<int> _ThumbnailWidth;
        /// <summary>
        /// ����ͼƬ��
        /// </summary>
        public Nullable<int> ThumbnailWidth
        {
            get { return _ThumbnailWidth; }
            set { _ThumbnailWidth = value; }
        }

        private Nullable<int> _ThumbnailHeight;
        /// <summary>
        /// ����ͼƬ��
        /// </summary>
        public Nullable<int> ThumbnailHeight
        {
            get { return _ThumbnailHeight; }
            set { _ThumbnailHeight = value; }
        }

        private string _CategoryName;
        /// <summary>
        /// �������ơ�
        /// </summary>
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private Guid _ParentGuid;
        /// <summary>
        /// �������š�
        /// </summary>
        public Guid ParentGuid
        {
            get { return _ParentGuid; }
            set { _ParentGuid = value; }
        }

        private string _ParentCategoryName;
        /// <summary>
        /// ���������ơ�
        /// </summary>
        public string ParentCategoryName
        {
            get { return _ParentCategoryName; }
            set { _ParentCategoryName = value; }
        }

        private int _Rank;
        /// <summary>
        /// ���ȼ�
        /// </summary>
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        public Category()
        { }


        public Category(int categoryId, Guid categoryGuid, string categoryName, Guid parentGuid, string parentCategoryName, int rank, byte articleType, int thumbnailWidth, int thumbnailHeight)
        {
            this.CategoryId = categoryId;
            this.CategoryGuid = categoryGuid;
            this.CategoryName = categoryName;
            this.ParentGuid = parentGuid;
            this.ParentCategoryName = parentCategoryName;
            this.Rank = rank;
            this.ArticleType = articleType;
            this.ThumbnailWidth = thumbnailWidth;
            this.ThumbnailHeight = thumbnailHeight;
        }

        public override string ToString()
        {
            return "CategoryId = " + CategoryId.ToString() + ",CategoryGuid = " + CategoryGuid.ToString() + ",CategoryName = " + CategoryName + ", ParentGuid = " + ParentGuid.ToString() + ", ParentCategoryName = " + this.ParentCategoryName.ToString() + ",Rank = " + Rank.ToString() + ",ArticleType = " + ArticleType.ToString() + ",thumbnailWidth = " + ThumbnailWidth + ",ThumbnailHeight = " + ThumbnailHeight;
        }

        public class CategoryIdComparer : System.Collections.Generic.IComparer<Category>
        {
            public SorterMode SorterMode;
            public CategoryIdComparer()
            { }
            public CategoryIdComparer(SorterMode SorterMode)
            {
                this.SorterMode = SorterMode;
            }
            #region IComparer<Category> Membres
            int System.Collections.Generic.IComparer<Category>.Compare(Category x, Category y)
            {
                if (SorterMode == SorterMode.Ascending)
                {
                    return y.CategoryId.CompareTo(x.CategoryId);
                }
                else
                {
                    return x.CategoryId.CompareTo(y.CategoryId);
                }
            }
            #endregion
        }

        public class CategoryNameComparer : System.Collections.Generic.IComparer<Category>
        {
            public SorterMode SorterMode;
            public CategoryNameComparer()
            { }
            public CategoryNameComparer(SorterMode SorterMode)
            {
                this.SorterMode = SorterMode;
            }
            #region IComparer<Category> Membres
            int System.Collections.Generic.IComparer<Category>.Compare(Category x, Category y)
            {
                if (SorterMode == SorterMode.Ascending)
                {
                    return y.CategoryName.CompareTo(x.CategoryName);
                }
                else
                {
                    return x.CategoryName.CompareTo(y.CategoryName);
                }
            }
            #endregion
        }

        public class RankComparer : System.Collections.Generic.IComparer<Category>
        {
            public SorterMode SorterMode;
            public RankComparer()
            { }
            public RankComparer(SorterMode SorterMode)
            {
                this.SorterMode = SorterMode;
            }
            #region IComparer<Category> Membres
            int System.Collections.Generic.IComparer<Category>.Compare(Category x, Category y)
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
    }
}
