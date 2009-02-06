//----------------------------------------------------------------
// Copyright (C) 2004-2005 Zehua Corporation
// All rights reserved.
//----------------------------------------------------------------

using System;
using System.Web.UI; 

[assembly:TagPrefix("WebControls","RichCalendar")]

namespace Wis.Toolkit.WebControls 
{ 
	[ToolboxData("<{0}:RichCalendar runat=server></{0}:RichCalendar>")] 
	public class RichCalendar : System.Web.UI.WebControls.WebControl, IPostBackDataHandler 
	{

		/// <summary>
		/// 日期控件中的文本
		/// </summary>
		public string Text 
		{ 
			get 
			{ 
				return ((string)(ViewState["Text"])); 
			} 
			set 
			{ 
				ViewState["Text"] = value; 
				this.Attributes["value"] = value; 
			} 
		}

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler TextChanged; 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="postDataKey"></param>
		/// <param name="postCollection"></param>
		/// <returns></returns>
		public virtual bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection) 
		{ 
			string presentValue = Text; 
			string postedValue = postCollection[postDataKey]; 
			if (presentValue == null) 
			{
				Text = postedValue; 
				return true; 
			} 
			if (!(presentValue.Equals(postedValue))) 
			{ 
				Text = postedValue; 
				return true; 
			} 
			return false; 
		} 

		/// <summary>
		/// 
		/// </summary>
		public virtual void RaisePostDataChangedEvent() 
		{ 
			OnTextChanged(EventArgs.Empty); 
		} 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnTextChanged(EventArgs e) 
		{ 
			if (TextChanged != null) 
			{ 
				TextChanged(this, e); 
			} 
		} 

		/// <summary>
		/// 
		/// </summary>
		public override bool Enabled 
		{ 
			get 
			{ 
				string sValue; 
				sValue = this.Attributes["disabled"]; 
				if (sValue == null) 
				{ 
					return false; 
				} 
				else 
				{ 
					if (sValue.Trim() == "true") 
					{ 
						return true; 
					} 
					else 
					{ 
						return false; 
					} 
				} 
			} 
			set 
			{ 
				if (value) 
				{ 
					this.Attributes["disabled"] = "true"; 
				} 
				else 
				{ 
					this.Attributes["disabled"] = "false"; 
				} 
			} 
		} 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected void RenderAttributes(System.Web.UI.HtmlTextWriter writer) 
		{ 
			writer.WriteAttribute("id", base.ClientID); 
			writer.WriteAttribute("name", this.UniqueID); 
			writer.WriteAttribute("type", "text"); 
			if (this.Text != null) 
			{ 
				writer.WriteAttribute("value", this.Text); 
			} 
			//if (this.TabIndex != null) 
			//{ 
				writer.WriteAttribute("tabindex", this.TabIndex.ToString()); 
			//} 
			if (this.ToolTip != null) 
			{ 
				writer.WriteAttribute("title", this.ToolTip); 
			} 
			//if (this.Width != null) 
			//{ 
				writer.WriteAttribute("size", "10"); 
			//} 
			writer.WriteAttribute("onkeydown", "if(event.keyCode==13)event.keyCode=9"); 
			writer.WriteAttribute("onmouseover", "window.status='日期录入框';return true;"); 
			writer.WriteAttribute("onmouseout", "window.status='';return true;"); 
			writer.WriteAttribute("style", "cursor:hand;"); 
			writer.WriteAttribute("ondblclick", "ShowCalendar(this.id);");

			if (this.Enabled) 
			{ 
				writer.WriteAttribute("disabled", "true"); 
			} 
			this.Attributes.Render(writer); 
		} 

		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(System.Web.UI.HtmlTextWriter writer) 
		{ 
			writer.WriteBeginTag("input"); 
			this.RenderAttributes(writer); 
			writer.Write(">"); 
		} 
	} 
}