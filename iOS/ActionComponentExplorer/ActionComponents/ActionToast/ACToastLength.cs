using System;

namespace ActionComponents
{
	/// <summary>
	/// Provides a alternative way to specify how long a <see cref="ActionComponents.ACToast"/> message will be displayed 
	/// </summary>
	public enum ACToastLength
	{
		/// <summary>
		/// The <see cref="ActionComponents.ACToast"/> message will be displayed until the user touches it or it is
		/// removed programatically
		/// </summary>
		Forever,
		/// <summary>
		/// The <see cref="ActionComponents.ACToast"/> message will be displayed for a short amount of time
		/// </summary>
		Short,
		/// <summary>
		/// The <see cref="ActionComponents.ACToast"/> message will be displayed for a medium amount of time
		/// </summary>
		Medium,
		/// <summary>
		/// The <see cref="ActionComponents.ACToast"/> message will be displayed for a long amount of time
		/// </summary>
		Long
	}
}

