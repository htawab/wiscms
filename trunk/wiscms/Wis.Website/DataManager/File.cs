using System;
using System.Text;

namespace Wis.Website.DataManager
{
	[Serializable()]
	public class File
	{
		private int _FileId;

		public int FileId
		{
			get { return _FileId; }
			set { _FileId = value; }
		}

		private Guid _FileGuid;

		public Guid FileGuid
		{
			get { return _FileGuid; }
			set { _FileGuid = value; }
		}

		private Guid _SubmissionGuid;

		public Guid SubmissionGuid
		{
			get { return _SubmissionGuid; }
			set { _SubmissionGuid = value; }
		}

		private string _OriginalFileName;

		public string OriginalFileName
		{
			get { return _OriginalFileName; }
			set { _OriginalFileName = value; }
		}

		private string _SaveAsFileName;

		public string SaveAsFileName
		{
			get { return _SaveAsFileName; }
			set { _SaveAsFileName = value; }
		}

		private int _Size;

		public int Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

		private int _Rank;

		public int Rank
		{
			get { return _Rank; }
			set { _Rank = value; }
		}

		private string _CreatedBy;

		public string CreatedBy
		{
			get { return _CreatedBy; }
			set { _CreatedBy = value; }
		}

		private DateTime _CreationDate;

		public DateTime CreationDate
		{
			get { return _CreationDate; }
			set { _CreationDate = value; }
		}

		private string _Description;

		public string Description
		{
			get { return _Description; }
			set { _Description = value; }
		}

		private int _Hits;

		public int Hits
		{
			get { return _Hits; }
			set { _Hits = value; }
		}

		public File()
		{ }

		public File(int FileId,Guid FileGuid,Guid SubmissionGuid,string OriginalFileName,string SaveAsFileName,int Size,int Rank,string CreatedBy,DateTime CreationDate,string Description,int Hits)
		{
			this.FileId = FileId;
			this.FileGuid = FileGuid;
			this.SubmissionGuid = SubmissionGuid;
			this.OriginalFileName = OriginalFileName;
			this.SaveAsFileName = SaveAsFileName;
			this.Size = Size;
			this.Rank = Rank;
			this.CreatedBy = CreatedBy;
			this.CreationDate = CreationDate;
			this.Description = Description;
			this.Hits = Hits;
		}

		public override string ToString()
		{
			return "FileId = " + FileId.ToString() + ",FileGuid = " + FileGuid.ToString() + ",SubmissionGuid = " + SubmissionGuid.ToString() + ",OriginalFileName = " + OriginalFileName + ",SaveAsFileName = " + SaveAsFileName + ",Size = " + Size.ToString() + ",Rank = " + Rank.ToString() + ",CreatedBy = " + CreatedBy + ",CreationDate = " + CreationDate.ToString() + ",Description = " + Description + ",Hits = " + Hits.ToString();
		}

		public class FileIdComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public FileIdComparer()
			{ }
			public FileIdComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.FileId.CompareTo(x.FileId);
				}
				else
				{
					return x.FileId.CompareTo(y.FileId);
				}
			}
			#endregion
		}
		public class FileGuidComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public FileGuidComparer()
			{ }
			public FileGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.FileGuid.CompareTo(x.FileGuid);
				}
				else
				{
					return x.FileGuid.CompareTo(y.FileGuid);
				}
			}
			#endregion
		}
		public class SubmissionGuidComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public SubmissionGuidComparer()
			{ }
			public SubmissionGuidComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
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
		public class OriginalFileNameComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public OriginalFileNameComparer()
			{ }
			public OriginalFileNameComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.OriginalFileName.CompareTo(x.OriginalFileName);
				}
				else
				{
					return x.OriginalFileName.CompareTo(y.OriginalFileName);
				}
			}
			#endregion
		}
		public class SaveAsFileNameComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public SaveAsFileNameComparer()
			{ }
			public SaveAsFileNameComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.SaveAsFileName.CompareTo(x.SaveAsFileName);
				}
				else
				{
					return x.SaveAsFileName.CompareTo(y.SaveAsFileName);
				}
			}
			#endregion
		}
		public class SizeComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public SizeComparer()
			{ }
			public SizeComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Size.CompareTo(x.Size);
				}
				else
				{
					return x.Size.CompareTo(y.Size);
				}
			}
			#endregion
		}
		public class RankComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public RankComparer()
			{ }
			public RankComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Rank.CompareTo(x.Rank);
				}
				else
				{
					return x.Rank.CompareTo(y.Rank);
				}
			}
			#endregion
		}
		public class CreatedByComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public CreatedByComparer()
			{ }
			public CreatedByComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CreatedBy.CompareTo(x.CreatedBy);
				}
				else
				{
					return x.CreatedBy.CompareTo(y.CreatedBy);
				}
			}
			#endregion
		}
		public class CreationDateComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public CreationDateComparer()
			{ }
			public CreationDateComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.CreationDate.CompareTo(x.CreationDate);
				}
				else
				{
					return x.CreationDate.CompareTo(y.CreationDate);
				}
			}
			#endregion
		}
		public class DescriptionComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public DescriptionComparer()
			{ }
			public DescriptionComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Description.CompareTo(x.Description);
				}
				else
				{
					return x.Description.CompareTo(y.Description);
				}
			}
			#endregion
		}
		public class HitsComparer : System.Collections.Generic.IComparer<File>
		{
			public SorterMode SorterMode;
			public HitsComparer()
			{ }
			public HitsComparer(SorterMode SorterMode)
			{
				this.SorterMode = SorterMode;
			}
			#region IComparer<File> Membres
			int System.Collections.Generic.IComparer<File>.Compare(File x, File y)
			{
				if (SorterMode == SorterMode.Ascending)
				{
					return y.Hits.CompareTo(x.Hits);
				}
				else
				{
					return x.Hits.CompareTo(y.Hits);
				}
			}
			#endregion
		}
	}
}
