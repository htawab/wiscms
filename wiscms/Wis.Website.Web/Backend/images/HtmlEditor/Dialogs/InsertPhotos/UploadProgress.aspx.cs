#region License
/*

File Upload HTTP module for ASP.Net (v 2.0)
Copyright (C) 2007-2008 Darren Johnstone (http://Wis.Toolkit.WebControls.FileUploads)

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

*/
#endregion

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Wis.Toolkit.WebControls.FileUploads;

namespace FileUploadV2
{
    /// <summary>
    /// The upload progress page. This is required to populate the IFrame or popup window
    /// for the progress bar.
    /// </summary>
    public partial class UploadProgress : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        { 
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Response.AppendHeader("Cache-Control", "no-cache");
            Response.AppendHeader("Cache-Control", "private");
            Response.AppendHeader("Cache-Control", "no-store");
            Response.AppendHeader("Cache-Control", "must-revalidate");
            Response.AppendHeader("Cache-Control", "max-stale=0");
            Response.AppendHeader("Cache-Control", "post-check=0");
            Response.AppendHeader("Cache-Control", "pre-check=0");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Keep-Alive", "timeout=3, max=993");
            Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT");
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            UploadStatus status;

            base.OnPreRender(e);

            status = UploadManager.Instance.Status;

            if (status != null)
            {
                upProgressBar.Width = new Unit(status.ProgressPercent, UnitType.Percentage);

                if (status.ProgressPercent > 0)
                {
                    lblStatus.Text = "Now uploading: " + status.CurrentFile + " " + status.ProgressPercent.ToString() + "%";
                }
                else
                {
                    lblStatus.Text = "Waiting for uploads";
                }
            }
            else
            {
                lblStatus.Text = "Waiting for uploads";
            }
        }
    }
}
