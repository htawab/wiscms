//------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// 模版页面集合，模板名称大小写不区分。
	/// </summary>
	public class PageCollection : CollectionBase
	{
		/// <summary>
		/// 获取或设置在指定的索引处的模版页面。
		/// </summary>
		public Page this[int index]
		{
			get { return ((Page) (List[index])); }
		}


		/// <summary>
		/// 获得指定模版页面名称的模版页面。
		/// </summary>
		public Page this[string requestUrl]
		{
			get 
			{ 
				for (int index = 0; index < List.Count; index++)
				{
					Page templatePage = (Page)(List[index]);
					if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
						return templatePage;
				}

				return null;
			}
			set
			{
				//存在指定名称的模版页面，则修改值
				for (int index = 0; index < List.Count; index++)
				{
					Page templatePage = (Page) (List[index]);
					if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
					{
						return;
					}
				}

				//不存在指定名称的模版页面，则增加配置节点
				List.Add(value);
			}
		}


		/// <summary>
		/// 将模版页面添加到模版页面集合。
		/// </summary>
		/// <param name="value">要添加到模版页面集合的 TemplatePage 对象。</param>
		/// <returns>新模版页面的插入位置。</returns>
		public int Add(Page value)
		{
			//存在指定名称的模版页面，则修改值
			for (int index = 0; index < List.Count; index++)
			{
				Page templatePage = (Page) (List[index]);
				if (templatePage.RequestUrl.ToUpper() == value.RequestUrl.ToUpper())
				{
					return index;
				}
			}

			//不存在指定名称的模版页面，则增加配置节点
			return List.Add(value);
		}


		/// <summary>
		/// 从模版页面集合中移除指定模版页面名称的模版页面。
		/// </summary>
		/// <param name="requestUrl">模版页面名称。</param>
		public void Remove(string requestUrl)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Page templatePage = (Page) (List[index]);
				if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
				{
					List.Remove(templatePage);
					return;
				}
			}	
		}


		/// <summary>
		/// 确定模版页面集合是否包含指定模版页面名称的模版页面。
		/// </summary>
		/// <param name="requestUrl">模版页面名称。</param>
		/// <returns>如果在模版页面集合中找到指定模版页面名称的模版页面，则为 true；否则为 false。</returns>
		public bool Contains(string requestUrl)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Page templatePage = (Page) (List[index]);
				if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
				{
					return true;
				}
			}	
	
			return false;
		}


		/// <summary>
		/// 确定模版页面集合中特定模版页面的索引。
		/// </summary>
		/// <param name="requestUrl">模版页面名称。</param>
		/// <returns>如果在模版页面集合中找到指定模版页面名称的模版页面，则返回模版页面的索引；否则为 -1。</returns>
		public int IndexOf(string requestUrl)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Page templatePage = (Page) (List[index]);
				if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
				{
					return index;
				}
			}	
	
			return -1;
		}
	}
}