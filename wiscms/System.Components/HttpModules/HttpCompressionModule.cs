//------------------------------------------------------------------------------
// <copyright file="HttpCompressionModule.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;
using System.IO;
using System.IO.Compression;

namespace Wis.Toolkit.HttpModules
{
    public class HttpCompressionModule : IHttpModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpCompressionModule"/> class.
        /// </summary>
        public HttpCompressionModule()
        {
        }

        #region IHttpModule Members

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        /// </summary>
        void IHttpModule.Dispose()
        {
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += (new System.EventHandler(this.context_BeginRequest));
        }

        #endregion

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void context_BeginRequest(object sender, System.EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            string encodings = app.Request.Headers.Get("Accept-Encoding");

            if (encodings == null)
                return;

            string url = app.Request.RawUrl.ToLower();

            if (!url.StartsWith((app.Request.ApplicationPath == "/" ? app.Request.ApplicationPath : app.Request.ApplicationPath + "/")))
                return;

            Stream baseStream = app.Response.Filter;
            encodings = encodings.ToLower();

            if (encodings.Contains("gzip"))
            {
                app.Response.Filter = new GZipStream(baseStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (encodings.Contains("deflate"))
            {
                app.Response.Filter = new DeflateStream(baseStream, CompressionMode.Compress);
                app.Response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}
