using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Changes the title, subtitle and description for the given <c>ACTile</c> 
	/// </summary>
	public class ACTileLiveUpdateTileText : ACTileLiveUpdate 
	{
		#region Private Variables
		private ACTile _tile;
		private List<string> _titles;
		private List<string> _subtitles;
		private List<string> _descriptions;
		private int _index = 0;
		#endregion

		#region Computed Properties
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileText</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="titles">Titles.</param>
		/// <param name="subtitles">Subtitles.</param>
		/// <param name="descriptions">Descriptions.</param>
		public ACTileLiveUpdateTileText (ACTile tile, string[] titles, string[] subtitles, string[] descriptions)
		{
			//Save values
			this._tile = tile;
			this._titles = new List<string> (titles);
			this._subtitles = new List<string> (subtitles);
			this._descriptions = new List<string> (descriptions);

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate(){

			try {
				//Increment pointer
				if (++_index >= _titles.Count ) _index = 0;

				_tile.title = _titles[_index];
				_tile.subtitle = _subtitles[_index];
				_tile.description = _descriptions[_index];
			}
			catch {
				//Just ignore for now
			}

		}
		#endregion 
	}
}

