<%@ Page Language="c#" CodePage="936" %>

<%@ Import Namespace="Msra.BackendLogic" %>
<%--<%@ Import Namespace="Msra.Data" %>--%>

<script runat="server">
    protected string AllowExtensions = "swf"; //?�������ļ��ж�ȡ
    protected Byte mediaType = 70;   //��ִ���ļ�


    /// <summary>
    /// �ж��Ƿ�Ϊ�������չ��
    /// </summary>
    /// <param name="extension">���жϵ���չ��</param>
    /// <param name="allowExtensions">�������չ���б�</param>
    protected bool IsInAllowExtensions(string extension, string allowExtensions)
    {
        if (extension == null || extension == "" || extension.Equals(string.Empty)) return false;

        extension = extension.Trim().ToLower();
        string[] arrExtensions = allowExtensions.Split('|');
        for (int index = 0; index < arrExtensions.Length; index++)
        {
            arrExtensions[index] = "." + arrExtensions[index].ToLower().Trim();

            if (arrExtensions[index] == extension)
                return true;
        }

        return false;
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        if (Request.Files.Count > 0)
        {

            System.Web.HttpPostedFile file = Request.Files[0];
            string extension = System.IO.Path.GetExtension(file.FileName).ToLower();
            if (IsInAllowExtensions(extension, AllowExtensions))
            {
                Guid fileguid = FilesHandle.UpLoad(file, mediaType, "EditorFlash", "Flash upload  by HtmlEditor");
                if (!fileguid.Equals(Guid.Empty))
                {
                    string filepath = FilesHandle.GetNonDownloadFileFullPath(fileguid);
                    ViewState["UploadFlash:Url"] = string.Format("Save('{0}', '{0}');", filepath);
                }
                else
                {
                    ViewState["UploadFlash:Url"] = " window.alert('�ϴ�ʧ��');";
                }
            }
            else
            {
                ViewState["UploadFlash:Url"] = " window.alert('�ϴ�ʧ�ܣ���ֻ���ϴ���չ��Ϊ" + this.AllowExtensions + "���ļ�');";
            }
        }
    }
		
</script>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <title></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="menu" topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
    <form runat="server" method="post" name="myform" id="myform" enctype="multipart/form-data">
    <input type="file" name="uploadflash" size="1" style="width: 100%" id="uploadflash">

    <script language="javascript">

var sAllowExt = "<%=AllowExtensions%>";

// ����ϴ���
function CheckUploadForm() {
	if (!IsExt(document.myform.uploadflash.value,sAllowExt)){
		parent.UploadError("��ʾ��\n\n��ѡ��һ����Ч���ļ���\n֧�ֵĸ�ʽ�У�"+sAllowExt+"����");
		return false;
	}
	
	return true
}

// �Ƿ���Ч����չ��
function IsExt(url, opt){
	var sTemp;
	var b = false;
	var s = opt.toUpperCase().split("|");
	for (var i=0;i<s.length ;i++ ){
		sTemp=url.substr(url.length-s[i].length-1);
		sTemp=sTemp.toUpperCase();
		s[i]="."+s[i];
		if (s[i]==sTemp){
			b=true;
			break;
		}
	}
	return b;
}

// �ύ�¼��������
var oForm = document.myform;
oForm.attachEvent("onsubmit", CheckUploadForm);

if (!oForm.submitUpload) oForm.submitUpload = new Array() ;
oForm.submitUpload[oForm.submitUpload.length] = CheckUploadForm ;
if (! oForm.originalSubmit) {
	oForm.originalSubmit = oForm.submit;
	oForm.submit = function() {
		if (this.submitUpload) {
			for (var i = 0 ; i < this.submitUpload.length ; i++) {
				this.submitUpload[i]() ;
			}
		}
		this.originalSubmit() ;
	}
}

// �ϴ�����װ�����
try {
	parent.UploadLoaded();
}
catch(e){
}

    </script>

    <script>
function Save(path)
{
	parent.UploadSaved(path);
}
<%=ViewState["UploadFlash:Url"]%>
    </script>

    </form>
</body>
</html>
