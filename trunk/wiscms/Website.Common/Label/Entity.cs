//------------------------------------------------------------------------------
// <copyright file="Entity.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website.Label
{/// <summary>
    /// 
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 初始化。
        /// </summary>
        public Entity()
        {
        }

        
        private System.String _CommandText = System.String.Empty;

        /// <summary>
        /// 查询条件
        /// </summary>
        public System.String CommandText
        {
            set { _CommandText = value; }
            get { return _CommandText; }
        }

        private System.String _Type = System.String.Empty;
        /// <summary>
        /// 标签类型
        /// </summary>
        public System.String Type
        {
            set { _Type = value; }
            get { return _Type; }
        }
        private System.Int32 _PageSize = 20;

        /// <summary>
        ///每页显示记录数
        /// </summary>
        public System.Int32 PageSize
        {
            set { _PageSize = value; }
            get { return _PageSize; }
        }
        private System.Boolean _IsPage = false;

        /// <summary>
        /// 是否分页显示
        /// </summary>
        public System.Boolean IsPage
        {
            set { _IsPage = value; }
            get { return _IsPage; }
        }


        private System.Int32 _CurPage =1;

        /// <summary>
        /// 当前页数
        /// </summary>
        public System.Int32 CurPage
        {
            set { _CurPage = value; }
            get { return _CurPage; }
        }
        private System.Int32 _TruncateNumber = 0;

        /// <summary>
        /// 标题显示字符数
        /// </summary>
        public System.Int32 TruncateNumber
        {
            set { _TruncateNumber = value; }
            get { return _TruncateNumber; }
        }

        private System.Int32 _SummaryNumber = 0;

        /// <summary>
        /// 摘要显示字符
        /// </summary>
        public System.Int32 SummaryNumber
        {
            set { _SummaryNumber = value; }
            get { return _SummaryNumber; }
        }

        public void Load(string name,string value)
        {
            switch (name)
            { 
                case "CommandText" :
                        CommandText = value;
                    break;
                case "PageSize":
                    if (Wis.Toolkit.Validator.IsInt(value))
                        PageSize = System.Convert.ToInt32( value);
                    break;
                case "IsPage":
                    if (Wis.Toolkit.Validator.IsBoolean(value))
                        IsPage = System.Convert.ToBoolean(value);
                    break;
                case "CurPage":
                    if (Wis.Toolkit.Validator.IsInt(value))
                        CurPage = System.Convert.ToInt32(value);
                    break;
                case "TruncateNumber":
                    if (Wis.Toolkit.Validator.IsInt(value))
                        TruncateNumber = System.Convert.ToInt32(value);
                    break;
                case "SummaryNumber":
                    if (Wis.Toolkit.Validator.IsInt(value))
                        SummaryNumber = System.Convert.ToInt32(value);
                    break;
                case "Type":
                    Type = value;
                    break;

            }
        }
    }
}