using System;
using System.Collections;
using System.Collections.Generic;

namespace ActionComponents
{
	/// <summary>
	/// Runs a sequence of <c>ACTileLiveUpdate</c> actions against the given
	/// <c>ACTile</c>.
	/// </summary>
	public class ACTileLiveUpdateTileSequence : ACTileLiveUpdate 
	{
		#region Private Variables
		private ACTile _tile;
		private List<ACTileLiveUpdate> _liveUpdateActions = new List<ACTileLiveUpdate>();
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the <c>ACTileLiveUpdate</c> at the
		/// specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACTileLiveUpdate this[int index]
		{
			get
			{
				return _liveUpdateActions[index];
			}

			set
			{
				_liveUpdateActions[index] = value;
			}
		}

		/// <summary>
		/// Gets the count of <c>ACTileLiveUpdate</c>s held in the sequence 
		/// </summary>
		/// <value>The count.</value>
		public int count {
			get { return _liveUpdateActions.Count;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileSequence</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public ACTileLiveUpdateTileSequence (ACTile tile)
		{
			//Initialize
			this._tile = tile;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileLiveUpdateTileSequence</c> class.
		/// </summary>
		/// <param name="tile">Tile.</param>
		/// <param name="liveUpdateActions">Live update actions.</param>
		public ACTileLiveUpdateTileSequence (ACTile tile, ACTileLiveUpdate[] liveUpdateActions)
		{
			//Initialize
			this._tile = tile;
			this._liveUpdateActions = new List<ACTileLiveUpdate> (liveUpdateActions);
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update.
		/// </summary>
		public override void PerformUpdate ()
		{
			//Process each action in this list
			foreach (ACTileLiveUpdate action in _liveUpdateActions) {
				action.PerformUpdate ();
			}
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear(){
			_liveUpdateActions.Clear ();
		}

		/// <summary>
		/// Removes the <c>ACTileLiveUpdate</c> at the given index 
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index){
			_liveUpdateActions.RemoveAt (index);
		}

		/// <summary>
		/// Adds a new <c>ACTileLiveUpdate</c> to this <c>ACTileLiveUpdateTileSequence</c>  
		/// </summary>
		/// <param name="liveUpdateAction">Live update action.</param>
		public void Add(ACTileLiveUpdate liveUpdateAction){
			_liveUpdateActions.Add (liveUpdateAction);
		}
		#endregion 
	}
}

