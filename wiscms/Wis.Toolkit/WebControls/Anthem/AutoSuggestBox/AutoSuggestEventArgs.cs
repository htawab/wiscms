using System;
using System.Collections.Generic;
using System.Text;

namespace Anthem
{
    public class AutoSuggestEventArgs : EventArgs
    {
        private string _currentText;

        public string CurrentText
        {
            get { return _currentText; }
            set { _currentText = value; }
        }

        public AutoSuggestEventArgs(string text)
        {
            this._currentText = text;
        }
    }
}
