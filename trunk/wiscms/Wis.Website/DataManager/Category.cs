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
        /// 分类编号
        /// </summary>
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        private Guid _CategoryGuid;
        /// <summary>
        /// 分类编号
        /// </summary>
        public Guid CategoryGuid
        {
            get { return _CategoryGuid; }
            set { _CategoryGuid = value; }
        }
        
        private string _CategoryName;
        /// <summary>
        /// 分类名称。
        /// </summary>
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        private Guid _ParentGuid;
        /// <summary>
        /// 父分类编号。
        /// </summary>
        public Guid ParentGuid
        {
            get { return _ParentGuid; }
            set { _ParentGuid = value; }
        }

        private string _ParentCategoryName;
        /// <summary>
        /// 父分类名称。
        /// </summary>
        public string ParentCategoryName
        {
            get { return _ParentCategoryName; }
            set { _ParentCategoryName = value; }
        }

        private int _Rank;
        /// <summary>
        /// 优先级
        /// </summary>
        public int Rank
        {
            get { return _Rank; }
            set { _Rank = value; }
        }

        private string _TemplatePath;
        /// <summary>
        /// 
        /// </summary>
        public string TemplatePath
        {
            get { return _TemplatePath; }
            set { _TemplatePath = value; }
        }

        private string _ReleasePath;
        /// <summary>
        /// 
        /// </summary>
        public string ReleasePath
        {
            get { return _ReleasePath; }
            set { _ReleasePath = value; }
        }

        public Category()
        { }


        public Category(int categoryId, Guid categoryGuid, string categoryName, Guid parentGuid, string parentCategoryName, int rank, string TemplatePath, string ReleasePath)
        {
            this.CategoryId = categoryId;
            this.CategoryGuid = categoryGuid;
            this.CategoryName = categoryName;
            this.ParentGuid = parentGuid;
            this.ParentCategoryName = parentCategoryName;
            this.Rank = rank;
            this.TemplatePath = TemplatePath;
            this.ReleasePath = ReleasePath;
        }

        public override string ToString()
        {
            return "CategoryId = " + CategoryId.ToString() + ",CategoryGuid = " + CategoryGuid.ToString() + ",CategoryName = " + CategoryName + ", ParentGuid = " + ParentGuid.ToString() + ", ParentCategoryName = " + this.ParentCategoryName.ToString() + ",Rank = " + Rank.ToString() + ",TemplatePath = " + TemplatePath + ",ReleasePath = " + ReleasePath;
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
