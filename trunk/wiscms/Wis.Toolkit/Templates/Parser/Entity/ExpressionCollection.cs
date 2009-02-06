//------------------------------------------------------------------------------
// <copyright file="ExpressionCollection.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Parser.Entity
{
	/// <summary>
	/// A collection of elements of type Expression
	/// </summary>
	public class ExpressionCollection: CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the ExpressionList class.
		/// </summary>
		public ExpressionCollection()
		{
			// empty
		}

		public Expression[] ToArray()
		{
			Expression[] exps = new Expression[Count];
			for (int i=0; i<this.Count; i++)
				exps[i] = this[i];

			return exps;
		}

		/// <summary>
		/// Initializes a new instance of the ExpressionList class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new ExpressionList.
		/// </param>
		public ExpressionCollection(Expression[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the ExpressionList class, containing elements
		/// copied from another instance of ExpressionList
		/// </summary>
		/// <param name="items">
		/// The ExpressionList whose elements are to be added to the new ExpressionList.
		/// </param>
		public ExpressionCollection(ExpressionCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this ExpressionList.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this ExpressionList.
		/// </param>
		public virtual void AddRange(Expression[] items)
		{
			foreach (Expression item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another ExpressionList to the end of this ExpressionList.
		/// </summary>
		/// <param name="items">
		/// The ExpressionList whose elements are to be added to the end of this ExpressionList.
		/// </param>
		public virtual void AddRange(ExpressionCollection items)
		{
			foreach (Expression item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type Expression to the end of this ExpressionList.
		/// </summary>
		/// <param name="value">
		/// The Expression to be added to the end of this ExpressionList.
		/// </param>
		public virtual void Add(Expression value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic Expression value is in this ExpressionList.
		/// </summary>
		/// <param name="value">
		/// The Expression value to locate in this ExpressionList.
		/// </param>
		/// <returns>
		/// true if value is found in this ExpressionList;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(Expression value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this ExpressionList
		/// </summary>
		/// <param name="value">
		/// The Expression value to locate in the ExpressionList.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(Expression value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the ExpressionList at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the Expression is to be inserted.
		/// </param>
		/// <param name="value">
		/// The Expression to insert.
		/// </param>
		public virtual void Insert(int index, Expression value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the Expression at the given index in this ExpressionList.
		/// </summary>
		public virtual Expression this[int index]
		{
			get
			{
				return (Expression) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific Expression from this ExpressionList.
		/// </summary>
		/// <param name="value">
		/// The Expression value to remove from this ExpressionList.
		/// </param>
		public virtual void Remove(Expression value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by ExpressionList.GetEnumerator.
		/// </summary>
		public class Enumerator: IEnumerator
		{
			private IEnumerator wrapped;

			public Enumerator(ExpressionCollection collection)
			{
				this.wrapped = ((CollectionBase)collection).GetEnumerator();
			}

			public Expression Current
			{
				get
				{
					return (Expression) (this.wrapped.Current);
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return (Expression) (this.wrapped.Current);
				}
			}

			public bool MoveNext()
			{
				return this.wrapped.MoveNext();
			}

			public void Reset()
			{
				this.wrapped.Reset();
			}
		}

		/// <summary>
		/// Returns an enumerator that can iterate through the elements of this ExpressionList.
		/// </summary>
		/// <returns>
		/// An object that implements System.Collections.IEnumerator.
		/// </returns>        
		public new virtual Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}
	}

}
