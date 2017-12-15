using System;
using System.Collections;
using System.Collections.Generic;

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
using Android.Animation;

namespace ActionComponents
{
	/// <summary>
	/// This collection holds all buttons within a given section (top, middle or bottom) of a <see cref="ActionComponents.ACNavBar"/> 
	/// and has properties to control the appearance of new <see cref="ActionComponents.ACNavBarButton"/>s added to the collection. 
	/// </summary>
	/// <remarks><see cref="ActionComponents.ACNavBarButtonCollection"/>s can not be directly created but can be interacted with via the
	/// parent containing <see cref="ActionComponents.ACNavBar"/> </remarks>
	public class ACNavBarButtonCollection
	{

		#region Private Variables
		private List<ACNavBarButton> _buttons = new List<ACNavBarButton> ();
		private bool _isMaster=false;
		private ACNavBarButtonAppearance _buttonAppearanceEnabled;
		private ACNavBarButtonAppearance _buttonAppearanceDisabled;
		private ACNavBarButtonAppearance _buttonAppearanceSelected;
		private bool _suspendUpdates;
		private int _counter=0;
		private float _top=0;
		private float _left=0;
		private float _width=64;
		private float _height=42;
		private ACNavBar _navBar;
		private ACNavBarButtonCollectionLocation _location;
		private int _rehydrationId=0;
		#endregion 

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] Tag to hold user information about this collection
		/// </summary>
		public object tag;
		#endregion
		
		#region Computed Properties
		/// <summary>
		/// Gets or sets the unique identifier for this <see cref="ActionComponents.ACNavBarButtonCollection"/> 
		/// </summary>
		/// <value>The identifier.</value>
		public int Id{ get; set;}

		/// <summary>
		/// Gets or sets the rehydration identifier used to restore the selected <see cref="ActionComponents.ACNavBarButton"/> after a state change such as rotation. 
		/// </summary>
		/// <value>The rehydration identifier.</value>
		/// <remarks>Call the <see cref="ActionComponents.ACNavBar.SelectedButtonID"/> method in the <c>OnSaveInstanceState</c> method of your <c>Action</c>
		/// to get the value to set this property to.</remarks>
		public int rehydrationId{
			get{return _rehydrationId;}
			set{_rehydrationId=value;}
		}

		/// <summary>
		/// Gets the top position for this virtual layout
		/// </summary>
		/// <value>The top.</value>
		public float top{
			get {return _top;}
		}

		/// <summary>
		/// Gets the left position for this virtual layout
		/// </summary>
		/// <value>The left.</value>
		public float left{
			get{return _left;}
		}

		/// <summary>
		/// Gets the width for this virtual layout
		/// </summary>
		/// <value>The width.</value>
		public float width{
			get{return _width;}
		}

		/// <summary>
		/// Gets the height for this virtual layout
		/// </summary>
		/// <value>The height.</value>
		public float height{
			get{return _height;}
		}

		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <value>The location.</value>
		public ACNavBarButtonCollectionLocation location{
			get{return _location;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButtonCollection"/>
		/// suspends the updating of the GUI.
		/// </summary>
		/// <value><c>true</c> if suspend updates; otherwise, <c>false</c>.</value>
		/// <remarks>If doing batch updates of the buttons in this collection it is best to suspend updating of the GUI first</remarks>
		public bool suspendUpdates {
			get { return _suspendUpdates;}
			set {
				_suspendUpdates=value;
				
				//Adjust frame height
				AdjustContainerHeight ();
				ReflowButtonPositions ();
				
				//Inform caller of modification
				RaiseCollectionModified ();
			}
		}

		/// <summary>
		/// Gets or sets the button appearance enabled values
		/// </summary>
		/// <value>The button appearance enabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance buttonAppearanceEnabled{
			get{ return _buttonAppearanceEnabled;}
			set{ 
				//Save value
				_buttonAppearanceEnabled=value;
				
				//Wireup modification handler
				_buttonAppearanceEnabled.AppearanceModified+=delegate() {
					//Cascade changes to all buttons
					CascadeAppearance (_buttonAppearanceEnabled,ACNavBarButtonState.Enabled);
				};
				
				//Force appearance to cascade
				_buttonAppearanceEnabled.RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the button appearance disabled values
		/// </summary>
		/// <value>The button appearance disabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance buttonAppearanceDisabled{
			get{ return _buttonAppearanceDisabled;}
			set{ 
				//Save value
				_buttonAppearanceDisabled=value;
				
				//Wireup modification handler
				_buttonAppearanceDisabled.AppearanceModified+=delegate() {
					//Cascade changes to all buttons
					CascadeAppearance (_buttonAppearanceDisabled,ACNavBarButtonState.Disabled);
				};
				
				//Force appearance to cascade
				_buttonAppearanceDisabled.RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Gets or sets the button appearance selected values
		/// </summary>
		/// <value>The button appearance selected values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance buttonAppearanceSelected{
			get{ return _buttonAppearanceSelected;}
			set{ 
				//Save value
				_buttonAppearanceSelected=value;
				
				//Wireup modification handler
				_buttonAppearanceSelected.AppearanceModified+=delegate() {
					//Cascade changes to all buttons
					CascadeAppearance (_buttonAppearanceSelected,ACNavBarButtonState.Selected);
				};
				
				//Force appearance to cascade
				_buttonAppearanceSelected.RaiseAppearanceModified();
			}
		}
		
		/// <summary>
		/// Returns the number of buttons in this collection
		/// </summary>
		/// <value>The count.</value>
		public int Count{
			get{ return _buttons.Count;}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACNavBarButtonCollection"/> is master.
		/// </summary>
		/// <value><c>true</c> if is master; otherwise, <c>false</c>.</value>
		/// <remarks>The "top" container is usually the one setup as master.</remarks>
		internal bool isMaster {
			get { return _isMaster;}
			set { _isMaster = value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACNavBarButtonCollection"/> class.
		/// </summary>
		/// <param name="navBar">Nav bar.</param>
		/// <param name="location">Location.</param>
		/// <param name="isMaster">If set to <c>true</c> is master.</param>
		/// <param name="buttonAppearanceEnabled">Button appearance enabled.</param>
		/// <param name="buttonAppearanceDisabled">Button appearance disabled.</param>
		/// <param name="buttonAppearanceSelected">Button appearance selected.</param>
		internal ACNavBarButtonCollection(ACNavBar navBar, ACNavBarButtonCollectionLocation location, bool isMaster, ACNavBarButtonAppearance buttonAppearanceEnabled, ACNavBarButtonAppearance buttonAppearanceDisabled, ACNavBarButtonAppearance buttonAppearanceSelected)
		{
			//Save values
			this._navBar=navBar;
			this._location=location;
			this._isMaster = isMaster;
			
			//Create the default appearances
			this.buttonAppearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			this.buttonAppearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			this.buttonAppearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);

			//Set the default location information
			_left=0;
			_width=64;
			_height=42;

			//Calculate top based on the collection's location
			switch(_location){
			case ACNavBarButtonCollectionLocation.Top:
				_top=0;
				break;
			case ACNavBarButtonCollectionLocation.Middle:
				_top=(_navBar.Height / 2f) - (_height / 2f);
				break;
			case ACNavBarButtonCollectionLocation.Bottom:
				_top=_navBar.Height - 2f - _height;
				break;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Creates a new button and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <remarks>Creates a <see cref="ActionComponents.ACNavBarButton"/> that controls an attached <c>UIView</c> </remarks>
		public ACNavBarButton AddButton(int image, bool enabled, bool hidden){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.View, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, null);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new button and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <param name="tag">Holds any user defined information for the button</param>
		/// <remarks>Creates a <see cref="ActionComponents.ACNavBarButton"/> that controls an attached <c>UIView</c>  </remarks>
		public ACNavBarButton AddButton(int image, bool enabled, bool hidden, object tag){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.View, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, tag);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new button that will automatically dispose of its attached view when it is unselected and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <remarks>Creates a <see cref="ActionComponents.ACNavBarButton"/> that controls an attached <c>UIView</c>. The view will be automatically
		/// removed from memory when it loses focus.</remarks>
		public ACNavBarButton AddAutoDisposingButton(int image, bool enabled, bool hidden){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.AutoDisposingView, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, null);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new button that will automatically dispose of its attached view when it is unselected and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <param name="tag">Holds any user defined information for the button</param>
		/// <remarks>Creates a <see cref="ActionComponents.ACNavBarButton"/> that controls an attached <c>UIView</c>. The view will be automatically
		/// removed from memroy when it loses focus.</remarks>
		public ACNavBarButton AddAutoDisposingButton(int image, bool enabled, bool hidden, object tag){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.AutoDisposingView, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, tag);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new tool button and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <remarks>Tool buttons do not have an attached <c>UIView</c>  or move the <see cref="ActionComponents.ACNavBarPointer"/>  when selected</remarks>
		public ACNavBarButton AddTool(int image, bool enabled, bool hidden){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.Tool, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, null);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new tool button and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="enabled">If set to <c>true</c> enabled.</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <param name="tag">Holds any user defined information for the button</param>
		/// <remarks>Tool buttons do not have an attached <c>UIView</c>  or move the <see cref="ActionComponents.ACNavBarPointer"/>  when selected</remarks>
		public ACNavBarButton AddTool(int image, bool enabled, bool hidden, object tag){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.Tool, appearanceEnabled, appearanceDisabled, appearanceSelected, enabled, hidden, tag);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Creates a new notification icon and adds it to the collection
		/// </summary>
		/// <returns>The button.</returns>
		/// <param name="image">The button's icon</param>
		/// <param name="tag">Holds any user defined information for the button</param>
		/// <param name="hidden">If set to <c>true</c> hidden.</param>
		/// <remarks>Notification icons are for display only and do not respond to touch</remarks>
		public ACNavBarButton AddNotification(int image, object tag, bool hidden){
			
			//Create needed appearances from the defaults
			ACNavBarButtonAppearance appearanceEnabled = new ACNavBarButtonAppearance (buttonAppearanceEnabled);
			ACNavBarButtonAppearance appearanceDisabled = new ACNavBarButtonAppearance (buttonAppearanceDisabled);
			ACNavBarButtonAppearance appearanceSelected = new ACNavBarButtonAppearance (buttonAppearanceSelected);
			
			//Create a new button
			appearanceEnabled.image = image;
			ACNavBarButton button = new ACNavBarButton (_navBar.Context, ACNavBarButtonType.Notification, appearanceEnabled, appearanceDisabled, appearanceSelected, true, hidden, tag);
			
			//Add the button to the collection
			AddButton (button);
			
			//Return the newly created button
			return button;
		}

		/// <summary>
		/// Adds the buttonto the collection
		/// </summary>
		/// <param name="button">Button.</param>
		internal void AddButton(ACNavBarButton button){

			//Add to collection
			_buttons.Add (button);

			//Recalculate container size
			AdjustContainerHeight ();

			//Make an Id for this button
			button.Id=((this.Id*1000)+(++_counter));

			//Are we rehydrating the NavBar after a state change?
			if (button.Id==rehydrationId) {
				//Yes, make this the selected button
				button.SetButtonState(ACNavBarButtonState.Selected);
				Console.WriteLine ("Rehydrating button {0}",rehydrationId);
			} else if (Count == 1 && _isMaster && button.state!=ACNavBarButtonState.Hidden && rehydrationId==0) {
				//Take action based on the button's type
				switch(button.type){
				case ACNavBarButtonType.AutoDisposingView:
				case ACNavBarButtonType.View:
					//This is the first button of the master group
					//automatically select it
					button.SetButtonState(ACNavBarButtonState.Selected);

					//Set the initial button location
					RaiseNewPointerPosition(0,false);
					
					//Ask caller to build the view for this button
					button.RaiseRequestNewView();
					break;
				}
			}

			//Set initial location at top of parent view
			button.MoveTo (2,0);

			//Add this button to the parent view
			_navBar.AddView (button);

			//Wire-up a handler for the button so the control can
			//report on the position change
			button.Touched += delegate(ACNavBarButton responder) {
				//Is this button already selected?
				if (responder.state==ACNavBarButtonState.Selected) {
					//Yes, we don't need to do anything so return
					return;
				}
				
				//Take action based on the type of button
				switch(responder.type){
				case ACNavBarButtonType.AutoDisposingView:
				case ACNavBarButtonType.View:
					//Unselect all other buttons from this collection
					UnselectAllButtons();
					
					//Select this button
					responder.SetButtonState(ACNavBarButtonState.Selected);
					break;
				}
				
			};
			
			//Wire-up a handler for the button's state changing
			button.StateChanged+= delegate(ACNavBarButton responder) {
				//Update containing controller
				Redraw();
				RaiseCollectionModified();
			};
			
			//Inform caller of modification
			RaiseCollectionModified ();
		}

		/// <summary>
		/// Returns the <see cref="ActionComponents.ACNavBarButton"/>  for the specified index
		/// </summary>
		/// <param name="index">The index of the button requested</param>
		/// <remarks>Requesting an index out of range will result in a <c>null</c> being returned </remarks>
		/// <returns>The <see cref="ActionComponents.ACNavBarButton"/> for the given index</returns>
		public ACNavBarButton Button(int index){
			
			//is this a valid index?
			if (index < 0 || index >= Count)
				return null;
			
			//Return the requested button
			return _buttons [index];
			
		}
		
		/// <summary>
		/// Removes the button at index.
		/// </summary>
		/// <param name="index">Index of the button to remove</param>
		/// <remarks>No action is taken if the index is out of range</remarks>
		public void RemoveButtonAt(int index) {
			
			//is this a valid index?
			if (index < 0 || index >= Count)
				return;
			
			//Pull button from collection
			_buttons.RemoveAt (index);
			
			//Adjust frame height
			AdjustContainerHeight ();
			
			//Inform caller of modification
			RaiseCollectionModified ();
		}
		
		/// <summary>
		/// Removes all <see cref="ActionComponents.ACNavBarButton"/>s from the collection
		/// </summary>
		public void Clear(){
			
			//Remove all buttons from the collection
			_buttons.Clear ();
			
			//Adjust frame height
			AdjustContainerHeight ();
			
			//Inform caller of modification
			RaiseCollectionModified ();
		}
		
		/// <summary>
		/// Selects the <see cref="ActionComponents.ACNavBarButton"/>  at the given index.
		/// </summary>
		/// <param name="index">Index of the button to select</param>
		/// <remarks>No action is taken if the index if out of range.</remarks>
		public void SelectButtonAt(int index){
			
			//is this a valid index?
			if (index < 0 || index >= Count)
				return;
			
			//Clear any currently selected buttons
			UnselectAllButtons ();
			
			//Grab the button we're selecting
			ACNavBarButton button = _buttons [index];
			
			//Select the button
			//button.SetButtonState (ACNavBarButtonState.Selected);
			
			//Raise the button's touched event will automatically it
			button.RaiseTouched ();
		}
		
		/// <summary>
		/// Returns the currently selected <see cref="ActionComponents.ACNavBarButton"/> in this <see cref="ActionComponents.ACNavBarButtonCollection"/> button group 
		/// </summary>
		/// <returns>Returns the <see cref="ActionComponents.ACNavBarButton"/> selected or <c>null</c> if no button is selected</returns>
		public ACNavBarButton SelectedButton(){
			
			//Scan all buttons for a selected one
			foreach (ACNavBarButton button in _buttons) {
				//Found?
				if (button.state==ACNavBarButtonState.Selected) return button;
			}
			
			//No buttons in the collection are selected
			return null;
		}
		
		/// <summary>
		/// Causes the selected <see cref="ActionComponents.ACNavBarButton"/> in this <see cref="ActionComponents.ACNavBarButtonCollection"/>
		/// to be displayed and raises the buttons <c>Touched</c> event
		/// </summary>
		/// <remarks>This method MUST be called after the first <see cref="ActionComponents.ACNavBarButton"/> is added to the <c>top</c> 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/> of the <see cref="ActionComponents.ACNavBar"/> so that the
		/// initial <c>View</c> will be correctly displayed.</remarks>
		internal void DisplaySelectedButtonView(){
			
			//Grab the selected button
			ACNavBarButton selected = SelectedButton ();
			
			//Is a button selected?
			if (selected == null)
				return;
			
			//Raise this button's touched event to display it
			selected.RaiseTouched ();
		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// If any button within this group is selected, this routine will move the pointer beside it
		/// </summary>
		/// <returns><c>true</c>, if pointer was repositioned, <c>false</c> otherwise.</returns>
		internal bool RepositionPointer(){
			
			//Scan all buttons for a selected one
			foreach (ACNavBarButton button in _buttons) {
				//Is this button selected?
				if (button.state==ACNavBarButtonState.Selected) {
					//Yes, ask NavBar to move pointer
					RaiseNewPointerPosition(button.Top,true);
					return true;
				}
			}
			
			//Not found
			return false;
		}

		/// <summary>
		/// Selects the button by identifier.
		/// </summary>
		/// <returns><c>true</c>, if button by identifier was selected, <c>false</c> otherwise.</returns>
		/// <param name="Id">Identifier.</param>
		internal bool SelectButtonById(int Id){
			
			//Scan all buttons for a selected one
			foreach (ACNavBarButton button in _buttons) {
				//Is this the button that we are looking for?
				if (button.Id==Id) {
					//Yes, Make it the selected button
					button.SetButtonState(ACNavBarButtonState.Selected);
					
					//Raise the button's touched event
					button.RaiseTouched();
					
					//Ask NavBar to move pointer to this button
					RaiseNewPointerPosition(button.Top,false);
					
					//Inform caller that we found it
					return true;
				}
			}
			
			//The button wasn't found
			return false;
		}

		/// <summary>
		/// Redraw this instance.
		/// </summary>
		internal void Redraw(){

			//Force the container to be redrawn totally
			AdjustContainerHeight();
			ReflowButtonPositions();
		}
		
		/// <summary>
		/// Unselects all buttons.
		/// </summary>
		internal void UnselectAllButtons(){
			
			//Disable updates
			_suspendUpdates = true;
			
			//Deselect all buttons in this group
			foreach (ACNavBarButton button in _buttons) {
				if (button.state==ACNavBarButtonState.Selected) {
					//Inform caller that this button is losing focus
					button.ButtonUnselected();
					
					//Unselect the button
					button.SetButtonState (ACNavBarButtonState.Enabled);
				}
			}
			
			//Return to normal controll
			_suspendUpdates = false;
		}
		#endregion 

		#region Private Methods
		/// <summary>
		/// Cascades the modified <see cref="ActionComponents.ACNavBarButtonAppearance"/> to every <see cref="ActionComponents.ACNavBarButton"/> in this
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/> 
		/// </summary>
		/// <param name="appearance">The modified appearance</param>
		/// <param name="state">The button state being modified</param>
		private void CascadeAppearance(ACNavBarButtonAppearance appearance, ACNavBarButtonState state){
			
			//Pass the change to each button in this collection
			foreach (ACNavBarButton button in _buttons) {
				//Take action based on the state being changed
				switch(state){
				case ACNavBarButtonState.Enabled:
					button.appearanceEnabled.Clone (appearance);
					break;
				case ACNavBarButtonState.Disabled:
					button.appearanceDisabled.Clone (appearance);
					break;
				case ACNavBarButtonState.Selected:
					button.appearanceSelected.Clone(appearance);
					break;
				}
			}
		}

		/// <summary>
		/// Reflows the button positions after a container has been moved
		/// </summary>
		private void ReflowButtonPositions() {
			float height=2f;

			//Suspended?
			if (_suspendUpdates) return;
			
			//Any buttons?
			if (Count == 0) {
				//No, force to one button high
				height=42f;
			} else {
				
				//Scan all buttons and calculate height
				foreach(ACNavBarButton button in _buttons){
					if (button.state!=ACNavBarButtonState.Hidden) { 
						//Adjust button's position
						button.MoveTo(2,_top+height);

						//Is this the selected button?
						if (button.state==ACNavBarButtonState.Selected) {
							//The point position needs to move too
							RaiseNewPointerPosition(button.Top,true);
						}
						
						//Adjust container's height
						height+=42f;
					}
				}
				
			}
		}

		/// <summary>
		/// Adjusts the height of the container.
		/// </summary>
		/// <remarks>If any button in this collection is selected, this routine will raise a NewPointerPosition
		/// to inform the caller of it's new location</remarks>
		private void AdjustContainerHeight(){
			float height=2f;

			//Any buttons?
			if (Count == 0) {
				//No, force to one button high
				height=42f;
			} else {

				//Scan all buttons and calculate height
				foreach(ACNavBarButton button in _buttons){
					if (button.state!=ACNavBarButtonState.Hidden) { 
						//Adjust container's height
						height+=42f;
					}
				}
				
			}

			//Save new height
			_height=height;

			//Take action based on the collection's location
			switch(_location){
			case ACNavBarButtonCollectionLocation.Top:
				_top=0;
				break;
			case ACNavBarButtonCollectionLocation.Middle:
				_top=(_navBar.Height / 2f) - (_height / 2f);
				break;
			case ACNavBarButtonCollectionLocation.Bottom:
				_top=_navBar.Height - 2f - _height;
				break;
			}

		}
		#endregion 
	
		#region Events
		/// <summary>
		/// Occurs when the collection is modified.
		/// </summary>
		internal delegate void CollectionModifiedDelegate();
		/// <summary>
		/// Occurs when the collection is modified.
		/// </summary>
		internal event CollectionModifiedDelegate CollectionModified;
		
		/// <summary>
		/// Raises the collection modified event
		/// </summary>
		/// <remarks>Used to inform the attached component that the 
		/// collection has been modified</remarks>
		private void RaiseCollectionModified(){
			//Are updates suspended?
			if (_suspendUpdates)
				return;
			
			//Inform caller that the appearance has changed
			if (this.CollectionModified != null)
				this.CollectionModified ();
		}
		
		/// <summary>
		/// Occurs when the NavBar's pointer need to be moved in responce to a 
		/// button in this group being touched
		/// </summary>
		internal delegate void NewPointerPositionDelegate(float y, bool animated);
		/// <summary>
		/// Occurs when the NavBar's pointer need to be moved in responce to a 
		/// button in this group being touched
		/// </summary>
		internal event NewPointerPositionDelegate NewPointerPosition;
		
		/// <summary>
		/// Raises the new pointer position.
		/// </summary>
		/// <param name="buttonAt">The y coordinate that the button is at within the
		/// container's view</param>
		/// <param name="animated">if <c>true</c> the move to the new position will be animated</param> 
		private void RaiseNewPointerPosition(float buttonAt, bool animated){
			
			//Are updates suspended?
			if (_suspendUpdates)
				return;
			
			//Anything to do?
			if (this.NewPointerPosition == null)
				return;
			
			//Calculate the pointer's position and return
			this.NewPointerPosition (buttonAt + 8f, animated);
		}
		#endregion

	}
}

