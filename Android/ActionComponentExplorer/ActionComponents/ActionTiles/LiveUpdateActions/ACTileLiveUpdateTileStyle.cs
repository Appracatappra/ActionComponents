using System;

namespace ActionComponents
{
	/// <summary>
	/// Cycles through all of the <c>ACTileStyle</c>s for the given
	/// <c>ACTile</c> attached to this <c>ACTileLiveAction</c> 
	/// </summary>
	public class ACTileLiveUpdateTileStyle : ACTileLiveUpdate 
	{
		#region Private Variables
		/// <summary>
		/// The Parent tile.
		/// </summary>
		private ACTile _tile;

		/// <summary>
		/// The index for the current tile.
		/// </summary>
		private int _index;
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileStyle</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public ACTileLiveUpdateTileStyle (ACTile tile)
		{
			Random rnd = new Random();

			//Initialize
			this._tile = tile;
			this._index = rnd.Next (0, 3);
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileStyle</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="styleIndex">Style index.</param>
		public ACTileLiveUpdateTileStyle (ACTile tile, int styleIndex)
		{

			//Validate
			if (styleIndex < 0)
				styleIndex = 0;
			if (styleIndex > 3)
				styleIndex = 3;

			//Initialize
			this._tile = tile;
			this._index = styleIndex;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate ()
		{
			//Increment index
			if (++_index > 3)
				_index = 0;

			//Take action based on the current index
			switch (_index) {
			case 0:
				_tile.style = ACTileStyle.CornerIcon;
				break;
			case 1:
				_tile.style = ACTileStyle.Default;
				break;
			case 2:
				_tile.style = ACTileStyle.DescriptionBlock;
				break;
			case 3:
				_tile.style = ACTileStyle.TopTitle;
				break;
			}
		}
		#endregion 
	}
}

