/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.Messaging;

namespace Wis.Toolkit.Message
{
	/// <summary>
	/// Message ��ժҪ˵����
	/// </summary>
	public class GeneralXmlMessage
	{
		private MessageQueue mq = null;
		private System.Messaging.Message m = null;

		public GeneralXmlMessage(string queueName)
		{
			if(queueName == null || queueName.Equals(string.Empty))
			{
				throw new System.Exception("�������Ʋ���Ϊ��");
			}
			this._QueueName = queueName; //��ö�������

			try
			{
				//�ж϶��������Ƿ����
				if (MessageQueue.Exists(queueName))
				{
					mq = new MessageQueue(queueName);
				}
				else
				{
					//QueueName �����ڣ����쳣
					throw new System.Exception("��������" + queueName + "������");
				}
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}

		private string _QueueName = null;
		/// <summary>
		/// ��������
		/// </summary>
		public string QueueName
		{
			get
			{
				return _QueueName;			
			}
		}


		/// <summary>
		/// ��Ϣ����
		/// </summary>
		public int Length
		{
			get
			{
				try
				{
					if(!(mq.CanRead))return int.MinValue;//���ɶ����򷵻�

					return mq.GetAllMessages().Length;
				}
				catch(System.Exception ex)
				{
					Kernel.ExceptionAppender.Append(ex);
					return int.MinValue;		
				}
			}
		}

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <returns></returns>
		public string Receive()
		{
			try
			{
				if(!(mq.CanRead))return null;//���ɶ����򷵻�

				XmlMessageFormatter formatter = (XmlMessageFormatter)mq.Formatter;
				formatter.TargetTypeNames = new string[]{"System.String"};
				 
				m = mq.Receive(new TimeSpan(0,0,3));

				return Convert.ToString(m.Body);
			}
			catch(System.Exception ex)
			{
				//����ع���������յ���Ϣ
				Kernel.ExceptionAppender.Append(ex);
			}

			return null;		
		}


		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool Send(string message)
		{
			try
			{
				if(message == null || message == "")return false;

				//У��message�ĸ�ʽ

				//д��ָ���Ķ���
				mq.Send(message);

				return true;
			}
			catch(System.Exception ex)
			{
				Kernel.ExceptionAppender.Append(ex);
				return false;
			}
		}

		/// <summary>
		/// �ͷ� System.Messaging.MessageQueue �����������Դ��
		/// </summary>
		public void Close()
		{
			mq.Close();
		}
	}
}