using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation
{
	/// <summary>
	/// Represents a simulated iOS <c>CGContext</c> used to ease the porting of UI code from iOS to Android. Use a <c>NSSet</c>
	/// to maintain an unordered collection of objects.
	/// </summary>
	public class NSSet : IEnumerator, IEnumerable
	{
		#region Public Properties
		public List<object> Objects { get; set; } = new List<object>();

		/// <summary>
		/// Gets the first avaialble object in the collection.
		/// </summary>
		/// <value>The first object if available, else <c>null</c>.</value>
		public object AnyObject {
			get {
				if (Count > 0) {
					return Objects[0];
				} else {
					return null;
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Foundation.NSSet"/> class.
		/// </summary>
		public NSSet()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Foundation.NSSet"/> class.
		/// </summary>
		/// <param name="item">Item.</param>
		public NSSet(object item) {
			Objects.Add(item);
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Gets or sets the object at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public object this[int index]
		{
			get
			{
				return Objects[index];
			}

			set
			{
				Objects[index] = value;
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return Objects.Count; }
		}
		#endregion

		#region Enumerable Routines
		/// <summary>
		/// The current position.
		/// </summary>
		private int _position = -1;

		// IEnumerator and IEnumerable require these methods.
		public IEnumerator GetEnumerator()
		{
			_position = -1;
			return (IEnumerator)this;
		}

		// IEnumerator
		public bool MoveNext()
		{
			_position++;
			return (_position < Count);
		}

		// IEnumerator
		public void Reset()
		{ _position = -1; }

		// IEnumerator
		public object Current
		{
			get
			{
				try
				{
					return Objects[_position];
				}

				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add(object item) {
			Objects.Add(item);
		}

		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Remove(object item) {
			Objects.Remove(item);
		}

		/// <summary>
		/// Removes at n.
		/// </summary>
		/// <param name="n">N.</param>
		public void RemoveAt(int n) {
			Objects.RemoveAt(n);
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear() {
			Objects.Clear();
		}
		#endregion
	}
}
