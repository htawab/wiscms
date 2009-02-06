//------------------------------------------------------------------------------
// <copyright file="ModuleCollection.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// ģ��ģ�鼯�ϡ�
	/// </summary>
	public class ModuleCollection : CollectionBase
	{
		/// <summary>
		/// ��ȡ��������ָ������������ģ��ģ�顣
		/// </summary>
		public Module this[int index]
		{
			get { return ((Module) (List[index])); }
		}


		/// <summary>
		/// ���ָ��ģ��ģ�����Ƶ�ģ��ģ�顣
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
				//����ָ�����Ƶ�ģ��ģ�飬���޸�ֵ
				for (int index = 0; index < List.Count; index++)
				{
					Module templateModule = (Module) (List[index]);
					if (templateModule.Name == name)
					{
						return;
					}
				}

				//������ָ�����Ƶ�ģ��ģ�飬���������ýڵ�
				List.Add(value);
			}
		}


		/// <summary>
		/// ��ģ��ģ����ӵ�ģ��ģ�鼯�ϡ�
		/// </summary>
		/// <param name="value">Ҫ��ӵ�ģ��ģ�鼯�ϵ� TemplateModule ����</param>
		/// <returns>��ģ��ģ��Ĳ���λ�á�</returns>
		public int Add(Module value)
		{
			//����ָ�����Ƶ�ģ��ģ�飬���޸�ֵ
			for (int index = 0; index < List.Count; index++)
			{
				Module templateModule = (Module) (List[index]);
				if (templateModule.Name == value.Name)
				{
					return index;
				}
			}

			//������ָ�����Ƶ�ģ��ģ�飬���������ýڵ�
			return List.Add(value);
		}


		/// <summary>
		/// ��ģ��ģ�鼯�����Ƴ�ָ��ģ��ģ�����Ƶ�ģ��ģ�顣
		/// </summary>
		/// <param name="name">ģ��ģ�����ơ�</param>
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
		/// ȷ��ģ��ģ�鼯���Ƿ����ָ��ģ��ģ�����Ƶ�ģ��ģ�顣
		/// </summary>
		/// <param name="name">ģ��ģ�����ơ�</param>
		/// <returns>�����ģ��ģ�鼯�����ҵ�ָ��ģ��ģ�����Ƶ�ģ��ģ�飬��Ϊ true������Ϊ false��</returns>
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
		/// ȷ��ģ��ģ�鼯�����ض�ģ��ģ���������
		/// </summary>
		/// <param name="name">ģ��ģ�����ơ�</param>
		/// <returns>�����ģ��ģ�鼯�����ҵ�ָ��ģ��ģ�����Ƶ�ģ��ģ�飬�򷵻�ģ��ģ�������������Ϊ -1��</returns>
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