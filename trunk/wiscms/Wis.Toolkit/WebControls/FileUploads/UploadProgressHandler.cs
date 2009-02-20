//------------------------------------------------------------------------------
// <copyright file="IFileProcessor.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// An HTTP handler used to get progress information on a file upload.
    /// </summary>
    public class UploadProgressHandler : IHttpHandler
    {
        #region IHttpHandler Members

        /// <summary>
        /// Return false to indicate that the handler is not reusable.
        /// </summary>
        public bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        /// Processes a request.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        public void ProcessRequest(HttpContext context)
        {
            UploadStatus status;

            status = UploadManager.Instance.Status;

            context.Response.ContentType = "text/xml";
            if (status != null)
            {
                context.Response.Write(status.Serialize().OuterXml);
            }
            else
            {
                context.Response.Write(UploadStatus.EMPTY_STATUS);
            }
            context.Response.Flush();
        }


        #endregion
    }
}
