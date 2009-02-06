//------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class Tag : Element
	{
		string name;
		TagAttributeCollection attribs;
		ElementCollection innerElements;
		TagClose closeTag;
		bool isClosed;	// set to true if tag ends with />

		public Tag(int line, int col, string name)
			:base(line, col)
		{
			this.name = name;
			this.attribs = new TagAttributeCollection();
			this.innerElements = new ElementCollection();
		}

		public TagAttributeCollection Attributes
		{
			get { return this.attribs; }
		}

		public Expression AttributeValue(string name)
		{
			foreach (TagAttribute attrib in attribs)
				if (string.Compare(attrib.Name, name, true) == 0)
					return attrib.Expression;

			return null;
		}

		public ElementCollection InnerElements
		{
			get { return this.innerElements; }
		}

		public string Name
		{
			get { return this.name; }
		}

		public TagClose CloseTag
		{
			get { return this.closeTag; }
			set { this.closeTag = value; }
		}

		public bool IsClosed
		{
			get { return this.isClosed; }
			set { this.isClosed = value; }
		}


	}
}
