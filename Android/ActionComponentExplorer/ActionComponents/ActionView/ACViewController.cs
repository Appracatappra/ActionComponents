using System;
using System.Xml;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACViewController"/> is a helper class for working with <c>Views</c> that have been inflated
	/// from a .axml file by providing a place to hold the code to handle any UI <c>Widgets</c> so you don't have to put it in the <c>Activity</c> class that is
	/// loading the view. Create a child of this class, override the <c>Initialize</c> method and place the code to handle your UI <c>Widgets</c> there.
	/// </summary>
	/// <remarks>This class is helpful when making the view that will be controlled by other <c>Action Components</c> such as <c>NavBar</c>.</remarks>
	public class ACViewController
	{
		#region Private Variables
		private Activity _activity;
		private View _view;
		private bool _attachedToWindow = false;
		private bool _initialized = false;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets the <c>Activity</c> this <see cref="ActionComponents.ACViewController"/> is attached to
		/// </summary>
		/// <value>The activity.</value>
		public Activity activity{
			get{ return _activity;}
		}

		/// <summary>
		/// Gets a value indicating whether the <c>View</c> being controller by this
		/// <see cref="ActionComponents.ACViewController"/> is attached to a window.
		/// </summary>
		/// <value><c>true</c> if attached to window; otherwise, <c>false</c>.</value>
		public bool attachedToWindow{
			get{ return _attachedToWindow;}
		}

		/// <summary>
		/// Gets a value indicating whether this
		/// <see cref="ActionComponents.ACViewController"/> is initialized.
		/// </summary>
		/// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
		public bool initialized{
			get{ return _initialized;}
		}

		/// <summary>
		/// Gets or sets the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/> 
		/// </summary>
		/// <value>The view.</value>
		public View view{
			get{ return _view;}
			set{ 
				//Save value
				_view = value;

				//Attach a listner
				ListenForViewGoingLive ();
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		public ACViewController (Activity activity)
		{
			//Initialize
			this._activity = activity;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="view">View.</param>
		/// <remarks>This constructor assumes that the View has already been attached to a window and displayed</remarks>
		public ACViewController (Activity activity, View view)
		{
			//Initialize
			this._activity = activity;
			this.view = view;

			//Assume this view has already been displayed
			_attachedToWindow = true;

			//Attempt to initialize
			RequestInitialization();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="parser">Parser.</param>
		/// <param name="root">Root.</param>
		public ACViewController (Activity activity, XmlReader parser, ViewGroup root)
		{
			//Initialize
			this._activity = activity;
			this.LoadLayout (parser, root);

			//Attempt to initialize
			RequestInitialization();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="resource">Resource.</param>
		/// <param name="root">Root.</param>
		public ACViewController (Activity activity, int resource, ViewGroup root)
		{
			//Initialize
			this._activity = activity;
			this.LoadLayout (resource, root);

			//Attempt to initialize
			RequestInitialization();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="resource">Resource.</param>
		public ACViewController (Activity activity, int resource)
		{
			//Initialize
			this._activity = activity;
			this.LoadLayout (resource);

			//Attempt to initialize
			RequestInitialization();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="parser">Parser.</param>
		/// <param name="root">Root.</param>
		/// <param name="attachToRoot">If set to <c>true</c> attach to root.</param>
		public ACViewController (Activity activity, XmlReader parser, ViewGroup root, bool attachToRoot)
		{
			//Initialize
			this._activity = activity;
			this.LoadLayout (parser, root, attachToRoot);

			//Attempt to initialize
			RequestInitialization();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACViewController"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="resource">Resource.</param>
		/// <param name="root">Root.</param>
		/// <param name="attachToRoot">If set to <c>true</c> attach to root.</param>
		public ACViewController (Activity activity, int resource, ViewGroup root, bool attachToRoot)
		{
			//Initialize
			this._activity = activity;
			this.LoadLayout (resource, root, attachToRoot);

			//Attempt to initialize
			RequestInitialization();
		}
		#endregion 

		#region Virtual Methods
		/// <summary>
		/// Called when a new version of the <see cref="ActionComponents.ACViewController"/> needs to be initialized 
		/// </summary>
		/// <remarks>Override this method and place the code to handle your UI elements here</remarks>
		public virtual void Initialize(){
			// Place holder for child classes
		}

		/// <summary>
		/// Called when any UI element in the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>
		/// needs to save their current state before a device state change
		/// </summary>
		/// <param name="outState">Out state.</param>
		public virtual void OnSaveInstanceState (Bundle outState) {
			// Place holder for child classes
		}

		/// <summary>
		/// Called when any UI element in the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/> 
		/// needs to restore it's state after a device state change
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		public virtual void OnRestoreInstanceState (Bundle savedInstanceState){
			// Place holder for child classes
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Checks to see if the view being controlled by this <see cref="ActionComponents.ACViewController"/> has been attached to a window
		/// and if it has already been initialized, if not the <c>Initialize</c> method is called.
		/// </summary>
		/// <returns><c>true</c>, if initialization was requested, <c>false</c> otherwise.</returns>
		private void RequestInitialization(){

			//Are we attached to a window and have we already been initialized?
			if (attachedToWindow && !initialized) {
				//Request initialization
				Initialize ();

				//Save initialization state
				_initialized = true;
			}
		}

		/// <summary>
		/// Listens for view going live and adjust the <c>attachedToWindow</c> state and attempts to initialize if needed
		/// </summary>
		private void ListenForViewGoingLive(){

			//Listen for when this view is fully displayed
			view.ViewAttachedToWindow += (sender, e) => {
				//Mark as attached
				_attachedToWindow = true;

				//Attempt to initialize
				RequestInitialization();
			};

			//Listen for the view being undisplayed
			view.ViewDetachedFromWindow += (sender, e) => {
				//Clear attached flag
				_attachedToWindow = false;
			};

		}

		/// <summary>
		/// Clears the attached and initialized states
		/// </summary>
		private void ClearStates(){
			//Reset states to default
			_attachedToWindow = false;
			_initialized = false;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Loads the layout into the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>. 
		/// </summary>
		/// <param name="parser">Parser.</param>
		/// <param name="root">Root.</param>
		public void LoadLayout(XmlReader parser, ViewGroup root){
			//Reset the controller
			ClearStates ();

			// Ask activity to load and inflate layout
			view = _activity.LayoutInflater.Inflate (parser, root);
		}

		/// <summary>
		/// Loads the layout into the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>.
		/// </summary>
		/// <param name="resource">Resource.</param>
		public void LoadLayout(int resource){
			//Reset the controller
			ClearStates ();

			// Ask activity to load and inflate layout
			view = _activity.LayoutInflater.Inflate (resource, null);
		}

		/// <summary>
		/// Loads the layout into the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>.
		/// </summary>
		/// <param name="resource">Resource.</param>
		/// <param name="root">Root.</param>
		public void LoadLayout(int resource, ViewGroup root){
			//Reset the controller
			ClearStates ();

			// Ask activity to load and inflate layout
			view = _activity.LayoutInflater.Inflate (resource, root);
		}

		/// <summary>
		/// Loads the layout into the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>.
		/// </summary>
		/// <param name="parser">Parser.</param>
		/// <param name="root">Root.</param>
		/// <param name="attachToRoot">If set to <c>true</c> attach to root.</param>
		public void LoadLayout(XmlReader parser, ViewGroup root, bool attachToRoot){
			// Ask activity to load and inflate layout
			view = _activity.LayoutInflater.Inflate (parser, root, attachToRoot);
		}

		/// <summary>
		/// Loads the layout into the <c>View</c> being controlled by this <see cref="ActionComponents.ACViewController"/>.
		/// </summary>
		/// <param name="resource">Resource.</param>
		/// <param name="root">Root.</param>
		/// <param name="attachToRoot">If set to <c>true</c> attach to root.</param>
		public void LoadLayout(int resource, ViewGroup root, bool attachToRoot){
			//Reset the controller
			ClearStates ();

			// Ask activity to load and inflate layout
			view = _activity.LayoutInflater.Inflate (resource, root, attachToRoot);
		}
		#endregion 
	}
}

