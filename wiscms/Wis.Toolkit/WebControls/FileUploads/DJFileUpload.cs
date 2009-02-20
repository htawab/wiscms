//------------------------------------------------------------------------------
// <copyright file="DJFileUpload.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// Multiple file upload control with progress bar.
    /// </summary>
    public class DJFileUpload : WebControl, INamingContainer
    {
        // http://Wis.Toolkit.WebControls.FileUploads/category/aspnet-file-uploads/ 2.03
        #region Declarations

        DJUploadController _controller;
        int DEFAULT_INITIAL = 1;
        int DEFAULT_MAXIMUM = 5;

        int _initialFileUploads;
        int _maxFileUploads;
        bool _showUploadButton = true;
        bool _showAddButton = true;
        bool _requiredField;
        bool _applyStyles = false;
        string _requiredMessage = "至少选择一个文件";
        string _invalidExtensionMessage = "文件类型只允许：";// + AllowedFileExtensions;
        string _AllowedFileExtensions = string.Empty;
        IFileProcessor _processor;

        internal static string UPLOAD_PARAMETER_TAG = "::DJ_UPLOAD_PARAMETER::";
        internal static string UPLOAD_END_TAG = "::DJ_UPLOAD_END::";

        #endregion

        /// <summary>
        /// Gets/sets a custom file processor which will override the global settings
        /// in UploadManager.
        /// </summary>
        public IFileProcessor FileProcessor
        {
            get { return _processor; }
            set
            {
                _processor = value as IFileProcessor;

                if (_processor == null)
                {
                    throw new ArgumentException("未实现接口 IFileProcessor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the allowed file extensions (a comma separated list .pdf,.zip,.gif).
        /// </summary>
        /// <value>The allowed file extensions.</value>
        public string AllowedFileExtensions
        {
            get { return _AllowedFileExtensions; }
            set { _AllowedFileExtensions = value; }
        }

        private string _ReferencePath = "/";
        /// <summary>
        /// 引用路径
        /// </summary>
        public string ReferencePath
        {
            get { return _ReferencePath; }
            set { _ReferencePath = value; }
        }


        /// <summary>
        /// Gets/sets a boolean value indicating if at least one upload file is required for this control.
        /// </summary>
        public bool RequiredField
        {
            get { return _requiredField; }
            set { _requiredField = value; }
        }

        /// <summary>
        /// Gets/sets a boolean value indicating if styles should be applied to this control.
        /// </summary>
        public bool ApplyStyles
        {
            get { return _applyStyles; }
            set { _applyStyles = value; }
        }

        /// <summary>
        /// Gets/sets the required field validation message.
        /// </summary>
        public string RequiredMessage
        {
            get { return _requiredMessage; }
            set { _requiredMessage = value; }
        }

        /// <summary>
        /// Gets/sets the message to display when a file with an invalid extension is selected.
        /// </summary>
        public string InvalidExtensionMessage
        {
            get { return _invalidExtensionMessage; }
            set { _invalidExtensionMessage = value; }
        }

        /// <summary>
        /// Gets or sets the initial number of file uploads.
        /// </summary>
        /// <value>Gets or sets the initial number of file uploads.</value>
        public int InitialFileUploads
        {
            get { return _initialFileUploads; }
            set { _initialFileUploads = value; }
        }

        /// <summary>
        /// Gets or sets the maximum number of file uploads.
        /// </summary>
        /// <value>Gets or sets the maximum number of file uploads.</value>
        public int MaxFileUploads
        {
            get { return _maxFileUploads; }
            set { _maxFileUploads = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the upload button should be shown.
        /// </summary>
        /// <value><c>true</c> if the upload button should be shown; otherwise, <c>false</c>.</value>
        public bool ShowUploadButton
        {
            get { return _showUploadButton; }
            set { _showUploadButton = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the add button should be shown.
        /// </summary>
        /// <value><c>true</c> if the add button should be shown; otherwise, <c>false</c>.</value>
        public bool ShowAddButton
        {
            get { return _showAddButton; }
            set { _showAddButton = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DJFileUpload"/> class.
        /// </summary>
        public DJFileUpload()
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnableViewState = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (InitialFileUploads <= 0)
            {
                InitialFileUploads = DEFAULT_INITIAL;
            }

            if (MaxFileUploads <= 0)
            {
                MaxFileUploads = DEFAULT_MAXIMUM;
            }
        }

        List<System.Web.UI.WebControls.FileUpload> _uploadControls;
        HiddenField _parameters;
        HiddenField _endMarker;

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            _controller = DJUploadController.GetController(Page);
            _uploadControls = new List<System.Web.UI.WebControls.FileUpload>();

            // Create the parameter field
            _parameters = new HiddenField();
            Controls.Add(_parameters);

            // Create the container
            Panel outerContainer = new Panel();
            outerContainer.CssClass = "upUploadBox";
            Controls.Add(outerContainer);

            // Create the file uploads
            for (int i = 0; i < MaxFileUploads; i++)
            {
                Panel fuContainer = new Panel();

                if (ApplyStyles)
                {
                    fuContainer.CssClass = "upFileInputs";
                }
                else
                {
                    fuContainer.CssClass = "upContainerNormal";
                }

                outerContainer.Controls.Add(fuContainer);

                System.Web.UI.WebControls.FileUpload fu = new System.Web.UI.WebControls.FileUpload();
                fu.CssClass = "upFileNormal";
                _uploadControls.Add(fu);
                fuContainer.Controls.Add(fu);

                if (ApplyStyles)
                {
                    ImageButton btnRemove = new ImageButton();
                    fuContainer.Controls.Add(btnRemove);
                    btnRemove.AlternateText = "移除";
                    btnRemove.ImageUrl = _controller.ImagePath + "removebutton.gif";
                    btnRemove.OnClientClick = "up_RemoveUpload(this); return false;";
                    btnRemove.CssClass = "upRemoveButton upShowDynamic";
                }
                else
                {
                    Button btnRemove = new Button();
                    fuContainer.Controls.Add(btnRemove);
                    btnRemove.OnClientClick = "up_RemoveUpload(this); return false;";
                    btnRemove.Text = "移除";
                    btnRemove.CssClass = "upButtonNormal upShowDynamic";
                }

                if (i >= InitialFileUploads)
                {
                    fuContainer.CssClass += " upHiddenDynamic";
                }
            }

            Panel buttonContainer = new Panel();
            buttonContainer.CssClass = "upButtons";

            // Create the buttons
            if (ApplyStyles)
            {
                ImageButton btnGo = new ImageButton();
                buttonContainer.Controls.Add(btnGo);
                btnGo.AlternateText = "Upload now";
                btnGo.ImageUrl = _controller.ImagePath + "uploadbutton.gif";
                btnGo.Visible = ShowUploadButton;
                btnGo.CausesValidation = true;
                btnGo.CssClass = "upGoButton";
            }
            else
            {
                Button btnGo = new Button();
                buttonContainer.Controls.Add(btnGo);
                btnGo.Text = "上传";
                btnGo.Visible = ShowUploadButton;
                btnGo.CausesValidation = true;
                btnGo.CssClass = "upButtonNormal";
                btnGo.Attributes.Add("ajaxcall", "none");
            }

            if (ApplyStyles)
            {
                ImageButton btnAdd = new ImageButton();
                buttonContainer.Controls.Add(btnAdd);
                btnAdd.AlternateText = "添加";
                btnAdd.ImageUrl = _controller.ImagePath + "addbutton.gif";
                btnAdd.OnClientClick = "up_AddUpload('" + ClientID + "'); return false;";
                btnAdd.Visible = ShowAddButton;
                btnAdd.CssClass = "upAddButton upShowDynamic";
            }
            else
            {
                Button btnAdd = new Button();
                buttonContainer.Controls.Add(btnAdd);
                btnAdd.Text = "添加";
                btnAdd.OnClientClick = "up_AddUpload('" + ClientID + "'); return false;";
                btnAdd.Visible = ShowAddButton;
                btnAdd.CssClass = "upButtonNormal upShowDynamic";
            }

            outerContainer.Controls.Add(buttonContainer);

            CustomValidator val = new CustomValidator();
            val.ServerValidate += new ServerValidateEventHandler(val_ServerValidate);
            val.ClientValidationFunction = "up_ValidateUpload";
            val.ErrorMessage = RequiredMessage;
            val.Enabled = val.EnableClientScript = RequiredField;
            val.Display = ValidatorDisplay.Dynamic;
            Controls.Add(val);

            HiddenField extensions = new HiddenField();
            extensions.Value = AllowedFileExtensions;
            Controls.Add(extensions);

            CustomValidator valExtensions = new CustomValidator();
            valExtensions.ClientValidationFunction = "up_ValidateUploadExtensions";
            valExtensions.ErrorMessage = InvalidExtensionMessage;
            valExtensions.EnableClientScript = valExtensions.EnableClientScript = (AllowedFileExtensions != null);
            valExtensions.Display = ValidatorDisplay.Dynamic;
            Controls.Add(valExtensions);

            // Create the end marker
            _endMarker = new HiddenField();
            Controls.Add(_endMarker);
        }

        /// <summary>
        /// Handles the ServerValidate event of the val control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        void val_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool ok = false;

            EnsureChildControls();
            foreach (System.Web.UI.WebControls.FileUpload fu in _uploadControls)
            {
                if (fu.FileName != String.Empty)
                {
                    ok = true;
                    break;
                }
            }

            args.IsValid = ok;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (_processor != null && UploadManager.Instance.ModuleInstalled)
            {
                _parameters.Value = UPLOAD_PARAMETER_TAG + UploadManager.Instance.SerializeProcessor(_processor);
                _endMarker.Value = UPLOAD_END_TAG;
            }
        }
    }
}
