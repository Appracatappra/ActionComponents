using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	/// <summary>
	/// Maintains a collection of <c>ACTile</c> objects that are contained within a <c>ACTileController</c>. The
	/// <c>ACTileGroup</c> handles the creation and layout of the <c>ACTile</c> objects and can control a collection
	/// of different sized and shaped tiles.
	/// </summary>
	public class ACTileGroup : UIView, IEnumerator, IEnumerable
	{
		#region Private Variables
		private bool _bringToFrontOnTouched;
		private CGPoint _startLocation;
		private ACTileController _controller;
		private List<ACTile> _tiles = new List<ACTile>();
		private string _title = "";
		private string _footer = "";
		private ACTileGroupAppearance _appearance;
		private ACTileAppearance _defaultTileAppearance;
		private ACTileGroupCellConstraint _columnConstraint = new ACTileGroupCellConstraint();
		private ACTileGroupCellConstraint _rowConstraint = new ACTileGroupCellConstraint();
		private bool _needsReflow = false;
		private ACTileGroupType _groupType;
		private bool _shrinkTilesToFit = false;
		private bool _autoFitTiles = false;
		private ACTileLiveUpdate _liveUpdateAction;
		private bool[,] grid;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <c>ACTileGroup</c> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets the default <c>ACTileAppearance</c> 
		/// </summary>
		/// <value>The default tile appearance.</value>
		public ACTileAppearance defaultTileAppearance
		{
			get { return _defaultTileAppearance; }
			set
			{
				_defaultTileAppearance = value;
				CascadeTileAppearance();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileLiveUpdate</c> action that will be performed via an automatic update
		/// kicked off by the <c>liveUpdateTimer</c> in the parent <c>ACTileController</c> 
		/// </summary>
		/// <value>The live update action.</value>
		public ACTileLiveUpdate liveUpdateAction
		{
			get { return _liveUpdateAction; }
			set { _liveUpdateAction = value; }
		}

		/// <summary>
		/// Gets the <c>ACTileGroupType</c> of this <c>ACTileGroup</c> .
		/// </summary>
		/// <value>The type of the group.</value>
		public ACTileGroupType groupType
		{
			get { return _groupType; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileGroup</c> will automatically shrink <c>ActionTiles</c> that are larger than a single
		/// cell by resizing them to a single cell to fit in their current location. 
		/// </summary>
		/// <value><c>true</c> if auto fit tiles; otherwise, <c>false</c>.</value>
		public bool shrinkTilesToFit
		{
			get { return _shrinkTilesToFit; }
			set
			{
				_shrinkTilesToFit = value;
				RecalculateSize();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTileGroup</c> will auto fit tiles into the first
		/// available space regardless of the actual tile order.
		/// </summary>
		/// <value><c>true</c> if auto fit tiles; otherwise, <c>false</c>.</value>
		public bool autoFitTiles {
			get { return _autoFitTiles; }
			set {
				_autoFitTiles = value;
				RecalculateSize();
			}
		}

		/// <summary>
		/// Gets the <c>ACTileController</c> that this <c>ACTileGroup</c>
		/// belongs to  
		/// </summary>
		/// <value>The controller.</value>
		public ACTileController controller
		{
			get { return _controller; }
		}

		/// <summary>
		/// Gets or sets the <c>ACTileGroupAppearance</c> for this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileGroupAppearance appearance
		{
			get { return _appearance; }
			set
			{
				_appearance = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets the column <c>ACTileGroupCellConstraint</c> fron this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The column constraint.</value>
		internal ACTileGroupCellConstraint columnConstraint
		{
			get { return _columnConstraint; }
		}

		/// <summary>
		/// Gets the row <c>ACTileGroupCellConstraint</c> for this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The row constraint.</value>
		internal ACTileGroupCellConstraint rowConstraint
		{
			get { return _rowConstraint; }
		}

		/// <summary>
		/// Gets or sets the title for this <c>ACTileGroup</c> 
		/// </summary>
		/// <value>The title.</value>
		public string title
		{
			get { return _title; }
			set
			{
				_title = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the footer for this <c>ACTileGroup</c> 
		/// </summary>
		/// <value>The footer.</value>
		public string footer
		{
			get { return _footer; }
			set
			{
				_footer = value;
				Redraw();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTile</c> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public ACTile this[int index]
		{
			get
			{
				return _tiles[index];
			}

			set
			{
				_tiles[index] = value;
			}
		}

		/// <summary>
		/// Gets the number of <c>ACTile</c>s contained in this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return _tiles.Count; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		internal bool bringToFrontOnTouched
		{
			get { return _bringToFrontOnTouched; }
			set { _bringToFrontOnTouched = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <c>ACTile</c> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled
		{
			get { return UserInteractionEnabled; }
			set { UserInteractionEnabled = value; }
		}
		#endregion

		#region Enumerable Routines
		private int _position = -1;

		//IEnumerator and IEnumerable require these methods.
		public IEnumerator GetEnumerator()
		{
			_position = -1;
			return (IEnumerator)this;
		}

		//IEnumerator
		public bool MoveNext()
		{
			_position++;
			return (_position < Count);
		}

		//IEnumerator
		public void Reset()
		{ _position = -1; }

		//IEnumerator
		public object Current
		{
			get
			{
				try
				{
					return _tiles[_position];
				}

				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileGroup"/> class.
		/// </summary>
		internal ACTileGroup()
		{
			//Set initial values
			this._groupType = ACTileGroupType.FixedSizeGroup;
			this._appearance = new ACTileGroupAppearance();
			this._defaultTileAppearance = new ACTileAppearance();

			// Initialize
			Initialize(new ACTileGroupCellConstraint(), new ACTileGroupCellConstraint());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileGroup"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="groupType">Group type.</param>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		internal ACTileGroup(ACTileController controller, ACTileGroupType groupType, string title, string footer) : base()
		{
			//Set initial values
			this._groupType = groupType;
			this._controller = controller;
			this._appearance = _controller.groupAppearance.Clone();
			this._defaultTileAppearance = _controller.tileAppearance.Clone();
			this._title = title;
			this._footer = footer;

			// Initialize based on scroll direction
			switch (_controller.scrollDirection)
			{
				case ACTileControllerScrollDirection.Horizontal:
					Initialize(new ACTileGroupCellConstraint(ACTileGroupCellConstraintType.Flexible, 0),
												   new ACTileGroupCellConstraint(ACTileGroupCellConstraintType.FitParent, 0));
					break;
				case ACTileControllerScrollDirection.Vertical:
					Initialize(new ACTileGroupCellConstraint(ACTileGroupCellConstraintType.FitParent, 0),
												   new ACTileGroupCellConstraint(ACTileGroupCellConstraintType.Flexible, 0));
					break;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileGroup"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="groupType">Group type.</param>
		/// <param name="title">Title.</param>
		/// <param name="footer">Footer.</param>
		/// <param name="columnConstraint">Column constraint.</param>
		/// <param name="rowConstraint">Row constraint.</param>
		internal ACTileGroup(ACTileController controller, ACTileGroupType groupType, string title, string footer, ACTileGroupCellConstraint columnConstraint, ACTileGroupCellConstraint rowConstraint) : base()
		{
			//Set initial values
			this._groupType = groupType;
			this._controller = controller;
			this._appearance = _controller.groupAppearance.Clone();
			this._defaultTileAppearance = _controller.tileAppearance.Clone();
			this._title = title;
			this._footer = footer;

			//Init
			Initialize(columnConstraint, rowConstraint);
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(ACTileGroupCellConstraint columnConstraint, ACTileGroupCellConstraint rowConstraint)
		{

			//Set defaults
			this._columnConstraint = columnConstraint;
			this._rowConstraint = rowConstraint;
			this.BackgroundColor = ACColor.Clear;
			this._bringToFrontOnTouched = false;
			this._startLocation = new CGPoint(0, 0);
			this._defaultTileAppearance = new ACTileAppearance();
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;

			// Wireup appearance changes
			this._appearance.AppearanceModified += () => {
				Redraw();
			};

			//Calculate the initial size
			RecalculateSize();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Forces this <c>ACTileGroup</c> to fully redraw itself
		/// </summary>
		public void Redraw()
		{

			// Force component to update view
			this.SetNeedsDisplay();


		}

		/// <summary>
		/// Adds the style changer <c>ACTileLiveUpdate</c> action to every
		/// <c>ACTile</c> in this <c>ACTileGroup</c>  
		/// </summary>
		/// <remarks>Avoids changing <c>CustomDrawn</c> or <c>BigPicture</c> styles</remarks>
		public void AddStyleChangeToAllTiles()
		{
			Random rnd = new Random();

			// Process all tiles
			foreach (ACTile tile in _tiles)
			{
				// Take action based on the style 
				if (tile.style != ACTileStyle.BigPicture && tile.style != ACTileStyle.CustomDrawn)
				{
					// TODO: Fix this 
					// tile.liveUpdateAction = new ACTileLiveUpdateTileStyle(tile, rnd.Next(0, 3));
				}
			}

		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>.  
		/// </summary>
		/// <returns>The new tile created.</returns>
		/// <param name="style">Style.</param>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		/// <param name="icon">Icon.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle, string description, UIImage icon)
		{
			ACTile tile;

			// Create new tile
			tile = new ACTile(this, defaultTileAppearance.Clone(), style, tileSize, title, subtitle, icon, description);

			// Add to collection
			AddTile(tile);

			// Return new tile
			return tile;
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>.  
		/// </summary>
		/// <returns>The new tile created.</returns>
		/// <param name="style">Style.</param>
		/// <param name="tileSize">Tile size.</param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		/// <param name="iconName">Icon name.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle, string description, string iconName)
		{
			ACTile tile;

			// Create new tile
			tile = new ACTile(this, defaultTileAppearance.Clone(), style, tileSize, title, subtitle, UIImage.FromBundle(iconName), description);

			// Add to collection
			AddTile(tile);

			// Return new tile
			return tile;
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>.
		/// </summary>
		/// <returns>The custom sized tile.</returns>
		/// <param name="rows">Rows.</param>
		/// <param name="cols">Cols.</param>
		/// <param name="style">Style.</param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		/// <param name="icon">Icon.</param>
		public ACTile AddCustomSizedTile(int rows, int cols, ACTileStyle style, string title, string subtitle, string description, UIImage icon)
		{
			ACTile tile;

			// Create new tile
			tile = new ACTile(this, defaultTileAppearance.Clone(), style, ACTileSize.Custom, title, subtitle, icon, description);
			tile.customRowHeight = rows;
			tile.customColumnHeight = cols;

			// Add to collection
			AddTile(tile);

			// Return new tile
			return tile;
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>.
		/// </summary>
		/// <returns>The custom sized tile.</returns>
		/// <param name="rows">Rows.</param>
		/// <param name="cols">Cols.</param>
		/// <param name="style">Style.</param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		/// <param name="iconName">Icon name.</param>
		public ACTile AddCustomSizedTile(int rows, int cols, ACTileStyle style, string title, string subtitle, string description, string iconName)
		{
			ACTile tile;

			// Create new tile
			tile = new ACTile(this, defaultTileAppearance.Clone(), style, ACTileSize.Custom, title, subtitle, UIImage.FromBundle(iconName), description);
			tile.customRowHeight = rows;
			tile.customColumnHeight = cols;

			// Add to collection
			AddTile(tile);

			// Return new tile
			return tile;
		}

		/// <summary>
		/// Adds the tile to the group's collection of tiles.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public void AddTile(ACTile tile)
		{

			// Add tile to collection and self
			_tiles.Add(tile);
			AddSubview(tile);

			// Wireup touch events
			tile.Touched += (t) => {
				RaiseTileTouched(t);
			};

			// Are we updating the display?
			if (!_controller.suspendUpdates)
			{
				// Force the controller to recalculate all sizes.
				_controller.SetNeedsLayout();
			}
		}

		/// <summary>
		/// Removes the <c>ACTile</c> at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveTileAt(int index)
		{
			//Remove requested tile
			_tiles.RemoveAt(index);

			//Are we updating the display?
			if (!_controller.suspendUpdates)
			{
				// Force the controller to recalculate all sizes.
				_controller.SetNeedsLayout();
			}
		}

		/// <summary>
		/// Removes all <c>ACTile</c> from this <c>ACTile</c> 
		/// </summary>
		public void ClearTiles()
		{
			//Remove all tiles
			_tiles.Clear();

			//Are we updating the display?
			if (!_controller.suspendUpdates)
			{
				// Force the controller to recalculate all sizes.
				_controller.SetNeedsLayout();
			}
		}

		/// <summary>
		/// Changes the background color of every <c>ACTile</c> in this <c>ACTileGroup</c>
		/// by creating a random brightness off the given base <c>ACColor</c> within the given <c>minimum</c> and <c>maximum</c> ranges
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="minimum">Minimum.</param>
		/// <param name="maximum">Maximum.</param>
		public void ChromaKeyTiles(ACColor background, int minimum, int maximum)
		{
			//Process all tiles
			foreach (ACTile tile in _tiles)
			{
				tile.ChromaKeyTile(background, minimum, maximum);
			}
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Checks each tile in this <c>ACTileGroup</c> and sees if it has an attached
		/// <c>ACTileLiveUpdate</c> and executes it if needed 
		/// </summary>
		internal void LiveUpdateTiles()
		{

			//Check all of the tiles in this collection and see if they have a live update available
			foreach (ACTile tile in _tiles)
			{
				//Does this tile have a live update?
				if (tile.liveUpdateAction != null)
				{
					//Yes, execute the action
					tile.liveUpdateAction.PerformUpdate();
				}

				//Tell the tile that it is live updating
				tile.RaiseLiveUpdating();
			}

		}

		/// <summary>
		/// Recalculates the height and width of this <c>ACTileGroup</c> based on the size of the containing
		/// <c>ACTileController</c>, the <c>ACTileControllerScrollDirection</c>
		/// and the <c>ACTileGroupCellConstraint</c>s placed on this group. 
		/// </summary>
		internal void RecalculateSize()
		{

			//Mark as dirty
			_needsReflow = true;

			//Calculate the new size
			var width = CalculateGroupWidth();
			var height = CalculateGroupHeight();

			//Adjust size based on calculations
			Resize(width, height);

			//Do we still need to reflow all of the tiles?
			if (_needsReflow)
			{
				//Yes, do it based on the scroll direction
				switch (_controller.scrollDirection)
				{
					case ACTileControllerScrollDirection.Horizontal:
						if (autoFitTiles) {
							ReflowTilesTopToBottomAndAutoFit(width, height);
						} else {
							ReflowTilesTopToBottom(width, height);
						}
						break;
					case ACTileControllerScrollDirection.Vertical:
						if (autoFitTiles) {
							ReflowTilesLeftToRightAndAutoFit(width, height);
						} else {
							ReflowTilesLeftToRight(width, height);
						}
						break;
				}

				//Clear flag
				_needsReflow = false;
			}

			// Force a redraw to adjust for size
			Redraw();

		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal void MoveToPoint(nfloat x, nfloat y)
		{

			//Ensure that we are moving as expected
			_startLocation = new CGPoint(0, 0);

			//Create a new point and move to it
			MoveToPoint(new CGPoint(x, y));
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		internal void MoveToPoint(CGPoint pt)
		{

			//Move to the given location
			Frame = new CGRect(pt.X, pt.Y, Frame.Width, Frame.Height);
		}

		/// <summary>
		/// Resize this <c>ACTile</c> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		internal void Resize(nfloat width, nfloat height)
		{
			//Resize this view
			Frame = new CGRect(Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <c>ACTile</c> to the given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		internal void RotateTo(float degrees)
		{
			this.Transform = CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if the point was inside, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal bool PointInside(nfloat x, nfloat y)
		{
			//Is the give x inside
			if (x >= Frame.X && x <= (Frame.X + Frame.Width))
			{
				if (y >= Frame.Y && y <= (Frame.Y + Frame.Height))
				{
					//Inside
					return true;
				}
			}

			//Not inside
			return false;
		}

		/// <summary>
		/// Test to see if the given point is inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if point was inside, <c>false</c> otherwise.</returns>
		/// <param name="pt">Point.</param>
		internal bool PointInside(CGPoint pt)
		{
			return PointInside(pt.X, pt.Y);
		}

		/// <summary>
		/// The <c>Purge</c> command causes this <c>ACTile</c> to force the removal of any
		/// subview from the screen and dispose of the memory that they contain. If <c>forceGarbageCollection</c> is <c>true</c>, garbage collection
		/// will be forced at the end of the purge cycle. The <c>Purge</c> command will cascade to any <c>ACTile</c>
		/// or <c>ACImageView</c> subviews attached to this <c>ACTile</c> 
		/// </summary>
		/// <param name="forceGarbageCollection">If set to <c>true</c> force garbage collection.</param>
		/// <remarks>Special handling is taken on <c>UIImageViews</c> to ensure that they fully release any image memory that they contain. Simply
		/// calling <c>Dispose()</c> doesn't always release the child bitmaps in the <c>UIImageView</c>'s <c>Image</c> property.</remarks>
		internal void Purge(bool forceGarbageCollection)
		{

			//Release any subviews that are attached to this view
			foreach (UIView view in Subviews)
			{

				//Remove the view from it's superview
				view.RemoveFromSuperview();

				//Trap any errors
				try
				{
					//Look for any speciality views and take extra cleaning action
					if (view is ACTile)
					{
						//Call child's purge routine
						((ACTile)view).Purge(false);
					}
					else if (view is UIImageView)
					{
						//Force the image view to release the memory being held by it's
						//image, if one exists
						if (((UIImageView)view).Image != null)
						{
							((UIImageView)view).Image.Dispose();
							((UIImageView)view).Image = null;
						}
						view.Dispose();
					}
					else
					{
						//Force disposal of this subview
						view.Dispose();
					}

				}
				catch
				{
					#if DEBUG
					//Report disposal issue
					Console.WriteLine("Unable to purge {0}", view);
					#endif
				}
			}

			//Are we forcing garbage collection?
			if (forceGarbageCollection)
			{
				//Yes, tell garbage collector to kick-off
				System.GC.Collect();

				#if DEBUG
				Console.WriteLine("GC Memory usage {0}", System.GC.GetTotalMemory(true));
				Console.WriteLine("GC GEN:{0} Count:{1}", GC.GetGeneration(this), GC.CollectionCount(GC.GetGeneration(this)));
				#endif
			}

		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Cascades the tile appearance to each tile in this group
		/// </summary>
		private void CascadeTileAppearance()
		{
			//Send appearance modifications to each tile in this group
			foreach (ACTile tile in _tiles)
			{
				tile.appearance = _defaultTileAppearance;
			}
		}

		/// <summary>
		/// Checks to see if the tile will fit in the current location.
		/// </summary>
		/// <returns><c>true</c>, if tile fits in location, <c>false</c> otherwise.</returns>
		/// <param name="rows">The maximum number of rows.</param>
		/// <param name="cols">The maximum number of columns.</param>
		/// <param name="r">The current row.</param>
		/// <param name="c">The current column.</param>
		/// <param name="tile">The tile to check.</param>
		private bool DoesTileFitInLocation(int rows, int cols, int r, int c, ACTile tile)
		{
			var occupied = false;

			//Test to see if the current cell will fit
			switch (tile.tileSize)
			{
				case ACTileSize.DoubleHorizontal:
					if ((c + 1) >= cols) {
						occupied = true;
					} else {
						occupied = grid[r, c] || grid[r, c + 1];
					}
					if (shrinkTilesToFit && !grid[r, c] && occupied)
					{
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell();
						occupied = false;
					}
					break;
				case ACTileSize.QuadHorizontal:
					if ((c + 3) >= cols)
					{
						occupied = true;
					}
					else
					{
						occupied = grid[r, c] || grid[r, c + 1] || grid[r, c + 2] || grid[r, c + 3];
					}
					if (shrinkTilesToFit && !grid[r, c] && occupied)
					{
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell();
						occupied = false;
					}
					break;
				case ACTileSize.DoubleVertical:
					if ((r + 1) >= rows)
					{
						occupied = true;
					}
					else
					{
						occupied = grid[r, c] || grid[r + 1, c];
					}
					if (shrinkTilesToFit && !grid[r, c] && occupied)
					{
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell();
						occupied = false;
					}
					break;
				case ACTileSize.Quad:
					if (((r + 1) >= rows) || ((c + 1) >= cols))
					{
						occupied = true;
					}
					else
					{
						occupied = grid[r, c] || grid[r, c + 1] || grid[r + 1, c] || grid[r + 1, c + 1];
					}
					if (shrinkTilesToFit && !grid[r, c] && occupied)
					{
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell();
						occupied = false;
					}
					break;
				case ACTileSize.Custom:
					if (((r + (tile.customRowHeight - 1)) >= rows) || ((c + (tile.customColumnHeight - 1)) >= cols))
					{
						occupied = true;
					}
					else
					{
						occupied = false;
						for (int x = 0; x < tile.customColumnHeight; ++ x) {
							for (int y = 0; y < tile.customRowHeight; ++ y) {
								occupied = occupied || grid[r + y, c + x];
							}
						}
					}
					if (shrinkTilesToFit && !grid[r, c] && occupied)
					{
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell();
						occupied = false;
					}
					break;
				default:
					occupied = grid[r, c];
					break;
			}

			// Return results
			return occupied;
		}

		/// <summary>
		/// Marks the cells used by the given tile at the give location.
		/// </summary>
		/// <param name="r">The current row.</param>
		/// <param name="c">The current column.</param>
		/// <param name="tile">The tile to drop into the given location.</param>
		private void MarkCellsUsed(int r, int c, ACTile tile)
		{

			//Mark cells as used based on the tile size
			switch (tile.tileSize)
			{
				case ACTileSize.Single:
					grid[r, c] = true;
					break;
				case ACTileSize.DoubleHorizontal:
					grid[r, c] = true;
					grid[r, c + 1] = true;
					break;
				case ACTileSize.QuadHorizontal:
					grid[r, c] = true;
					grid[r, c + 1] = true;
					grid[r, c + 2] = true;
					grid[r, c + 3] = true;
					break;
				case ACTileSize.DoubleVertical:
					grid[r, c] = true;
					grid[r + 1, c] = true;
					break;
				case ACTileSize.Quad:
					grid[r, c] = true;
					grid[r + 1, c] = true;
					grid[r, c + 1] = true;
					grid[r + 1, c + 1] = true;
					break;
				case ACTileSize.Custom:
					for (int x = 0; x < tile.customColumnHeight; ++x)
					{
						for (int y = 0; y < tile.customRowHeight; ++y)
						{
							grid[r + y, c + x] = true;
						}
					}
					break;
			}
		}

		/// <summary>
		/// Reflows the tiles top to bottom into the first available place that they will fit regardless of the
		/// actual order of the tiles.
		/// </summary>
		/// <returns>The widest point found.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesTopToBottomAndAutoFit(nfloat width, nfloat height) {
			nfloat x = 0, y = 0, w = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			nfloat widestPoint = 0;
			bool occupied;

			// Anything to process?
			if (_tiles.Count == 0)
				return 0;

			// Does this group have a title?
			if (title != "")
			{
				// Calculate title height
				titleHeight = ACFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			// Does this group have a footer?
			if (footer != "")
			{
				// Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize(footer, UIFont.SystemFontOfSize(appearance.footerSize), new CGSize(100f, 35f), UILineBreakMode.Clip).Height;
			}

			// Subtract title and footer from available tile space
			height -= (titleHeight + footerHeight);

			// Calculate the number of rows that can fit inside the given height
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int rows = (int)(height / cellSpace);

			// Calculate the number of virtual columns that would be needed to hold the largest tiles
			int cols = ((_tiles.Count / rows) + 1) * 4;

			// Create the virtual grid required to layout the tiles
			grid = new bool[rows, cols];
			int r = 0, c = 0;

			// Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles)
			{
				// Scan columns 
				c = 0;
				do
				{
					// Scan rows in the given column
					r = 0;
					do
					{
						// Does the tile fit in the current location?
						occupied = DoesTileFitInLocation(rows, cols, r, c, tile);

						// Was the location found?
						if (occupied)
						{
							// No, move to the next row
							++r;
						}
					} while (occupied && (r < rows));

					// Was the location found?
					if (occupied) {
						// No, move to the next column
						++c;
					}
				} while (occupied && (c < cols));

				// Was a location found?
				if (occupied) {
					// No, return the widest point
					return widestPoint;
				}

				// Calculate x and y coordinates off of the given grid
				x = _controller.appearance.cellGap + (c * cellSpace);
				y = titleHeight + _controller.appearance.cellGap + (r * cellSpace);

				// Calculate width at this location
				w = x + tile.Frame.Width + _controller.appearance.cellGap;

				// Is this point wider?
				if (w > widestPoint)
					widestPoint = w;

				// Are we at our maximum width
				if (width != 0)
				{
					if (widestPoint > width)
						return width;
				}

				// Move tile into position
				tile.MoveToPoint(x, y);

				// Mark cells as used based on the tile size
				MarkCellsUsed(r, c, tile);
			}

			//Return new width
			return widestPoint;
		}

		/// <summary>
		/// Reflows the tiles top to bottom.
		/// </summary>
		/// <returns>The tiles top to bottom.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesTopToBottom(nfloat width, nfloat height)
		{
			nfloat x = 0, y = 0, w = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			nfloat widestPoint = 0;
			bool occupied;

			//Anything to process?
			if (_tiles.Count == 0)
				return 0;

			//Does this group have a title?
			if (title != "")
			{
				//Calculate title height
				titleHeight = ACFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Does this group have a footer?
			if (footer != "")
			{
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize(footer, UIFont.SystemFontOfSize(appearance.footerSize), new CGSize(100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Subtract title and footer from available tile space
			height -= (titleHeight + footerHeight);

			//Calculate the number of rows that can fit inside the given height
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int rows = (int)(height / cellSpace);

			//Calculate the number of virtual columns that would be needed to hold the largest tiles
			int cols = ((_tiles.Count / rows) + 1) * 4;

			//Create the virtual grid required to layout the tiles
			grid = new bool[rows, cols];
			int r = 0, c = 0;

			//Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles)
			{
				// Test to see if the current cell will fit
				occupied = DoesTileFitInLocation(rows, cols, r, c, tile);

				// Find the next available cell
				while (occupied)
				{
					//Increment row
					if (++r >= rows)
					{
						//Reset row and increment column
						r = 0;
						++c;
					}

					//Can the tile fit in the requested space?
					if ((r == (rows - 1)) && (tile.tileSize == ACTileSize.DoubleVertical || tile.tileSize == ACTileSize.Quad))
					{
						if (shrinkTilesToFit)
						{
							//Adjust tile to fit
							tile.ShrinkTileToSingleCell();
						}
						else
						{
							//Won't fit, reset row and increment column
							r = 0;
							++c;
						}
					}

					//Are we out of room?
					if (c >= cols)
					{
						//Yes, return current estimated width
						return widestPoint;
					}

					//Test to see if the current cell will fit
					occupied = DoesTileFitInLocation(rows, cols, r, c, tile);
				}

				// Calculate x and y coordinates off of the given grid
				x = _controller.appearance.cellGap + (c * cellSpace);
				y = titleHeight + _controller.appearance.cellGap + (r * cellSpace);

				// Calculate width at this location
				w = x + tile.Frame.Width + _controller.appearance.cellGap;

				// Is this point wider?
				if (w > widestPoint)
					widestPoint = w;

				// Are we at our maximum width
				if (width != 0)
				{
					if (widestPoint > width)
						return width;
				}

				// Move tile into position
				tile.MoveToPoint(x, y);

				// Mark cells as used based on the tile size
				MarkCellsUsed(r, c, tile);
			}

			//Return new width
			return widestPoint;
		}

		/// <summary>
		/// Reflows the tiles left to right and auto fits the tiles into the first available location regardless
		/// of the tile order.
		/// </summary>
		/// <returns>The tallest point.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesLeftToRightAndAutoFit(nfloat width, nfloat height)
		{
			nfloat x = 0, y = 0, h = 0;
			nfloat tallestPoint = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			bool occupied;

			//Anything to process?
			if (_tiles.Count == 0)
				return 0;

			//Does this group have a title?
			if (title != "")
			{
				//Calculate title height
				titleHeight = ACFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Does this group have a footer?
			if (footer != "")
			{
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize(footer, UIFont.SystemFontOfSize(appearance.footerSize), new CGSize(100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Calculate the number of columns that can fit inside the given width
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int cols = (int)(width / cellSpace);

			//Calculate the number of virtual rows that would be needed to hold the largest tiles
			int rows = ((_tiles.Count / cols) + 1) * 4;

			//Create the virtual grid required to layout the tiles
			grid = new bool[rows, cols];
			int r = 0, c = 0;

			//Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles)
			{
				// Scan Rows 
				r = 0;
				do
				{
					// Scan columns in the given row
					c = 0;
					do
					{
						// Does the tile fit in the current location?
						occupied = DoesTileFitInLocation(rows, cols, r, c, tile);

						// Was the location found?
						if (occupied)
						{
							// No, move to the next column
							++c;
						}
					} while (occupied && (c < cols));

					// Was the location found?
					if (occupied)
					{
						// No, move to the next row
						++r;
					}
				} while (occupied && (r < rows));

				// Was a location found?
				if (occupied)
				{
					// No, return the tallest point
					return tallestPoint;
				}

				//Calculate x and y coordinates off of the given grid
				x = _controller.appearance.cellGap + (c * cellSpace);
				y = titleHeight + _controller.appearance.cellGap + (r * cellSpace);

				//Calculate height at this location
				h = y + tile.Frame.Height + _controller.appearance.cellGap;

				//Is this point taller?
				if (h > tallestPoint)
					tallestPoint = h;

				//Are we at our maximum height
				if (height != 0)
				{
					if (tallestPoint > height)
						return height;
				}

				//Move tile into position
				tile.MoveToPoint(x, y);

				// Mark cells as used based on the tile size
				MarkCellsUsed(r, c, tile);
			}

			//Return new width
			return tallestPoint;
		}

		/// <summary>
		/// Reflows the tiles left to right.
		/// </summary>
		/// <returns>The tiles left to right.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesLeftToRight(nfloat width, nfloat height)
		{
			nfloat x = 0, y = 0, h = 0;
			nfloat tallestPoint = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			bool occupied;

			//Anything to process?
			if (_tiles.Count == 0)
				return 0;

			//Does this group have a title?
			if (title != "")
			{
				//Calculate title height
				titleHeight = ACFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Does this group have a footer?
			if (footer != "")
			{
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize(footer, UIFont.SystemFontOfSize(appearance.footerSize), new CGSize(100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Calculate the number of columns that can fit inside the given width
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int cols = (int)(width / cellSpace);

			//Calculate the number of virtual rows that would be needed to hold the largest tiles
			int rows = ((_tiles.Count / cols) + 1) * 4;

			//Create the virtual grid required to layout the tiles
			grid = new bool[rows, cols];
			int r = 0, c = 0;

			//Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles)
			{
				// Check to see if the tile fits in the current location
				occupied = DoesTileFitInLocation(rows, cols, r, c, tile);

				// Find the next available cell
				while (occupied)
				{
					//Increment column
					if (++c >= cols)
					{
						//Reset column and increment row
						c = 0;
						++r;
					}

					//Can the tile fit in the requested space?
					switch (tile.tileSize)
					{
						case ACTileSize.DoubleHorizontal:
						case ACTileSize.Quad:
							if (c == (cols - 1))
							{
								if (shrinkTilesToFit)
								{
									//Adjust tile to fit
									tile.ShrinkTileToSingleCell();
								}
								else
								{
									//Won't fit, reset row and increment column
									c = 0;
									++r;
								}
							}
							break;
						case ACTileSize.QuadHorizontal:
							if ((c + 3) > (cols -1)) {
								if (shrinkTilesToFit)
								{
									//Adjust tile to fit
									tile.ShrinkTileToSingleCell();
								}
								else
								{
									//Won't fit, reset row and increment column
									c = 0;
									++r;
								}
							}
							break;
					}

					//Are we out of room?
					if (r >= rows)
					{
						//Yes, return current estimated height
						return tallestPoint;
					}

					// Check to see if the tile fits in the current location
					occupied = DoesTileFitInLocation(rows, cols, r, c, tile);
				}

				//Calculate x and y coordinates off of the given grid
				x = _controller.appearance.cellGap + (c * cellSpace);
				y = titleHeight + _controller.appearance.cellGap + (r * cellSpace);

				//Calculate height at this location
				h = y + tile.Frame.Height + _controller.appearance.cellGap;

				//Is this point taller?
				if (h > tallestPoint)
					tallestPoint = h;

				//Are we at our maximum height
				if (height != 0)
				{
					if (tallestPoint > height)
						return height;
				}

				//Move tile into position
				tile.MoveToPoint(x, y);

				// Mark cells as used based on the tile size
				MarkCellsUsed(r, c, tile);
			}

			//Return new width
			return tallestPoint;
		}

		/// <summary>
		/// Calculates the width of the group.
		/// </summary>
		/// <returns>The group width.</returns>
		private nfloat CalculateGroupWidth()
		{
			nfloat width = 0, height = 0;

			//Take action based on the column constraints
			switch (columnConstraint.constraintType)
			{
				case ACTileGroupCellConstraintType.Fixed:
					//Calculate width based on a fixed number of cells
					width = _controller.appearance.cellGap + ((_controller.appearance.cellSize + _controller.appearance.cellGap) * columnConstraint.maximum);
					break;
				case ACTileGroupCellConstraintType.FitParent:
					//Calulate a width that will fit inside the controller sans margins
					width = _controller.scrollView.Frame.Width;
					break;
				case ACTileGroupCellConstraintType.Flexible:
					// Take action based on the scroll direction
					if (_controller.scrollDirection == ACTileControllerScrollDirection.Vertical) {
						width = _controller.scrollView.Frame.Width;
					} else {
						//Calculate the height
						height = CalculateGroupHeight();

						//Reflow all the tiles using the given height and calculate a new width
						if (autoFitTiles) {
							width = ReflowTilesTopToBottomAndAutoFit(0, height);
						} else {
							width = ReflowTilesTopToBottom(0, height);
						}

						//Inform caller that we've already reflowed the tiles as a result of the calculation
						_needsReflow = false;
					}
					break;
			}

			//Is this the minimum size?
			if (width == 0)
			{
				//Set as cell size and two gaps
				width = (_controller.appearance.cellGap * 2) + _controller.appearance.cellSize;
			}

			//Return adjust width
			return width;
		}

		/// <summary>
		/// Calculates the height of the group.
		/// </summary>
		/// <returns>The group height.</returns>
		private nfloat CalculateGroupHeight()
		{
			var barHeight = _controller.navigationBar.hidden ? 0 : _controller.navigationBar.appearance.totalHeight;
			nfloat height = 0, width = 0, titleHeight = 0, footerHeight = 0;
			var includeTitles = false;

			//Does this group have a title?
			if (title != "")
			{
				//Calculate title height
				titleHeight = ACFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(Frame.Width, _controller.Frame.Height), UILineBreakMode.Clip).Height;
			}

			//Does this group have a footer?
			if (footer != "")
			{
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize(footer, UIFont.SystemFontOfSize(appearance.footerSize), new CGSize(Frame.Width, _controller.Frame.Height), UILineBreakMode.Clip).Height;
			}

			//Take action based on the column constraints
			switch (rowConstraint.constraintType)
			{
				case ACTileGroupCellConstraintType.Fixed:
					//Calculate width based on a fixed number of cells
					height = _controller.appearance.cellGap + ((_controller.appearance.cellSize + _controller.appearance.cellGap) * rowConstraint.maximum);

					//Add in title and footer heights
					includeTitles = true;
					break;
				case ACTileGroupCellConstraintType.FitParent:
					//Calulate a height that will fit inside the controller sans margins
					height = _controller.scrollView.Frame.Height;
					break;
				case ACTileGroupCellConstraintType.Flexible:
					// Calculate based on the scroll direction
					if (_controller.scrollDirection == ACTileControllerScrollDirection.Horizontal) {
						height = _controller.scrollView.Frame.Height;
					} else {
						//Calculate the width
						width = CalculateGroupWidth();

						// Reflow all the tiles using the given width and calculate a new height
						if (autoFitTiles) {
							height = ReflowTilesLeftToRightAndAutoFit(width, 0);
						} else {
							height = ReflowTilesLeftToRight(width, 0);
						}

						//Add in title and footer heights
						includeTitles = true;

						//Inform caller that we've already reflowed the tiles as a result of the calculation
						_needsReflow = false;
					}
					break;
			}

			//Is this the minimum size?
			if (height == 0)
			{
				//Set as cell size and two gaps
				height = (_controller.appearance.cellGap * 2) + _controller.appearance.cellSize;
			}

			// Needs title heights?
			if (includeTitles) {
				height += titleHeight + footerHeight;
			}

			//Return adjust width
			return height;
		}
		#endregion

		#region Private Drawing Methods
		/// <summary>
		/// Sets the corner.
		/// </summary>
		/// <returns>The corner.</returns>
		/// <param name="corners">Corners.</param>
		/// <param name="addCorner">Add corner.</param>
		private UIRectCorner SetCorner(UIRectCorner corners, UIRectCorner addCorner)
		{

			//Has a corner been set yet?
			if (corners == UIRectCorner.AllCorners)
			{
				corners = addCorner;
			}
			else
			{
				corners |= addCorner;
			}

			//Return adjusted corners
			return corners;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			// Initial states
			nfloat titleHeight = 0, footerHeight = 0;

			// Has header?
			if (title != "")
			{
				var textContent = title;
				appearance.titleColor.SetFill();
				var titleStyle = new NSMutableParagraphStyle();
				titleStyle.Alignment = UITextAlignment.Left;
				var titleFontAttributes = new UIStringAttributes { Font = UIFont.SystemFontOfSize(appearance.titleSize), ForegroundColor = appearance.titleColor.ToUIColor(), ParagraphStyle = titleStyle };
				var titleTextHeight = new NSString(textContent).GetBoundingRect(new CGSize(rect.Width, nfloat.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, titleFontAttributes, null).Height;
				context.SaveState();
				var titleRect = new CGRect(rect.X, rect.Y, rect.Width, titleTextHeight);
				context.ClipToRect(titleRect);
				new NSString(textContent).DrawString(titleRect, titleFontAttributes);
				context.RestoreState();
				titleHeight = _controller.appearance.cellGap + titleTextHeight;
			}

			// Has footer?
			if (footer != "")
			{
				var textContent = footer;
				appearance.footerColor.SetFill();
				var footerStyle = new NSMutableParagraphStyle();
				footerStyle.Alignment = UITextAlignment.Left;
				var footerFontAttributes = new UIStringAttributes { Font = UIFont.SystemFontOfSize(appearance.footerSize), ForegroundColor = appearance.footerColor.ToUIColor(), ParagraphStyle = footerStyle };
				var footerTextHeight = new NSString(textContent).GetBoundingRect(new CGSize(rect.Width, nfloat.MaxValue), NSStringDrawingOptions.UsesLineFragmentOrigin, footerFontAttributes, null).Height;
				context.SaveState();
				var footerRect = new CGRect(rect.X, rect.Height - footerTextHeight, rect.Width, footerTextHeight);
				context.ClipToRect(footerRect);
				new NSString(textContent).DrawString(new CGRect(footerRect.GetMinX(), footerRect.GetMinY() + (footerRect.Height - footerTextHeight) / 2.0f, footerRect.Width, footerTextHeight), footerFontAttributes);
				context.RestoreState();
				footerHeight = _controller.appearance.cellGap + footerTextHeight;
			}

			//Does this group have a background?
			if (appearance.hasBackground)
			{
				UIBezierPath groupBodyPath;
				UIRectCorner corners = UIRectCorner.AllCorners;
				var groupRect = new CGRect(rect.X, rect.Y + titleHeight, rect.Width, rect.Height - (titleHeight + footerHeight));

				//// Shadow Declarations
				var groupShadow = appearance.shadow.CGColor;
				var groupShadowOffset = new CGSize(0.1f, 3.1f);
				var groupShadowBlurRadius = 5;

				//// groupBox
				{

					//// AlertBody Drawing
					if (appearance.isRect)
					{
						//It is a perfect rectangle
						groupBodyPath = UIBezierPath.FromRect(groupRect);
					}
					else if (appearance.isRoundRect)
					{
						//It is a perfect round rectangle
						groupBodyPath = UIBezierPath.FromRoundedRect(groupRect, appearance.borderRadius);
					}
					else
					{
						//It is a round rectangle with one or more square corners
						//Calculate corners
						if (appearance.roundTopLeftCorner)
							corners = SetCorner(corners, UIRectCorner.TopLeft);
						if (appearance.roundTopRightCorner)
							corners = SetCorner(corners, UIRectCorner.TopRight);
						if (appearance.roundBottomLeftCorner)
							corners = SetCorner(corners, UIRectCorner.BottomLeft);
						if (appearance.roundBottomRightCorner)
							corners = SetCorner(corners, UIRectCorner.BottomRight);

						//Make path
						groupBodyPath = UIBezierPath.FromRoundedRect(groupRect, corners, new CGSize(appearance.borderRadius, appearance.borderRadius));
					}
					context.SaveState();
					context.SetShadow(groupShadowOffset, groupShadowBlurRadius, groupShadow);
					appearance.background.SetFill();
					groupBodyPath.Fill();
					context.RestoreState();

					appearance.border.SetStroke();
					groupBodyPath.LineWidth = appearance.borderWidth;
					groupBodyPath.Stroke();

				}
			}
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			if (_bringToFrontOnTouched && this.Superview != null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched();

			//Pass call to base object
			base.TouchesBegan(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <c>ACTile</c> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			//Pass call to base object
			base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			//Clear starting point
			_startLocation = new CGPoint(0, 0);

			//Inform caller of event
			RaiseReleased();

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is touched 
		/// </summary>
		public delegate void ACTileGroupTouchedDelegate(ACTileGroup view);
		public event ACTileGroupTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched()
		{
			if (this.Touched != null)
				this.Touched(this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is moved
		/// </summary>
		public delegate void ACTileGroupMovedDelegate(ACTileGroup view);
		public event ACTileGroupMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved()
		{
			if (this.Moved != null)
				this.Moved(this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is released 
		/// </summary>
		public delegate void ACTileGroupReleasedDelegate(ACTileGroup view);
		public event ACTileGroupReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased()
		{
			if (this.Released != null)
				this.Released(this);
		}

		/// <summary>
		/// Occurs when live updating has been kicked off by the <c>ACTileController</c> 
		/// </summary>
		public delegate void ACTileGroupLiveUpdatingDelegate(ACTileGroup group);
		public event ACTileGroupLiveUpdatingDelegate LiveUpdating;

		/// <summary>
		/// Raises the live updating.
		/// </summary>
		internal void RaiseLiveUpdating()
		{
			if (this.LiveUpdating != null)
				this.LiveUpdating(this);
		}

		/// <summary>
		/// Occurs when a tile is touched in this group.
		/// </summary>
		public delegate void ACTileGroupTileTouchedDelegate(ACTileGroup group, ACTile tile);
		public event ACTileGroupTileTouchedDelegate TileTouched;

		/// <summary>
		/// Raises the tile touched event.
		/// </summary>
		/// <param name="tile">Tile.</param>
		internal void RaiseTileTouched(ACTile tile) {
			if (this.TileTouched != null) {
				this.TileTouched(this, tile);
			}
		}
		#endregion
	}
}
