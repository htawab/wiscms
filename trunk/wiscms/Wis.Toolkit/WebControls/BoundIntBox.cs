using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * 改良建议：
 * 1、增加StartText和EndText文本支持
 * 2、可以自由控制每个控件的宽度和样式
 * 3、增强校验的支持，校验的错误文本统一输出
 * 4、增加错误提示的图片，增强操作失误的的体验效果
 * 5、需要制作数值范围控件、时间范围控件等，其中数值可细分为float，Decemal和Double控件
 * 
 * */
namespace Wis.Toolkit.WebControls
{
	/// <summary>
	/// BoundIntBox 的摘要说明。
	/// </summary>
	[ToolboxData("<{0}:BoundIntBox runat=server></{0}:BoundIntBox>")]
	public class BoundIntBox: WebControl, INamingContainer
	{
		private Label lbl1;
		private IntBox ib1;
		private Label lbl2;
		private IntBox ib2;
		private CompareValidator cv;

		public BoundIntBox()
		{
			ib1 = new IntBox();
			ib1.Width = Unit.Pixel(90);
			lbl1 = new Label();
			lbl1.Text = "开始";

			ib2 = new IntBox();
			ib2.Width = Unit.Pixel(90);
			lbl2 = new Label();
			lbl2.Text = "结束";

			cv = new CompareValidator();
			this.ValidatorColor = System.Drawing.Color.Red;
		}

		protected override void CreateChildControls()
		{
			lbl1.ID = string.Format("lbl1{0}",this.UniqueID);//开始
			this.Controls.Add(lbl1);
			ib1.ID = string.Format("ib1{0}",this.UniqueID);
			this.Controls.Add(ib1);

			this.Controls.Add(new LiteralControl(" "));
			
			lbl2.ID = string.Format("lbl2{0}",this.UniqueID);//结束
			this.Controls.Add(lbl2);
			ib2.ID = string.Format("ib2{0}",this.UniqueID);
			this.Controls.Add(ib2);

			cv.ControlToCompare = ib1.ID;
			cv.ControlToValidate = ib2.ID;
			cv.ErrorMessage = this.ErrorMessage;
			cv.ForeColor = this.ValidatorColor;
			cv.Text = this.ValidatorText;

			if (IsErrorTextBelow)
				Controls.Add(new LiteralControl("<br>"));

			this.Controls.Add(cv);
		}

		[Category("Appearance"), DefaultValue("")]
        public string MaxValue
		{
			get
			{
				return ib2.Text;
			}
			set
			{
				ib2.Text = value;
			}
		}

		
		[Category("Appearance"), DefaultValue("")]
        public string MinValue
		{
			get
			{
				return ib1.Text;
			}
			set
			{
				ib1.Text = value;
			}
		}

		
		[Bindable(false),
		Category("Validator")]
		public string ErrorMessage
		{
			get	{return cv.ErrorMessage;}
			set	{cv.ErrorMessage = value;}
		}

		
		[Bindable(false),
		Category("Validator"),
		DefaultValue("")]
		public string ValidatorText
		{
			get	{return cv.Text;}
			set	{cv.Text = value;}
		}

		
		[Bindable(false),
		Category("Validator")]
		public System.Drawing.Color ValidatorColor
		{
			get	{return cv.ForeColor;} 
			set	{cv.ForeColor = value;}
		}

		
		[Bindable(false),
		Category("Validator"),
		DefaultValue(true)]
		public bool IsErrorTextBelow
		{
			get	{return (bool)ViewState["isErrorTextBelow"];}
			set	{ViewState["isErrorTextBelow"] = value;}
		}


	}
}
