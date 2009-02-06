//------------------------------------------------------------------------------
// <copyright file="ResourceHandler.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;
using System.IO;

namespace System.Components.WebControls.HtmlEditorControls
{
    public class ResourceHandler : IHttpHandler
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context == null) return;

            string filename = context.Request.QueryString["filename"];
            if (string.IsNullOrEmpty(filename)) return;

            string typeName = this.GetType().Namespace.ToString() + ".Images." + filename;
            
            // Case Insensitivity
            string[] resourceNames = this.GetType().Assembly.GetManifestResourceNames();
            if (resourceNames.Length < 1) return;

            System.Array.Sort(resourceNames, System.Collections.CaseInsensitiveComparer.Default);
            int index = System.Array.BinarySearch(resourceNames, typeName, System.Collections.CaseInsensitiveComparer.Default);
            if (index == -1) return;

            // Get the resource
            // this.GetType().Assembly.GetManifestResourceStream(resourceNames[index]) Ϊnullʱ?
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream(resourceNames[index]))
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
                {
                    HttpContext.Current.Response.Cache.SetExpires(System.DateTime.Now.AddSeconds(30));
                    HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                    HttpContext.Current.Response.ContentType = "image/gif";
                    image.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                }
            }
        }

        #endregion
    }
}
