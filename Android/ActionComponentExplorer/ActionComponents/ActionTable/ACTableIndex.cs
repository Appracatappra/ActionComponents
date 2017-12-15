using System;

namespace ActionComponents
{
	/// <summary>
	/// Index used to translate a <c>ListView</c> <c>position</c> into an <see cref="ActionComponents.ACTableViewController"/> 
	/// <see cref="ActionComponents.ACTableSection"/> and <see cref="ActionComponents.ACTableItem"/> index.  
	/// </summary>
	/// <remarks>An <c>itemPosition</c> of <c>-1</c> indicates the <c>Header</c> of a <see cref="ActionComponents.ACTableSection"/> and a value of
	/// <c>-2</c> indicates the <c>Footer</c>. Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTableIndex
	{
		#region Private Variables
		private int _sectionPosition = 0;
		private int _itemPosition = 0;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the section position.
		/// </summary>
		/// <value>The section position.</value>
		public int sectionPosition{
			get{ return _sectionPosition;}
			set{ _sectionPosition = value;}
		}

		/// <summary>
		/// Gets or sets the item position.
		/// </summary>
		/// <value>The item position.</value>
		/// <remarks>A position of <c>-1</c> indicates the <c>Header</c> of a <see cref="ActionComponents.ACTableSection"/> and a value of
		/// <c>-2</c> indicates the <c>Footer</c>.</remarks>
		public int itemPosition{
			get{ return _itemPosition;}
			set{ _itemPosition = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTableIndex"/>
		/// is a <see cref="ActionComponents.ACTableSection"/> header.
		/// </summary>
		/// <value><c>true</c> if is header; otherwise, <c>false</c>.</value>
		public bool isHeader{
			get{ return (_itemPosition == -1);}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponents.ACTableIndex"/>
		/// is a <see cref="ActionComponents.ACTableSection"/> footer.
		/// </summary>
		/// <value><c>true</c> if is footer; otherwise, <c>false</c>.</value>
		public bool isFooter{
			get{ return (_itemPosition == -2);}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableIndex"/> class.
		/// </summary>
		public ACTableIndex ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTableIndex"/> class.
		/// </summary>
		/// <param name="sectionPosition">Section position.</param>
		/// <param name="itemPosition">Item position.</param>
		public ACTableIndex (int sectionPosition, int itemPosition)
		{
			//Initialize
			this._sectionPosition = sectionPosition;
			this._itemPosition = itemPosition;
		}
		#endregion 

	}
}

