//------------------------------------------------------------------------------
// <copyright file="Validator.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace Wis.Toolkit.Templates.Functions
{
	/// <summary>
	/// Validator 的摘要说明。
	/// </summary>
	public sealed class Validator
	{
		// TODO:如何获取当前实例的当前表达式 CurrentExpression?

		/// <summary>
		/// Determines whether the argument is equal to the DBNull.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static object IsDBNull(object[] args)
		{
			if (args.Length != 1)
			{
				// 抛出 arguments 参数数量不一致的异常
				return false;
			}

			return args[0].Equals(System.DBNull.Value);
		}


		/// <summary>
		/// Determines whether the argument is equal to the DBNull, null, empty.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static object IsDBNullOrNullOrEmpty(object[] args)
		{
			if (args.Length != 1)
			{
				// 抛出 arguments 参数数量不一致的异常
				return false;
			}

			if (args[0].Equals(System.DBNull.Value)) return true;
			if (args[0].Equals(null)) return true;
			if (args[0].Equals(string.Empty)) return true;

			return false;
		}


		/// <summary>
		/// Determines whether the argument is equal to the null, empty.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static object IsNullOrEmpty(object[] args)
		{
			if (args.Length != 1)
			{
				// 抛出 arguments 参数数量不一致的异常
				return false;
			}

			if (args[0].Equals(null)) return true;
			if (args[0].Equals(string.Empty)) return true;

			return false;
		}


		/// <summary>
		/// 判断第一个参数能否整除第二个参数。
		/// </summary>
		/// <param name="args">包括两个参数，必须为整型。</param>
		/// <returns>如果能够被整除，返回True，否则返回False。</returns>
		public static object IsDivideExactly(object[] args)
		{
			if (args.Length != 1)
			{
				return null;
			}

			if(!IsInteger(args[0].ToString())) return false;
			if(!IsInteger(args[1].ToString())) return false;

			int arg1 = Convert.ToInt32(args[0]);
			int arg2 = Convert.ToInt32(args[1]);

			if(arg2 == 0) return false;

			return arg1%arg2 == 0;
		}


		/// <summary>
		/// 验证给定字符串是否是合法的整数
		/// </summary>
		/// <param name="value">待判断的值</param>
		/// <returns></returns>
		public static bool IsInteger(string value)
		{
			//System.Diagnostics.Debug.Assert(value != null, "Unexpected null value.", "The method requires a value.");
			if (value == null)
				return false;

			try
			{
				//int parsedValue = 
				Int32.Parse(value, CultureInfo.CurrentCulture);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}
	}
}
