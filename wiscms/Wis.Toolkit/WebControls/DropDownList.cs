
namespace Wis.Toolkit.WebControls
{
	/// <summary>
	/// DropDownList 的摘要说明。
	/// </summary>
	public class DropDownList : System.Web.UI.WebControls.DropDownList
	{
		public DropDownList()
		{
		}

		/// <summary>
		/// 排序还没有完成
		/// </summary>
		public void SortByText()
		{
			if(this.Items.Count == 0)return;
			System.Web.UI.WebControls.ListItem[] items = new System.Web.UI.WebControls.ListItem[this.Items.Count];
			for(int index=0;index<this.Items.Count;index++)
			{
				items[index] = this.Items[index];
			}

			//ListItemComparer lic = new ListItemComparer();
			//Array arr = items;
			
			this.Items.Clear();
			//this.Items.AddRange(arr);
		}

		public void SortByValue()
		{
			//
		}

//		private class ListItemComparer : IComparer
//		{
//			public System.Int32 Compare ( System.Object x , System.Object y )
//			{
//				System.Web.UI.WebControls.ListItem a = (System.Web.UI.WebControls.ListItem)x;
//				System.Web.UI.WebControls.ListItem b = (System.Web.UI.WebControls.ListItem)y;
//				CaseInsensitiveComparer c = new CaseInsensitiveComparer();
//				return c.Compare(a.Text,b.Text);
//			}
//		}
	}
}
