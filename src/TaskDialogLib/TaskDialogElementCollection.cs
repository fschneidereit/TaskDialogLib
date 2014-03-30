/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
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

namespace TaskDialogLib
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
		/// 
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
		/// 
		/// </summary>
		public Int32 Count {
			get { return items.Count; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Boolean IsReadOnly {
			get { return false; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Add(T item)
		{
			// Add to collection
			items.Add(item);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			items.Clear();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public Boolean Contains(T item)
		{
			return items.Contains(item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(T[] array, Int32 index)
		{
			items.CopyTo(array, index);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public Boolean Remove(T item)
		{
			return items.Remove(item);
		}

		#endregion

		#region Methods: Implementation

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
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
