using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * �������飺
 * 1������StartText��EndText�ı�֧��
 * 2���������ɿ���ÿ���ؼ��Ŀ�Ⱥ���ʽ
 * 3����ǿУ���֧�֣�У��Ĵ����ı�ͳһ���
 * 4�����Ӵ�����ʾ��ͼƬ����ǿ����ʧ��ĵ�����Ч��
 * 5����Ҫ������ֵ��Χ�ؼ���ʱ�䷶Χ�ؼ��ȣ�������ֵ��ϸ��Ϊfloat��Decemal��Double�ؼ�
 * 
 * */
namespace Wis.Toolkit.WebControls
{
	/// <summary>
	/// BoundIntBox ��ժҪ˵����
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
			lbl1.Text = "��ʼ";

			ib2 = new IntBox();
			ib2.Width = Unit.Pixel(90);
			lbl2 = new Label();
			lbl2.Text = "����";

			cv = new CompareValidator();
			this.ValidatorColor = System.Drawing.Color.Red;
		}

		protected override void CreateChildControls()
		{
			lbl1.ID = string.Format("lbl1{0}",this.UniqueID);//��ʼ
			this.Controls.Add(lbl1);
			ib1.ID = string.Format("ib1{0}",this.UniqueID);
			this.Controls.Add(ib1);

			this.Controls.Add(new LiteralControl(" "));
			
			lbl2.ID = string.Format("lbl2{0}",this.UniqueID);//����
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
