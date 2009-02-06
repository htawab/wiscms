//------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.Tag
{/// <summary>
    /// 
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// ≥ı ºªØ°£
        /// </summary>
        public Entity()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="tagGuid"></param>
        /// <param name="objectGuid"></param>
        /// <param name="tabloidPath"></param>
        /// <param name="tagName"></param>
        /// <param name="description"></param>
        /// <param name="title"></param>
        public Entity(System.Int32 tagId, System.Guid tagGuid, System.Guid objectGuid, System.String tagName, System.String description)
        {
            TagId = tagId;	
            TagGuid = tagGuid;	
            ObjectGuid = objectGuid;	
            TagName = tagName;		
            Description = description;  	//     
        }


        private System.Int32 _TagId = System.Int32.MinValue;

        /// <summary>
        /// </summary>
        public System.Int32 TagId
        {
            set { _TagId = value; }
            get { return _TagId; }
        }
        private System.Guid _TagGuid =System.Guid.Empty;

        /// <summary>
        ///
        /// </summary>
        public System.Guid TagGuid
        {
            set { _TagGuid = value; }
            get { return _TagGuid; }
        }
        private System.Guid _ObjectGuid = System.Guid.Empty;

        /// <summary>
        /// 
        /// </summary>
        public System.Guid ObjectGuid
        {
            set { _ObjectGuid = value; }
            get { return _ObjectGuid; }
        }

     
        private System.String _TagName = System.String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public System.String TagName
        {
            set { _TagName = value; }
            get { return _TagName; }
        }
        private System.String _Description = System.String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public System.String Description
        {
            set { _Description = value; }
            get { return _Description; }
        }
        
        private System.String _ContentHtml = System.String.Empty;
        /// <summary>
        ///
        /// </summary>
        public System.String ContentHtml
        {
            set { _ContentHtml = value; }
            get { return _ContentHtml; }
        }
       
        private System.DateTime _DateCreated = System.DateTime.MinValue;
        /// <summary>
        ///
        /// </summary>
        public System.DateTime DateCreated
        {
            set { _DateCreated = value; }
            get { return _DateCreated; }
        }
        
    }
}