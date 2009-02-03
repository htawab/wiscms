/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

using System.Xml;
using System.IO;
namespace Wis.Toolkit.XML
{
	/// <summary>
	/// XMLSerializer 的摘要说明。
	/// </summary>
	public class XMLSerializer
	{

		/// <summary>
        /// 缺省使用utf-8字符编码序列化指定的对象。
		/// </summary>
		/// <param name="o">将要序列化的 System.Object。</param>
		/// <param name="path">要序列化的 System.Object 存储的绝对路径。</param>
		public static void Serializer(object o, string path)
		{
			Serializer(o, path, System.Text.Encoding.UTF8);
		}


		/// <summary>
		/// 序列化指定的对象。
		/// </summary>
		/// <param name="o">将要序列化的 System.Object。</param>
		/// <param name="path">要序列化的 System.Object 存储的绝对路径。</param>
		/// <param name="encoding">要使用的字符编码。</param>
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
		/// 反序列化为对象。
		/// </summary>
		/// <param name="path">要反序列化的 System.Object 存储的绝对路径。</param>
		/// <returns></returns>
		public static object Deserialize(string typeName, string path)
		{
			return Deserialize(System.Type.GetType(typeName), path);
		}


		/// <summary>
		/// 反序列化为对象。
		/// </summary>
		/// <param name="path">要反序列化的 System.Object 存储的绝对路径。</param>
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