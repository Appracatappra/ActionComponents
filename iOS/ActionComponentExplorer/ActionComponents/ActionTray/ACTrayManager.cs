using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACTrayManager"/> controls a collection of <see cref="ActionComponents.ACTray"/>s and manages them like
	/// a set of palettes or menus. Only one <see cref="ActionComponents.ACTray"/> in the collection can be open at one time. The
	/// <see cref="ActionComponents.ACTrayManager"/> defines events that can be handled based on the user's interaction with any <see cref="ActionComponents.ACTray"/> 
	/// in the group.
	/// </summary>
	/// <remarks>The <see cref="ActionComponents.ACTrayManager"/> will automatically close any open <see cref="ActionComponents.ACTray"/> when another tray
	/// in the group is opened.</remarks>
	[Register("ACTrayManager")]
	public class ACTrayManager
	{
		#region Private Variables
		private List<ACTray> _trays;
		private float _spacer = 10F;
		private ACTrayTabLocation _tabLocation = ACTrayTabLocation.Custom;
		private ACTrayOrientation _trayOrientation = ACTrayOrientation.Top;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Returns the number of <see cref="ActionComponents.ACTray"/>s that this <see cref="ActionComponents.ACTrayManager"/> is controlling
		/// </summary>
		/// <value>The count.</value>
		public int count{
			get{return _trays.Count;}
		}

		/// <summary>
		/// Returns the collection of <see cref="ActionComponents.ACTray"/>s that this <see cref="ActionComponents.ACTrayManager"/> is controlling  
		/// </summary>
		/// <value>The trays.</value>
		public List<ACTray> trays{
			get{return _trays;}
		}

		/// <summary>
		/// Gets or sets the tab spacer.
		/// </summary>
		/// <value>The tab spacer.</value>
		public float TabSpacer {
			get { return _spacer; }
			set {
				// Save value
				_spacer = value;

				// Update layout
				LayoutTrays();
			}
		}

		/// <summary>
		/// Gets or sets the tray orientation.
		/// </summary>
		/// <value>The tray orientation.</value>
		public ACTrayOrientation TrayOrientation {
			get { return _trayOrientation; }
			set {
				// Save value
				_trayOrientation = value;

				// Set all trays to the same orientation
				foreach(ACTray tray in trays) {
					tray.orientation = _trayOrientation;
				}
			}
		}

		/// <summary>
		/// Gets or sets the tab location.
		/// </summary>
		/// <value>The tab location.</value>
		public ACTrayTabLocation TabLocation {
			get { return _tabLocation; } 
			set {
				// Save value
				_tabLocation = value;

				// Set the tab location for all trays
				if (_tabLocation != ACTrayTabLocation.Custom) {
					foreach(ACTray tray in trays) {
						tray.tabLocation = _tabLocation;
					}
				}

				// Adjust layout
				LayoutTrays();
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACTrayManager"/> class.
		/// </summary>
		public ACTrayManager ()
		{
			//Initialize
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Build storage
			_trays = new List<ACTray> ();
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Adds the <see cref="ActionComponents.ACTray"/> to this <see cref="ActionComponents.ACTrayManager"/>'s
		/// collection
		/// </summary>
		/// <param name="tray">The <see cref="ActionComponents.ACTray"/> to add </param>
		public void AddTray(ACTray tray){

			//Make this tray automatically become the top view
			//when it is touched and ensure it is initially closed
			tray.bringToFrontOnTouch=true;
			tray.CloseTray (false);

			//Wireup a listner to respond to touches for a tray
			//in this collection
			tray.Touched+= (t) => {
				//Close any other open tray in this collection
				CloseOtherOpenTrays (t);

				//Inform caller
				RaiseTrayTouched(t);
			};

			//Wireup master moved event
			tray.Moved+= (t) => {
				//Inform caller
				RaiseTrayMoved(t);
			};

			//Wireup master released event
			tray.Released+= (t) => {
				//Inform caller
				RaiseTrayReleased(t);
			};

			//Wireup a master opened event
			tray.Opened+= (t) => {
				//Raise the master opened event on this collection
				RaiseTrayOpened (t);
			};

			//Wireup master closed event
			tray.Closed+= (t) => {
				//Inform caller
				RaiseTrayClosed (t);
			};

			// Wireup resize and location events
			tray.TraySizeChanged += (t, s) =>
			{
				// Adjust tray layouts
				LayoutTrays();
			};

			tray.TrayLocationChanged += (t, l) =>
			{
				// Adjust tray layouts
				LayoutTrays();
			};

			//Add tray to collection
			_trays.Add (tray);

			// Adjust layout
			LayoutTrays();
		}

		/// <summary>
		/// Returns the requested <see cref="ActionComponents.ACTray"/> at the given <c>index</c> from this 
		/// <see cref="ActionComponents.ACTrayManager"/>'s collection 
		/// </summary>
		/// <param name="index">Index.</param>
		/// <returns>The requested <see cref="ActionComponents.ACTray"/> or <c>null</c> if the <c>index</c> is out of range</returns>
		public ACTray Tray(int index){

			//Valid index?
			if (index < 0 || index > (_trays.Count - 1))
				return null;

			//Return requested tray from the collection
			return _trays [index];
		
		}

		/// <summary>
		/// Removes the <see cref="ActionComponents.ACTray"/> at the given <c>index</c> from this <see cref="ActionComponents.ACTrayManager"/>'s
		/// collection
		/// </summary>
		/// <param name="index">Index.</param>
		/// <remarks>This method is ignored if the <c>index</c> is out of range</remarks>
		public void RemoveTray(int index){

			//Valid index?
			if (index < 0 || index > (_trays.Count - 1))
				return;

			//Remove the tray from the collection
			_trays.RemoveAt (index);
		}

		/// <summary>
		/// Closes all open <see cref="ActionComponents.ACTray"/> in this <see cref="ActionComponents.ACTrayManager"/>'s collection  
		/// </summary>
		public void CloseAllTrays(){

			//Ask all trays in the collection to close
			foreach (ACTray tray in _trays) {
				tray.CloseTray (true);
			}
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Closes any other open <see cref="ActionComponents.ACTray"/>s in this <see cref="ActionComponents.ACTrayManager"/>
	    /// collection when another tray in this collection is opened or moved.
		/// </summary>
		/// <param name="tray">Tray.</param>
		private void CloseOtherOpenTrays(ACTray tray){

			//Look at all siblings
			foreach (ACTray sibling in _trays) {
				//Is it the current tray?
				if (sibling!=tray) sibling.CloseTray (true);
			}

		}

		/// <summary>
		/// Laysout the tray from left to right.
		/// </summary>
		private void LayoutLeftToRight() {
			nfloat x = 0f;

			// Process all trays
			foreach(ACTray tray in trays) {
				var tabOffset = tray.TabArea.X;
				var trayX = x - tabOffset;

				// Valid?
				if (trayX < 0f) trayX = 0f;

				// Move tray
				tray.Frame = new CGRect(trayX, tray.Frame.Y, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				x = tray.Frame.X + tray.TabArea.X + tray.TabArea.Width + TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays from top to bottom.
		/// </summary>
		private void LayoutTopToBottom()
		{
			nfloat y = 0f;

			// Process all trays
			foreach (ACTray tray in trays)
			{
				var tabOffset = tray.TabArea.Y;
				var trayY = y - tabOffset;

				// Valid?
				if (trayY < 0f) trayY = 0f;

				// Move tray
				tray.Frame = new CGRect(tray.Frame.X, trayY, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				y = tray.Frame.Y + tray.TabArea.Y + tray.TabArea.Height + TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays centered horizontally.
		/// </summary>
		private void LayoutCenterHorizontal() {
			nfloat width = 0f;

			// Calculate widths
			foreach(ACTray tray in trays) {
				width += tray.TabArea.Width + TabSpacer;
			}

			// Calculate the starting position
			nfloat x = (iOSDevice.AvailableScreenBounds.Width / 2f) - (width / 2f);

			// Process all trays
			foreach (ACTray tray in trays)
			{
				var tabOffset = tray.TabArea.X;
				var trayX = x - tabOffset;

				// Valid?
				if (trayX < 0f) trayX = 0f;

				// Move tray
				tray.Frame = new CGRect(trayX, tray.Frame.Y, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				x = tray.Frame.X + tray.TabArea.X + tray.TabArea.Width + TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays centered vertically.
		/// </summary>
		private void LayoutCenterVertical()
		{
			nfloat height = 0f;

			// Calculate widths
			foreach (ACTray tray in trays)
			{
				height += tray.TabArea.Height + TabSpacer;
			}

			// Calculate the starting position
			nfloat y = (iOSDevice.AvailableScreenBounds.Height / 2f) - (height / 2f);

			// Process all trays
			foreach (ACTray tray in trays)
			{
				var tabOffset = tray.TabArea.Y;
				var trayY = y - tabOffset;

				// Valid?
				if (trayY < 0f) trayY = 0f;

				// Move tray
				tray.Frame = new CGRect(tray.Frame.X, trayY, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				y = tray.Frame.Y + tray.TabArea.Y + tray.TabArea.Height + TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays right to left.
		/// </summary>
		private void LayoutRightToLeft() {
			nfloat x = iOSDevice.AvailableScreenBounds.Width;

			// Process all trays
			for (int n = count - 1; n >= 0; --n) {
				var tray = trays[n];
				var tabOffset = tray.TabArea.X + tray.TabArea.Width;
				var trayX = x - tabOffset;

				// Valid?
				if (trayX + tray.Frame.Width > iOSDevice.AvailableScreenBounds.Width) trayX = iOSDevice.AvailableScreenBounds.Width - tray.Frame.Width;

				// Move tray
				tray.Frame = new CGRect(trayX, tray.Frame.Y, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				x = tray.Frame.X + tray.TabArea.X - TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays from top to bottom.
		/// </summary>
		private void LayoutBottomToTop()
		{
			nfloat y = iOSDevice.AvailableScreenBounds.Height;

			// Process all trays
			for (int n = count - 1; n >= 0; --n)
			{
				var tray = trays[n];
				var tabOffset = tray.TabArea.Y + tray.TabArea.Height;
				var trayY = y - tabOffset;

				// Valid?
				if (trayY + tray.Frame.Height > iOSDevice.AvailableScreenBounds.Height) trayY = iOSDevice.AvailableScreenBounds.Height - tray.Frame.Height;

				// Move tray
				tray.Frame = new CGRect(tray.Frame.X, trayY, tray.Frame.Width, tray.Frame.Height);

				// Calculate next position
				y = tray.Frame.Y + tray.TabArea.Y - TabSpacer;
			}
		}

		/// <summary>
		/// Laysout the trays based on the tray orientation and the tab location.
		/// </summary>
		private void LayoutTrays() {
			// Anything to process?
			if (TabLocation == ACTrayTabLocation.Custom) return;

			// Take action based on the location of the trays inside the window
			switch(TrayOrientation) {
				case ACTrayOrientation.Top:
				case ACTrayOrientation.Bottom:
					// Take action based on the tab location
					switch(TabLocation) {
						case ACTrayTabLocation.TopOrLeft:
							LayoutLeftToRight();
							break;
						case ACTrayTabLocation.Middle:
							LayoutCenterHorizontal();
							break;
						case ACTrayTabLocation.BottomOrRight:
							LayoutRightToLeft();
							break;
					}
					break;
				case ACTrayOrientation.Left:
				case ACTrayOrientation.Right:
					// Take action based on the tab location
					switch (TabLocation)
					{
						case ACTrayTabLocation.TopOrLeft:
							LayoutTopToBottom();
							break;
						case ACTrayTabLocation.Middle:
							LayoutCenterVertical();
							break;
						case ACTrayTabLocation.BottomOrRight:
							LayoutBottomToTop();
							break;
					}
					break;
			}
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is touched
		/// </summary>
		public delegate void ACTrayTouchDelegate(ACTray tray);
		public event ACTrayTouchDelegate TrayTouched;
		
		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTrayTouched(ACTray tray){
			//Inform caller
			if (this.TrayTouched != null)
				this.TrayTouched (tray);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is moved
		/// </summary>
		public delegate void ACTrayMovedDelegate(ACTray tray);
		public event ACTrayMovedDelegate TrayMoved;
		
		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseTrayMoved(ACTray tray){
			//Inform caller
			if(this.TrayMoved!=null) 
				this.TrayMoved(tray);
		}
		
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> was <c>Touched</c> and released 
		/// </summary>
		public delegate void ACTrayReleasedDelegate(ACTray tray);
		public event ACTrayReleasedDelegate TrayReleased;
		
		/// <summary>
		/// Raises the released event
		/// </summary>
		private void RaiseTrayReleased(ACTray tray){
			//Inform caller
			if (this.TrayReleased != null)
				this.TrayReleased (tray);
		}

		/// <summary>
		/// Occurs when any <see cref="ActionComponents.ACTray"/> in this <see cref="ActionComponents.ACTrayManager"/>'s collection
		/// is opened fully by the user
		/// </summary>
		public delegate void ACTrayOpenedDelegate(ACTray tray);
		public event ACTrayOpenedDelegate TrayOpened;
		
		/// <summary>
		/// Raises the opened.
		/// </summary>
		private void RaiseTrayOpened(ACTray tray){
			
			//Inform caller
			if (this.TrayOpened != null)
				this.TrayOpened (tray);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACTray"/> is completely closed by the user
		/// </summary>
		public delegate void ACTrayClosedDelegate(ACTray tray);
		public event ACTrayClosedDelegate TrayClosed;
		
		/// <summary>
		/// Raises the closed.
		/// </summary>
		private void RaiseTrayClosed(ACTray tray){
			
			//Inform caller
			if (this.TrayClosed != null)
				this.TrayClosed (tray);
		}
		#endregion
	}
}

