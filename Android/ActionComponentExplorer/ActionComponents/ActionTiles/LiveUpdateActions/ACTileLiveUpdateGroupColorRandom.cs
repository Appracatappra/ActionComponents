using System;
using System.Collections;
using System.Collections.Generic;

namespace ActionComponents
{
	/// <summary>
	/// Changes the background color of every <c>ACTile</c> in the <c>ACTileGroup</c>
	/// attached to this <c>ACTileLiveUpdate</c> action to one of the colors listed in sequence.
	/// </summary>
	public class ACTileLiveUpdateGroupColorRandom : ACTileLiveUpdate 
	{
		#region Private Variables
		/// <summary>
		/// The parent group.
		/// </summary>
		private ACTileGroup _group;

		/// <summary>
		/// The colors to cycle through.
		/// </summary>
		private List<ACColor> _colors = new List<ACColor>();
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
		/// <c>ACTileLiveUpdateGroupColor</c> class.
		/// </summary>
		/// <param name="group">Group.</param>
		public ACTileLiveUpdateGroupColorRandom (ACTileGroup group)
		{
			//Initialize
			this._group = group;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateGroupColor</c> class.
		/// </summary>
		/// <param name="group">Group.</param>
		/// <param name="colors">Colors.</param>
		public ACTileLiveUpdateGroupColorRandom (ACTileGroup group, ACColor[] colors)
		{
			//Initialize
			this._group = group;
			this._colors = new List<ACColor> (colors);
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate ()
		{

			Random rnd = new Random();


			try {
				var color = _colors[rnd.Next(0, _colors.Count)];
				_group.SetTileColor(color);
			}
			catch {
				//Ignore errors
			}

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

