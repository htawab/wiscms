//------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// ģ��ҳ�漯�ϣ�ģ�����ƴ�Сд�����֡�
	/// </summary>
	public class PageCollection : CollectionBase
	{
		/// <summary>
		/// ��ȡ��������ָ������������ģ��ҳ�档
		/// </summary>
		public Page this[int index]
		{
			get { return ((Page) (List[index])); }
		}


		/// <summary>
		/// ���ָ��ģ��ҳ�����Ƶ�ģ��ҳ�档
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
				//����ָ�����Ƶ�ģ��ҳ�棬���޸�ֵ
				for (int index = 0; index < List.Count; index++)
				{
					Page templatePage = (Page) (List[index]);
					if (templatePage.RequestUrl.ToUpper() == requestUrl.ToUpper())
					{
						return;
					}
				}

				//������ָ�����Ƶ�ģ��ҳ�棬���������ýڵ�
				List.Add(value);
			}
		}


		/// <summary>
		/// ��ģ��ҳ����ӵ�ģ��ҳ�漯�ϡ�
		/// </summary>
		/// <param name="value">Ҫ��ӵ�ģ��ҳ�漯�ϵ� TemplatePage ����</param>
		/// <returns>��ģ��ҳ��Ĳ���λ�á�</returns>
		public int Add(Page value)
		{
			//����ָ�����Ƶ�ģ��ҳ�棬���޸�ֵ
			for (int index = 0; index < List.Count; index++)
			{
				Page templatePage = (Page) (List[index]);
				if (templatePage.RequestUrl.ToUpper() == value.RequestUrl.ToUpper())
				{
					return index;
				}
			}

			//������ָ�����Ƶ�ģ��ҳ�棬���������ýڵ�
			return List.Add(value);
		}


		/// <summary>
		/// ��ģ��ҳ�漯�����Ƴ�ָ��ģ��ҳ�����Ƶ�ģ��ҳ�档
		/// </summary>
		/// <param name="requestUrl">ģ��ҳ�����ơ�</param>
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
		/// ȷ��ģ��ҳ�漯���Ƿ����ָ��ģ��ҳ�����Ƶ�ģ��ҳ�档
		/// </summary>
		/// <param name="requestUrl">ģ��ҳ�����ơ�</param>
		/// <returns>�����ģ��ҳ�漯�����ҵ�ָ��ģ��ҳ�����Ƶ�ģ��ҳ�棬��Ϊ true������Ϊ false��</returns>
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
		/// ȷ��ģ��ҳ�漯�����ض�ģ��ҳ���������
		/// </summary>
		/// <param name="requestUrl">ģ��ҳ�����ơ�</param>
		/// <returns>�����ģ��ҳ�漯�����ҵ�ָ��ģ��ҳ�����Ƶ�ģ��ҳ�棬�򷵻�ģ��ҳ�������������Ϊ -1��</returns>
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