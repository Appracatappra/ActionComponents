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
	/// Mutates the image of a selected <c>ACTile</c> by cycling through a list of provided images.
	/// </summary>
	public class ACTileLiveUpdateTileImages : ACTileLiveUpdate 
	{
		#region Private Variables
		private ACTile _tile;
		private List<UIImage> _images; 
		private int _index = 0;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the <c>string</c> or <c>UIImage</c> at the
		/// specified index based on which was passed when the <c>ACTileLiveUpdate</c> was configured
		/// </summary>
		/// <param name="index">Index.</param>
		public object this[int index]
		{
			get
			{
				return _images[index];
			}

			set
			{
				_images[index] = (UIImage)value;
			}
		}

		/// <summary>
		/// Gets the number of filenames or images
		/// </summary>
		/// <value>The count.</value>
		public int count {
			get { 
				return _images.Count;
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileImages</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="images">Images.</param>
		public ACTileLiveUpdateTileImages (ACTile tile, UIImage[] images)
		{
			//Save values
			this._tile = tile;
			this._images = new List<UIImage> (images);

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate(){

			try {
				//Increment pointer
				if (++_index >= _images.Count) _index = 0;

				//Update image
				_tile.icon = _images[_index];
			}
			catch {
				//Just ignore for now
			}

		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear(){
			_images.Clear();
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index){
			_images.RemoveAt(index);
		}

		/// <summary>
		/// Add the specified image.
		/// </summary>
		/// <param name="image">Image.</param>
		public void Add(UIImage image){
			_images.Add (image);
		}
		#endregion 
	}
}

