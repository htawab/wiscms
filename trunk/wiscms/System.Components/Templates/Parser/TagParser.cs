//------------------------------------------------------------------------------
// <copyright file="TagParser.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Wis.Toolkit.Templates.Parser.Entity;

namespace Wis.Toolkit.Templates.Parser
{
	public class TagParser
	{
		ElementCollection elements;

		public TagParser(ElementCollection elements)
		{
			this.elements = elements;
		}

		public ElementCollection CreateHierarchy()
		{
			ElementCollection result = new ElementCollection();

			for (int index = 0; index < elements.Count; index++)
			{
				Element element = elements[index];

                if (element is Entity.Text)
					result.Add(element);
				else if (element is Expression)
					result.Add(element);
				else if (element is Tag)
				{
					result.Add(CollectForTag((Tag) element, ref index));
				}
				else if (element is TagClose)
				{
					throw new ParseException("Close tag for " + ((TagClose) element).Name + " doesn't have matching start tag.", element.Line, element.Col);
				}
				else
					throw new ParseException("Invalid element: " + element.GetType().ToString(), element.Line, element.Col);
			}

			return result;
		}

		private Tag CollectForTag(Tag tag, ref int index)
		{
			if (tag.IsClosed) // if self-closing tag, do not collect inner elements
			{
				return tag;
			}

			if (string.Compare(tag.Name, "if", true) == 0)
			{
				tag = new TagIf(tag.Line, tag.Col, tag.AttributeValue("test"));
			}

			Tag collectTag = tag;

			for (index++; index < elements.Count; index++)
			{
				Element elem = elements[index];

                if (elem is Entity.Text)
					collectTag.InnerElements.Add(elem);
				else if (elem is Expression)
					collectTag.InnerElements.Add(elem);
				else if (elem is Tag)
				{
					Tag innerTag = (Tag) elem;
					if (string.Compare(innerTag.Name, "else", true) == 0)
					{
						if (collectTag is TagIf)
						{
							((TagIf) collectTag).FalseBranch = innerTag;
							collectTag = innerTag;
						}
						else
							throw new ParseException("else tag has to be positioned inside of if or elseif tag", innerTag.Line, innerTag.Col);

					}
					else if (string.Compare(innerTag.Name, "elseif", true) == 0)
					{
						if (collectTag is TagIf)
						{
							Tag newTag = new TagIf(innerTag.Line, innerTag.Col, innerTag.AttributeValue("test"));
							((TagIf) collectTag).FalseBranch = newTag;
							collectTag = newTag;
						}
						else
							throw new ParseException("elseif tag is not positioned properly", innerTag.Line, innerTag.Col);
					}
					else
						collectTag.InnerElements.Add(CollectForTag(innerTag, ref index));
				}
				else if (elem is TagClose)
				{
					TagClose tagClose = (TagClose) elem;
					if (string.Compare(tag.Name, tagClose.Name, true) == 0)
						return tag;

					throw new ParseException("闭合标签" + tagClose.Name + "找不到开始标签，错误标签位于：行" + elem.Line.ToString() + "列" + elem.Col.ToString(), elem.Line, elem.Col);
				}
				else
					throw new ParseException("Invalid element: " + elem.GetType().ToString(), elem.Line, elem.Col);

			}

			throw new ParseException("开始标签" + tag.Name + "找不到闭合标签，错误标签位于：行" + tag.Line.ToString() + "列" + tag.Col.ToString(), tag.Line, tag.Col);

		}

	}
}