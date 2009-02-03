/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

using System.Xml;
using System.IO;
namespace Wis.Toolkit.XML
{
	/// <summary>
	/// XMLSerializer ��ժҪ˵����
	/// </summary>
	public class XMLSerializer
	{

		/// <summary>
        /// ȱʡʹ��utf-8�ַ��������л�ָ���Ķ���
		/// </summary>
		/// <param name="o">��Ҫ���л��� System.Object��</param>
		/// <param name="path">Ҫ���л��� System.Object �洢�ľ���·����</param>
		public static void Serializer(object o, string path)
		{
			Serializer(o, path, System.Text.Encoding.UTF8);
		}


		/// <summary>
		/// ���л�ָ���Ķ���
		/// </summary>
		/// <param name="o">��Ҫ���л��� System.Object��</param>
		/// <param name="path">Ҫ���л��� System.Object �洢�ľ���·����</param>
		/// <param name="encoding">Ҫʹ�õ��ַ����롣</param>
		public static void Serializer(object o, string path, System.Text.Encoding encoding)
		{
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(o.GetType());
            //XmlDocument xmldocument = new XmlDocument();
            //xmldocument
            //DocumentNavigator. 
            System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
			System.IO.StreamWriter writer = new System.IO.StreamWriter(fileStream, encoding);
			serializer.Serialize(writer, o);
			writer.Close();
		}


		/// <summary>
		/// �����л�Ϊ����
		/// </summary>
		/// <param name="path">Ҫ�����л��� System.Object �洢�ľ���·����</param>
		/// <returns></returns>
		public static object Deserialize(string typeName, string path)
		{
			return Deserialize(System.Type.GetType(typeName), path);
		}


		/// <summary>
		/// �����л�Ϊ����
		/// </summary>
		/// <param name="path">Ҫ�����л��� System.Object �洢�ľ���·����</param>
		/// <returns></returns>
		public static object Deserialize(System.Type type, string path)
		{
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
			System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			object obj = serializer.Deserialize(fileStream);
			fileStream.Close();
			return obj;
		}
	}
}