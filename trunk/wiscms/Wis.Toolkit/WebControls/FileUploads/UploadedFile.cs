//------------------------------------------------------------------------------
// <copyright file="UploadedFile.cs" company="Everwis">
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
    /// Contains identifying information about an uploaded file.
    /// </summary>
    public class UploadedFile
    {
        #region Declarations

        string _fileName;
        object _identifier;
        Dictionary<string, string> _headerItems;
        Exception _exception;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }

        /// <summary>
        /// Gets the container identifier returned from the processor.
        /// </summary>
        public object Identifier
        {
            get { return _identifier; }
        }

        /// <summary>
        /// Gets a dictionary of all items in the header.
        /// </summary>
        public Dictionary<string, string> HeaderItems
        {
            get { return _headerItems; }
        }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception
        {
            get { return _exception; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="headerItems">The header items.</param>
        public UploadedFile(string fileName, object identifier, Dictionary<string, string> headerItems)
        {
            _fileName = fileName;
            _identifier = identifier;
            _headerItems = headerItems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadedFile"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="identifier">The identifier.</param>
        /// <param name="headerItems">The header items.</param>
        /// <param name="ex">The exception that was raised.</param>
        public UploadedFile(string fileName, object identifier, Dictionary<string, string> headerItems, Exception ex) : this(fileName, identifier, headerItems)
        {
            _exception = ex;
        }

        #endregion
    }
}
