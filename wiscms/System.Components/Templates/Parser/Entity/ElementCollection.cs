//------------------------------------------------------------------------------
// <copyright file="ElementCollection.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Parser.Entity
{
	/// <summary>
	/// A collection of elements of type Element
	/// </summary>
	public class ElementCollection: CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the ElementList class.
		/// </summary>
		public ElementCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the ElementList class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new ElementList.
		/// </param>
		public ElementCollection(Element[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the ElementList class, containing elements
		/// copied from another instance of ElementList
		/// </summary>
		/// <param name="items">
		/// The ElementList whose elements are to be added to the new ElementList.
		/// </param>
		public ElementCollection(ElementCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this ElementList.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this ElementList.
		/// </param>
		public virtual void AddRange(Element[] items)
		{
			foreach (Element item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another ElementList to the end of this ElementList.
		/// </summary>
		/// <param name="items">
		/// The ElementList whose elements are to be added to the end of this ElementList.
		/// </param>
		public virtual void AddRange(ElementCollection items)
		{
			foreach (Element item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type Element to the end of this ElementList.
		/// </summary>
		/// <param name="value">
		/// The Element to be added to the end of this ElementList.
		/// </param>
		public virtual void Add(Element value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic Element value is in this ElementList.
		/// </summary>
		/// <param name="value">
		/// The Element value to locate in this ElementList.
		/// </param>
		/// <returns>
		/// true if value is found in this ElementList;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(Element value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this ElementList
		/// </summary>
		/// <param name="value">
		/// The Element value to locate in the ElementList.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(Element value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the ElementList at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the Element is to be inserted.
		/// </param>
		/// <param name="value">
		/// The Element to insert.
		/// </param>
		public virtual void Insert(int index, Element value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the Element at the given index in this ElementList.
		/// </summary>
		public virtual Element this[int index]
		{
			get
			{
				return (Element) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific Element from this ElementList.
		/// </summary>
		/// <param name="value">
		/// The Element value to remove from this ElementList.
		/// </param>
		public virtual void Remove(Element value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by ElementList.GetEnumerator.
		/// </summary>
		public class Enumerator: IEnumerator
		{
			private IEnumerator wrapped;

			public Enumerator(ElementCollection collection)
			{
				this.wrapped = ((CollectionBase)collection).GetEnumerator();
			}

			public Element Current
			{
				get
				{
					return (Element) (this.wrapped.Current);
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return (Element) (this.wrapped.Current);
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
		/// Returns an enumerator that can iterate through the elements of this ElementList.
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
