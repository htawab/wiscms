using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Data;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Specialized;


[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.AutoSuggestBox.css", "text/css")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.AutoSuggestBox.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.Blank.html", "text/html")]
//http://www.codeproject.com/Ajax/AJAXAutoSuggest.asp
namespace Anthem
{
    [ParseChildren(true)]	
	
	public class AutoSuggestBox : Control, INamingContainer, IUpdatableControl, ICallBackControl
	{

        #region Template & DataBound Properties
        private ITemplate _itemTemplate = null;
		private ITemplate _headerTemplate = null;
        private IEnumerable _dataSource = null;

        public IEnumerable DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        TemplateContainer(typeof(AutoSuggestItem))]
        public ITemplate ItemTemplate
        {
            get
            {
                return _itemTemplate;
            }
            set
            {
                _itemTemplate = value;
            }
        }

		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(AutoSuggestItem))]
		public ITemplate HeaderTemplate
		{
			get
			{
				return _headerTemplate;
			}
			set
			{
				_headerTemplate = value;
			}
		} 
        #endregion
        
        #region Inner Controls

        private TextBox _textBox;
        private TextBox _hiddenTextBox;
        private CustomValidator _validator;        
		#endregion

        #region Create Child Controls
        protected override void CreateChildControls()
        {
            _textBox = new TextBox();
            _textBox.ID = this.TextBoxID;
            _textBox.Width = this.Width;
            _textBox.UpdateAfterCallBack = _updateAfterCallBack;			

            _hiddenTextBox = new TextBox();
            _hiddenTextBox.ID = this.TextBoxID + "_SelectedValue"; 
            _hiddenTextBox.UpdateAfterCallBack = _updateAfterCallBack;            
            _hiddenTextBox.Style.Add("display", "none");
                                    
            this.Controls.Add(_textBox);
            this.Controls.Add(_hiddenTextBox);

            if (Required)
            {
                _validator = new CustomValidator();
                _validator.ID = "val" + this.ID;
                _validator.ControlToValidate = _hiddenTextBox.ID;
                _validator.ErrorMessage = this.ErrorMessage;
                _validator.ValidationGroup = this.ValidationGroup;
                _validator.Display = ValidatorDisplay.Dynamic;
                _validator.ClientValidationFunction = string.Format("{0}ClientValidate", this.ID);
                _validator.ValidateEmptyText = true;
                _validator.ServerValidate += new ServerValidateEventHandler(_validator_ServerValidate);
                
                Literal literal = new Literal();
                literal.Text = "&nbsp;";

                this.Controls.Add(literal);
                this.Controls.Add(_validator);
            }         

        }
        
        void _validator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args.Value != "0" && args.Value != "");
        }
        #endregion

        #region Inicialização
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
			if (!DesignMode)
				RegisterClientScript();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            //access the textboxes values to keep it after callback
            string s = this.Text;
            s = this.SelectedValue; 

            base.OnLoad(e);            
            Anthem.Manager.Register(this);
        }

        #endregion
                
        #region IUpdatableControl implementation

        [DefaultValue(false)]
        public bool AutoUpdateAfterCallBack
        {
            get
            {
                if (ViewState["AutoUpdateAfterCallBack"] == null)
                    return false;
                else
                    return (bool)ViewState["AutoUpdateAfterCallBack"];
            }
            set
            {
                if (value) UpdateAfterCallBack = true;
                ViewState["AutoUpdateAfterCallBack"] = value;
            }
        }

        private bool _updateAfterCallBack = false;

        [Browsable(false), DefaultValue(false)]
        public bool UpdateAfterCallBack
        {
            get { return _updateAfterCallBack; }
            set { _updateAfterCallBack = value; }
        }

        [
        Category("Misc"),
        Description("Fires before the control is rendered with updated values.")
        ]
        public event EventHandler PreUpdate
        {
            add { Events.AddHandler(EventPreUpdateKey, value); }
            remove { Events.RemoveHandler(EventPreUpdateKey, value); }
        }
        private static readonly object EventPreUpdateKey = new object();

        public virtual void OnPreUpdate()
        {
            EventHandler EditHandler = (EventHandler)Events[EventPreUpdateKey];
            if (EditHandler != null)
                EditHandler(this, EventArgs.Empty);
        }

        #endregion

        #region ICallBackControl implementation

        [DefaultValue("")]
        public string CallBackCancelledFunction
        {
            get
            {
                if (null == ViewState["CallBackCancelledFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["CallBackCancelledFunction"];
            }
            set { ViewState["CallBackCancelledFunction"] = value; }
        }

        [DefaultValue(true)]
        public bool EnableCallBack
        {
            get
            {
                if (ViewState["EnableCallBack"] == null)
                    return true;
                else
                    return (bool)ViewState["EnableCallBack"];
            }
            set
            {
                ViewState["EnableCallBack"] = value;
            }
        }

        [DefaultValue(true)]
        public bool EnabledDuringCallBack
        {
            get
            {
                if (null == ViewState["EnabledDuringCallBack"])
                    return true;
                else
                    return (bool)ViewState["EnabledDuringCallBack"];
            }
            set { ViewState["EnabledDuringCallBack"] = value; }
        }

        [DefaultValue("")]
        public string PostCallBackFunction
        {
            get
            {
                if (null == ViewState["PostCallBackFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["PostCallBackFunction"];
            }
            set { ViewState["PostCallBackFunction"] = value; }
        }

        [DefaultValue("")]
        public string PreCallBackFunction
        {
            get
            {
                if (null == ViewState["PreCallBackFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["PreCallBackFunction"];
            }
            set { ViewState["PreCallBackFunction"] = value; }
        }

        [DefaultValue("")]
        public string TextDuringCallBack
        {
            get
            {
                if (null == ViewState["TextDuringCallBack"])
                    return string.Empty;
                else
                    return (string)ViewState["TextDuringCallBack"];
            }
            set { ViewState["TextDuringCallBack"] = value; }
        }

        #endregion

        #region Anthem Methods
        [DefaultValue(false)]
        public bool AutoCallBack
        {
            get
            {
                if (null == ViewState["AutoCallBack"])
                    return false;
                else
                    return (bool)ViewState["AutoCallBack"];
            }
            set { ViewState["AutoCallBack"] = value; }
        }

        public override bool Visible
        {
            get
            {
                return Anthem.Manager.GetControlVisible(this, ViewState, DesignMode);
            }
            set { Anthem.Manager.SetControlVisible(ViewState, value); }
        }

        public override void RenderControl(HtmlTextWriter writer)
        {
            base.Visible = true;
            base.RenderControl(writer);
        } 
        #endregion

        #region Register Resources
        private void RegisterClientScript()
        {

            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("asb"))
            {

#if DEBUG
                // this.GetType().Namespace
                Stream stream = typeof(Anthem.AutoSuggestBox).Assembly.GetManifestResourceStream("Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.AutoSuggestBox.js");
                StreamReader sr = new StreamReader(stream);
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asb",
                        @"<script type=""text/javascript"">
                        //![CDATA[
                        " + sr.ReadToEnd() + @"
                        //]]>
                        </script>");
#else
                this.Page.ClientScript.RegisterClientScriptInclude(
                         this.GetType(), "asb",
                         Page.ClientScript.GetWebResourceUrl(this.GetType(),
                         "Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.AutoSuggestBox.js"));
#endif


                string csslink = "<link href='" +
                          Page.ClientScript.GetWebResourceUrl(this.GetType(),
                          "Wis.Toolkit.WebControls.Anthem.AutoSuggestBox.includes.AutoSuggestBox.css")
                          + "' rel='stylesheet' type='text/css' />";


				if (Page.Header == null)
					throw new Exception("The page head is null. Add the runat=\"server\" attribute to it.");
				else
				{
					LiteralControl include = new LiteralControl(csslink);
					Page.Header.Controls.Add(include);
				}

            }
        }
        #endregion

        #region Contructor
        /// <summary>Initializes new instance of AutoSuggestBox/// </summary>
        /// <remarks>Wire the events so the control can participate in them</remarks>
        public AutoSuggestBox()
        {              
            
        } 
        #endregion
        
		#region Properties
		public string Text
		{
			get
			{
				this.EnsureChildControls();
				return _textBox.Text;
			}
			set
			{
				this.EnsureChildControls();
				_textBox.Text = value;
			}
		}

		public string SelectedValue
		{
			get
			{
				this.EnsureChildControls();
				return _hiddenTextBox.Text;
			}
			set
			{
				this.EnsureChildControls();
				_hiddenTextBox.Text = value;
			}
		}

		/// <summary>
		/// The message that will be displayed in case the datasource's Count property equals zero
		/// </summary>
		[Description("The message that will be displayed in case the datasource's Count property equals zero")]
		public string ItemNotFoundMessage
		{
			get { return ViewState["ItemNotFoundMessage"] == null ? "" : ViewState["ItemNotFoundMessage"].ToString(); }
			set { ViewState["ItemNotFoundMessage"] = value; }
		}

		/// <summary>
		/// After the search string reaches a determined length, the control will not fire the callback
		/// </summary>
		[Description("After the search string reaches a determined length, the control will not fire the callback")]
		public int MaxSuggestChars
		{
            get { return ViewState["MaxSuggestChars"] == null ? 20 : Convert.ToInt32(ViewState["MaxSuggestChars"]); }
            set { ViewState["MaxSuggestChars"] = value; }
		}
        
		/// <summary>
		/// The time interval, in miliseconds, that the callback is executed after the user presses a key
		/// </summary>
		[Description("The time interval, in miliseconds, that the callback is executed after the user presses a key")]
		public int KeyPressDelay
		{
            get { return ViewState["KeyPressDelay"] == null ? 300 : Convert.ToInt32(ViewState["KeyPressDelay"]); }
            set { ViewState["KeyPressDelay"] = value; }
		}

		public string MenuCSSClass
		{
            get { return ViewState["MenuCSSClass"] == null ? "asbMenu" : ViewState["MenuCSSClass"].ToString(); }
            set { ViewState["MenuCSSClass"] = value; }
		}
        
		public string MenuItemCSSClass
		{
            get { return ViewState["MenuItemCSSClass"] == null ? "asbMenuItem" : ViewState["MenuItemCSSClass"].ToString(); }
            set { ViewState["MenuItemCSSClass"] = value; }
		}
        
		public string SelectedMenuItemCSSClass
		{
			get { return ViewState["SelectedMenuItemCSSClass"] == null ? "asbSelMenuItem" : ViewState["SelectedMenuItemCSSClass"].ToString(); }
			set { ViewState["SelectedMenuItemCSSClass"] = value; }
		}
        
		public bool UseIFrame
		{
            get { return ViewState["UseIFrame"] == null ? true : Convert.ToBoolean(ViewState["UseIFrame"]); }
            set { ViewState["UseIFrame"] = value; }
		}

        public string CallBackControlID
        {
            get { return ViewState["CallBackControlID"] == null ? this.ClientID : ViewState["CallBackControlID"].ToString(); }
            set { ViewState["CallBackControlID"] = value; }
        }
        	   
		public string TextBoxID
		{
			get	{
				this.EnsureChildControls();
				return this._textBox.ClientID;
			}
		}

		private string MenuDivID
		{
			get	{return "divMenu_" + this.TextBoxID;}
		}

		/// <summary>
		/// Indicates if the field is required
		/// </summary>
		[Description("Indicates if the field is required")]
        public bool Required
        {
            get { return ViewState["Required"] == null ? false : Convert.ToBoolean(ViewState["Required"]); }
            set { ViewState["Required"] = value; }
        }

		/// <summary>
		/// RequiredFieldValidator error message
		/// </summary>
		[Description("RequiredFieldValidator error message")]
        public string ErrorMessage
        {
            get { return ViewState["ErrorMessage"] == null ? "This field is required" : ViewState["ErrorMessage"].ToString(); }
            set { ViewState["ErrorMessage"] = value; }
        }

		/// <summary>
		/// RequiredFieldValidator validation group
		/// </summary>
		[Description("RequiredFieldValidator validation group")]
        public string ValidationGroup
        {
            get { return ViewState["ValidationGroup"] == null ? "" : ViewState["ValidationGroup"].ToString(); }
            set { ViewState["ValidationGroup"] = value; }
        }

        public string TableHeaderCssClass
        {
            get { return ViewState["TableHeaderCssClass"] == null ? "" : ViewState["TableHeaderCssClass"].ToString(); }
            set { ViewState["TableHeaderCssClass"] = value; }
        }

        public string TableCssClass
        {
            get { return ViewState["TableCssClass"] == null ? "" : ViewState["TableCssClass"].ToString(); }
            set { ViewState["TableCssClass"] = value; }
        }
				
        public string DataKeyField
        {
            get { return ViewState["DataKeyField"] == null ? "" : ViewState["DataKeyField"].ToString(); }
            set { ViewState["DataKeyField"] = value; }
        }

		/// <summary>
		/// The field that will be displayed on the textbox after an item is selected
		/// </summary>
		[Description("The field that will be displayed on the textbox after an item is selected")]
        public string TextBoxDisplayField
        {
            get { return ViewState["TextBoxDisplayField"] == null ? "" : ViewState["TextBoxDisplayField"].ToString(); }
            set { ViewState["TextBoxDisplayField"] = value; }
        }

        public Unit Width
        {
            get { return ViewState["Width"] == null ? new Unit("200px") : (Unit)ViewState["Width"]; }
            set { ViewState["Width"] = value; }
        } 
		#endregion
        
        #region GetFirst

        private Control GetFirstCtrl(string sCtrlType, ControlCollection colControls)
        {

            Control oFirstCtrl = null;

            foreach (Control oCtrl in colControls)
            {
                //Check if control type matches sCtrlType 
                if (oCtrl.GetType().ToString() == sCtrlType)
                {
                    //Make sure the control is visible
                    if (oCtrl.Visible)
                        return oCtrl;
                    else
                        continue;
                }


                if (oCtrl.HasControls())
                {
                    oFirstCtrl = GetFirstCtrl(sCtrlType, oCtrl.Controls);
                    if (oFirstCtrl != null)
                        break;
                }
            }

            return oFirstCtrl;
        }

        private bool IsTopASBCtrl()
        {
            Control oCtrl = GetFirstCtrl("ASB.AutoSuggestBox", Page.Controls);
            if (oCtrl == null)
                return false;

            if (oCtrl.ClientID == this.ClientID)
                return true;
            else
                return false;
        } 
        #endregion
		
        	
		/// <summary>
		/// Writes the initialization javascript code for the control
		/// </summary>
		/// <param name="output">Page output</param>
		protected void WriteJSAutoSuggestBox(HtmlTextWriter output)
		{
            //Check if this is the first visible ASP control on the page
            if (this.IsTopASBCtrl())
            {
                //Declare auto suggest box object
                output.WriteLine("<script language=\"javascript\">");
                output.WriteLine("<!--");

                output.WriteLine("var oJSAutoSuggestBox;");

                output.WriteLine("//-->");
                output.WriteLine("</script>");
                output.WriteLine("");
            }

			output.WriteLine("<script language=\"javascript\">");
			output.WriteLine("<!--");

            output.Write(GetInitializationScript());

			output.WriteLine("//-->");
			output.WriteLine("</script>");
			output.WriteLine("");

            //Validation Script
            if (this.Required)
            {
                string str = @"<script type=""text/javascript"">
                                //![CDATA[
                                function " + this.ClientID + @"ClientValidate(sender, args)
                                {
                                      args.IsValid = !(args.Value == '' || args.Value == '0');
                                      return;                                    
                                }
                               //]]>
                            </script>";

                output.Write(str);
            }			
		}

		/// <summary>
		/// Returns the javascript code necessary to instanciate the object that represents the control
		/// </summary>		
        private string GetInitializationScript()
        {
            StringBuilder str = new StringBuilder();

            str.Append("oJSAutoSuggestBox=new JSAutoSuggestBox();");

            str.Append("oJSAutoSuggestBox.msTextBoxID='" + this.TextBoxID + "';");
            str.Append("oJSAutoSuggestBox.msMenuDivID='" + this.MenuDivID + "';");            
            str.Append("oJSAutoSuggestBox.mnMaxSuggestChars=" + this.MaxSuggestChars.ToString() + ";");
            str.Append("oJSAutoSuggestBox.mnKeyPressDelay=" + this.KeyPressDelay.ToString() + ";");
            str.Append("oJSAutoSuggestBox.msMenuCSSClass='" + this.MenuCSSClass + "';");
            str.Append("oJSAutoSuggestBox.msMenuItemCSSClass='" + this.MenuItemCSSClass + "';");
            str.Append("oJSAutoSuggestBox.msSelMenuItemCSSClass='" + this.SelectedMenuItemCSSClass + "';");
            str.Append("oJSAutoSuggestBox.mbUseIFrame=" + this.UseIFrame.ToString().ToLower() + ";");            
            str.Append("oJSAutoSuggestBox.ControlID='" +  this.CallBackControlID + "';");
            str.Append("oJSAutoSuggestBox.HiddenTextBoxID='" + this._hiddenTextBox.ClientID + "';");            
            str.Append("oJSAutoSuggestBox.AutoCallBack=" + this.AutoCallBack.ToString().ToLower() + ";");
            str.Append("oJSAutoSuggestBox.mnSelMenuItem=0;");
            str.Append("oJSAutoSuggestBox.BlankPageURL='" + Page.ClientScript.GetWebResourceUrl(this.GetType(),
                                                        "Anthem.includes.Blank.html") + "';");
                      

            str.Append("asbAddObj('" + this.TextBoxID + "', oJSAutoSuggestBox);");

            return str.ToString();
        }

		/// <summary>Renders AutoSuggestBox to the output HTML parameter specified.</summary>
		/// <param name="output"> The HTML writer to write out to</param>
		protected override void Render(HtmlTextWriter output)
		{
			if(!DesignMode)
				RegisterClientScript();

            if (!Manager.IsCallBack)
                WriteJSAutoSuggestBox(output);
            else
                Manager.AddScriptForClientSideEval(GetInitializationScript());
            
			string sObj="asbGetObj('" + this.TextBoxID + "')";

			//Set the javascript event handlers
            this._textBox.Attributes["onblur"] = sObj + ".OnBlur();";
            this._textBox.Attributes["onkeydown"] = sObj + ".OnKeyDown(event);";
            this._textBox.Attributes["onkeypress"] = "return " + sObj + ".OnKeyPress(event);";
            this._textBox.Attributes["onkeyup"] = sObj + ".OnKeyUp(event);";
            this._textBox.Attributes["autocomplete"] = "off";

			AnthemDefaultRenderCode(output);				
		}


		private void AnthemDefaultRenderCode(HtmlTextWriter output)
		{
			if (!DesignMode)
			{
				// parentTagName must be defined as a private const string field in this class.
				Anthem.Manager.WriteBeginControlMarker(output, "span", this);
			}
			if (Visible)
			{
				base.Render(output);

                output.WriteLine("<br><div class='" + this.MenuCSSClass + "' style='visibility:hidden;POSITION: absolute;' id='" + this.MenuDivID + "'></div>");
			}
			if (!DesignMode)
			{
				Anthem.Manager.WriteEndControlMarker(output, "span", this);
			}
		}        

        #region GetSuggestions
        public delegate void TextChangedEventHandler(object source, AutoSuggestEventArgs e);
        public event TextChangedEventHandler TextChanged;

        public delegate void SelectedValueChangedHandler(object source, EventArgs e);
        public event SelectedValueChangedHandler SelectedValueChanged;

		public delegate void ItemDataBoundHandler(object source, AutoSuggestItemEventArgs e);
		public event ItemDataBoundHandler ItemDataBound;
        
        [Anthem.Method]
        public void GetSuggestions(string text)
        {
            if (TextChanged != null)
                TextChanged(this, new AutoSuggestEventArgs(text));
        }

		/// <summary>
		/// This method is supposed to be called everytime the suggested list changes,
		/// usually when the SelectedValueChanged event is fired
		/// </summary>
		public override void DataBind()
		{
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());

			int i = 0;
			if (DataSource != null)
			{
				//Header template
				if (HeaderTemplate != null)
				{
					AutoSuggestItem item = new AutoSuggestItem(-1, "", AutoSuggestItemType.Header);
					HeaderTemplate.InstantiateIn(item);
					//Fires the ItemDataBound event
					if(ItemDataBound != null)
						ItemDataBound(this, new AutoSuggestItemEventArgs(item)); 

					if (item.Controls.Count > 0)
					{
						foreach (Control control in item.Controls)
						{
							control.RenderControl(writer);							
						}
					}
				}


				//ItemTemplate				
				IEnumerator dataEnum = DataSource.GetEnumerator();								
				while (dataEnum.MoveNext())
				{
					// Find out if its an alternating item
					AutoSuggestItemType itemType = (i + 1) % 2 == 0 ? AutoSuggestItemType.AlternatingItem : AutoSuggestItemType.Item;
					//Creates the item
					AutoSuggestItem item = new AutoSuggestItem(i, dataEnum.Current, itemType);
					if(this.ItemTemplate != null)
						this.ItemTemplate.InstantiateIn(item);

					HtmlTextWriter w = new HtmlTextWriter(new StringWriter());
					AutoSuggestMenuItem asbItem = new AutoSuggestMenuItem();
					asbItem.CSSClass = this.MenuItemCSSClass;

					
					item.DataBind();

					//Fires the ItemDataBound event before rendering the item
					if (ItemDataBound != null)
						ItemDataBound(this, new AutoSuggestItemEventArgs(item)); 

					item.RenderControl(w);

					asbItem.Text = w.InnerWriter.ToString();
					if (DataKeyField != "")
						asbItem.Value = GetField(item, this.DataKeyField);

					asbItem.TextBoxText = GetField(item, this.TextBoxDisplayField);

					writer.WriteLine(asbItem.GenHtml(i + 1, this.TextBoxID));
					this.Controls.Add(item);
					i++;
				}				
			}


			string html = "";
			if (i > 0)
			{
				html = string.Format("<table style=\"width:{0}\"><tr><td>{1}</td></tr></table>",
					this.Width.ToString(),
					writer.InnerWriter.ToString());
			}
			else
			{
				html = string.Format("<table style=\"width:{0}\"><tr><td align=\"center\" style=\"color:red;\"><b>{1}</b></td></tr></table>",
					this.Width.ToString(),
					this.ItemNotFoundMessage);
			}
			StringBuilder sb = new StringBuilder();
			Manager.WriteValue(sb, html);

			string script = string.Format("asbGetObj('{0}').ShowMenuDiv({1});", this.TextBoxID, sb.ToString());
			Manager.AddScriptForClientSideEval(script);
		}

		
        private string GetField(AutoSuggestItem item, string field)
        {
			
			if (item.DataItem is DataRowView)
				return ((DataRowView)item.DataItem)[field].ToString();
			else if (item.DataItem is DataRow)
				return ((DataRow)item.DataItem)[field].ToString();
			else if (item.DataItem is ICollection)
			{
				Type type = item.DataItem.GetType();
				MethodInfo m = type.GetMethod("get_Item");
				return m.Invoke(item.DataItem, new object[] { field }).ToString();
			}
			else
			{
				//Here we use reflection to get the value of the any field of the defined 
				//in the DataItem property of AutoSuggestItem
				Type type = item.DataItem.GetType();
				FieldInfo fieldInfo = type.GetField(field);
				if (fieldInfo != null)
					return fieldInfo.GetValue(item.DataItem).ToString();
				else
				{
					PropertyInfo prop = type.GetProperty(field);
					if (prop != null)
						return prop.GetValue(item.DataItem, null).ToString();
				}
			}
			throw new Exception("The datasource type is not supported");
        }

        [Anthem.Method]
        public void FireSelectedValueChangedEvent(string newValue)
        {
            this.SelectedValue = newValue;
            if (SelectedValueChanged != null)
                SelectedValueChanged(this, new EventArgs());
        }
        #endregion
       
    }
}
