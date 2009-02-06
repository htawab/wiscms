//------------------------------------------------------------------------------
// <copyright file="ITemplateHandler.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates
{
	public interface ITemplateHandler
	{
		/// <summary>
		/// this method will be called before any processing
		/// </summary>
		/// <param name="manager">manager doing the execution</param>
		void BeforeProcess(TemplateManager manager);

		/// <summary>
		/// this method will be called after all processing is done
		/// </summary>
		/// <param name="manager">manager doing the execution</param>
		void AfterProcess(TemplateManager manager);
	}
}
