/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
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

namespace TaskDialogLib
{
	/// <summary>
	/// 
	/// </summary>
	public class TaskDialogCollection : IList, ICollection<TaskDialog>
	{
		#region Fields

		readonly List<TaskDialog> items;

		#endregion

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public TaskDialogCollection()
		{
			// Default initialization
			items = new List<TaskDialog>();
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
			get { return ((ICollection<TaskDialog>)items).IsReadOnly; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public void Add(TaskDialog item)
		{
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
		public Boolean Contains(TaskDialog item)
		{
			return items.Contains(item);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(TaskDialog[] array, Int32 index)
		{
			items.CopyTo(array, index);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator<TaskDialog> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
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
