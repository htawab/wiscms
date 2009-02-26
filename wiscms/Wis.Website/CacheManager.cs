using System;
using System.Collections.Generic;
using System.Text;
using Wis.Website.DataManager;

namespace Wis.Website
{
    public class CacheManager
    {
        private const string CacheCategoryDictionary = "Cache:CategoryDictionary";
        /// <summary>
        /// 分类集合，根据分类编号可以获取分类的实体类。
        /// </summary>
        public static SortedList<Guid, Category> CategoryDictionary
        {
            get
            {
                if (System.Web.HttpContext.Current.Cache[CacheCategoryDictionary] == null)
                {
                    //SortedList<Guid, Category> categorys = new SortedList<Guid, Category>();
                    CategoryManager categoryManager = new CategoryManager();
                    System.Web.HttpContext.Current.Cache[CacheCategoryDictionary] = categoryManager.GetCategoryDictionaries(); 
                }
                
                return System.Web.HttpContext.Current.Cache[CacheCategoryDictionary] as SortedList<Guid, Category>;
            }
        }
    }
}