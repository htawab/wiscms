//------------------------------------------------------------------------------
// <copyright file="VariableScope.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates
{
	public class VariableScope
	{
		VariableScope parent;
		Hashtable values;

		public VariableScope()
			:this(null)
		{
		}

		public VariableScope(VariableScope parent)
		{
			this.parent = parent;
			this.values = new Hashtable();
		}

		/// <summary>
		/// clear all variables from this scope
		/// </summary>
		public void Clear()
		{
			values.Clear();
		}

		/// <summary>
		/// gets the parent scope for this scope
		/// </summary>
		public VariableScope Parent
		{
			get { return parent; }
		}

		/// <summary>
		/// returns true if variable variableName is defined
		/// otherwise returns parents isDefined if parent != null
		/// </summary>
		public bool IsDefined(string variableName)
		{
			if (values.Contains(variableName))
				return true;
			else if (parent != null)
				return parent.IsDefined(variableName);
			else
				return false;
		}

		/// <summary>
		/// returns value of variable variableName
		/// If variableName is not in this scope and parent != null
		/// parents this[variableName] is called
		/// </summary>
		public object this[string variableName]
		{
			get {
				if (!values.Contains(variableName))
				{
					if (parent != null)
						return parent[variableName];
					else
						return null;
				}
				else
					return values[variableName];
			}
			set { values[variableName] = value; }
		}
	}
}
