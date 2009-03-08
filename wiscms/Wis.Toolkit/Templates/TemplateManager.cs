//------------------------------------------------------------------------------
// <copyright file="TemplateManager.cs" company="Microsoft">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;
using Wis.Toolkit.Templates.Parser.Entity;

namespace Wis.Toolkit.Templates
{
	public delegate object TemplateFunction(object[] args);

	public class TemplateManager
	{
		bool _SilentErrors;

		Hashtable functions; // string -> TemplateFunction
		Hashtable customTags; // string ->ITagHandler

		VariableScope variables; // current variable scope
		Expression currentExpression; // current expression being evaluated

		TextWriter writer; // all output is sent here

		TemplateEntity mainTemplateEntity; // main template to execute
		TemplateEntity currentTemplateEntity; // current template being executed

		ITemplateHandler handler; // handler will be set as "this" object

		private string _TemplateData;
		public string TemplateData
		{
			get { return _TemplateData; }
			set
			{
				_TemplateData = value;

				TemplateEntity templateEntity = TemplateEntity.FromString("", value);
				this.mainTemplateEntity = templateEntity;
				this.currentTemplateEntity = templateEntity;
			}
		}
		public TemplateManager()
		{
			this._SilentErrors = false;
			InitializeManager();
		}
		/// <summary>
		/// create templateEntity manager using a templateEntity
		/// </summary>
		public TemplateManager(TemplateEntity templateEntity)
		{
			this.mainTemplateEntity = templateEntity;
			this.currentTemplateEntity = templateEntity;
			this._SilentErrors = false;

			InitializeManager();
		}

		public static TemplateManager LoadData(string templateData)
		{
			TemplateEntity itemplate = TemplateEntity.FromString("", templateData);
			return new TemplateManager(itemplate);
		}

		public static TemplateManager LoadFile(string filename)
		{
			TemplateEntity templateEntity = TemplateEntity.FromFile("", filename, Encoding.Default);
			return new TemplateManager(templateEntity);
		}


		public static TemplateManager LoadFile(string filename, System.Text.Encoding encoding)
		{
			TemplateEntity templateEntity = TemplateEntity.FromFile("", filename, encoding);
			return new TemplateManager(templateEntity);
		}


		/// <summary>
		/// handler is used as "this" object, and will receive
		/// before after process message
		/// </summary>
		public ITemplateHandler Handler
		{
			get { return this.handler; }
			set { this.handler = value; }
		}

		/// <summary>
		/// if silet errors is set to true, then any exceptions will not show in the output
		/// If set to false, all exceptions will be displayed.
		/// </summary>
		public bool SilentErrors
		{
			get { return this._SilentErrors; }
			set { this._SilentErrors = value; }
		}

		private Hashtable CustomTags
		{
			get
			{
				if (customTags == null)
					customTags = new Hashtable();

				return customTags;
			}
		}

		/// <summary>
		/// registers custom tag processor
		/// </summary>
		public void RegisterCustomTag(string tagName, ITagHandler tagHandler)
		{
			CustomTags.Add(tagName, tagHandler);
		}

		/// <summary>
		/// checks whether there is a handler for tagName
		/// </summary>
		public bool IsCustomTagRegistered(string tagName)
		{
			return CustomTags.Contains(tagName);
		}

		/// <summary>
		/// unregistered tagName from custom tags
		/// </summary>
		public void UnRegisterCustomTag(string tagName)
		{
			CustomTags.Remove(tagName);
		}

		/// <summary>
		/// adds templateEntity that can be used within execution 
		/// </summary>
		/// <param name="templateEntity"></param>
		public void AddTemplate(TemplateEntity templateEntity)
		{
			mainTemplateEntity.Templates.Add(templateEntity.Name, templateEntity);
		}

		void InitializeManager()
		{
			this.functions = new Hashtable();

			this.variables = new VariableScope();

			functions.Add("Equals", new TemplateFunction(FuncEquals));
			functions.Add("NotEquals", new TemplateFunction(FuncNotEquals));
			functions.Add("IsEven", new TemplateFunction(FuncIsEven));
			functions.Add("isodd", new TemplateFunction(FuncIsOdd));
			functions.Add("IsEmpty", new TemplateFunction(FuncIsEmpty));
			functions.Add("IsNotEmpty", new TemplateFunction(FuncIsNotEmpty));
			functions.Add("IsNumber", new TemplateFunction(FuncIsNumber));
			functions.Add("ToUpper", new TemplateFunction(FuncToUpper));
			functions.Add("ToLower", new TemplateFunction(FuncToLower));
			functions.Add("IsDefined", new TemplateFunction(FuncIsDefined));
			functions.Add("IfDefined", new TemplateFunction(FuncIfDefined));
			functions.Add("Len", new TemplateFunction(FuncLen));
			functions.Add("ToList", new TemplateFunction(FuncToList));
			functions.Add("IsNull", new TemplateFunction(FuncIsNull)); // 判断是否为null
			functions.Add("IsDBNull", new TemplateFunction(FuncIsDBNull)); // 判断是否为DBNull
            functions.Add("IsNullOrEmpty", new TemplateFunction(Templates.Functions.Validator.IsNullOrEmpty)); // 判断是否为Null或Empty
            functions.Add("IsDBNullOrNullOrEmpty", new TemplateFunction(Templates.Functions.Validator.IsDBNullOrNullOrEmpty)); // 判断是否为DBNull、Null或Empty
			functions.Add("Not", new TemplateFunction(FuncNot));
			functions.Add("iif", new TemplateFunction(FuncIif));
			functions.Add("format", new TemplateFunction(FuncFormat));
			functions.Add("Trim", new TemplateFunction(FuncTrim));
			functions.Add("Filter", new TemplateFunction(FuncFilter));
            
            functions.Add("GreaterThan", new TemplateFunction(Templates.Functions.Operators.GreaterThan));
            functions.Add("LessThan", new TemplateFunction(Templates.Functions.Operators.LessThan));
            functions.Add("Compare", new TemplateFunction(Templates.Functions.Operators.Compare));
            functions.Add("IsDivideExactly", new TemplateFunction(Templates.Functions.Validator.IsDivideExactly)); // 判断是否能整除

            functions.Add("Or", new TemplateFunction(FuncOr));
			functions.Add("And", new TemplateFunction(FuncAnd));
			functions.Add("CompareNoCase", new TemplateFunction(FuncCompareNoCase));
			functions.Add("Stripnewlines", new TemplateFunction(FuncStripNewLines));
			functions.Add("typeof", new TemplateFunction(FuncTypeOf));
			functions.Add("cint", new TemplateFunction(FuncCInt));
			functions.Add("cdouble", new TemplateFunction(FuncCDouble));
			functions.Add("cdate", new TemplateFunction(FuncCDate));
			functions.Add("now", new TemplateFunction(FuncNow));

			// 创建标签
			functions.Add("CreateLabel", new TemplateFunction(FuncCreateLabel));
			// 创建引用
			functions.Add("CreateTypeReference", new TemplateFunction(FuncCreateTypeReference));
		}


		#region Functions

		bool CheckArgCount(int count, string funcName, object[] args)
		{
			if (count != args.Length)
			{
				DisplayError(string.Format("Function {0} requires {1} arguments and {2} were passed", funcName, count, args.Length), currentExpression.Line, currentExpression.Col);
				return false;
			}
			else
				return true;
		}

		bool CheckArgCount(int count1, int count2, string funcName, object[] args)
		{
			if (args.Length < count1 || args.Length > count2)
			{
				string msg = string.Format("Function {0} requires between {1} and {2} arguments and {3} were passed", funcName, count1, count2, args.Length);
				DisplayError(msg, currentExpression.Line, currentExpression.Col);
				return false;
			}
			else
				return true;
		}

		object FuncIsEven(object[] args)
		{
			if (!CheckArgCount(1, "iseven", args))
				return null;

			try
			{
				int value = Convert.ToInt32(args[0]);
				return value%2 == 0;
			}
			catch (FormatException)
			{
				throw new TemplateRuntimeException("IsEven cannot convert parameter to int", currentExpression.Line, currentExpression.Col);
			}
		}

		object FuncIsOdd(object[] args)
		{
			if (!CheckArgCount(1, "isdd", args))
				return null;

			try
			{
				int value = Convert.ToInt32(args[0]);
				return value%2 == 1;
			}
			catch (FormatException)
			{
				throw new TemplateRuntimeException("IsOdd cannot convert parameter to int", currentExpression.Line, currentExpression.Col);
			}
		}

		object FuncIsEmpty(object[] args)
		{
			if (!CheckArgCount(1, "isempty", args))
				return null;

			string value = args[0].ToString();
			return value.Length == 0;
		}

		object FuncIsNotEmpty(object[] args)
		{
			if (!CheckArgCount(1, "isnotempty", args))
				return null;

			if (args[0] == null)
				return false;

			string value = args[0].ToString();
			return value.Length > 0;
		}


		object FuncEquals(object[] args)
		{
            if (!CheckArgCount(2, "Equals", args))
				return null;

			return args[0].Equals(args[1]);
		}


		object FuncNotEquals(object[] args)
		{
            if (!CheckArgCount(2, "NotEquals", args))
				return null;

			return !args[0].Equals(args[1]);
		}


		object FuncIsNumber(object[] args)
		{
			if (!CheckArgCount(1, "isnumber", args))
				return null;

			try
			{
				int value = Convert.ToInt32(args[0]);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		object FuncToUpper(object[] args)
		{
			if (!CheckArgCount(1, "toupper", args))
				return null;

			return args[0].ToString().ToUpper();
		}

		object FuncToLower(object[] args)
		{
			if (!CheckArgCount(1, "toupper", args))
				return null;

			return args[0].ToString().ToLower();
		}

		object FuncLen(object[] args)
		{
			if (!CheckArgCount(1, "len", args))
				return null;

			return args[0].ToString().Length;
		}


		object FuncIsDefined(object[] args)
		{
			if (!CheckArgCount(1, "isdefined", args))
				return null;

			return variables.IsDefined(args[0].ToString());
		}

		object FuncIfDefined(object[] args)
		{
			if (!CheckArgCount(2, "ifdefined", args))
				return null;

			if (variables.IsDefined(args[0].ToString()))
			{
				return args[1];
			}
			else
				return string.Empty;
		}

		object FuncToList(object[] args)
		{
			if (!CheckArgCount(2, 3, "tolist", args))
				return null;

			object list = args[0];

			string property;
			string delim;

			if (args.Length == 3)
			{
				property = args[1].ToString();
				delim = args[2].ToString();
			}
			else
			{
				property = string.Empty;
				delim = args[1].ToString();
			}

			if (!(list is IEnumerable))
			{
				throw new TemplateRuntimeException("argument 1 of tolist has to be IEnumerable", currentExpression.Line, currentExpression.Col);
			}

			IEnumerator ienum = ((IEnumerable) list).GetEnumerator();
			StringBuilder sb = new StringBuilder();
			int index = 0;
			while (ienum.MoveNext())
			{
				if (index > 0)
					sb.Append(delim);

				if (args.Length == 2) // do not evalulate property
					sb.Append(ienum.Current);
				else
				{
					sb.Append(EvalProperty(ienum.Current, property));
				}
				index++;
			}

			return sb.ToString();

		}

		object FuncIsNull(object[] args)
		{
			if (!CheckArgCount(1, "isnull", args))
				return null;

			return args[0] == null;
		}
		
		object FuncIsDBNull(object[] args)
		{
			if (!CheckArgCount(1, "IsDBNull", args))
				return null;

			return args[0].Equals(System.DBNull.Value);
		}

		object FuncNot(object[] args)
		{
			if (!CheckArgCount(1, "not", args))
				return null;

			if (args[0] is bool)
				return !(bool) args[0];
			else
			{
				throw new TemplateRuntimeException("Parameter 1 of function 'not' is not boolean", currentExpression.Line, currentExpression.Col);
			}

		}

		object FuncIif(object[] args)
		{
			if (!CheckArgCount(3, "iif", args))
				return null;

			if (args[0] is bool)
			{
				bool test = (bool) args[0];
				return test ? args[1] : args[2];
			}
			else
			{
				throw new TemplateRuntimeException("Parameter 1 of function 'iif' is not boolean", currentExpression.Line, currentExpression.Col);
			}
		}

		object FuncFormat(object[] args)
		{
			if (!CheckArgCount(2, "format", args))
				return null;

			string format = args[1].ToString();

			if (args[0] is IFormattable)
				return ((IFormattable) args[0]).ToString(format, null);
			else
				return args[0].ToString();
		}

		object FuncTrim(object[] args)
		{
			if (!CheckArgCount(1, "trim", args))
				return null;

			return args[0].ToString().Trim();
		}

		object FuncFilter(object[] args)
		{
			if (!CheckArgCount(2, "filter", args))
				return null;

			object list = args[0];

			string property;
			property = args[1].ToString();

			if (!(list is IEnumerable))
			{
				throw new TemplateRuntimeException("argument 1 of filter has to be IEnumerable", currentExpression.Line, currentExpression.Col);
			}

			IEnumerator ienum = ((IEnumerable) list).GetEnumerator();
			ArrayList newList = new ArrayList();

			while (ienum.MoveNext())
			{
				object val = EvalProperty(ienum.Current, property);
				if (val is bool && (bool) val)
					newList.Add(ienum.Current);
			}

			return newList;

		}

		object FuncOr(object[] args)
		{
			if (!CheckArgCount(2, "or", args))
				return null;

			if (args[0] is bool && args[1] is bool)
				return (bool) args[0] || (bool) args[1];
			else
				return false;
		}

		object FuncAnd(object[] args)
		{
			if (!CheckArgCount(2, "add", args))
				return null;

			if (args[0] is bool && args[1] is bool)
				return (bool) args[0] && (bool) args[1];
			else
				return false;
		}

		object FuncCompareNoCase(object[] args)
		{
			if (!CheckArgCount(2, "compareNoCase", args))
				return null;

			string s1 = args[0].ToString();
			string s2 = args[1].ToString();

			return string.Compare(s1, s2, true) == 0;
		}

		object FuncStripNewLines(object[] args)
		{
			if (!CheckArgCount(1, "StripNewLines", args))
				return null;

			string s1 = args[0].ToString();
			return s1.Replace(Environment.NewLine, " ");

		}

		object FuncTypeOf(object[] args)
		{
			if (!CheckArgCount(1, "TypeOf", args))
				return null;

			return args[0].GetType().Name;

		}

		object FuncCInt(object[] args)
		{
			if (!CheckArgCount(1, "cint", args))
				return null;

			return Convert.ToInt32(args[0]);
		}

		object FuncCDouble(object[] args)
		{
			if (!CheckArgCount(1, "cdouble", args))
				return null;

			return Convert.ToDouble(args[0]);
		}

		object FuncCDate(object[] args)
		{
			if (!CheckArgCount(1, "cdate", args))
				return null;

			return Convert.ToDateTime(args[0]);
		}

		object FuncNow(object[] args)
		{
			if (!CheckArgCount(0, "now", args))
				return null;

			return DateTime.Now;
		}

		
		/// <summary>
		/// 创建模版标签实例。

		/// </summary>
		/// <param name="args">参数，包括Name和Value。</param>
		/// <returns>返回模版标签实例。</returns>
		object FuncCreateLabel(object[] args)
		{
			if (!CheckArgCount(2, "CreateLabel", args))
				return null;
			
			string labelName = args[0].ToString();
			object labelValue = args[1];
			
			System.Collections.DictionaryEntry entry = new System.Collections.DictionaryEntry();
			entry.Key = labelName;
			entry.Value = labelValue;
			
			return entry;
		}
		
		object FuncCreateTypeReference(object[] args)
		{
			if (!CheckArgCount(1, "CreateTypeReference", args))
				return null;

			string typeName = args[0].ToString();


			Type type = System.Type.GetType(typeName, false, true);
			if (type != null)
				return new StaticTypeReference(type);

			Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly asm in asms)
			{
				type = asm.GetType(typeName, false, true);
				if (type != null)
					return new StaticTypeReference(type);
			}

			throw new TemplateRuntimeException("创建类型" + typeName + "失败", currentExpression.Line, currentExpression.Col);
		}

		#endregion

		/// <summary>
		/// gets library of functions that are available
		/// for the tempalte execution
		/// </summary>
		public Hashtable Functions
		{
			get { return functions; }
		}

		public VariableScope Variables
		{
			get { return variables; }
			set { variables = value;}
		}

		/// <summary>
		/// sets value for variable called name
		/// </summary>
		public void SetVariable(string variableName, object variableValue)
		{
			variables[variableName] = variableValue;
		}

		/// <summary>
		/// gets value for variable called variableName.
		/// Throws exception if value is not found
		/// </summary>
		public object GetVariable(string variableName)
		{
			if (variables.IsDefined(variableName))
				return variables[variableName];
			else
                throw new System.Exception("Variable '" + variableName + "' cannot be found in current scope.");
		}

		/// <summary>
		/// processes current template and sends output to writer
		/// </summary>
		/// <param name="textWriter"></param>
		public void Process(TextWriter textWriter)
		{
			this.writer = textWriter;
			this.currentTemplateEntity = mainTemplateEntity;

			if (handler != null)
			{
				SetVariable("this", handler);
				handler.BeforeProcess(this);
			}

			ProcessElements(mainTemplateEntity.Elements);

			if (handler != null)
				handler.AfterProcess(this);
		}

		/// <summary>
		/// processes templates and returns string value
		/// </summary>
		public string Process()
		{
			StringWriter writer = new StringWriter();
			Process(writer);
			return writer.ToString();
		}

		/// <summary>
		/// resets all variables. If TemplateManager is used to 
		/// process template multiple times, Reset() must be 
		/// called prior to Process if varialbes need to be cleared
		/// </summary>
		public void Reset()
		{
			variables.Clear();
		}

		/// <summary>
		/// processes list of elements.
		/// This method is mostly used by extenders of the manager
		/// from custom functions or custom tags.
		/// </summary>
		public void ProcessElements(ElementCollection collection)
		{
			foreach (Element elem in collection)
			{
				ProcessElement(elem);
			}
		}

		protected void ProcessElement(Element elem)
		{
            if (elem is Templates.Parser.Entity.Text)
			{
                Templates.Parser.Entity.Text text = (Templates.Parser.Entity.Text)elem;
				WriteValue(text.Data);
			}
			else if (elem is Expression)
				ProcessExpression((Expression) elem);
			else if (elem is TagIf)
				ProcessIf((TagIf) elem);
			else if (elem is Tag)
				ProcessTag((Tag) elem);
		}

		protected void ProcessExpression(Expression exp)
		{
			object value = EvalExpression(exp);
			WriteValue(value);
		}

		/// <summary>
		/// evaluates expression.
		/// This method is used by TemplateManager extensibility.
		/// </summary>
		public object EvalExpression(Expression exp)
		{
			currentExpression = exp;

			try
			{
				if (exp is StringLiteral)
					return ((StringLiteral) exp).Content;
				else if (exp is Name)
				{
					return GetVariable(((Name) exp).Id);
				}
				else if (exp is FieldAccess)
				{
					FieldAccess fa = (FieldAccess) exp;
					object obj = EvalExpression(fa.Exp);
					string propertyName = fa.Field;
					return EvalProperty(obj, propertyName);
				}
				else if (exp is MethodCall)
				{
					MethodCall ma = (MethodCall) exp;
					object obj = EvalExpression(ma.CallObject);
					string methodName = ma.Name;

					return EvalMethodCall(obj, methodName, EvalArguments(ma.Args));
				}
				else if (exp is IntLiteral)
					return ((IntLiteral) exp).Value;
				else if (exp is DoubleLiteral)
					return ((DoubleLiteral) exp).Value;
				else if (exp is FCall)
				{
					FCall fcall = (FCall) exp;
					if (!functions.Contains(fcall.Name))
					{
						string msg = string.Format("Function {0} is not defined", fcall.Name);
						throw new TemplateRuntimeException(msg, exp.Line, exp.Col);
					}

					TemplateFunction func = (TemplateFunction) functions[fcall.Name];
					object[] values = EvalArguments(fcall.Args);

					return func(values);
				}
				else if (exp is StringExpression)
				{
					StringExpression stringExp = (StringExpression) exp;
					StringBuilder sb = new StringBuilder();
					foreach (Expression ex in stringExp.Expressions)
						sb.Append(EvalExpression(ex));

					return sb.ToString();
				}
				else if (exp is BinaryExpression)
					return EvalBinaryExpression(exp as BinaryExpression);
				else if (exp is ArrayAccess)
					return EvalArrayAccess(exp as ArrayAccess);
				else
					throw new TemplateRuntimeException("Invalid expression type: " + exp.GetType().Name, exp.Line, exp.Col);

			}
			catch (TemplateRuntimeException ex)
			{
				DisplayError(ex);
				return null;
			}
            catch (System.Exception ex)
			{
				DisplayError(new TemplateRuntimeException(ex.Message, currentExpression.Line, currentExpression.Col));
				return null;
			}
		}

		protected object EvalArrayAccess(ArrayAccess arrayAccess)
		{
			object obj = EvalExpression(arrayAccess.Exp);

			object index = EvalExpression(arrayAccess.Index);

			if (obj is Array)
			{
				Array array = (Array) obj;
				if (index is Int32)
				{
					return array.GetValue((int) index);
				}
				else
					throw new TemplateRuntimeException("Index of array has to be integer", arrayAccess.Line, arrayAccess.Col);
			}
			else
				return EvalMethodCall(obj, "get_Item", new object[] {index});

		}

		protected object EvalBinaryExpression(BinaryExpression exp)
		{
			switch (exp.Operator)
			{
                case Templates.Parser.TokenKind.OpOr:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						if (Util.ToBool(lhsValue))
							return true;

						object rhsValue = EvalExpression(exp.Rhs);
						return Util.ToBool(rhsValue);
					}
                case Templates.Parser.TokenKind.OpAnd:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						if (!Util.ToBool(lhsValue))
							return false;

						object rhsValue = EvalExpression(exp.Rhs);
						return Util.ToBool(rhsValue);

					}
                case Templates.Parser.TokenKind.OpIs:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						return lhsValue.Equals(rhsValue);
					}
                case Templates.Parser.TokenKind.OpIsNot:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						return !lhsValue.Equals(rhsValue);

					}
				case Templates.Parser.TokenKind.OpGt:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						IComparable c1 = lhsValue as IComparable;
						IComparable c2 = rhsValue as IComparable;
						if (c1 == null || c2 == null)
							return false;
						else
							return c1.CompareTo(c2) == 1;

					}
                case Templates.Parser.TokenKind.OpLt:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						IComparable c1 = lhsValue as IComparable;
						IComparable c2 = rhsValue as IComparable;
						if (c1 == null || c2 == null)
							return false;
						else
							return c1.CompareTo(c2) == -1;

					}
				case Templates.Parser.TokenKind.OpGte:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						IComparable c1 = lhsValue as IComparable;
						IComparable c2 = rhsValue as IComparable;
						if (c1 == null || c2 == null)
							return false;
						else
							return c1.CompareTo(c2) >= 0;

					}
				case Templates.Parser.TokenKind.OpLte:
					{
						object lhsValue = EvalExpression(exp.Lhs);
						object rhsValue = EvalExpression(exp.Rhs);

						IComparable c1 = lhsValue as IComparable;
						IComparable c2 = rhsValue as IComparable;
						if (c1 == null || c2 == null)
							return false;
						else
							return c1.CompareTo(c2) <= 0;

					}
				default:
					throw new TemplateRuntimeException("不支持 " + exp.Operator.ToString() + " 操作。", exp.Line, exp.Col);
			}
		}

		protected object[] EvalArguments(Expression[] args)
		{
			object[] values = new object[args.Length];
			for (int i = 0; i < values.Length; i++)
				values[i] = EvalExpression(args[i]);

			return values;
		}

		protected static object EvalProperty(object obj, string propertyName)
		{
			if (obj is StaticTypeReference)
			{
				Type type = (obj as StaticTypeReference).Type;

				PropertyInfo pinfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetProperty | BindingFlags.Static);
				if (pinfo != null)
					return pinfo.GetValue(null, null);

				FieldInfo finfo = type.GetField(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Static);
				if (finfo != null)
					return finfo.GetValue(null);
				else
                    throw new System.Exception("Cannot find property or field named '" + propertyName + "' in object of type '" + type.Name + "'");


			}
			else
			{
				PropertyInfo pinfo = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetProperty | BindingFlags.Instance);

				if (pinfo != null)
					return pinfo.GetValue(obj, null);

				FieldInfo finfo = obj.GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.GetField | BindingFlags.Instance);

				if (finfo != null)
					return finfo.GetValue(obj);
				else
                    throw new System.Exception("Cannot find property or field named '" + propertyName + "' in object of type '" + obj.GetType().Name + "'");

			}

		}


		protected object EvalMethodCall(object obj, string methodName, object[] args)
		{
			Type[] types = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
                if (args[i] == null)
                {
                    args[i] = DBNull.Value;
                    types[i] = typeof(DBNull);
                }
                else
                    types[i] = args[i].GetType();

			if (obj is StaticTypeReference)
			{
                // obj {Name = "CategoryManager" FullName = "Wis.Website.DataManager.CategoryManager"}
				Type type = (obj as StaticTypeReference).Type;
				MethodInfo method = type.GetMethod(methodName,
				                                   BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Static,
				                                   null, types, null);

                if (method == null)
                {
                    MethodInfo[] methodInfos = type.GetMethods();
                    System.Text.StringBuilder sbMethodNames = new System.Text.StringBuilder();
                    foreach (MethodInfo m in methodInfos)
                    {
                        sbMethodNames.Append(m.Name);
                        sbMethodNames.Append(" ");
                    }
                    string methodNames = sbMethodNames.ToString();

                    System.Text.StringBuilder sbArguments = new System.Text.StringBuilder();
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (args[i] == null)
                            sbArguments.Append("DBNull");
                        else
                            sbArguments.Append(args[i].GetType().ToString());
                        
                        sbArguments.Append(", ");
                    }
                    string arguments = sbArguments.ToString().TrimEnd(new char[]{',', ' '});
                    throw new System.Exception(string.Format("类型 {1} 没有找到静态方法 {0}({3})，类型 {1} 支持的静态方法有：{2}", methodName, type.Name, methodNames, arguments));
                }
				return method.Invoke(null, args);
			}
			else
			{
				MethodInfo method = obj.GetType().GetMethod(methodName,
				                                            BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance,
				                                            null, types, null);

                if (method == null)
                {
                    MethodInfo[] methodInfos = obj.GetType().GetMethods();
                    System.Text.StringBuilder sbMethodName = new System.Text.StringBuilder();
                    foreach (MethodInfo m in methodInfos)
                    {
                        sbMethodName.Append(m.Name);
                        sbMethodName.Append(" ");
                    }
                    throw new System.Exception(string.Format("类型 {1} 未找到方法 {0}，类型 {1} 支持的方法有：{2}", methodName, obj.GetType().Name, sbMethodName.ToString()));
                }

				return method.Invoke(obj, args);
			}
		}


		protected void ProcessIf(TagIf tagIf)
		{
			bool condition = false;

			try
			{
				object value = EvalExpression(tagIf.Test);

				condition = Util.ToBool(value);
			}
            catch (System.Exception ex)
			{
				DisplayError("Error evaluating condition for if statement: " + ex.Message,
				             tagIf.Line, tagIf.Col);
				return;
			}

			if (condition)
				ProcessElements(tagIf.InnerElements);
			else
				ProcessElement(tagIf.FalseBranch);

		}

		protected void ProcessTag(Tag tag)
		{
			string name = tag.Name.ToLower();
			try
			{
				switch (name)
				{
					case "template":
						// skip those, because those are processed first
						break;
					case "else":
						ProcessElements(tag.InnerElements);
						break;
					case "apply":
						object val = EvalExpression(tag.AttributeValue("template"));
						ProcessTemplate(val.ToString(), tag);
						break;
					case "foreach":
						ProcessForEach(tag);
						break;
					case "for":
						ProcessFor(tag);
						break;
					case "set":
						ProcessTagSet(tag);
						break;
					default:
						ProcessTemplate(tag.Name, tag);
						break;
				}
			}
			catch (TemplateRuntimeException ex)
			{
				DisplayError(ex);
			}
            catch (System.Exception ex)
			{
				DisplayError("Error executing tag '" + name + "': " + ex.Message, tag.Line, tag.Col);

			}
		}

		protected void ProcessTagSet(Tag tag)
		{
			Expression expName = tag.AttributeValue("name");
			if (expName == null)
			{
				throw new TemplateRuntimeException("Set is missing required attribute: name", tag.Line, tag.Col);
			}

			Expression expValue = tag.AttributeValue("value");
			if (expValue == null)
			{
				throw new TemplateRuntimeException("Set is missing required attribute: value", tag.Line, tag.Col);
			}


			string name = EvalExpression(expName).ToString();
			if (!Util.IsValidVariableName(name))
				throw new TemplateRuntimeException("'" + name + "' is not valid variable name.", expName.Line, expName.Col);

			object value = EvalExpression(expValue);

			this.SetVariable(name, value);
		}

		protected void ProcessForEach(Tag tag)
		{
			Expression expCollection = tag.AttributeValue("collection");
			if (expCollection == null)
			{
				throw new TemplateRuntimeException("Foreach is missing required attribute: collection", tag.Line, tag.Col);
			}

			object collection = EvalExpression(expCollection);
			if (!(collection is IEnumerable))
			{
				throw new TemplateRuntimeException("Collection used in foreach has to be enumerable", tag.Line, tag.Col);
			}

			Expression expVar = tag.AttributeValue("var");
			if (expCollection == null)
			{
				throw new TemplateRuntimeException("Foreach is missing required attribute: var", tag.Line, tag.Col);
			}
			object varObject = EvalExpression(expVar);
			if (varObject == null)
				varObject = "foreach";
			string varname = varObject.ToString();

			Expression expIndex = tag.AttributeValue("index");
			string indexname = null;
			if (expIndex != null)
			{
				object obj = EvalExpression(expIndex);
				if (obj != null)
					indexname = obj.ToString();
			}

			IEnumerator ienum = ((IEnumerable) collection).GetEnumerator();
			int index = 0;
			while (ienum.MoveNext())
			{
				index++;
				object value = ienum.Current;
				variables[varname] = value;
				if (indexname != null)
					variables[indexname] = index;

				ProcessElements(tag.InnerElements);
			}
		}

		protected void ProcessFor(Tag tag)
		{
			Expression expFrom = tag.AttributeValue("from");
			if (expFrom == null)
			{
				throw new TemplateRuntimeException("For is missing required attribute: start", tag.Line, tag.Col);
			}

			Expression expTo = tag.AttributeValue("to");
			if (expTo == null)
			{
				throw new TemplateRuntimeException("For is missing required attribute: to", tag.Line, tag.Col);
			}

			Expression expIndex = tag.AttributeValue("index");
			if (expIndex == null)
			{
				throw new TemplateRuntimeException("For is missing required attribute: index", tag.Line, tag.Col);
			}

			object obj = EvalExpression(expIndex);
			string indexName = obj.ToString();

			int start = Convert.ToInt32(EvalExpression(expFrom));
			int end = Convert.ToInt32(EvalExpression(expTo));

			for (int index = start; index <= end; index++)
			{
				SetVariable(indexName, index);
				//variables[indexName] = index;

				ProcessElements(tag.InnerElements);
			}
		}

		protected void ExecuteCustomTag(Tag tag)
		{
			ITagHandler tagHandler = (ITagHandler) customTags[tag.Name];

			bool processInnerElements = true;
			bool captureInnerContent = false;

			tagHandler.TagBeginProcess(this, tag, ref processInnerElements, ref captureInnerContent);

			string innerContent = null;

			if (processInnerElements)
			{
				TextWriter saveWriter = writer;

				if (captureInnerContent)
					writer = new StringWriter();

				try
				{
					ProcessElements(tag.InnerElements);

					innerContent = writer.ToString();
				}
				finally
				{
					writer = saveWriter;
				}
			}

			tagHandler.TagEndProcess(this, tag, innerContent);

		}

		protected void ProcessTemplate(string name, Tag tag)
		{
			if (customTags != null && customTags.Contains(name))
			{
				ExecuteCustomTag(tag);
				return;
			}

			TemplateEntity useTemplateEntity = currentTemplateEntity.FindTemplate(name);
			if (useTemplateEntity == null)
			{
				string msg = string.Format("TemplateEntity '{0}' not found", name);
				throw new TemplateRuntimeException(msg, tag.Line, tag.Col);
			}

			// process inner elements and save content
			TextWriter saveWriter = writer;
			writer = new StringWriter();
			string content = string.Empty;

			try
			{
				ProcessElements(tag.InnerElements);

				content = writer.ToString();
			}
			finally
			{
				writer = saveWriter;
			}

			TemplateEntity saveTemplateEntity = currentTemplateEntity;
			variables = new VariableScope(variables);
			variables["innerText"] = content;

			try
			{
				foreach (TagAttribute attrib in tag.Attributes)
				{
					object val = EvalExpression(attrib.Expression);
					variables[attrib.Name] = val;
				}

				currentTemplateEntity = useTemplateEntity;
				ProcessElements(currentTemplateEntity.Elements);
			}
			finally
			{
				variables = variables.Parent;
				currentTemplateEntity = saveTemplateEntity;
			}


		}

		/// <summary>
		/// writes value to current writer
		/// </summary>
		/// <param name="value">value to be written</param>
		public void WriteValue(object value)
		{
			if (value == null)
				writer.Write("[null]");
			else
				writer.Write(value);
		}

        private void DisplayError(System.Exception ex)
		{
			if (ex is TemplateRuntimeException)
			{
				TemplateRuntimeException tex = (TemplateRuntimeException) ex;
				DisplayError(ex.Message, tex.Line, tex.Col);
			}
			else
				DisplayError(ex.Message, 0, 0);
		}

		private void DisplayError(string msg, int line, int col)
		{
			if (!_SilentErrors)
				writer.Write("[ERROR ({0}, {1}): {2}]", line, col, msg);
		}

	}
}