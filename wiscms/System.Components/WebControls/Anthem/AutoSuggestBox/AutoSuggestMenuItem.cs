using System;
using System.Text;
using System.Web;


namespace Anthem
{
	/// <summary>Data structure for menu items in suggestion div</summary>
	public class AutoSuggestMenuItem
	{
		private string _text;
		private string _value;
		private bool _isSelectable;
		private string _cssClass;
        private string _textBoxText;

		#region Class Properties
		
		public string Text
		{
			get	{return _text;}
			set	{_text=value;}
		}

		public string Value
		{
			get	{return _value;}
			set	{_value=value;}
		}

		
		public bool IsSelectable
		{
			get	{return _isSelectable;}
			set	{_isSelectable=value;}
		}

		public string CSSClass
		{
			get	{return _cssClass;}
			set	{_cssClass=value;}
		}

        public string TextBoxText
        {
            get { return _textBoxText; }
            set { _textBoxText = value; }
        }
		#endregion


		//Constructor
		public AutoSuggestMenuItem()
		{
			_cssClass="asbMenuItem";
			_isSelectable=true;
		}


		public string GenHtml(int nItemIndex, string sTextBoxID)
		{
			string sMenuItemValueID;
			string sFunc1;
			string sFunc2;
			
			string sHtml="";
			if (this.IsSelectable)
			{
				string sCtrlID=sTextBoxID + "_mi_" + nItemIndex;
				string sObj="asbGetObj('" + sTextBoxID + "')";

				sFunc1=sObj + ".OnMouseClick(" + nItemIndex + ")";
				sFunc2=sObj + ".OnMouseOver(" + nItemIndex  + ")";
				
				sHtml += "<div class=\"" + this.CSSClass + "\"" +
								" id=\"" + sCtrlID + "\"" +
								" name=\"" + sCtrlID + "\"" +
								" value=\"" + System.Web.HttpUtility.HtmlEncode(this.Value) + "\"" +
                                " textboxdisplay=\"" + System.Web.HttpUtility.HtmlEncode(this.TextBoxText) + "\"" +
								" onclick=\"" + sFunc1 + "\"" + 
								" onmouseover=\"" + sFunc2 + "\">" + this.Text + "</div>";
				sMenuItemValueID=sCtrlID + "_value";
				sHtml += "\n\r";
			}
			else
			{
				sHtml += "<div class=\"" + this.CSSClass + "\" style=\"cursor:auto\">" + this.Text + "</div>";
			}

			return sHtml;
		}
	}

}
