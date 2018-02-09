using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Mutates the color of the given <c>ACTile</c> attached to this <c>ACTileLiveUpdate</c>
	/// with in the given brightness range for the given base color. 
	/// </summary>
	public class ACTileLiveUpdateTileChromaKey : ACTileLiveUpdate 
	{
		#region Private Variables
		private ACTile _tile;
		#endregion

		#region Computed Properties
		public ACColor background { get; set;}
		public int minimum { get; set;}
		public int maximum { get; set;}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileChromaKey</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="background">Background.</param>
		/// <param name="minimum">Minimum.</param>
		/// <param name="maximum">Maximum.</param>
		public ACTileLiveUpdateTileChromaKey (ACTile tile, ACColor background, int minimum, int maximum)
		{
			//Save values
			this._tile = tile;
			this.background = background;
			this.minimum = minimum;
			this.maximum = maximum;

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate(){

			_tile.ChromaKeyTile(background, minimum, maximum);

		}
		#endregion 
	}
}

