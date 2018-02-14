using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the <c>ACTileDragConstraintType</c> of constraint placed on the <c>X</c> or <c>Y</c> axis of
	/// a <c>ACTile</c> and the optional <c>minimumValue</c> and <c>maximumValue</c> for <c>Constrained</c>
	/// axis types
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACTileDragConstraint
	{
		#region Private Variables
		private ACTileDragConstraintType _constraintType;
		private float _minimumValue;
		private float _maximumValue;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the <c>ACTileDragConstraintType</c> of the
		/// <c>ACTile</c> 
		/// </summary>
		/// <value>The type of the constraint.</value>
		public ACTileDragConstraintType constraintType{
			get { return _constraintType;}
			set {
				_constraintType = value;
				RaiseConstraintChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the minimum value that this axis can be moved to if the <c>ACTileDragConstraintType</c>
		/// is <c>Constrained</c>
		/// </summary>
		/// <value>The minimum value.</value>
		public float minimumValue{
			get { return _minimumValue;}
			set {
				_minimumValue = value;
				RaiseConstraintChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the maximum value that this axis can be moved to if the <c>ACTileDragConstraintType</c>
		/// is <c>Constrained</c>
		/// </summary>
		/// <value>The maximum value.</value>
		public float maximumValue{
			get { return _maximumValue;}
			set {
				_maximumValue = value;
				RaiseConstraintChanged ();
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileDragConstraint</c> class.
		/// </summary>
		public ACTileDragConstraint () {
			this._constraintType=ACTileDragConstraintType.None;
			this._minimumValue=0f;
			this._maximumValue=1000000f;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <c>ACTileDragConstraint</c> class.
		/// </summary>
		/// <param name="constraint">The default <c>ACTileDragConstraintType</c> </param>
		/// <param name="minimum">Minimum axis value</param>
		/// <param name="maximum">Maximum axis value</param>
		public ACTileDragConstraint(ACTileDragConstraintType constraint, float minimum, float maximum) {
			this._constraintType=constraint;
			this._minimumValue=minimum;
			this._maximumValue=maximum;
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

