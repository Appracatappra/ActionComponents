using System;

namespace ActionComponents
{
	/// <summary>
	/// User interface action view drag constraint type.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only.</remarks>
	public enum ACViewDragConstraintType
	{
		/// <summary>
		/// No constraint, this axis can be moved to any position
		/// </summary>
		None,
		/// <summary>
		/// This axis is locked to it's current position and cannot be moved
		/// </summary>
		Locked,
		/// <summary>
		/// This axis is constrained between a <c>minimalValue</c> and <c>maximumValue</c> specified by the user
		/// </summary>
		Constrained
	}
}

