//------------------------------------------------------------------------------
// <copyright file="CategoryManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
    public class CategoryManager
    {
        public CategoryManager()
        {
            DbProviderHelper.GetConnection();
        }

        /// <summary>
        /// 获取分类的字典集合。
        /// </summary>
        /// <returns></returns>
        public SortedList<Guid, Category> GetCategoryDictionaries()
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectCategorys", CommandType.StoredProcedure);
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            SortedList<Guid, Category> categorys = new SortedList<Guid, Category>();
            while (dataReader.Read())
            {
                Category category = new Category();
                category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                category.ParentCategoryName = dataReader[CategoryField.ParentCategoryName].ToString(); // 父分类名称
                category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                category.ArticleType = (byte)dataReader[CategoryField.ArticleType];
                if (dataReader[CategoryField.ThumbnailWidth] != DBNull.Value)
                    category.ThumbnailWidth = (int)dataReader[CategoryField.ThumbnailWidth];
                if (dataReader[CategoryField.ThumbnailHeight] != DBNull.Value)
                    category.ThumbnailHeight = (int)dataReader[CategoryField.ThumbnailHeight];
                category.RecordCount = (int)dataReader[CategoryField.RecordCount];

                categorys.Add(category.CategoryGuid, category);
            }
            dataReader.Close();
            return categorys;
        }


        public static string GetSiteMapPath(string releaseDirectory, Guid categoryGuid, string pathSeparator)
        {
            // $ReleaseDirectory$/$CategoryManager.GetCategoryIdByCategoryName("党群", "教育动态")$/1.htm
            List<Category> categorys = new List<Category>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectSiteMapPath", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseDirectory", DbType.String, releaseDirectory));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PathSeparator", DbType.String, pathSeparator));
            object o = DbProviderHelper.ExecuteScalar(command);
            if (o == null)
                return string.Empty;
            else
                return o.ToString();
        }


        public static string GetPageTitle(Guid categoryGuid, string separator)
        {
            // 党群 - 教育动态
            List<Category> categorys = new List<Category>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectPageTitle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Separator", DbType.String, separator));
            object o = DbProviderHelper.ExecuteScalar(command);
            if (o == null)
                return string.Empty;
            else
                return o.ToString();
        }


        /// <summary>
        /// 获取所有的
        /// </summary>
        /// <returns></returns>
        public List<Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem> GetCategoryMenuItems()
        {
            DbCommand command = DbProviderHelper.CreateCommand("SELECTCategorys", CommandType.StoredProcedure);
            DbDataAdapter adapter = DbProviderHelper.CreateDataAdapter(command);
            DataSet ds = DbProviderHelper.FillDataSet(adapter);

            return GetMenuItems(ds, "00000000-0000-0000-0000-000000000000");
        }

        private List<Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem> GetMenuItems(DataSet ds, string parentGuid)
        {
            List<Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem> menuItems = new List<Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem>();

            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = string.Format("ParentGuid='{0}'", parentGuid);
            foreach (DataRowView dataRowView in dv)
            {
                Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem menuItem = new Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem();
                menuItem.Text = dataRowView[CategoryField.CategoryName].ToString();
                menuItem.Value = dataRowView[CategoryField.CategoryGuid].ToString();

                dv = ds.Tables[0].DefaultView;
                dv.RowFilter = string.Format("ParentGuid='{0}'", menuItem.Value);
                if (dv.Count > 0)
                {
                    menuItem.SubMenuItems = GetMenuItems(ds, menuItem.Value);
                }

                menuItems.Add(menuItem);
            }

            return menuItems;
        }


        public List<Category> GetCategorys()
        {
            DbCommand command = DbProviderHelper.CreateCommand("SELECTCategorys", CommandType.StoredProcedure);
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetCategorys(dataReader);
        }


        public static List<Category> GetSubCategorysByParentGuid(Guid parentGuid)
        {
            DbProviderHelper.GetConnection();
            DbCommand command = DbProviderHelper.CreateCommand("SelectSubCategorysByParentGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ParentGuid", DbType.Guid, parentGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetCategorys(dataReader);
        }


        public static List<Category> GetCategorys(DbDataReader dataReader)
        {
            List<Category> categorys = new List<Category>();
            while (dataReader.Read())
            {
                Category category = new Category();
                category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                category.ParentCategoryName = dataReader[CategoryField.ParentCategoryName].ToString();
                category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                category.ArticleType = (byte)dataReader[CategoryField.ArticleType];
                if (dataReader[CategoryField.ThumbnailWidth] != DBNull.Value)
                    category.ThumbnailWidth = (int)dataReader[CategoryField.ThumbnailWidth];
                if (dataReader[CategoryField.ThumbnailHeight] != DBNull.Value)
                    category.ThumbnailHeight = (int)dataReader[CategoryField.ThumbnailHeight];
                category.RecordCount = (int)dataReader[CategoryField.RecordCount];

                categorys.Add(category);
            }
            dataReader.Close();
            return categorys;
        }

        public Category GetCategory(int CategoryId)
        {
            Category category = new Category();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTCategory", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryId", DbType.Int32, CategoryId));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                category.ParentCategoryName = dataReader[CategoryField.ParentCategoryName].ToString();
                category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                category.ArticleType = (byte)dataReader[CategoryField.ArticleType];
                if (dataReader[CategoryField.ThumbnailWidth] != DBNull.Value)
                    category.ThumbnailWidth = (int)dataReader[CategoryField.ThumbnailWidth];
                if (dataReader[CategoryField.ThumbnailHeight] != DBNull.Value)
                    category.ThumbnailHeight = (int)dataReader[CategoryField.ThumbnailHeight];
                category.RecordCount = (int)dataReader[CategoryField.RecordCount];
            }
            dataReader.Close();
            return category;
        }


        /// <summary>
        /// 根据当前目录名称和父目录名称获取当前目录的编号
        /// </summary>
        /// <param name="categoryName">当前目录名称</param>
        /// <param name="parentCategoryName">父目录名称</param>
        /// <returns>返回目录编号</returns>
        public static int GetCategoryIdByCategoryName(string categoryName, string parentCategoryName)
        {
            DbProviderHelper.GetConnection();
            using (DbCommand command = DbProviderHelper.CreateCommand("SelectCategoryIdByCategoryName", CommandType.StoredProcedure))
            {
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryName", DbType.String, categoryName));
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ParentCategoryName", DbType.String, parentCategoryName));
                Object o = DbProviderHelper.ExecuteScalar(command);
                if (o != null && !o.Equals(DBNull.Value))
                {
                    return (int)o;
                }
            }
            return -1;
        }

        public List<Category> GetCategorysByReleaseGuid(Guid releaseGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectCategorysByReleaseGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetCategorys(dataReader);
        }

        /// <summary>
        /// 根据分类编号获取分类实体对象。
        /// </summary>
        /// <param name="categoryGuid">分类编号。</param>
        /// <returns>返回分类实体对象。</returns>
        public Category GetCategoryByCategoryGuid(Guid categoryGuid)
        {
            Category category = new Category();
            DbCommand command = DbProviderHelper.CreateCommand("SelectCategoryByCategoryGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                category.ParentCategoryName = dataReader[CategoryField.ParentCategoryName].ToString();
                category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                category.ArticleType = (byte)dataReader[CategoryField.ArticleType];
                if (dataReader[CategoryField.ThumbnailWidth] != DBNull.Value)
                    category.ThumbnailWidth = (int)dataReader[CategoryField.ThumbnailWidth];
                if (dataReader[CategoryField.ThumbnailHeight] != DBNull.Value)
                    category.ThumbnailHeight = (int)dataReader[CategoryField.ThumbnailHeight];
                category.RecordCount = (int)dataReader[CategoryField.RecordCount];
            }
            dataReader.Close();
            return category;
        }


        public int AddNew(Category category)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTCategory", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, category.CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryName", DbType.String, category.CategoryName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ParentGuid", DbType.Guid, category.ParentGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, category.Rank));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.String, category.ArticleType));
            if (category.ThumbnailWidth != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailWidth", DbType.String, category.ThumbnailWidth));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailWidth", DbType.String, DBNull.Value));
            if (category.ThumbnailHeight != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailHeight", DbType.String, category.ThumbnailHeight));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailHeight", DbType.String, DBNull.Value));

            return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
        }

        public int Update(Category category)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UPDATECategory", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryId", DbType.Int32, category.CategoryId));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, category.CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryName", DbType.String, category.CategoryName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ParentGuid", DbType.Guid, category.ParentGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, category.Rank));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.String, category.ArticleType));
            if (category.ThumbnailWidth != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailWidth", DbType.String, category.ThumbnailWidth));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailWidth", DbType.String, DBNull.Value));
            if (category.ThumbnailHeight != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailHeight", DbType.String, category.ThumbnailHeight));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailHeight", DbType.String, DBNull.Value));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Remove(int CategoryId)
        {
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETECategory", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryId", DbType.Int32, CategoryId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }
    }
}
