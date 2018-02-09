using System;
using System.Drawing;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Defines the base prototype for all <c>ACTileLiveUpdate</c> types
	/// </summary>
	public class ACTileLiveUpdate
	{
		#region Private Variables
		#endregion

		#region Computed Properties
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACTileLiveUpdate</c> class.
		/// </summary>
		public ACTileLiveUpdate ()
		{
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Performs the update on the given <c>ACTile</c> or <c>ACTileGroup</c> 
		/// this <c>ACTileLiveUpdate</c> is attached to 
		/// </summary>
		public virtual void PerformUpdate(){

		}
		#endregion 
	}
}

