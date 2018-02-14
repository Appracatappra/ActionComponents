using System;

namespace ActionComponents
{
	/// <summary>
	/// Causes a random color mutation within a given brightness range off the provided base color for every <c>ACTile</c> in the
	/// <c>ACTileGroup</c> attached to this <c>ACTileLiveUpdate</c> 
	/// </summary>
	public class ACTileLiveUpdateGroupChromaKey : ACTileLiveUpdate
	{
		#region Private Variables
		private ACTileGroup _group;
		#endregion

		#region Computed Properties
		public ACColor background { get; set;}
		public int minimum { get; set;}
		public int maximum { get; set;}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateGroupChromaKey</c> class.
		/// </summary>
		/// <param name="group">Group.</param>
		/// <param name="background">Background.</param>
		/// <param name="minimum">Minimum.</param>
		/// <param name="maximum">Maximum.</param>
		public ACTileLiveUpdateGroupChromaKey (ACTileGroup group, ACColor background, int minimum, int maximum)
		{
			//Save values
			this._group = group;
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

			//Tell group to run a chromakey on all tiles
			_group.ChromaKeyTiles (background, minimum, maximum);

		}
		#endregion 
	}
}

