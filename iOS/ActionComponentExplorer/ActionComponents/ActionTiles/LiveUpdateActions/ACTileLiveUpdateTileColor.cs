using System;
using System.Collections;
using System.Collections.Generic;

namespace ActionComponents
{
	/// <summary>
	/// Changes the background color of the <c>ActionTile.ACTile</c> 
	/// attached to this <c>ActionTile.ACTileLiveUpdate</c> action to one of the colors listed in sequence.
	/// </summary>
	public class ACTileLiveUpdateTileColor : ACTileLiveUpdate 
	{
		#region Private Variables
		private ACTile _tile;
		private List<ACColor> _colors = new List<ACColor>();
		private int _index;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the <c>color</c> at the
		/// specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACColor this[int index]
		{
			get
			{
				return _colors[index];
			}

			set
			{
				_colors[index] = value;
			}
		}

		/// <summary>
		/// Gets the count of <c>colors</c>
		/// </summary>
		/// <value>The count.</value>
		public int count{
			get { return _colors.Count;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ActionTile.ACTileLiveUpdateTileColor</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public ACTileLiveUpdateTileColor (ACTile tile)
		{
			//Initialize
			this._tile = tile;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ActionTile.ACTileLiveUpdateTileColor</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="colors">Colors.</param>
		public ACTileLiveUpdateTileColor (ACTile tile, ACColor[] colors)
		{
			//Initialize
			this._tile = tile;
			this._colors = new List<ACColor> (colors);
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate ()
		{

			//Increment pointer
			if (++_index >= _colors.Count ) _index = 0;

			//Change the color of every time in the group
			_tile.appearance.background = _colors [_index];

		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear(){
			_colors.Clear ();
		}

		/// <summary>
		/// Removes the <c>color</c> at the given index
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index){
			_colors.RemoveAt (index);
		}

		/// <summary>
		/// Add the specified color.
		/// </summary>
		/// <param name="color">Color.</param>
		public void Add(ACColor color){
			_colors.Add (color);
		}
		#endregion
	}
}

