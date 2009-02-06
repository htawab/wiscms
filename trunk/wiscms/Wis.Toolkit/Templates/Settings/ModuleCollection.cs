//------------------------------------------------------------------------------
// <copyright file="ModuleCollection.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// 模版模块集合。
	/// </summary>
	public class ModuleCollection : CollectionBase
	{
		/// <summary>
		/// 获取或设置在指定的索引处的模版模块。
		/// </summary>
		public Module this[int index]
		{
			get { return ((Module) (List[index])); }
		}


		/// <summary>
		/// 获得指定模版模块名称的模版模块。
		/// </summary>
		public Module this[string name]
		{
			get 
			{ 
				for (int index = 0; index < List.Count; index++)
				{
					Module templateModule = (Module)(List[index]);
					if (templateModule.Name == name)
						return templateModule;
				}

				return null;
			}
			set
			{
				//存在指定名称的模版模块，则修改值
				for (int index = 0; index < List.Count; index++)
				{
					Module templateModule = (Module) (List[index]);
					if (templateModule.Name == name)
					{
						return;
					}
				}

				//不存在指定名称的模版模块，则增加配置节点
				List.Add(value);
			}
		}


		/// <summary>
		/// 将模版模块添加到模版模块集合。
		/// </summary>
		/// <param name="value">要添加到模版模块集合的 TemplateModule 对象。</param>
		/// <returns>新模版模块的插入位置。</returns>
		public int Add(Module value)
		{
			//存在指定名称的模版模块，则修改值
			for (int index = 0; index < List.Count; index++)
			{
				Module templateModule = (Module) (List[index]);
				if (templateModule.Name == value.Name)
				{
					return index;
				}
			}

			//不存在指定名称的模版模块，则增加配置节点
			return List.Add(value);
		}


		/// <summary>
		/// 从模版模块集合中移除指定模版模块名称的模版模块。
		/// </summary>
		/// <param name="name">模版模块名称。</param>
		public void Remove(string name)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Module templateModule = (Module) (List[index]);
				if (templateModule.Name == name)
				{
					List.Remove(templateModule);
					return;
				}
			}	
		}


		/// <summary>
		/// 确定模版模块集合是否包含指定模版模块名称的模版模块。
		/// </summary>
		/// <param name="name">模版模块名称。</param>
		/// <returns>如果在模版模块集合中找到指定模版模块名称的模版模块，则为 true；否则为 false。</returns>
		public bool Contains(string name)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Module templateModule = (Module) (List[index]);
				if (templateModule.Name == name)
				{
					return true;
				}
			}	
	
			return false;
		}


		/// <summary>
		/// 确定模版模块集合中特定模版模块的索引。
		/// </summary>
		/// <param name="name">模版模块名称。</param>
		/// <returns>如果在模版模块集合中找到指定模版模块名称的模版模块，则返回模版模块的索引；否则为 -1。</returns>
		public int IndexOf(string name)
		{
			for (int index = 0; index < List.Count; index++)
			{
				Module templateModule = (Module) (List[index]);
				if (templateModule.Name == name)
				{
					return index;
				}
			}	
	
			return -1;
		}
	}
}