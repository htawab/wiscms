//------------------------------------------------------------------------------
// <copyright file="DropdownMenuItem.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Wis.Toolkit.WebControls.DropdownMenus
{
    /// <summary>
    /// 下拉菜单项
    /// </summary>
    public class DropdownMenuItem
    {
        public DropdownMenuItem()
        {
            _SubMenuItems = new List<DropdownMenuItem>();
        }

        private string _Text;
        /// <summary>
        /// 文本。
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private string _Value;
        /// <summary>
        /// 值。
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private List<DropdownMenuItem> _SubMenuItems;
        /// <summary>
        /// 子项集合。
        /// </summary>
        public List<DropdownMenuItem> SubMenuItems
        {
            get { return _SubMenuItems; }
            set { _SubMenuItems = value; }
        }
    }
}
