//------------------------------------------------------------------------------
// <copyright file="IntBox.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * 1、添加验证控件
 * 2、添加错误图片支持
 * 3、动态检测值，调用 JavaScript 的通用校验
 * 4、提供对smallint，int，Bigint的支持
 * 
 * */

namespace Wis.Toolkit.WebControls
{
	/// <summary>
    /// 整型输入框。
	/// </summary>
	[DefaultProperty("Text"),ToolboxData("<{0}:IntBox runat=server></{0}:IntBox>"),Designer("Wis.Toolkit.WebControls.IntBoxDesigner, Wis.Toolkit.WebControls")]
	public class IntBox: WebControl, INamingContainer, IPostBackDataHandler
	{
		private TextBox tb;
		public IntBox()
		{
			tb = new TextBox();
			tb.BackColor = System.Drawing.Color.LightSeaGreen;
			tb.Attributes.Add("ondragenter","return false");
			tb.Attributes.Add("onkeydown", "if(event.keyCode==13)event.keyCode=9");//回车转到下一录入框
			tb.Attributes.Add("onkeypress", "if ( !(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) || (window.event.keyCode == 13) || (window.event.keyCode == 46)|| (window.event.keyCode == 45)))return false;");//只能录入数字
		}

        /// <summary>
        /// 获得整型输入框的属性。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(0)]
        public new System.Web.UI.AttributeCollection Attributes
        {
            get { return tb.Attributes; } 
        }

		[Bindable(true),
		Category("Appearance"),
		DefaultValue("")]
		public string Text
		{
            get { return tb.Text; }
			set { tb.Text = value;}
		}

		[Description("TextBox_MaxLength"), Bindable(true), Category("Behavior"), DefaultValue(0)]
		public virtual int MaxLength
		{
			get
			{
				return tb.MaxLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				tb.MaxLength = value;
			}
		}

		[Category("Behavior"), Bindable(true), Description("TextBox_ReadOnly"), DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return tb.ReadOnly;
			}
			set
			{
				tb.ReadOnly = value;
			}
		}

		[DefaultValue(false), Description("TextBox_AutoPostBack"), Category("Behavior")]
		public virtual bool AutoPostBack
		{
			get
			{
				return tb.AutoPostBack;
			}
			set
			{
				tb.AutoPostBack = value;
			}
		}

		public event EventHandler EventValueChanged;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected void OnValueChanged(EventArgs e)
		{
			EventValueChanged(this,e);
		}

		[Category("Action"), Description("TextBox_OnTextChanged")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(EventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(EventValueChanged, value);
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataKey"></param>
        /// <param name="postCollection"></param>
        /// <returns></returns>
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
            if (postCollection == null) return false;

			int value1 = Int32.Parse(this.Text);
			int value2 = Int32.Parse(postCollection[postDataKey]);
			if (!value1.Equals(value2))
			{
				this.Text = value2.ToString();
				return true; 
			}
			return false;
		}

		public void RaisePostDataChangedEvent()
		{
			this.OnValueChanged(EventArgs.Empty);
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void CreateChildControls()
		{
			tb.ID = UniqueID;
			tb.Width = this.Width;
			tb.CssClass = this.CssClass;
			tb.Height = this.Height;
			tb.ToolTip = this.ToolTip;
			tb.TabIndex = this.TabIndex;
//			if(tb.AutoPostBack)
//				tb.TextChanged += new EventHandler(this.TextBox_Change);
			Controls.Add(tb);
		}
	}

	/// <summary>
	/// 设计时支持。 
	/// </summary>
	public class IntBoxDesigner : System.Web.UI.Design.ControlDesigner 
	{
		/// <summary>
		/// 
		/// </summary>
		public IntBoxDesigner()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);

			TextBox tb = new TextBox();
			tb.BackColor = System.Drawing.Color.LightSeaGreen;
			tb.RenderControl(tw);

			return sw.ToString();
		}
	}
}
