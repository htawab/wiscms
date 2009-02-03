//------------------------------------------------------------------------------
// <copyright file="TagAttributeCollection.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Parser.Entity
{
	/// <summary>
	/// A collection of elements of type TagAttribute
	/// </summary>
	public class TagAttributeCollection: CollectionBase
	{
		/// <summary>
		/// Initializes a new empty instance of the TagAttributeList class.
		/// </summary>
		public TagAttributeCollection()
		{
			// empty
		}

		/// <summary>
		/// Initializes a new instance of the TagAttributeList class, containing elements
		/// copied from an array.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the new TagAttributeList.
		/// </param>
		public TagAttributeCollection(TagAttribute[] items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Initializes a new instance of the TagAttributeList class, containing elements
		/// copied from another instance of TagAttributeList
		/// </summary>
		/// <param name="items">
		/// The TagAttributeList whose elements are to be added to the new TagAttributeList.
		/// </param>
		public TagAttributeCollection(TagAttributeCollection items)
		{
			this.AddRange(items);
		}

		/// <summary>
		/// Adds the elements of an array to the end of this TagAttributeList.
		/// </summary>
		/// <param name="items">
		/// The array whose elements are to be added to the end of this TagAttributeList.
		/// </param>
		public virtual void AddRange(TagAttribute[] items)
		{
			foreach (TagAttribute item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds the elements of another TagAttributeList to the end of this TagAttributeList.
		/// </summary>
		/// <param name="items">
		/// The TagAttributeList whose elements are to be added to the end of this TagAttributeList.
		/// </param>
		public virtual void AddRange(TagAttributeCollection items)
		{
			foreach (TagAttribute item in items)
			{
				this.List.Add(item);
			}
		}

		/// <summary>
		/// Adds an instance of type TagAttribute to the end of this TagAttributeList.
		/// </summary>
		/// <param name="value">
		/// The TagAttribute to be added to the end of this TagAttributeList.
		/// </param>
		public virtual void Add(TagAttribute value)
		{
			this.List.Add(value);
		}

		/// <summary>
		/// Determines whether a specfic TagAttribute value is in this TagAttributeList.
		/// </summary>
		/// <param name="value">
		/// The TagAttribute value to locate in this TagAttributeList.
		/// </param>
		/// <returns>
		/// true if value is found in this TagAttributeList;
		/// false otherwise.
		/// </returns>
		public virtual bool Contains(TagAttribute value)
		{
			return this.List.Contains(value);
		}

		/// <summary>
		/// Return the zero-based index of the first occurrence of a specific value
		/// in this TagAttributeList
		/// </summary>
		/// <param name="value">
		/// The TagAttribute value to locate in the TagAttributeList.
		/// </param>
		/// <returns>
		/// The zero-based index of the first occurrence of the _ELEMENT value if found;
		/// -1 otherwise.
		/// </returns>
		public virtual int IndexOf(TagAttribute value)
		{
			return this.List.IndexOf(value);
		}

		/// <summary>
		/// Inserts an element into the TagAttributeList at the specified index
		/// </summary>
		/// <param name="index">
		/// The index at which the TagAttribute is to be inserted.
		/// </param>
		/// <param name="value">
		/// The TagAttribute to insert.
		/// </param>
		public virtual void Insert(int index, TagAttribute value)
		{
			this.List.Insert(index, value);
		}

		/// <summary>
		/// Gets or sets the TagAttribute at the given index in this TagAttributeList.
		/// </summary>
		public virtual TagAttribute this[int index]
		{
			get
			{
				return (TagAttribute) this.List[index];
			}
			set
			{
				this.List[index] = value;
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific TagAttribute from this TagAttributeList.
		/// </summary>
		/// <param name="value">
		/// The TagAttribute value to remove from this TagAttributeList.
		/// </param>
		public virtual void Remove(TagAttribute value)
		{
			this.List.Remove(value);
		}

		/// <summary>
		/// Type-specific enumeration class, used by TagAttributeList.GetEnumerator.
		/// </summary>
		public class Enumerator: IEnumerator
		{
			private IEnumerator wrapped;

			public Enumerator(TagAttributeCollection collection)
			{
				this.wrapped = ((CollectionBase)collection).GetEnumerator();
			}

			public TagAttribute Current
			{
				get
				{
					return (TagAttribute) (this.wrapped.Current);
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return (TagAttribute) (this.wrapped.Current);
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
		/// Returns an enumerator that can iterate through the elements of this TagAttributeList.
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
