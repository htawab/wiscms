//------------------------------------------------------------------------------
// <copyright file="UploadConfigurationSection.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// Configuration section for the upload module. Defines global settings for the module
    /// in web.config.
    /// </summary>
    public sealed class UploadConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadConfigurationSection"/> class.
        /// </summary>
        public UploadConfigurationSection()
        {
        }

        /// <summary>
        /// Gets or sets the HTTP status code to return when the maximum length is exceeded.
        /// </summary>
        /// <value>The allowed file extensions.</value>
        [ConfigurationProperty("lengthExceededHttpCode", DefaultValue = 400, IsRequired = false)]
        public int LengthExceededHttpCode
        {
            get { return (int)this["lengthExceededHttpCode"]; }
        }

        /// <summary>
        /// Gets or sets the allowed file extensions (a comma separated list .pdf,.zip,.gif).
        /// </summary>
        /// <value>The allowed file extensions.</value>
        [ConfigurationProperty("allowedFileExtensions", DefaultValue = "", IsRequired = false)]
        public string AllowedFileExtensions
        {
            get { return this["allowedFileExtensions"] as string; }
        }

        /// <summary>
        /// Gets or sets the path to the script file.
        /// </summary>
        /// <value>The script path.</value>
        [ConfigurationProperty("scriptPath", DefaultValue = "/upload_scripts", IsRequired = false)]
        public string ScriptPath
        {
            get { return this["scriptPath"] as string; }
        }

        /// <summary>
        /// Gets or sets the path to the css file.
        /// </summary>
        /// <value>The image path.</value>
        [ConfigurationProperty("cssPath", DefaultValue = "/upload_styles", IsRequired = false)]
        public string CSSPath
        {
            get { return this["cssPath"] as string; }
        }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>The image path.</value>
        [ConfigurationProperty("imagePath", DefaultValue = "/upload_images", IsRequired = false)]
        public string ImagePath
        {
            get { return this["imagePath"] as string; }
        }

        /// <summary>
        /// Gets or sets the progress page.
        /// </summary>
        /// <value>The URL of the progress page.</value>
        [ConfigurationProperty("progressUrl", DefaultValue = "UploadProgress.aspx", IsRequired = false)]
        public string ProgressUrl
        {
            get { return this["progressUrl"] as string; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cancel button should be shown.
        /// </summary>
        /// <value><c>true</c> if the cancel button should be shown; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("showCancelButton", DefaultValue = true, IsRequired = false)]
        public bool ShowCancelButton
        {
            get { return (bool)this["showCancelButton"]; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the progress bar should be shown.
        /// </summary>
        /// <value><c>true</c> if the progress bar should be shown; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("showProgressBar", DefaultValue = true, IsRequired = false)]
        public bool ShowProgressBar
        {
            get { return (bool)this["showProgressBar"]; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether manual processing should be allowed if the
        /// upload module is not installed.
        /// </summary>
        /// <value><c>true</c> if manual processing is allowed; otherwise, <c>false</c>.</value>
        [ConfigurationProperty("enableManualProcessing", DefaultValue = true, IsRequired = false)]
        public bool EnableManualProcessing
        {
            get { return (bool)this["enableManualProcessing"]; }
        }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <returns>The configuration section.</returns>
        public static UploadConfigurationSection GetConfig()
        {
            return ConfigurationManager.GetSection("uploadSettings") as UploadConfigurationSection;
        }
    }
}
