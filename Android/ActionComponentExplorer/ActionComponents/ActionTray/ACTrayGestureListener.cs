using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ActionComponents
{
	internal partial class ACTrayGestureListener : GestureDetector.SimpleOnGestureListener, GestureDetector.IOnDoubleTapListener
	{
		#region Private Variables
		private ACTray _actionTray;
		#endregion

		#region Constructors
		public ACTrayGestureListener (ACTray actionTray)
		{
			//Save parent ActionTray
			_actionTray=actionTray;
		}
		#endregion 

		#region Override Methods
		public override bool OnDoubleTap (MotionEvent e)
		{
			//Inform parent tray that it's been double tapped
			_actionTray.DoubleTapped((int)e.GetX (),(int)e.GetY ());

			return true;
		}

		public override bool OnSingleTapConfirmed (MotionEvent e)
		{
			return false;
		}
		#endregion
	}
}

