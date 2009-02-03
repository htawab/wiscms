using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Log
	{
		private int _LoggerId;

		public int LoggerId
		{
			get { return _LoggerId; }
			set { _LoggerId = value; }
		}

		private Nullable<Guid> _LoggerGuid;

		public Nullable<Guid> LoggerGuid
		{
			get { return _LoggerGuid; }
			set { _LoggerGuid = value; }
		}

		private Nullable<Guid> _UserGuid;

		public Nullable<Guid> UserGuid
		{
			get { return _UserGuid; }
			set { _UserGuid = value; }
		}

		private string _Title;

		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}

		private Guid _SubmissionGuid;

		public Guid SubmissionGuid
		{
			get { return _SubmissionGuid; }
			set { _SubmissionGuid = value; }
		}

		private string _Message;

		public string Message
		{
			get { return _Message; }
			set { _Message = value; }
		}

		private DateTime _DateCreated;

		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Log()
		{ }

		public Log(int LoggerId,Nullable<Guid> LoggerGuid,Nullable<Guid> UserGuid,string Title,Guid SubmissionGuid,string Message,DateTime DateCreated)
		{
			this.LoggerId = LoggerId;
			this.LoggerGuid = LoggerGuid;
			this.UserGuid = UserGuid;
			this.Title = Title;
			this.SubmissionGuid = SubmissionGuid;
			this.Message = Message;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "LoggerId = " + LoggerId.ToString() + ",LoggerGuid = " + LoggerGuid.ToString() + ",UserGuid = " + UserGuid.ToString() + ",Title = " + Title + ",SubmissionGuid = " + SubmissionGuid.ToString() + ",Message = " + Message + ",DateCreated = " + DateCreated.ToString();
		}

		public class LoggerIdComparer : System.Collections.Generic.IComparer<Log>
		{
			public SorterMode SorterMode;
			public LoggerIdComparer()
			{ }
			public LoggerIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Log> Membres
			int System.Collections.Generic.IComparer<Log>.Compare(Log x, Log y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.LoggerId.CompareTo(x.LoggerId);
				}
				else
				{
					return x.LoggerId.CompareTo(y.LoggerId);
				}
			}
			#endregion
		}
		public class TitleComparer : System.Collections.Generic.IComparer<Log>
		{
			public SorterMode SorterMode;
			public TitleComparer()
			{ }
			public TitleComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Log> Membres
			int System.Collections.Generic.IComparer<Log>.Compare(Log x, Log y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Title.CompareTo(x.Title);
				}
				else
				{
					return x.Title.CompareTo(y.Title);
				}
			}
			#endregion
		}
		public class SubmissionGuidComparer : System.Collections.Generic.IComparer<Log>
		{
			public SorterMode SorterMode;
			public SubmissionGuidComparer()
			{ }
			public SubmissionGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Log> Membres
			int System.Collections.Generic.IComparer<Log>.Compare(Log x, Log y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SubmissionGuid.CompareTo(x.SubmissionGuid);
				}
				else
				{
					return x.SubmissionGuid.CompareTo(y.SubmissionGuid);
				}
			}
			#endregion
		}
		public class MessageComparer : System.Collections.Generic.IComparer<Log>
		{
			public SorterMode SorterMode;
			public MessageComparer()
			{ }
			public MessageComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Log> Membres
			int System.Collections.Generic.IComparer<Log>.Compare(Log x, Log y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Message.CompareTo(x.Message);
				}
				else
				{
					return x.Message.CompareTo(y.Message);
				}
			}
			#endregion
		}
		public class DateCreatedComparer : System.Collections.Generic.IComparer<Log>
		{
			public SorterMode SorterMode;
			public DateCreatedComparer()
			{ }
			public DateCreatedComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Log> Membres
			int System.Collections.Generic.IComparer<Log>.Compare(Log x, Log y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.DateCreated.CompareTo(x.DateCreated);
				}
				else
				{
					return x.DateCreated.CompareTo(y.DateCreated);
				}
			}
			#endregion
		}
	}
}
