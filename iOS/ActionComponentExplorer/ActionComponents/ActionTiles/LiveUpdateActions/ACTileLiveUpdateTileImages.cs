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
		private List<string> _filenames;
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
				if (_images == null) {
					return _filenames [index];
				} else {
					return _images [index];
				}
			}

			set
			{
				if (_images == null) {
					_filenames [index] = (string)value;
				} else {
					_images [index] = (UIImage)value;
				}
			}
		}

		/// <summary>
		/// Gets the number of filenames or images
		/// </summary>
		/// <value>The count.</value>
		public int count {
			get { 
				if (_images == null) {
					return _filenames.Count;
				} else {
					return _images.Count;
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileImages</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="filenames">Filenames.</param>
		public ACTileLiveUpdateTileImages (ACTile tile, string[] filenames)
		{
			//Save values
			this._tile = tile;
			this._filenames = new List<string> (filenames);

		}

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
				//Images or filenames?
				if (_images ==null) {
					//Increment pointer
					if (++_index >= _filenames.Count ) _index = 0;

					//Update image
					_tile.icon = UIImage.FromBundle(_filenames[_index]);
				} else {
					//Increment pointer
					if (++_index >= _images.Count ) _index = 0;

					//Update image
					_tile.icon = _images[_index];
				}
			}
			catch {
				//Just ignore for now
			}

		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear(){
			if (_images == null) {
				_filenames.Clear ();
			} else {
				_images.Clear ();
			}
		}

		/// <summary>
		/// Removes at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index){
			if (_images == null) {
				_filenames.RemoveAt (index);
			} else {
				_images.RemoveAt (index);
			}
		}

		/// <summary>
		/// Add the specified filename.
		/// </summary>
		/// <param name="filename">Filename.</param>
		public void Add(string filename){
			_filenames.Add (filename);
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

