using System;

namespace ActionComponents
{
	/// <summary>
	/// Defines the <see cref="ActionComponents.ACViewDragConstraintType"/> of constraint placed on the <c>X</c> or <c>Y</c> axis of
	/// a <see cref="ActionComponents.ACView"/> and the optional <c>minimumValue</c> and <c>maximumValue</c> for <c>Constrained</c>
	/// axis types
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public class ACViewDragConstraint
	{
		#region Private Variables
		private ACViewDragConstraintType _constraintType;
		private int _minimumValue;
		private int _maximumValue;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACViewDragConstraintType"/> of the
		/// <see cref="ActionComponents.ACView"/> 
		/// </summary>
		/// <value>The type of the constraint.</value>
		public ACViewDragConstraintType constraintType{
			get { return _constraintType;}
			set {
				_constraintType = value;
				RaiseConstraintChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the minimum value that this axis can be moved to if the <see cref="ActionComponents.ACViewDragConstraintType"/>
		/// is <c>Constrained</c>
		/// </summary>
		/// <value>The minimum value.</value>
		public int minimumValue{
			get { return _minimumValue;}
			set {
				_minimumValue = value;
				RaiseConstraintChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the maximum value that this axis can be moved to if the <see cref="ActionComponents.ACViewDragConstraintType"/>
		/// is <c>Constrained</c>
		/// </summary>
		/// <value>The maximum value.</value>
		public int maximumValue{
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
		/// <see cref="ActionComponents.ACViewDragConstraint"/> class.
		/// </summary>
		public ACViewDragConstraint () {
			this._constraintType=ACViewDragConstraintType.None;
			this._minimumValue=0;
			this._maximumValue=1000000;
		}

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ActionComponents.ACViewDragConstraint"/> class.
		/// </summary>
		/// <param name="constraint">The default <see cref="ActionComponents.ACViewDragConstraintType"/> </param>
		/// <param name="minimum">Minimum axis value</param>
		/// <param name="maximum">Maximum axis value</param>
		public ACViewDragConstraint(ACViewDragConstraintType constraint, int minimum, int maximum) {
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

