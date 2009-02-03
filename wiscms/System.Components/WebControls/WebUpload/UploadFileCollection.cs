using System;
using System.Collections;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadFileCollection.
	/// </summary>
	public class UploadFileCollection:ArrayList
	{
		new UploadFile this[int i_index]
		{
			get{return base[i_index] as UploadFile;}
			set{base[i_index] = value;}			
		}
		public int Add(UploadFile value)
		{
			return base.Add (value);
		}

		public UploadFileCollection()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}