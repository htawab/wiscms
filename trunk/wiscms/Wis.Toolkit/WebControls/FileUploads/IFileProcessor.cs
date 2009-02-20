//------------------------------------------------------------------------------
// <copyright file="IFileProcessor.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// The IFileProcessor interface defines classes which are used to
    /// process an individual file coming from a form stream.
    /// 
    /// The interface defines methods to start the file processing (with a file
    /// name and content type), write data, and end the upload process.
    /// 
    /// IFileProcessor implementations are used to write uploaded data to
    /// persistant storage such as the file system or a database.
    /// </summary>
    public interface IFileProcessor : IDisposable
    {
        /// <summary>
        /// Starts a new file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="contentType">The content type of the file.</param>
        /// <param name="headerItems">A dictionary of items pulled from the header of the field.</param>
        /// <param name="previousFields">A dictionary of previous fields.</param>
        /// <returns>An optional object used to identify the item in the storage container.</returns>
        object StartNewFile(string fileName, string contentType, Dictionary<string, string> headerItems, Dictionary<string, string> previousFields);

        /// <summary>
        /// Writes to the output file.
        /// </summary>
        /// <param name="buffer">Buffer to write from.</param>
        /// <param name="offset">Offset in the buffer to write from.</param>
        /// <param name="count">Count of bytes to write.</param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Ends current file processing.
        /// </summary>
        void EndFile();

        /// <summary>
        /// Returns the name of the file that is currently being processed.
        /// Null if there is no file.
        /// </summary>
        /// <returns>The file name.</returns>
        string GetFileName();

        /// <summary>
        /// Gets the identifier in the container.
        /// </summary>
        /// <returns>The container identifier.</returns>
        object GetIdentifier();

        /// <summary>
        /// Gets the header items.
        /// </summary>
        Dictionary<string, string> GetHeaderItems();
    }
}
