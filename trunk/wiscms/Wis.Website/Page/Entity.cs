//------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.Page
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
        /// <param name="pageId"></param>
        /// <param name="pageGuid"></param>
        /// <param name="categoryId"></param>
        /// <param name="tabloidPath"></param>
        /// <param name="metaKeywords"></param>
        /// <param name="metaDesc"></param>
        /// <param name="title"></param>
        public Entity(System.Int32 pageId, System.Guid pageGuid, System.Int32 categoryId, System.String metaKeywords, System.String metaDesc, System.String title)
        {
            PageId = pageId;	
            PageGuid = pageGuid;	
            CategoryId = categoryId;	
            MetaKeywords = metaKeywords;		
            MetaDesc = metaDesc;  	//     
            Title = title;//
        }


        private System.Int32 _PageId = System.Int32.MinValue;

        /// <summary>
        /// </summary>
        public System.Int32 PageId
        {
            set { _PageId = value; }
            get { return _PageId; }
        }
        private System.Guid _PageGuid =System.Guid.Empty;

        /// <summary>
        ///
        /// </summary>
        public System.Guid PageGuid
        {
            set { _PageGuid = value; }
            get { return _PageGuid; }
        }
        private System.Int32 _CategoryId =System. Int32.MinValue;

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CategoryId
        {
            set { _CategoryId = value; }
            get { return _CategoryId; }
        }

     
        private System.String _MetaKeywords = System.String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public System.String MetaKeywords
        {
            set { _MetaKeywords = value; }
            get { return _MetaKeywords; }
        }
        private System.String _MetaDesc = System.String.Empty;

        /// <summary>
        /// 
        /// </summary>
        public System.String MetaDesc
        {
            set { _MetaDesc = value; }
            get { return _MetaDesc; }
        }
        private System.String _Title = System.String.Empty;
        /// <summary>
        ///
        /// </summary>
        public System.String Title
        {
            set { _Title = value; }
            get { return _Title; }
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
       
       
        private System.String _TemplatePath = System.String.Empty;
        /// <summary>
        ///
        /// </summary>
        public System.String TemplatePath
        {
            set { _TemplatePath = value; }
            get { return _TemplatePath; }
        }
        private System.String _ReleasePath = System.String.Empty;
        /// <summary>
        ///
        /// </summary>
        public System.String ReleasePath
        {
            set { _ReleasePath = value; }
            get { return _ReleasePath; }
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