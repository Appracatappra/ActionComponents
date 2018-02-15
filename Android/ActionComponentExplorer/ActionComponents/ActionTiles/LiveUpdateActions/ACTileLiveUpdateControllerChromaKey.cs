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
	public class ACTileLiveUpdateControllerChromaKey : ACTileLiveUpdate 
	{
		#region Private Variables
		/// <summary>
		/// The parent tile controller.
		/// </summary>
		private ACTileController _controller;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		/// <value>The background.</value>
		public ACColor background { get; set;}

		/// <summary>
		/// Gets or sets the minimum hue variance.
		/// </summary>
		/// <value>The minimum.</value>
		public int minimum { get; set;}

		/// <summary>
		/// Gets or sets the maximum hue variance.
		/// </summary>
		/// <value>The maximum.</value>
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
		public ACTileLiveUpdateControllerChromaKey (ACTileController controller, ACColor background, int minimum, int maximum)
		{
			//Save values
			this._controller = controller;
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

			// Update the tiles in every group of this controller
			foreach(ACTileGroup group in _controller.groups) {
				group.ChromaKeyTiles(background, minimum, maximum);
			}

		}
		#endregion 
	}
}

