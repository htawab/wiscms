//------------------------------------------------------------------------------
// <copyright file="SettingsStorageObject.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Toolkit.WebControls.FileUploads
{
    /// <summary>
    /// Stores settings for a processor object.
    /// </summary>
    [Serializable()]
    internal class SettingsStorageObject
    {
        /// <summary>
        /// The encrypted settings cipher text.
        /// </summary>
        public byte[] CipherText;

        /// <summary>
        /// The validation hash.
        /// </summary>
        public byte[] Hash;

        /// <summary>
        /// The cipher initialistion vector.
        /// </summary>
        public byte[] CipherIV;
    }
}
