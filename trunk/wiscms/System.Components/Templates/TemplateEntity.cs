//------------------------------------------------------------------------------
// <copyright file="TemplateEntity.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.IO;
using Wis.Toolkit.Templates.Parser;
using Wis.Toolkit.Templates.Parser.Entity;

namespace Wis.Toolkit.Templates
{
	public class TemplateEntity
	{
		string name;
		ElementCollection elements;
		TemplateEntity parent;

		Hashtable templates;		// string -> TemplateEntity

		public TemplateEntity(string name, ElementCollection elements)
		{
			this.name = name;
			this.elements = elements;
			this.parent = null;

			InitTemplates();
		}

		public TemplateEntity(string name, ElementCollection elements, TemplateEntity parent)
		{
			this.name = name;
			this.elements = elements;
			this.parent = parent;

			InitTemplates();
		}

		/// <summary>
		/// load template from file
		/// </summary>
		/// <param name="name">name of template</param>
		/// <param name="filename">file from which to load template</param>
		/// <returns></returns>
		public static TemplateEntity FromFile(string name, string filename)
		{
			using (System.IO.StreamReader reader = new System.IO.StreamReader(filename))
			{
				string data = reader.ReadToEnd();
				return TemplateEntity.FromString(name, data);
			}
		}


		/// <summary>
		/// load template from file
		/// </summary>
		/// <param name="name">name of template</param>
		/// <param name="filename">file from which to load template</param>
		/// <returns></returns>
		public static TemplateEntity FromFile(string name, string filename, System.Text.Encoding encoding )
		{
			using (StreamReader reader = new StreamReader(filename, encoding))
			{
				string data = reader.ReadToEnd();
				return TemplateEntity.FromString(name, data);
			}
		}

		/// <summary>
		/// load template from string
		/// </summary>
		/// <param name="name">name of template</param>
		/// <param name="data">string containg code for template</param>
		/// <returns></returns>
		public static TemplateEntity FromString(string name, string data)
		{
			TemplateLexer lexer = new TemplateLexer(data);
			TemplateParser parser = new TemplateParser(lexer);
			ElementCollection elems = parser.Parse();

			TagParser tagParser = new TagParser(elems);
			elems = tagParser.CreateHierarchy();

			return new TemplateEntity(name, elems);
		}

		/// <summary>
		/// go thru all tags and see if they are template tags and add
		/// them to this.templates collection
		/// </summary>
		private void InitTemplates()
		{
			this.templates = new Hashtable();
            
			foreach (Element elem in elements)
			{
				if (elem is Tag)
				{
					Tag tag = (Tag)elem;
					if (string.Compare(tag.Name, "template", true) == 0)
					{
						Expression ename = tag.AttributeValue("name");
						string tname;
						if (ename is StringLiteral)
							tname = ((StringLiteral)ename).Content;
						else
							tname = "?";

						TemplateEntity templateEntity = new TemplateEntity(tname, tag.InnerElements, this);
						templates[tname] = templateEntity;
					}
				}
			}
		}

		/// <summary>
		/// gets a list of elements for this template
		/// </summary>
		public ElementCollection Elements
		{
			get { return this.elements; }
		}

		/// <summary>
		/// gets the name of this template
		/// </summary>
		public string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// returns true if this template has parent template
		/// </summary>
		public bool HasParent
		{
			get { return parent != null; }
		}

		/// <summary>
		/// gets parent template of this template
		/// </summary>
		/// <value></value>
		public TemplateEntity Parent
		{
			get { return this.parent; }
		}

		/// <summary>
		/// finds template matching name. If this template does not
		/// contain template called name, and parent != null then
		/// FindTemplate is called on the parent
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual TemplateEntity FindTemplate(string name)
		{
			if (templates.Contains(name))
				return (TemplateEntity)templates[name];
			else if (parent != null)
				return parent.FindTemplate(name);
			else
				return null;
		}

		/// <summary>
		/// gets dictionary of templates defined in this template
		/// </summary>
		public Hashtable Templates
		{
			get { return this.templates; }
		}
    }
}