using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Soft
	{
		private int _SoftId;

		public int SoftId
		{
			get { return _SoftId; }
			set { _SoftId = value; }
		}

		private Guid _SoftGuid;

		public Guid SoftGuid
		{
			get { return _SoftGuid; }
			set { _SoftGuid = value; }
		}

		private string _SoftType;

		public string SoftType
		{
			get { return _SoftType; }
			set { _SoftType = value; }
		}

		private string _Version;

		public string Version
		{
			get { return _Version; }
			set { _Version = value; }
		}

		private string _Language;

		public string Language
		{
			get { return _Language; }
			set { _Language = value; }
		}

		private string _Copyright;

		public string Copyright
		{
			get { return _Copyright; }
			set { _Copyright = value; }
		}

		private string _OperatingSystem;

		public string OperatingSystem
		{
			get { return _OperatingSystem; }
			set { _OperatingSystem = value; }
		}

		private string _DemoUri;

		public string DemoUri
		{
			get { return _DemoUri; }
			set { _DemoUri = value; }
		}

		private string _RegUri;

		public string RegUri
		{
			get { return _RegUri; }
			set { _RegUri = value; }
		}

		private string _UnzipPassword;

		public string UnzipPassword
		{
			get { return _UnzipPassword; }
			set { _UnzipPassword = value; }
		}

		public Soft()
		{ }

		public Soft(int SoftId,Guid SoftGuid,string SoftType,string Version,string Language,string Copyright,string OperatingSystem,string DemoUri,string RegUri,string UnzipPassword)
		{
			this.SoftId = SoftId;
			this.SoftGuid = SoftGuid;
			this.SoftType = SoftType;
			this.Version = Version;
			this.Language = Language;
			this.Copyright = Copyright;
			this.OperatingSystem = OperatingSystem;
			this.DemoUri = DemoUri;
			this.RegUri = RegUri;
			this.UnzipPassword = UnzipPassword;
		}

		public override string ToString()
		{
			return "SoftId = " + SoftId.ToString() + ",SoftGuid = " + SoftGuid.ToString() + ",SoftType = " + SoftType + ",Version = " + Version + ",Language = " + Language + ",Copyright = " + Copyright + ",OperatingSystem = " + OperatingSystem + ",DemoUri = " + DemoUri + ",RegUri = " + RegUri + ",UnzipPassword = " + UnzipPassword;
		}

		public class SoftIdComparer : System.Collections.Generic.IComparer<Soft>
		{
			public SorterMode SorterMode;
			public SoftIdComparer()
			{ }
			public SoftIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Soft> Membres
			int System.Collections.Generic.IComparer<Soft>.Compare(Soft x, Soft y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SoftId.CompareTo(x.SoftId);
				}
				else
				{
					return x.SoftId.CompareTo(y.SoftId);
				}
			}
			#endregion
		}
	}
}
