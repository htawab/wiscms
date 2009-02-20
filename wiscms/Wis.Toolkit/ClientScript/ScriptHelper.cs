//------------------------------------------------------------------------------
// <copyright file="ScriptHelper.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.ClientScript
{
	/// <summary>
	/// ScriptHelper 的摘要说明。
	/// </summary>
	public class ScriptHelper
	{
		/// <summary>
		/// stores the script
		/// </summary>
		/// <example>
		/// ClientScript.ScriptHelper js = new ClientScript.ScriptHelper();
		/// js.Add("alert('Hi');");
		/// js.Add("alert('This is a sample javascript');");
		/// js.End();
		/// RegisterClientScriptBlock("myScript", js.ToString());
		/// </example>
		private System.Text.StringBuilder script;

		#region Constructors
		/// <summary>
		/// Default language is JavaScript.
		/// </summary>
		public ScriptHelper()
		{
			script = new System.Text.StringBuilder();

			// default language is JavaScript
			
			script.Append("\n<script Language=\"JavaScript\">\n");
		}

		/// <summary>
		/// Creates a script of given language type
		/// </summary>
		/// <param name="language">Script langauge</param>
		public ScriptHelper(string language)
		{
			script = new System.Text.StringBuilder();
			script.Append("\n<script Language=\"" + language +"\">\n");
		}
		#endregion

		
		#region Methods
		/// <summary>
		/// Appends a statement to the script
		/// </summary>
		/// <param name="statement">valid script statement</param>
		public void Add( string statement )
		{
			script.Append("\t" + statement + "\n");
		}

		/// <summary>
		/// Ends the script by appending script end tag to it
		/// </summary>
		public void End()
		{
			script.Append("</script>");
		}

		/// <summary>
		/// can be used for string concatenations
		/// </summary>
		/// <returns>string object of the script</returns>
		public override string ToString()
		{
			return( script.ToString() ) ;
		}
		#endregion

	}
}
