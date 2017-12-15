using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace ActionComponents
{
	public class ACAlertOverlay : RelativeLayout
	{
		#region Private variables
		private ACAlert _alert;
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertOverlay"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACAlertOverlay(Context context)
			: base(context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertOverlay"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="alert">Alert.</param>
		public ACAlertOverlay(Context context, ACAlert alert)
			: base(context)
		{
			//Save parent alert
			_alert = alert;

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertOverlay"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACAlertOverlay(Context context, Display display)
			: base(context)
		{

			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACAlertOverlay"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACAlertOverlay(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{

			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize() {
			bool isModal = (_alert != null) ? _alert.modal : true;
			Color overlay;

			//Is this a modal dialog?
			if (isModal) {
				//Yes, Red just for testing
				overlay=Color.Argb (128,0,0,0);
			} else {
				//No, make totally clear
				overlay=Color.Argb (0,0,0,0);
			}

			//Make background clear
			this.SetBackgroundColor (overlay);
			this.Clickable=isModal; // Controls whether or not this eats click events

		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Removes this view from its parent view
		/// </summary>
		public void RemoveFromSuperview(){
			//Nab the parent, cast it into a ViewGroup and remove self
			((ViewGroup)this.Parent).RemoveView(this);
		}

		/// <summary>
		/// Adjusts the modality of this overlay
		/// </summary>
		/// <param name="isModal">If set to <c>true</c> is modal.</param>
		public void AdjustModality(bool isModal){
			Color overlay;

			//Is this a modal dialog?
			if (isModal) {
				//Yes, Red just for testing
				overlay=Color.Argb (128,255,0,0);
			} else {
				//No, make totally clear
				overlay=Color.Argb (0,0,0,0);
			}

			//Make background clear
			this.SetBackgroundColor (overlay);
			this.Clickable=isModal; // Controls whether or not this eats click events
		}

		/// <summary>
		/// Adjusts the appearance.
		/// </summary>
		public void AdjustAppearance() {
			Color overlay = _alert.appearance.overlay;
			overlay.A=(byte)_alert.appearance.overlayAlpha;

			this.SetBackgroundColor (overlay);
		}
		#endregion 

		#region Override Methods
		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnTouchEvent (MotionEvent e)
		{

			//Take action based on the event type
			switch(e.Action){
			case MotionEventActions.Down:
				//Inform caller of event
				RaiseTouched ();

				//Is this a modal alert?
				if (_alert.modal) {
					//Inform system that we've handled this event 
					return true;
				}
				break;
			case MotionEventActions.Move:
				break;
			case MotionEventActions.Up: 
				break;
			case MotionEventActions.Cancel:
				break;
			}

			// Not handled by this routine
			return base.OnTouchEvent(e);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACAlertOverlay"/> is touched 
		/// </summary>
		public delegate void ACAlertOverlayTouchedDelegate ();
		public event ACAlertOverlayTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched ();
		}
		#endregion 
	}
}

