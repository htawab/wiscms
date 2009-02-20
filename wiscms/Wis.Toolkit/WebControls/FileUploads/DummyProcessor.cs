//------------------------------------------------------------------------------
// <copyright file="DummyProcessor.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// Implements the IFileProcessor interface as a dummy testing stub.
    /// The byte stream is simply discarded.
    /// </summary>
    [Serializable()]
    public class DummyProcessor : IFileProcessor
    {
        #region Declarations

        string _fileName;
        Dictionary<string, string> _headerItems;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DummyProcessor()
        {
        }

        #endregion

        #region IFileProcessor Members

        /// <summary>
        /// Starts a new file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="contentType">The content type of the file.</param>
        /// <param name="headerItems">A dictionary of items pulled from the header of the field.</param>
        /// <param name="previousFields">A dictionary of previous fields.</param>
        /// <returns>An optional object used to identify the item in the storage container.</returns>
        public object StartNewFile(string fileName, string contentType, Dictionary<string, string> headerItems, Dictionary<string, string> previousFields)
        {
            _fileName = fileName;
            _headerItems = headerItems;
            return null;
        }

        /// <summary>
        /// Writes to the output file.
        /// </summary>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="offset">Offset in the buffer to write from.</param>
        /// <param name="count">Count of bytes to write.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
        }

        /// <summary>
        /// Ends current file processing.
        /// </summary>
        public void EndFile()
        {
        }

        /// <summary>
        /// Returns the name of the file that is currently being processed.
        /// Null if there is no file.
        /// </summary>
        /// <returns>The file name.</returns>
        public string GetFileName()
        {
            return _fileName;
        }

        /// <summary>
        /// Returns the container identifier.
        /// </summary>
        /// <returns>The container identifier.</returns>
        public object GetIdentifier()
        {
            return null;
        }

        /// <summary>
        /// Gets the header items.
        /// </summary>
        public Dictionary<string, string> GetHeaderItems()
        {
            return _headerItems;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose of the object.
        /// </summary>
        void IDisposable.Dispose()
        {
        }

        #endregion
    }
}
