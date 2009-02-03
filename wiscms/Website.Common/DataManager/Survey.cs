using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class Survey
	{
		private int _SurveyId;

		public int SurveyId
		{
			get { return _SurveyId; }
			set { _SurveyId = value; }
		}

		private Nullable<Guid> _SurveyGuid;

		public Nullable<Guid> SurveyGuid
		{
			get { return _SurveyGuid; }
			set { _SurveyGuid = value; }
		}

		private Guid _SubmissionGuid;

		public Guid SubmissionGuid
		{
			get { return _SubmissionGuid; }
			set { _SubmissionGuid = value; }
		}

		private string _Voter;

		public string Voter
		{
			get { return _Voter; }
			set { _Voter = value; }
		}

		private int _Vote;

		public int Vote
		{
			get { return _Vote; }
			set { _Vote = value; }
		}

		private string _IPAddress;

		public string IPAddress
		{
			get { return _IPAddress; }
			set { _IPAddress = value; }
		}

		private Nullable<DateTime> _DateCreated;

		public Nullable<DateTime> DateCreated
		{
			get { return _DateCreated; }
			set { _DateCreated = value; }
		}

		public Survey()
		{ }

		public Survey(int SurveyId,Nullable<Guid> SurveyGuid,Guid SubmissionGuid,string Voter,int Vote,string IPAddress,Nullable<DateTime> DateCreated)
		{
			this.SurveyId = SurveyId;
			this.SurveyGuid = SurveyGuid;
			this.SubmissionGuid = SubmissionGuid;
			this.Voter = Voter;
			this.Vote = Vote;
			this.IPAddress = IPAddress;
			this.DateCreated = DateCreated;
		}

		public override string ToString()
		{
			return "SurveyId = " + SurveyId.ToString() + ",SurveyGuid = " + SurveyGuid.ToString() + ",SubmissionGuid = " + SubmissionGuid.ToString() + ",Voter = " + Voter + ",Vote = " + Vote.ToString() + ",IPAddress = " + IPAddress + ",DateCreated = " + DateCreated.ToString();
		}

		public class SurveyIdComparer : System.Collections.Generic.IComparer<Survey>
		{
			public SorterMode SorterMode;
			public SurveyIdComparer()
			{ }
			public SurveyIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Survey> Membres
			int System.Collections.Generic.IComparer<Survey>.Compare(Survey x, Survey y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SurveyId.CompareTo(x.SurveyId);
				}
				else
				{
					return x.SurveyId.CompareTo(y.SurveyId);
				}
			}
			#endregion
		}
		public class SubmissionGuidComparer : System.Collections.Generic.IComparer<Survey>
		{
			public SorterMode SorterMode;
			public SubmissionGuidComparer()
			{ }
			public SubmissionGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Survey> Membres
			int System.Collections.Generic.IComparer<Survey>.Compare(Survey x, Survey y)
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
		public class VoterComparer : System.Collections.Generic.IComparer<Survey>
		{
			public SorterMode SorterMode;
			public VoterComparer()
			{ }
			public VoterComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Survey> Membres
			int System.Collections.Generic.IComparer<Survey>.Compare(Survey x, Survey y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Voter.CompareTo(x.Voter);
				}
				else
				{
					return x.Voter.CompareTo(y.Voter);
				}
			}
			#endregion
		}
		public class VoteComparer : System.Collections.Generic.IComparer<Survey>
		{
			public SorterMode SorterMode;
			public VoteComparer()
			{ }
			public VoteComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Survey> Membres
			int System.Collections.Generic.IComparer<Survey>.Compare(Survey x, Survey y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Vote.CompareTo(x.Vote);
				}
				else
				{
					return x.Vote.CompareTo(y.Vote);
				}
			}
			#endregion
		}
		public class IPAddressComparer : System.Collections.Generic.IComparer<Survey>
		{
			public SorterMode SorterMode;
			public IPAddressComparer()
			{ }
			public IPAddressComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<Survey> Membres
			int System.Collections.Generic.IComparer<Survey>.Compare(Survey x, Survey y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.IPAddress.CompareTo(x.IPAddress);
				}
				else
				{
					return x.IPAddress.CompareTo(y.IPAddress);
				}
			}
			#endregion
		}
	}
}
