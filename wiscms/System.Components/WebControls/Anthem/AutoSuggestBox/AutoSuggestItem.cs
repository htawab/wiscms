using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Anthem
{
    public class AutoSuggestItem : Control, INamingContainer
    {

        private int _itemIndex;
        private object _dataItem;
		private AutoSuggestItemType _itemType;
				

        public AutoSuggestItem(int itemIndex, object dataItem, AutoSuggestItemType itemType)
        {
            this._itemIndex = itemIndex;
            this._dataItem = dataItem;
			this._itemType = itemType;
        }

        public object DataItem
        {
            get{ return _dataItem; }
        }

        public int ItemIndex
        {
            get{ return _itemIndex; }
        }

		public AutoSuggestItemType ItemType
		{
			get { return _itemType; }
		}
				
		public override Control FindControl(string controlID)
		{
			foreach (Control c in this.Controls)
			{
				if (c.ID == controlID)
					return c;
			}
			return null;
		}
    }

}
