/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

namespace Wis.Toolkit.ClientScript
{
	/// <summary>
	/// 消息窗口，跟common.js配合使用。
	/// </summary>
	public class MessageBox
	{

		System.Text.StringBuilder sbMessage = new System.Text.StringBuilder();
		
		/// <summary>
		/// 指定消息。
		/// </summary>
		/// <param name="type">消息类型</param>
		/// <param name="message">消息内容</param>
		public void Message(MessageBoxType type, string message)
		{
			string msgType;
			switch(type)
			{
				case MessageBoxType.Advert:
					msgType = "Advert";
					break;
				case MessageBoxType.Error:
					msgType = "Error";
					break;
				case MessageBoxType.Mail:
					msgType = "Mail";
					break;
				case MessageBoxType.Save:
					msgType = "Save";
					break;
				case MessageBoxType.Wait:
					msgType = "Wait";
					break;
				default:
					msgType = "Info";
					break;
			}
			
			//清除 回车符；将双引号修改为 \"
			if(message == null)message = "";
			message = message.Replace("\r","").Replace("\n","").Replace("\"", "\\\"");

			sbMessage.Append(string.Format("var mb{2} = MessageBox('{0}','{1}');\n", msgType, message, System.DateTime.Now.Ticks.ToString()));
		}
		
		
		/// <summary>
		/// 弹出消息。
		/// </summary>
		public void Call()
		{
			if( sbMessage.Length != 0)
			{
				System.Web.HttpContext.Current.Response.Write("<script language=\"javascript\">\n"); 
				System.Web.HttpContext.Current.Response.Write(sbMessage.ToString());
				System.Web.HttpContext.Current.Response.Write("</script>\n"); 
			}
		}
	}
	
	/// <summary>
	/// 消息类型，包括Mail、Info、Error、Save、Advert和Wait。
	/// </summary>
	public enum MessageBoxType
	{
		/// <summary>
		/// 邮件。
		/// </summary>
		Mail,
		/// <summary>
		/// 信息。
		/// </summary>
		Info,
		/// <summary>
		/// 错误。
		/// </summary>
		Error,
		/// <summary>
		/// 保存。
		/// </summary>
		Save,
		/// <summary>
		/// 提示信息。
		/// </summary>
		Advert,
		/// <summary>
		/// 等待。
		/// </summary>
		Wait
	}
}