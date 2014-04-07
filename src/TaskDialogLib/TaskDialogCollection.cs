/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogCollection.cs
 *  Purpose:
 *    Task dialog collection object.
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
	/// Represents a collection of <see cref="TaskDialog"/> instances.
	/// </summary>
	public class TaskDialogCollection : IList, ICollection<TaskDialog>
	{
		#region Fields

		readonly List<TaskDialog> items;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialogCollection class. 
		/// </summary>
		public TaskDialogCollection()
		{
			// Default initialization
			items = new List<TaskDialog>();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the number of <see cref="TaskDialog"/> instances contained in this
		/// <see cref="TaskDialogCollection"/>.
		/// </summary>
		public Int32 Count {
			get { return items.Count; }
		}

		/// <summary>
		/// Indicates whether this <see cref="TaskDialogCollection"/> is read-only.
		/// </summary>
		public Boolean IsReadOnly {
			get { return ((ICollection<TaskDialog>)items).IsReadOnly; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a <see cref="TaskDialog"/> instance to this <see cref="TaskDialogCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialog"/> instance to add to this
		/// <see cref="TaskDialogCollection"/>.</param>
		public void Add(TaskDialog item)
		{
			items.Add(item);
		}

		/// <summary>
		/// Removes all <see cref="TaskDialog"/> instances from this
		/// <see cref="TaskDialogCollection"/>.
		/// </summary>
		public void Clear()
		{
			items.Clear();
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialogCollection"/> contains a specific
		/// <see cref="TaskDialog"/> instance. 
		/// </summary>
		/// <param name="item">The <see cref="TaskDialog"/> instance to locate.</param>
		/// <returns>True if <paramref name="item"/> is found in this TaskDialogCollection;
		/// otherwise, False.</returns>
		public Boolean Contains(TaskDialog item)
		{
			return items.Contains(item);
		}

		/// <summary>
		/// Copies the <see cref="TaskDialog"/> instances of this <see cref="TaskDialogCollection"/>
		/// to an array, starting at the specified index.
		/// </summary>
		/// <param name="array">A one-dimensional, zero-based <see cref="TaskDialog"/> array that
		/// is the destination of the TaskDialog instances copied from this TaskDialogCollection.
		/// </param>
		/// <param name="index">A <see cref="Int32"/> that specifies the zero-based index in
		/// <paramref name="array"/> at which copying begins.</param>
		public void CopyTo(TaskDialog[] array, Int32 index)
		{
			items.CopyTo(array, index);
		}

		/// <summary>
		/// Returns an enumerator that iterates through this <see cref="TaskDialogCollection"/>.
		/// </summary>
		/// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through this
		/// <see cref="TaskDialogElementCollection{T}"/>.</returns>
		public IEnumerator<TaskDialog> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		/// <summary>
		/// Removes the first occurence of a <see cref="TaskDialog"/> instance from this
		/// <see cref="TaskDialogCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialog"/> instance to remove from this
		/// TaskDialogCollection.</param>
		/// <returns>True if <paramref name="item"/> was removed from the current instance;
		/// otherwise, False.</returns>
		public Boolean Remove(TaskDialog item)
		{
			return items.Remove(item);
		}

		#endregion

		#region Explicit Interfaces

		#region Indexer

		Object IList.this[Int32 index] {
			get { return ((IList)items)[index]; }
			set { ((IList)items)[index] = value; }
		}

		#endregion

		#region Properties

		Boolean IList.IsFixedSize {
			get { return ((IList)items).IsFixedSize; }
		}

		Boolean IList.IsReadOnly {
			get { return ((IList)items).IsReadOnly; }
		}

		Boolean ICollection.IsSynchronized {
			get { return ((ICollection)items).IsSynchronized; }
		}

		Object ICollection.SyncRoot {
			get { return ((ICollection)items).SyncRoot; }
		}

		#endregion

		#region Methods

		Int32 IList.Add(Object item)
		{
			return ((IList)items).Add(item);
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
			return ((IEnumerable)items).GetEnumerator();
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
