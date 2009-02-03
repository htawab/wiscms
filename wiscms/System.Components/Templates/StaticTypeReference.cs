//------------------------------------------------------------------------------
// <copyright file="StaticTypeReference.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Wis.Toolkit.Templates
{
	/// <summary>
	/// StaticTypeReference is used by TemplateManager to hold references to types.
	/// When invoking methods, or accessing properties of this object, it will actually
	/// do static methods of the type
	/// </summary>
	public class StaticTypeReference
	{
		readonly Type type;

		public StaticTypeReference(Type type)
		{
			this.type = type;
		}

		public Type Type
		{
			get { return type; }
		}
	}
}
