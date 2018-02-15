using System;

namespace ActionComponents
{
	/// <summary>
	/// Controls the number of <c>rows</c> or <c>columns</c> that will appear inside a given <c>ACTileGroup</c>. 
	/// </summary>
	public class ACTileGroupCellConstraint
	{
		#region Private Variables
		private ACTileGroupCellConstraintType _constraintType;
		private int _maximum;
		#endregion 

		#region Calculated Properties
		/// <summary>
		/// Gets or sets the type of the <c>ACTileGroupCellConstriantType</c>
		/// </summary>
		/// <value>The type of the constraint.</value>
		public ACTileGroupCellConstraintType constraintType{
			get { return _constraintType;}
			set {
				_constraintType = value;
				RaiseConstraintChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the maximum number of <c>rows</c> or <c>columns</c> based on the given <c>ACTileGroupCellConstraintType</c> 
		/// </summary>
		/// <value>The maximum.</value>
		public int maximum {
			get { return _maximum;}
			set {
				_maximum = value;
				RaiseConstraintChanged ();
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileGroupCellConstraint</c> class.
		/// </summary>
		public ACTileGroupCellConstraint ()
		{
			//Set initial values
			this._constraintType = ACTileGroupCellConstraintType.Flexible;
			this._maximum=4;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileGroupCellConstraint</c> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="maximum">Maximum.</param>
		public ACTileGroupCellConstraint (ACTileGroupCellConstraintType type, int maximum)
		{
			//Set initial values
			this._constraintType = type;
			this._maximum=maximum;
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when a value on this constraint has been changed
		/// </summary>
		public delegate void ConstraintChangedDelegate();
		public event ConstraintChangedDelegate ConstraintChanged;

		/// <summary>
		/// Raises the constraint changed event
		/// </summary>
		private void RaiseConstraintChanged(){
			if (this.ConstraintChanged != null) this.ConstraintChanged ();
		}
		#endregion 
	}
}

