//------------------------------------------------------------------------------
// <copyright file="Util.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using System.Threading;

namespace Wis.Toolkit.Templates
{
	public class Util
	{
		static object syncObject = new object();

		static Regex regExVarName;

		public static bool ToBool(object obj)
		{
			if (obj is bool)
				return (bool)obj;
			else if (obj is string)
			{
				string str = (string)obj;
				if (string.Compare(str, "true", true) == 0)
					return true;
				else if (string.Compare(str, "yes", true) == 0)
					return true;
				else
					return false;
			}
			else
				return false;
		}

		public static bool IsValidVariableName(string name)
		{
			return RegExVarName.IsMatch(name);
		}

		private static Regex RegExVarName
		{
			get
			{
				if ((regExVarName == null))
				{
					Monitor.Enter(syncObject);
					if (regExVarName == null)
					{
						try
						{
							regExVarName = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
						}
						finally
						{
							Monitor.Exit(syncObject);
						}
					}
				}

				return regExVarName;
			}
		}
	}
}
