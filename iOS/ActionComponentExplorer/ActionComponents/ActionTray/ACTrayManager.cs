using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;

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

			//Add tray to collection
			_trays.Add (tray);
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

