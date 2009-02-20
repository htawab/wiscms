//------------------------------------------------------------------------------
// <copyright file="SQLFileDownloadHandler.cs" company="Everwis">
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
    /// An HTTP handler which allows files to be downloaded from a SQL database.
    /// </summary>
    public class SQLFileDownloadHandler : IHttpHandler
    {
        SQLProcessor _processor;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLFileDownloadHandler"/> class.
        /// </summary>
        public SQLFileDownloadHandler()
        {
            _processor = UploadManager.Instance.GetProcessor() as SQLProcessor;

            if (_processor == null)
            {
                throw new Exception("The default processor must be of type SQLProcessor for downloads.");
            }
        }

        #endregion

        #region IHttpHandler Members

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        public void ProcessRequest(HttpContext context)
        {
            int id;
            string contentType;
            string fileName;

            if (int.TryParse(context.Request["id"], out id))
            {
                if (_processor.GetFileDetails(id, out fileName, out contentType))
                {
                    context.Response.ContentType = contentType;

                    if (context.Request["attach"] == "yes")
                    {
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
                    }
                    context.Response.Flush();
                    _processor.SaveFileToStream(context.Response.OutputStream, id, UploadManager.Instance.BufferSize);
                    context.Response.Flush();
                }
            }
        }

        #endregion
    }
}
