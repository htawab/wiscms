using System;
using System.Collections.Generic;
using System.Text;

namespace Anthem
{
    public class AutoSuggestItemEventArgs : EventArgs
    {
        private AutoSuggestItem _item;

		public AutoSuggestItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

		public AutoSuggestItemEventArgs(AutoSuggestItem item)
        {
            this._item = item;
        }
    }
}
