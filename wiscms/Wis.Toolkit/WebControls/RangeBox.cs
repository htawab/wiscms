using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wis.Toolkit.WebControls
{
	/// <summary>
	/// RangeBox 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"),ToolboxData("<{0}:RangeBox runat=server></{0}:RangeBox>"),Designer("WebControls.RangeBoxDesigner, RangeBox")]
	public class RangeBox: System.Web.UI.Control, INamingContainer
	{
		private System.Web.UI.WebControls.TextBox tb;
		private System.Web.UI.WebControls.RangeValidator rangeValidator;

		public RangeBox()
		{
			tb = new TextBox();
			rangeValidator = new RangeValidator();

			//设定默认值
			IsErrorTextBelow = true;
			IsRequired = false;
			ValidatorColor = System.Drawing.Color.Red;
			Type = ValidationDataType.Currency;
			MaxValue = "1000000";
			MinValue = "0";
		}

		[Bindable(true),
		Category("Appearance"),
		DefaultValue("Text")]
		public string Text
		{
			get	{return tb.Text;}
			set	{tb.Text = value;}
		}

		[Bindable(false),
		Category("Appearance"),
		DefaultValue("")]
		public string CssClass
		{
			get	{return tb.CssClass;}
			set	{tb.CssClass = value;}
		}

		[Bindable(false),
		Category("Validator")]
		public string ErrorMessage
		{
			get	{return rangeValidator.ErrorMessage;}
			set	{rangeValidator.ErrorMessage = value;}
		}

		[Bindable(false),
		Category("Validator"),
		DefaultValue("")]
		public string ValidatorText
		{
			get	{return rangeValidator.Text;}
			set	{rangeValidator.Text = value;}
		}

		[Bindable(false),
		Category("Validator")]
		public System.Drawing.Color ValidatorColor
		{
			get	{return rangeValidator.ForeColor;} 
			set	{rangeValidator.ForeColor = value;}
		}

		[Bindable(false),
		Category("Validator"),
		DefaultValue("1000000")]
		public string MaxValue
		{
			get	{return rangeValidator.MaximumValue;}
			set	{rangeValidator.MaximumValue = value;}
		}

		[Bindable(false),
		Category("Validator"),
		DefaultValue("0")]
		public string MinValue
		{
			get	{return rangeValidator.MinimumValue;}
			set	{rangeValidator.MinimumValue = value;}
		}

		/// <summary>
		/// This is a new custom property, so it is saved into viewstate.
		/// </summary>
		[Bindable(false),
		Category("Validator"),
		DefaultValue(true)]
		public bool IsErrorTextBelow
		{
			get	{return (bool)ViewState["isErrorTextBelow"];}
			set	{ViewState["isErrorTextBelow"] = value;}
		}

		[Bindable(false),
		Category("Validator"),
		DefaultValue(false)]
		public bool IsRequired
		{
			get	{return (bool)ViewState["isRequired"];}
			set	{ViewState["isRequired"] = value;}
		}

		[Bindable(false),
		Category("Validator"),
		DefaultValue(ValidationDataType.Currency)]
		public ValidationDataType Type
		{
			get	{return rangeValidator.Type;}
			set	{rangeValidator.Type = value;}
		}

		/// <summary>
		/// This method is the most important method here.
		/// </summary>
		protected override void CreateChildControls()
		{
			//--- UniqueID is a fully-qualified identifier for the server control. It is generated 
			//--- automatically when a page request is processed
			tb.ID = UniqueID;
			tb.Text = this.Text;
			tb.CssClass = this.CssClass;

			//--- Calculating the MaxLength for the textbox. There is no need to enter six numbers
			//--- when the largest number that is allowed is 10.
			tb.MaxLength = (this.MaxValue.Length>this.MinValue.Length?this.MaxValue.Length:this.MinValue.Length);

			rangeValidator.ErrorMessage = this.ErrorMessage;
			rangeValidator.ForeColor = this.ValidatorColor;
			rangeValidator.Text = this.ValidatorText;
			rangeValidator.Font.Size = 8;
			rangeValidator.MaximumValue = this.MaxValue.ToString();
			rangeValidator.MinimumValue = this.MinValue.ToString();

			//--- Make a unique ID for the range-validator
			rangeValidator.ID = UniqueID + "Validator";
			rangeValidator.ControlToValidate = tb.ID;
			rangeValidator.Type = this.Type;
			rangeValidator.Display = ValidatorDisplay.Dynamic;

			//--- Align the text in the textbox to the right when it contains a currency
			if (rangeValidator.Type == ValidationDataType.Currency)
				tb.Style["TEXT-ALIGN"] = TextAlign.Right.ToString();

			//--- Add the textbox.
			Controls.Add(tb);

			//--- When a value is required, a requiredfieldvalidator is required
			if (IsRequired)
			{
				RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
				requiredFieldValidator.ID = UniqueID + "Required";
				requiredFieldValidator.ControlToValidate = tb.ID;
				requiredFieldValidator.ErrorMessage = "&nbsp;*";
				requiredFieldValidator.Display = ValidatorDisplay.Dynamic;
				Controls.Add(requiredFieldValidator);
			}
			
			//--- Placing a <br> will be enough
			if (IsErrorTextBelow)
				Controls.Add(new LiteralControl("<br>"));

			Controls.Add(rangeValidator);
		}
	}

	/// <summary>
	/// 提供设计器类。
	/// </summary>
	public class RangeBoxDesigner : System.Web.UI.Design.ControlDesigner 
	{
		public RangeBoxDesigner()
		{
		}

		public override string GetDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);

			TextBox tb = new TextBox();
			tb.Text = "RangeBox";
			tb.ControlStyle.BackColor = System.Drawing.Color.Red;
			tb.RenderControl(tw);

			return sw.ToString();
		}
	}

}
