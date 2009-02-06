/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.Messaging;

namespace Wis.Toolkit.Message
{
	/// <summary>
	/// Message 的摘要说明。
	/// </summary>
	public class GeneralXmlMessage
	{
		private MessageQueue mq = null;
		private System.Messaging.Message m = null;

		public GeneralXmlMessage(string queueName)
		{
			if(queueName == null || queueName.Equals(string.Empty))
			{
				throw new System.Exception("队列名称不能为空");
			}
			this._QueueName = queueName; //获得队列名称

			try
			{
				//判断队列名称是否存在
				if (MessageQueue.Exists(queueName))
				{
					mq = new MessageQueue(queueName);
				}
				else
				{
					//QueueName 不存在，抛异常
					throw new System.Exception("队列名称" + queueName + "不存在");
				}
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
		}

		private string _QueueName = null;
		/// <summary>
		/// 队列名称
		/// </summary>
		public string QueueName
		{
			get
			{
				return _QueueName;			
			}
		}


		/// <summary>
		/// 消息总数
		/// </summary>
		public int Length
		{
			get
			{
				try
				{
					if(!(mq.CanRead))return int.MinValue;//不可读，则返回

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
		/// 接收消息
		/// </summary>
		/// <returns></returns>
		public string Receive()
		{
			try
			{
				if(!(mq.CanRead))return null;//不可读，则返回

				XmlMessageFormatter formatter = (XmlMessageFormatter)mq.Formatter;
				formatter.TargetTypeNames = new string[]{"System.String"};
				 
				m = mq.Receive(new TimeSpan(0,0,3));

				return Convert.ToString(m.Body);
			}
			catch(System.Exception ex)
			{
				//事务回滚，处理接收的消息
				Kernel.ExceptionAppender.Append(ex);
			}

			return null;		
		}


		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool Send(string message)
		{
			try
			{
				if(message == null || message == "")return false;

				//校验message的格式

				//写入指定的队列
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
		/// 释放 System.Messaging.MessageQueue 分配的所有资源。
		/// </summary>
		public void Close()
		{
			mq.Close();
		}
	}
}