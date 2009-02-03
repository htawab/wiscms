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
	/// Validator ��ժҪ˵����
	/// </summary>
	public sealed class Validator
	{
		// TODO:��λ�ȡ��ǰʵ���ĵ�ǰ���ʽ CurrentExpression?

		/// <summary>
		/// Determines whether the argument is equal to the DBNull.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static object IsDBNull(object[] args)
		{
			if (args.Length != 1)
			{
				// �׳� arguments ����������һ�µ��쳣
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
				// �׳� arguments ����������һ�µ��쳣
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
				// �׳� arguments ����������һ�µ��쳣
				return false;
			}

			if (args[0].Equals(null)) return true;
			if (args[0].Equals(string.Empty)) return true;

			return false;
		}


		/// <summary>
		/// �жϵ�һ�������ܷ������ڶ���������
		/// </summary>
		/// <param name="args">������������������Ϊ���͡�</param>
		/// <returns>����ܹ�������������True�����򷵻�False��</returns>
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
		/// ��֤�����ַ����Ƿ��ǺϷ�������
		/// </summary>
		/// <param name="value">���жϵ�ֵ</param>
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
