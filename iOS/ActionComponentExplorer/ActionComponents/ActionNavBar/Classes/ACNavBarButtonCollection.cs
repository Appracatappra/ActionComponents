using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents
{
	/// <summary>
	/// This collection holds all buttons within a given section (top, middle or bottom) of a <see cref="ActionComponents.ACNavBar"/> 
	/// and has properties to control the appearance of new <see cref="ActionComponents.ACNavBarButton"/>s added to the collection. 
	/// </summary>
	/// <remarks><see cref="ActionComponents.ACNavBarButtonCollection"/>s can not be directly created but can be interacted with via the
	/// parent containing <see cref="ActionComponents.ACNavBar"/> </remarks>
	[Register("ACNavBarButtonCollection")]
	public class ACNavBarButtonCollection : UIView, IEnumerator, IEnumerable
	{
		#region Private Variables
		private List<ACNavBarButton> Buttons = new List<ACNavBarButton>();
		private bool _suspendUpdates = false;
		private ACNavBarButtonAppearance _appearanceEnabled;
		private ACNavBarButtonAppearance _appearanceDisabled;
		private ACNavBarButtonAppearance _appearanceSelected;
		#endregion

		#region Public Properties
		/// <summary>
		/// [OPTIONAL] Tag to hold user information about this collection.
		/// </summary>
		public object tag;
		#endregion

		#region Enumerable Routines
		/// <summary>
		/// The position in the enumeration list.
		/// </summary>
		private int _position = -1;

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public new IEnumerator GetEnumerator()
		{
			_position = -1;
			return (IEnumerator)this;
		}

		/// <summary>
		/// Moves to the next item.
		/// </summary>
		/// <returns><c>true</c>, if next was moved, <c>false</c> otherwise.</returns>
		public bool MoveNext()
		{
			_position++;
			return (_position < Buttons.Count);
		}

		/// <summary>
		/// Reset the enumeration to zero
		/// </summary>
		public void Reset()
		{ _position = -1; }

		/// <summary>
		/// Gets the current item in the enumeration.
		/// </summary>
		/// <value>The current.</value>
		public object Current
		{
			get
			{
				try
				{
					return Buttons[_position];
				}

				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Gets or sets the <see cref="T:ActionComponents.ACNavBarButton"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACNavBarButton this[int index]
		{
			get
			{
				return Buttons[index];
			}

			set
			{
				Buttons[index] = value;
			}
		}

		/// <summary>
		/// Gets the count of buttons in the collection.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return Buttons.Count; }
		}
		#endregion

		#region Computed Properties
		public ACNavBar NavBar { get; set; }

		/// <summary>
		/// Gets or sets the button appearance enabled values
		/// </summary>
		/// <value>The button appearance enabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance ButtonAppearanceEnabled
		{
			get { return _appearanceEnabled; }
			set
			{
				// Save values
				_appearanceEnabled = value;

				// Wireup events
				_appearanceEnabled.AppearanceModified += () => {
					CascadeAppearanceChanges(_appearanceEnabled, ACNavBarButtonState.Enabled);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceEnabled, ACNavBarButtonState.Enabled);
			}
		}

		/// <summary>
		/// Gets or sets the button appearance disabled values
		/// </summary>
		/// <value>The button appearance disabled values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance ButtonAppearanceDisabled
		{
			get { return _appearanceDisabled; }
			set
			{
				// Save values
				_appearanceDisabled = value;

				// Wireup events
				_appearanceDisabled.AppearanceModified += () =>
				{
					CascadeAppearanceChanges(_appearanceDisabled, ACNavBarButtonState.Disabled);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceDisabled, ACNavBarButtonState.Disabled);
			}
		}

		/// <summary>
		/// Gets or sets the button appearance selected values
		/// </summary>
		/// <value>The button appearance selected values</value>
		/// <remarks>Any changes to this appearance will cascade to all <see cref="ActionComponents.ACNavBarButton"/>s in this 
		/// <see cref="ActionComponents.ACNavBarButtonCollection"/></remarks>
		public ACNavBarButtonAppearance ButtonAppearanceSelected
		{
			get { return _appearanceSelected; }
			set
			{
				// Save values
				_appearanceSelected = value;

				// Wireup events
				_appearanceSelected.AppearanceModified += () =>
				{
					CascadeAppearanceChanges(_appearanceSelected, ACNavBarButtonState.Selected);
				};

				// Apply change
				CascadeAppearanceChanges(_appearanceSelected, ACNavBarButtonState.Selected);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:ActionComponents.ACNavBarButtonCollection"/> has suspend updates.
		/// </summary>
		/// <value><c>true</c> if suspend updates; otherwise, <c>false</c>.</value>
		public bool SuspendUpdates
		{
			get { return _suspendUpdates; }
			set
			{
				_suspendUpdates = value;

				//Adjust frame height
				AdjustContainerHeight();
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACNavBarButtonCollection"/> class.
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <param name="top">Top.</param>
		public ACNavBarButtonCollection(ACNavBar parent, nfloat top)
		{
			// Initialize 
			NavBar = parent;
			Frame = new CGRect(0, top, 64, 42);

			// Clear background
			BackgroundColor = UIColor.Clear;

			//Set default appearances
			ButtonAppearanceEnabled = new ACNavBarButtonAppearance();

			ButtonAppearanceDisabled = new ACNavBarButtonAppearance()
			{
				Alpha = 0.5f
			};

			ButtonAppearanceSelected = new ACNavBarButtonAppearance();

			// Enable touches
			UserInteractionEnabled = true;
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Moves the group to the given location in the Nav Bar.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal void MoveTo(nfloat x, nfloat y)
		{
			// Move to new location
			Frame = new CGRect(x, y, Frame.Width, Frame.Height);

			// Check to see if the pointer needs to be repositioned
			RepositionPointer();
		}

		/// <summary>
		/// Repositions the pointer after the group has been moved.
		/// </summary>
		internal void RepositionPointer()
		{

			// Scan all buttons for any selected buttons
			foreach (ACNavBarButton button in Buttons)
			{
				// Is this the selected button?
				if (button.State == ACNavBarButtonState.Selected)
				{
					// The point position needs to move too
					RequestNewPointerPosition(button.Frame.Top);
				}
			}
		}

		/// <summary>
		/// Cascades the appearance changes to every button in the group for the given button state.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		/// <param name="state">State.</param>
		internal void CascadeAppearanceChanges(ACNavBarButtonAppearance appearance, ACNavBarButtonState state){

			// Process all buttons in this collection
			foreach(ACNavBarButton button in Buttons){
				// Take action based on the element being copied
				switch(state){
					case ACNavBarButtonState.Enabled:
						button.AppearanceEnabled.CopyAppearance(appearance, false);
						break;
					case ACNavBarButtonState.Disabled:
						button.AppearanceDisabled.CopyAppearance(appearance, false);
						break;
					case ACNavBarButtonState.Selected:
						button.AppearanceSelected.CopyAppearance(appearance, false);
						break;
				}
			}

			// Cause this collection to redraw
			SetNeedsDisplay();
		}
		#endregion

		#region Private Variables
		/// <summary>
		/// Requests the new pointer position.
		/// </summary>
		/// <param name="buttonAt">Button at.</param>
		private void RequestNewPointerPosition(nfloat buttonAt)
		{
			PointerPositionChanged?.Invoke(Frame.Top + buttonAt + 8f);
		}

		/// <summary>
		/// Sets the button appearance defaults.
		/// </summary>
		/// <param name="button">Button.</param>
		private void SetButtonAppearanceDefaults(ACNavBarButton button) {

			// Set the default appearance to match this collection's defaults
			button.AppearanceEnabled.CopyAppearance(ButtonAppearanceEnabled, false);
			button.AppearanceDisabled.CopyAppearance(ButtonAppearanceDisabled, false);
			button.AppearanceSelected.CopyAppearance(ButtonAppearanceSelected, false);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a view with the given image and auto dispose state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="image">Image.</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(UIImage image, bool autoDispose) {

			// Create new view button
			var button = new ACNavBarButton(image);
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Returns the button currently selected in a group of buttons.
		/// </summary>
		/// <returns>The selected button.</returns>
		/// <param name="group">Group to search for.</param>
		public ACNavBarButton SelectedForGroup(string group) {

			// Scan all buttons
			foreach(ACNavBarButton button in Buttons) {
				if (button.Group == group && button.State == ACNavBarButtonState.GroupSelected) {
					return button;
				}
			}

			// Not found
			return null;
		}

		/// <summary>
		/// Adds a view with the given image filename and auto disposing state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="imageName">Image name.</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(string imageName, bool autoDispose)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName);
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a view with the given image, view name and auto disposing state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="image">Image.</param>
		/// <param name="viewName">The name of the view to load from the storyboard when the button is selected.</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(UIImage image, string viewName, bool autoDispose)
		{

			// Create new view button
			var button = new ACNavBarButton(image){
				ViewName = viewName
			};
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a view with the given image filename, view name and auto disposing state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="imageName">Image name.</param>
		/// <param name="viewName">The name of the view to load from the storyboard when the button is selected..</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(string imageName, string viewName, bool autoDispose)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName){
				ViewName = viewName
			};
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a view with the given image, storyboard name, view name and auto disposing state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="image">Image.</param>
		/// <param name="storyboardName">The name of the storyboard to load the view from.</param>
		/// <param name="viewName">The name of the view to load from the storyboard when the button is selected.</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(UIImage image, string storyboardName, string viewName, bool autoDispose)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				StoryboardName = storyboardName,
				ViewName = viewName
			};
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a view with the given image filename, storyboard name, view name and auto disposing state.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="imageName">Image name.</param>
		/// <param name="storyboardName">The name of the storyboard to load the view from.</param>
		/// <param name="viewName">The name of the view to load from the storyboard when the button is selected.</param>
		/// <param name="autoDispose">If set to <c>true</c> auto dispose.</param>
		public ACNavBarButton AddView(string imageName, string storyboardName, string viewName, bool autoDispose)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				StoryboardName = storyboardName,
				ViewName = viewName
			};
			if (autoDispose) button.Type = ACNavBarButtonType.AutoDisposingView;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a tool to the bar with the given image
		/// </summary>
		/// <returns>The tool.</returns>
		/// <param name="image">Image.</param>
		public ACNavBarButton AddTool(UIImage image)
		{

			// Create new view button
			var button = new ACNavBarButton(image){
				Type = ACNavBarButtonType.Tool
			};

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a tool to the bar with the given image filename.
		/// </summary>
		/// <returns>The tool.</returns>
		/// <param name="imageName">Image name.</param>
		public ACNavBarButton AddTool(string imageName)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Type = ACNavBarButtonType.Tool
			};

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a tool to the bar with the given image and action.
		/// </summary>
		/// <returns>The tool.</returns>
		/// <param name="image">Image.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddTool(UIImage image, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				Type = ACNavBarButtonType.Tool
			};
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a tool to the bar with the given image filename and action.
		/// </summary>
		/// <returns>The tool.</returns>
		/// <param name="imageName">Image name.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddTool(string imageName, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Type = ACNavBarButtonType.Tool
			};
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the given group with the given image
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="image">Image.</param>
		public ACNavBarButton AddSelection(string group, UIImage image)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the group with the given image filename.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="imageName">Image name.</param>
		public ACNavBarButton AddSelection(string group, string imageName)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the given group with the given image and action.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="image">Image.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddSelection(string group, UIImage image, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the given group with the given image filename and action.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="imageName">Image name.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddSelection(string group, string imageName, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection with the given group with the image and selected image.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="image">Image.</param>
		/// <param name="selectedImage">Selected image.</param>
		public ACNavBarButton AddSelection(string group, UIImage image, UIImage selectedImage)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.AppearanceSelected.Image = selectedImage;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the given group with the image filename and selected image filename.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="imageName">Image name.</param>
		/// <param name="selectedImageName">Selected image name.</param>
		public ACNavBarButton AddSelection(string group, string imageName, string selectedImageName)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.AppearanceSelected.Image = UIImage.FromBundle(selectedImageName);

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds a selection to the given group with the image, selected image and action.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="image">Image.</param>
		/// <param name="selectedImage">Selected image.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddSelection(string group, UIImage image, UIImage selectedImage, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(image)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.AppearanceSelected.Image = selectedImage;
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds the selection to the given group with the image filename, selected image filename and action.
		/// </summary>
		/// <returns>The selection.</returns>
		/// <param name="group">Group.</param>
		/// <param name="imageName">Image name.</param>
		/// <param name="selectedImageName">Selected image name.</param>
		/// <param name="action">Action.</param>
		public ACNavBarButton AddSelection(string group, string imageName, string selectedImageName, ACNavBarButtonActionDelegate action)
		{

			// Create new view button
			var button = new ACNavBarButton(imageName)
			{
				Group = group,
				Type = ACNavBarButtonType.Selection
			};
			button.AppearanceSelected.Image = UIImage.FromBundle(selectedImageName);
			button.Touched += action;

			// Set the default appearance
			SetButtonAppearanceDefaults(button);

			// Add the button to the collection
			Add(button);

			// Return the new button
			return button;
		}

		/// <summary>
		/// Adds the given Nav Bar Button to the collection.
		/// </summary>
		/// <returns>The add.</returns>
		/// <param name="button">Button.</param>
		public void Add(ACNavBarButton button) {
			nfloat y = (Count * 42f);

			// Set the button position
			button.Frame = new CGRect(2, y, 60, 40);

			// Adding a selection group type of button?
			if (button.Type == ACNavBarButtonType.Selection) {
				// Is there already a button selected for this group?
				var selected = SelectedForGroup(button.Group);
				if (selected == null) button.State = ACNavBarButtonState.GroupSelected;
			}

			// Handle the button's state changing
			button.StateChanged += (responder) => {
				// Take action based on state
				switch(responder.State){
					case ACNavBarButtonState.Selected:
						// Take action based on the button type
						switch(responder.Type) {
							case ACNavBarButtonType.View:
							case ACNavBarButtonType.AutoDisposingView:
								// Was a button already selected that was not this button?
								if (NavBar.SelectedButton != null && NavBar.SelectedButton != responder)
								{
									// Yes, deselect that button
									NavBar.SelectedButton.State = ACNavBarButtonState.Enabled;
								}

								// Save new selection
								NavBar.SelectedButton = responder;

								// Request new pointer position
								RequestNewPointerPosition(responder.Frame.Top);
								break;
							case ACNavBarButtonType.Selection:
								
								break;
						}
						break;
					case ACNavBarButtonState.Hidden:
					case ACNavBarButtonState.Enabled:
						// The container might have changed size
						AdjustContainerHeight();
						break;
					case ACNavBarButtonState.GroupSelected:
						// Get previously selected button for group
						foreach (ACNavBarButton btn in Buttons)
						{
							if (btn != responder) btn.State = ACNavBarButtonState.Enabled;
						}
						break;
				}
			};

			// Add button to collection and view
			Buttons.Add(button);
			AddSubview(button);

			//Adjust frame height
			AdjustContainerHeight();

			// Auto Select the first valid button
			if (NavBar.SelectedButton == null && 
			    (button.Type == ACNavBarButtonType.View || button.Type == ACNavBarButtonType.AutoDisposingView)) {
				// Invoke the buttons action to bring the fist view into focus and
				// automatically select this button
				button.Invoke();
			}
		}

		/// <summary>
		/// Remove the specified button from the collection.
		/// </summary>
		/// <param name="button">The button to remove.</param>
		public void Remove(ACNavBarButton button){

			// Remove the button from the screen
			button.RemoveFromSuperview();

			// Remove the button from the collection
			Buttons.Remove(button);

			// Adjust container height
			AdjustContainerHeight();

		}

		/// <summary>
		/// Removes the button at the given location from the collection.
		/// </summary>
		/// <param name="n">The location to remove the button from.</param>
		public void RemoveAt(int n)
		{

			// Remove the button from the screen
			Buttons[n].RemoveFromSuperview();

			// Remove the button from the collection
			Buttons.RemoveAt(n);

			// Adjust container height
			AdjustContainerHeight();

		}

		/// <summary>
		/// Removes all buttons from the collection.
		/// </summary>
		public void Clear() {

			// Remove all buttons from the screen
			foreach(ACNavBarButton button in Buttons){
				button.RemoveFromSuperview();
			}

			// Empty collection
			Buttons.Clear();

			// Adjust container height
			AdjustContainerHeight();
		}

		/// <summary>
		/// Adjusts the height of the container.
		/// </summary>
		public void AdjustContainerHeight()
		{
			nfloat height = 2f;
			bool adjusted = false;

			// Are updates suspended?
			if (SuspendUpdates)
				return;

			// Any buttons?
			if (Count == 0)
			{
				// No, force to one button high
				height = 42f;
			}
			else
			{
				// Prep animation of buttons moving
				// Define Animation
				UIView.BeginAnimations("ContainerResize");
				UIView.SetAnimationDuration(0.5f);

				// Set end of Animation handler
				UIView.SetAnimationDelegate(this);

				// Scan all buttons and calculate height
				foreach (ACNavBarButton button in Buttons)
				{
					if (button.State != ACNavBarButtonState.Hidden)
					{
						// Adjust button's position
						button.MoveTo(2, height);

						// Adjust container's height
						height += 42f;
					}
				}

				// Execute Animation
				UIView.CommitAnimations();

			}

			// Adjust my size to hold the new number of buttons
			adjusted = (Frame.Height != height);
			Frame = new CGRect(Frame.Left, Frame.Top, Frame.Width, height);

			// Inform caller of change
			CollectionModified?.Invoke(this);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when collection modified.
		/// </summary>
		public event ACNavBarCollectionModifiedDelegate CollectionModified;

		/// <summary>
		/// Occurs when pointer position changed.
		/// </summary>
		public event ACNavBarPointerPositionDelegate PointerPositionChanged;
  		#endregion
	}
}
