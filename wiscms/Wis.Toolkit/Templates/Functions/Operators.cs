using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Toolkit.Templates.Functions
{
    public sealed class Operators
    {
        /// <summary>
        /// Greater than operator
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object GreaterThan(object[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("args");

			IComparable argument1 = args[0] as IComparable;
			IComparable argument2 = args[1] as IComparable;
			if (argument1 == null || argument2 == null)
				return false;
			else
				return argument1.CompareTo(argument2) > 0;
        }

        /// <summary>
        /// Less than operator
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object LessThan(object[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("args");

            IComparable argument1 = args[0] as IComparable;
            IComparable argument2 = args[1] as IComparable;
            if (argument1 == null || argument2 == null)
                return false;
            else
                return argument1.CompareTo(argument2) < 0;
        }

        /// <summary>
        /// Compare operator
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Compare(object[] args)
        {
            if (args.Length != 2)
                throw new ArgumentException("args");

            IComparable argument1 = args[0] as IComparable;
            IComparable argument2 = args[1] as IComparable;
            
            if (argument1 == null || argument2 == null)
                throw new ArgumentException("args");
            else
                return argument1.CompareTo(argument2);
        }
    }
}
