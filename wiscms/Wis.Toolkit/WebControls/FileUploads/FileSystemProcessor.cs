//------------------------------------------------------------------------------
// <copyright file="FileSystemProcessor.cs" company="Everwis">
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
    /// Implements the IFileProcessor interface to stream uploaded files to
    /// a directory in the file system.
    /// </summary>
    [Serializable()]
    public class FileSystemProcessor : IFileProcessor
    {
        #region Declarations

        [NonSerialized()]
        FileStream _fs;

        string _outputPath;

        [NonSerialized()]
        string _fileName;

        [NonSerialized()]
        string _fullFileName = String.Empty;

        [NonSerialized()]
        bool _errorState;

        [NonSerialized()]
        Dictionary<string, string> _headerItems;

        #endregion

        #region Properties

        /// <summary>
        /// ���Ŀ¼.
        /// </summary>
        public string OutputPath
        {
            get { return _outputPath; }
            set
            {
                value = value.Trim();
                if (!value.EndsWith("/")) value += "/";
                _outputPath = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FileSystemProcessor()
        {
            // Default to the root of the web application
            _outputPath = System.Web.HttpContext.Current.Server.MapPath("~/");
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
            _errorState = false;
            _headerItems = headerItems;

            try
            {
                _fileName = fileName;
                string outputPath = System.Web.HttpContext.Current.Server.MapPath(_outputPath);
                if (System.IO.Directory.Exists(outputPath)) System.IO.Directory.CreateDirectory(outputPath);
                _fullFileName = outputPath + Path.GetFileName(fileName);
                _fs = new FileStream(_fullFileName, FileMode.Create);
            }
            catch (Exception ex)
            {
                _errorState = true;
                throw ex;
            }

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
            if (_errorState) return;

            try
            {
                _fs.Write(buffer, offset, count);
            }
            catch (Exception ex)
            {
                _errorState = true;
                throw ex;
            }
        }

        /// <summary>
        /// Ends current file processing.
        /// </summary>
        public void EndFile()
        {
            if (_errorState) return;

            if (_fs != null)
            {
                _fs.Flush();
                _fs.Close();
                _fs.Dispose();
            }
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
            if (_fs != null)
            {
                _fs.Dispose();
            }
        }

        #endregion
    }
}
