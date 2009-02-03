/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

namespace Wis.Toolkit.ClientScript
{
	/// <summary>
	/// ��Ϣ���ڣ���common.js���ʹ�á�
	/// </summary>
	public class MessageBox
	{

		System.Text.StringBuilder sbMessage = new System.Text.StringBuilder();
		
		/// <summary>
		/// ָ����Ϣ��
		/// </summary>
		/// <param name="type">��Ϣ����</param>
		/// <param name="message">��Ϣ����</param>
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
			
			//��� �س�������˫�����޸�Ϊ \"
			if(message == null)message = "";
			message = message.Replace("\r","").Replace("\n","").Replace("\"", "\\\"");

			sbMessage.Append(string.Format("var mb{2} = MessageBox('{0}','{1}');\n", msgType, message, System.DateTime.Now.Ticks.ToString()));
		}
		
		
		/// <summary>
		/// ������Ϣ��
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
	/// ��Ϣ���ͣ�����Mail��Info��Error��Save��Advert��Wait��
	/// </summary>
	public enum MessageBoxType
	{
		/// <summary>
		/// �ʼ���
		/// </summary>
		Mail,
		/// <summary>
		/// ��Ϣ��
		/// </summary>
		Info,
		/// <summary>
		/// ����
		/// </summary>
		Error,
		/// <summary>
		/// ���档
		/// </summary>
		Save,
		/// <summary>
		/// ��ʾ��Ϣ��
		/// </summary>
		Advert,
		/// <summary>
		/// �ȴ���
		/// </summary>
		Wait
	}
}