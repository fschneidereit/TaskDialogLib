/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogElementCollection.cs
 *  Purpose:
 *    Task dialog element collection object.
 *  Authors:
 *    Florian Schneidereit <florian.schneidereit@outlook.com>
 *
 *  This library is free software; you can redistribute it and/or modify it under the terms of
 *  the GNU Lesser General Public License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License along with this
 *  library; if not, write to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 *  Boston, MA  02110-1301  USA
 *
 **************************************************************************************************/

#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace Flatcode.Presentation
{
	/// <summary>
	/// Represents a collection of <see cref="TaskDialogElement"/> instances.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the collection. This can be any type that
	/// derives from <see cref="TaskDialogElement"/>.</typeparam>
	public class TaskDialogElementCollection<T> : IList, ICollection<T> where T : TaskDialogElement
	{
		#region Fields

		readonly List<T> items;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialogElementCollection class.
		/// </summary>
		public TaskDialogElementCollection()
		{
			this.items = new List<T>();
		}

		#endregion

		#region Indexer

		internal T this[Int32 index] {
			get { return items[index]; }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of <see cref="TaskDialogElement"/> instances contained in this
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </summary>
		public Int32 Count {
			get { return items.Count; }
		}

		/// <summary>
		/// Indicates whether this <see cref="TaskDialogElementCollection{T}"/> is read-only.
		/// </summary>
		public Boolean IsReadOnly {
			get { return false; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a <see cref="TaskDialogElement"/> instance to this
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialogElement"/> instance to add to this
		/// <see cref="TaskDialogElementCollection{T}"/>.</param>
		public void Add(T item)
		{
			// Add to collection
			items.Add(item);
		}

		/// <summary>
		/// Removes all <see cref="TaskDialogElement"/> instances from this
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </summary>
		public void Clear()
		{
			items.Clear();
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialogElementCollection{T}"/> contains a specific
		/// <see cref="TaskDialogElement"/> instance. 
		/// </summary>
		/// <param name="item">The <see cref="TaskDialogElement"/> instance to locate.</param>
		/// <returns>True if <paramref name="item"/> is found in this
		/// <see cref="TaskDialogElementCollection{T}"/>; otherwise, False.</returns>
		public Boolean Contains(T item)
		{
			return items.Contains(item);
		}

		/// <summary>
		/// Copies the <see cref="TaskDialogElement"/> instances of this
		/// <see cref="TaskDialogElementCollection{T}"/> to an array, starting at the specified
		/// index.
		/// </summary>
		/// <param name="array">A one-dimensional, zero-based <see cref="TaskDialogElement"/> array
		/// that is the destination of the TaskDialog instances copied from this 
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </param>
		/// <param name="index">A <see cref="Int32"/> that specifies the zero-based index in
		/// <paramref name="array"/> at which copying begins.</param>
		public void CopyTo(T[] array, Int32 index)
		{
			items.CopyTo(array, index);
		}

		/// <summary>
		/// Returns an enumerator that iterates through this
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </summary>
		/// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through this
		/// <see cref="TaskDialogCollection"/>.</returns>
		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		/// <summary>
		/// Removes the first occurence of a <see cref="TaskDialogElement"/> instance from this
		/// <see cref="TaskDialogElementCollection{T}"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialogElement"/> instance to remove from this
		/// <see cref="TaskDialogElementCollection{T}"/>.</param>
		/// <returns>True if <paramref name="item"/> was removed from the current instance;
		/// otherwise, False.</returns>
		public Boolean Remove(T item)
		{
			return items.Remove(item);
		}

		#endregion

		#region Methods: Implementation

		/// <summary>
		/// This method is implementation-specific and not intended to be used from third-party
		/// code.
		/// </summary>
		/// <param name="item">This argument is implementation-specific and not intended to be used
		/// from third-party code.</param>
		/// <returns>The return value is implementation-specific and not intended to be used from
		/// third-party code.</returns>
		protected internal virtual Int32 AddInternal(Object item)
		{
			return ((IList)items).Add(item);
		}

		internal Int32 IndexOf(T item)
		{
			return items.IndexOf(item);
		}

		#endregion

		#region Explicit Interfaces

		#region Indexer

		Object IList.this[Int32 index] {
			get { return this[index]; }
			set {
				if (value is T) {
					items[index] = (T)value;
				}
			}
		}

		#endregion

		#region Properties

		Boolean IList.IsFixedSize {
			get { return ((IList)items).IsFixedSize; }
		}

		Boolean ICollection.IsSynchronized
		{
			get { return ((ICollection)items).IsSynchronized; }
		}

		Object ICollection.SyncRoot
		{
			get { return ((ICollection)items).SyncRoot; }
		}

		#endregion

		#region Methods

		Int32 IList.Add(Object item)
		{
			return AddInternal(item);
		}

		Boolean IList.Contains(Object item)
		{
			return ((IList)items).Contains(item);
		}

		void ICollection.CopyTo(Array array, Int32 index)
		{
			((IList)items).CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IList)items).GetEnumerator();
		}

		Int32 IList.IndexOf(Object item)
		{
			return ((IList)items).IndexOf(item);
		}

		void IList.Insert(Int32 index, Object item)
		{
			((IList)items).Insert(index, item);
		}

		void IList.Remove(Object item)
		{
			((IList)items).Remove(item);
		}

		void IList.RemoveAt(Int32 index)
		{
			((IList)items).RemoveAt(index);
		}

		#endregion

		#endregion
	}
}
