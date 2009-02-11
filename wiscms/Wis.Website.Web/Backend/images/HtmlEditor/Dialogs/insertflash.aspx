<%@ Page Language="c#" CodePage="936" %>

<%@ Import Namespace="Msra.BackendLogic" %>
<%--<%@ Import Namespace="Msra.Data" %>--%>

<script runat="server">
    protected string AllowExtensions = "swf"; //?从配置文件中读取
    protected Byte mediaType = 70;   //可执行文件


    /// <summary>
    /// 判断是否为允许的扩展名
    /// </summary>
    /// <param name="extension">待判断的扩展名</param>
    /// <param name="allowExtensions">允许的扩展名列表</param>
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
                    ViewState["UploadFlash:Url"] = " window.alert('上传失败');";
                }
            }
            else
            {
                ViewState["UploadFlash:Url"] = " window.alert('上传失败，您只能上传扩展名为" + this.AllowExtensions + "的文件');";
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

// 检测上传表单
function CheckUploadForm() {
	if (!IsExt(document.myform.uploadflash.value,sAllowExt)){
		parent.UploadError("提示：\n\n请选择一个有效的文件，\n支持的格式有（"+sAllowExt+"）！");
		return false;
	}
	
	return true
}

// 是否有效的扩展名
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

// 提交事件加入检测表单
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

// 上传表单已装入完成
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
