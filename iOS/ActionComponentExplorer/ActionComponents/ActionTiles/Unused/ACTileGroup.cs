using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;

namespace ActionComponents
{
	public class ACTileGroup : UIView
	{
		#region Private Variables
		private bool _isDraggable;
		private bool _dragging;
		private bool _bringToFrontOnTouched;
		private ACTileDragConstraint _xConstraint;
		private ACTileDragConstraint _yConstraint;
		private CGPoint _startLocation;
		private ACTileController _controller;
		private List<ACTile> _tiles = new List<ACTile> ();
		private string _title = "";
		private string _footer = "";
		private ACTileGroupAppearance _appearance;
		private ACTileAppearance _defaultTileAppearance;
		private ACTileGroupCellConstraint _columnConstraint;
		private ACTileGroupCellConstraint _rowConstraint;
		private bool _needsReflow = false;
		private ACTileGroupType _groupType;
		private bool _autoFitTiles = false;
		private ACTileLiveUpdate _liveUpdateAction;
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
		public ACTileAppearance defaultTileAppearance {
			get { return _defaultTileAppearance;}
			set {
				_defaultTileAppearance = value;
				CascadeTileAppearance ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileLiveUpdate</c> action that will be performed via an automatic update
		/// kicked off by the <c>liveUpdateTimer</c> in the parent <c>ACTileController</c> 
		/// </summary>
		/// <value>The live update action.</value>
		public ACTileLiveUpdate liveUpdateAction{
			get { return _liveUpdateAction;}
			set{ _liveUpdateAction = value;}
		}

		/// <summary>
		/// Gets the <c>ACTileGroupType</c> of this <c>ACTileGroup</c> .
		/// </summary>
		/// <value>The type of the group.</value>
		public ACTileGroupType groupType{
			get { return _groupType;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <c>ACTileGroup</c> will auto fit <c>ActionTile</c>s that are larger than a single
		/// cell by resizing them to a single cell. 
		/// </summary>
		/// <value><c>true</c> if auto fit tiles; otherwise, <c>false</c>.</value>
		public bool autoFitTiles{
			get { return _autoFitTiles;}
			set {
				_autoFitTiles = value;
				Redraw (true);
			}
		}

		/// <summary>
		/// Gets the <c>ACTileController</c> that this <c>ACTileGroup</c>
		/// belongs to  
		/// </summary>
		/// <value>The controller.</value>
		public ACTileController controller {
			get { return _controller;}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileGroupAppearance</c> for this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileGroupAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;
				Redraw(false);
			}
		}

		/// <summary>
		/// Gets the column <c>ACTileGroupCellConstraint</c> fron this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The column constraint.</value>
		internal ACTileGroupCellConstraint columnConstraint{
			get { return _columnConstraint;}
		}

		/// <summary>
		/// Gets the row <c>ACTileGroupCellConstraint</c> for this <c>ACTileGroup</c>  
		/// </summary>
		/// <value>The row constraint.</value>
		internal ACTileGroupCellConstraint rowConstraint{
			get { return _rowConstraint;}
		}

		/// <summary>
		/// Gets or sets the title for this <c>ACTileGroup</c> 
		/// </summary>
		/// <value>The title.</value>
		public string title {
			get { return _title;}
			set {
				_title = value;
				Redraw (true);
			}
		}

		/// <summary>
		/// Gets or sets the footer for this <c>ACTileGroup</c> 
		/// </summary>
		/// <value>The footer.</value>
		public string footer {
			get { return _footer;}
			set {
				_footer = value;
				Redraw (true);
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
		public int Count {
			get { return _tiles.Count;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is draggable.
		/// </summary>
		/// <value><c>true</c> if is draggable; otherwise, <c>false</c>.</value>
		internal bool draggable {
			get { return _isDraggable;}
			set { _isDraggable = value;}
		}

		/// <summary>
		/// Gets a value indicating whether this <c>ACTile</c> is being dragged by the user.
		/// </summary>
		/// <value><c>true</c> if dragging; otherwise, <c>false</c>.</value>
		public bool dragging{
			get { return _dragging;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		internal bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileDragConstraint</c> applied to the <c>x axis</c> of this
		/// <c>ACTile</c> 
		/// </summary>
		/// <value>The x constraint.</value>
		internal ACTileDragConstraint xConstraint{
			get { return _xConstraint;}
			set {
				_xConstraint = value;

				//Wireup changed event
				_xConstraint.ConstraintChanged += () => {
					XConstraintModified();
				};

				//Fire event
				XConstraintModified ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileDragConstraint</c> applied to the <c>y axis</c> of this
		/// <c>ACTile</c> 
		/// </summary>
		/// <value>The y constraint.</value>
		internal ACTileDragConstraint yConstraint{
			get { return _yConstraint;}
			set {
				_yConstraint = value;

				//Wireup changed event
				_yConstraint.ConstraintChanged += () => {
					YConstraintModified();
				};

				//Fire event
				YConstraintModified ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <c>ACTile</c>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <c>ACTile</c> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public bool Enabled{
			get { return UserInteractionEnabled;}
			set { UserInteractionEnabled = value;}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ActionComponents.ACTileGroup"/> class.
		/// </summary>
		internal ACTileGroup() {
			//Set initial values
			this._groupType = ACTileGroupType.FixedSizeGroup;
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACTileGroup</c> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		/// <param name="groupType">Group type.</param>
		/// <param name="columnConstraint">Column constraint.</param>
		/// <param name="rowConstraint">Row constraint.</param>
		internal ACTileGroup (ACTileController controller, ACTileGroupType groupType, string title, string footer, ACTileGroupCellConstraint columnConstraint, ACTileGroupCellConstraint rowConstraint) : base()
		{
			//Set initial values
			this._groupType = groupType;
			this._controller = controller;
			this._columnConstraint = columnConstraint;
			this._rowConstraint = rowConstraint;
			this._appearance = _controller.groupAppearance.Clone();
			this._title = title;
			this._footer = footer;

			//TODO: Might need to wire-up changes to row and column constraints

			//wire-up events
			this._appearance.AppearanceModified += () => {
				Redraw(false);
			};

			//Init
			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set defaults
			this.BackgroundColor = ACColor.Clear;
			this._isDraggable = false;
			this._dragging = false;
			this._bringToFrontOnTouched = false;
			this._xConstraint = new ACTileDragConstraint ();
			this._yConstraint = new ACTileDragConstraint ();
			this._startLocation = new CGPoint (0, 0);
			this._defaultTileAppearance = new ACTileAppearance ();
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;

			//Wireup change events
			this._xConstraint.ConstraintChanged+= () => {
				XConstraintModified();
			};
			this._yConstraint.ConstraintChanged+= () => {
				YConstraintModified();
			};

			//Calculate the initial size
			RecalculateSize ();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Cascades the tile appearance to each tile in this group
		/// </summary>
		private void CascadeTileAppearance(){
			//Send appearance modifications to each tile in this group
			foreach (ACTile tile in _tiles) {
				tile.appearance = _defaultTileAppearance;
			}
		}

		/// <summary>
		/// Reflows the tiles top to bottom.
		/// </summary>
		/// <returns>The tiles top to bottom.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesTopToBottom(nfloat width, nfloat height) {
			nfloat x = 0, y = 0, w = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			nfloat widestPoint = 0;
			bool occupied;

			//Anything to process?
			if (_tiles.Count == 0)
				return width;

			//Does this group have a title?
			if (title != "") {
				//Calculate title height
				titleHeight = ACFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Does this group have a footer?
			if (footer != "") {
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize (footer, UIFont.SystemFontOfSize (appearance.footerSize), new CGSize (100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Subtract title and footer from available tile space
			height -= (titleHeight + footerHeight);

			//Calculate the number of rows that can fit inside the given height
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int rows = (int)(height / cellSpace);

			//Calculate the number of virtual columns that would be needed to hold the largest tiles
			int cols = ((_tiles.Count / rows)+1)*4;

			//Create the virtual grid required to layout the tiles
			bool[,] grid = new bool[rows, cols];
			int r = 0, c = 0;

			//Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles) {
				//Test to see if the current cell will fit
				switch (tile.tileSize) {
				case ACTileSize.DoubleVertical:
				case ACTileSize.Quad:
					//Auto resizing tiles?
					if (autoFitTiles && !grid [r, c] && grid [r + 1, c]) {
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell ();
						occupied = false;
					} else {
						occupied = grid [r, c] || grid [r + 1, c];
					}
					break;
				default:
					occupied = grid [r, c];
					break;
				}

				// Find the next available cell
				while (occupied) {
					//Increment row
					if (++r >= rows) {
						//Reset row and increment column
						r = 0;
						++c;
					}

					//Can the tile fit in the requested space?
					if ((r==(rows-1)) && (tile.tileSize == ACTileSize.DoubleVertical || tile.tileSize == ACTileSize.Quad)) {
						if (autoFitTiles) {
							//Adjust tile to fit
							tile.ShrinkTileToSingleCell ();
						} else {
							//Won't fit, reset row and increment column
							r = 0;
							++c;
						}
					}

					//Are we out of room?
					if (c >= cols) {
						//Yes, return current estimated width
						return widestPoint;
					}

					//Test to see if the current cell will fit
					switch (tile.tileSize) {
					case ACTileSize.DoubleVertical:
					case ACTileSize.Quad:
						//Auto resizing tiles?
						if (autoFitTiles && !grid [r, c] && grid [r + 1, c]) {
							//Adjust tile to fit
							tile.ShrinkTileToSingleCell ();
							occupied = false;
						} else {
							occupied = grid [r, c] || grid [r + 1, c];
						}
						break;
					default:
						occupied = grid [r, c];
						break;
					}
				}

				//Calculate x and y coordinates off of the given grid
				x = _controller.appearance.cellGap + (c * cellSpace);
				y = titleHeight + _controller.appearance.cellGap + (r * cellSpace);

				//Calculate width at this location
				w = x + tile.Frame.Width + _controller.appearance.cellGap;

				//Is this point wider?
				if (w > widestPoint)
					widestPoint = w;

				//Are we at our maximum width
				if (width != 0) {
					if (widestPoint > width)
						return width;
				}

				//Move tile into position
				tile.MoveToPoint (x, y);

				//Mark cells as used based on the tile size
				switch (tile.tileSize) {
				case ACTileSize.Single:
					grid [r, c] = true;
					break;
				case ACTileSize.DoubleHorizontal:
					grid [r, c] = true;
					grid [r, c+1] = true;
					break;
				case ACTileSize.DoubleVertical:
					grid [r, c] = true;
					grid [r+1, c] = true;
					break;
				case ACTileSize.Quad:
					grid [r, c] = true;
					grid [r+1, c] = true;
					grid [r, c+1] = true;
					grid [r+1, c+1] = true;
					break;
				}
			}

			//Return new width
			return widestPoint;
		}

		/// <summary>
		/// Reflows the tiles left to right.
		/// </summary>
		/// <returns>The tiles left to right.</returns>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		private nfloat ReflowTilesLeftToRight(nfloat width, nfloat height) {
			nfloat x = 0, y = 0, h = 0;
			nfloat tallestPoint = 0;
			nfloat titleHeight = 0, footerHeight = 0;
			bool occupied;

			//Anything to process?
			if (_tiles.Count == 0)
				return width;

			//Does this group have a title?
			if (title != "") {
				//Calculate title height
				titleHeight = ACFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (100f, 50f), UILineBreakMode.Clip).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Does this group have a footer?
			if (footer != "") {
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize (footer, UIFont.SystemFontOfSize (appearance.footerSize), new CGSize (100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Calculate the number of columns that can fit inside the given width
			float cellSpace = (_controller.appearance.cellSize + _controller.appearance.cellGap);
			int cols = (int)(width / cellSpace);

			//Calculate the number of virtual rows that would be needed to hold the largest tiles
			int rows = ((_tiles.Count / cols)+1)*4;

			//Create the virtual grid required to layout the tiles
			bool[,] grid = new bool[rows, cols];
			int r = 0, c = 0;

			//Process every tile in this group fit it into the virtual grid
			foreach (ACTile tile in _tiles) {
				//Test to see if the current cell will fit
				switch (tile.tileSize) {
				case ACTileSize.DoubleHorizontal:
				case ACTileSize.Quad:
					//Auto resizing tiles?
					if (autoFitTiles && !grid [r, c] && grid [r, c + 1]) {
						//Adjust tile to fit
						tile.ShrinkTileToSingleCell ();
						occupied = false;
					} else {
						occupied = grid [r, c] || grid [r, c + 1];
					}
					break;
				default:
					occupied = grid [r, c];
					break;
				}

				// Find the next available cell
				while (occupied) {
					//Increment column
					if (++c >= cols) {
						//Reset column and increment row
						c = 0;
						++r;
					}

					//Can the tile fit in the requested space?
					switch (tile.tileSize) {
					case ACTileSize.DoubleHorizontal:
					case ACTileSize.Quad:
						if (c == (cols - 1)) {
							if (autoFitTiles) {
								//Adjust tile to fit
								tile.ShrinkTileToSingleCell ();
							} else {
								//Won't fit, reset row and increment column
								c = 0;
								++r;
							}
						} 
						break;
					}

					//Are we out of room?
					if (r >= rows) {
						//Yes, return current estimated height
						return tallestPoint;
					}

					//Test to see if the current cell will fit
					switch (tile.tileSize) {
					case ACTileSize.DoubleHorizontal:
					case ACTileSize.Quad:
						//Auto resizing tiles?
						if (autoFitTiles && !grid [r, c] && grid [r, c + 1]) {
							//Adjust tile to fit
							tile.ShrinkTileToSingleCell ();
							occupied = false;
						} else {
							occupied = grid [r, c] || grid [r, c + 1];
						}
						break;
					default:
						occupied = grid [r, c];
						break;
					}
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
				if (height != 0) {
					if (tallestPoint > height)
						return height;
				}

				//Move tile into position
				tile.MoveToPoint (x, y);

				//Mark cells as used based on the tile size
				switch (tile.tileSize) {
				case ACTileSize.Single:
					grid [r, c] = true;
					break;
				case ACTileSize.DoubleHorizontal:
					grid [r, c] = true;
					grid [r, c+1] = true;
					break;
				case ACTileSize.DoubleVertical:
					grid [r, c] = true;
					grid [r+1, c] = true;
					break;
				case ACTileSize.Quad:
					grid [r, c] = true;
					grid [r+1, c] = true;
					grid [r, c+1] = true;
					grid [r+1, c+1] = true;
					break;
				}
			}

			//Return new width
			return tallestPoint;
		}

		/// <summary>
		/// Calculates the width of the group.
		/// </summary>
		/// <returns>The group width.</returns>
		private nfloat CalculateGroupWidth(){
			nfloat width = 0, height = 0;

			//Take action based on the column constraints
			switch (columnConstraint.constraintType) {
			case ACTileGroupCellConstraintType.Fixed:
				//Calculate width based on a fixed number of cells
				width = _controller.appearance.cellGap + ((_controller.appearance.cellSize + _controller.appearance.cellGap) * columnConstraint.maximum);
				break;
			case ACTileGroupCellConstraintType.FitParent:
				//Calulate a width that will fit inside the controller sans margins
				width = _controller.Frame.Width - (_controller.appearance.indentLeft + _controller.appearance.indentRight);
				break;
			case ACTileGroupCellConstraintType.Flexible:
				//Calculate the height
				height = CalculateGroupHeight ();

				//Reflow all the tiles using the given height and calculate a new width
				width = ReflowTilesTopToBottom (0, height);

				//Inform caller that we've already reflowed the tiles as a result of the calculation
				_needsReflow = false;
				break;
			}

			//Is this the minimum size?
			if (width == 0) {
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
		private nfloat CalculateGroupHeight(){
			nfloat height = 0, width = 0, titleHeight = 0, footerHeight = 0;

			//Does this group have a title?
			if (title != "") {
				//Calculate title height
				titleHeight = ACFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (100f, 50f), UILineBreakMode.Clip).Height;
			}

			//Does this group have a footer?
			if (footer != "") {
				//Set the footer to a fixed size that is 2x the line height
				footerHeight = ACFont.StringSize (footer, UIFont.SystemFontOfSize (appearance.footerSize), new CGSize (100f, 35f), UILineBreakMode.Clip).Height;
			}

			//Take action based on the column constraints
			switch (rowConstraint.constraintType) {
			case ACTileGroupCellConstraintType.Fixed:
				//Calculate width based on a fixed number of cells
				height = _controller.appearance.cellGap + ((_controller.appearance.cellSize + _controller.appearance.cellGap) * rowConstraint.maximum);

				//Add in title and footer heights
				height += titleHeight + footerHeight;
				break;
			case ACTileGroupCellConstraintType.FitParent:
				//Calulate a height that will fit inside the controller sans margins
				height = _controller.Frame.Height - ((_controller.navigationBar.hidden ? 0 : 50) +  _controller.appearance.indentTop + _controller.appearance.indentBottom);
				break;
			case ACTileGroupCellConstraintType.Flexible:
				//Calculate the width
				width = CalculateGroupWidth ();

				//Reflow all the tiles using the given width and calculate a new height
				height = ReflowTilesLeftToRight (width, 0);

				//Add in title and footer heights
				height += footerHeight;

				//Inform caller that we've already reflowed the tiles as a result of the calculation
				_needsReflow = false;
				break;
			}

			//Is this the minimum size?
			if (height == 0) {
				//Set as cell size and two gaps
				height = (_controller.appearance.cellGap * 2) + _controller.appearance.cellSize;
			}

			//Return adjust width
			return height;
		}

		/// <summary>
		/// Adjust this view if the <c>xConstraint</c> has been modified
		/// </summary>
		private void XConstraintModified(){

			//Take action based on the constraint type
			switch (_xConstraint.constraintType) {
				case ACTileDragConstraintType.Constrained:
				//Make sure the x axis is inside the given range
				if (Frame.Left < _xConstraint.minimumValue || Frame.Left > _xConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (_xConstraint.minimumValue, Frame.Top, Frame.Width, Frame.Height);
				}
				break;
			}

		}

		/// <summary>
		/// Adjust this view if the <c>yConstraint</c> has been modified
		/// </summary>
		private void YConstraintModified(){

			//Take action based on the constraint type
			switch (_yConstraint.constraintType) {
				case ACTileDragConstraintType.Constrained:
				//Make sure the y axis is inside the given range
				if (Frame.Top < _yConstraint.minimumValue || Frame.Top > _yConstraint.maximumValue) {
					//Pin to the minimum value
					Frame = new CGRect (Frame.Left, _yConstraint.minimumValue, Frame.Width, Frame.Height); 
				}
				break;
			}
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Adds the style changer <c>ACTileLiveUpdate</c> action to every
		/// <c>ACTile</c> in this <c>ACTileGroup</c>  
		/// </summary>
		/// <remarks>Avoids changing <c>CustomDrawn</c> or <c>BigPicture</c> styles</remarks>
		public void AddStyleChangeToAllTiles(){
			Random rnd = new Random();

			// Process all tiles
			foreach (ACTile tile in _tiles) {
				// Take action based on the style 
				if (tile.style != ACTileStyle.BigPicture && tile.style != ACTileStyle.CustomDrawn) {
					tile.liveUpdateAction = new ACTileLiveUpdateTileStyle (tile,rnd.Next(0,3));
				}
			}

		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">The title </param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title) {

			//Return new tile
			return AddTile(style, tileSize, title, "", "", null);
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, UIImage icon) {

			//Return new tile
			return AddTile(style, tileSize, title, "", "", icon);
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle) {

			//Return new tile
			return AddTile(style, tileSize, title, subtitle, "", null);
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="icon">Icon.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle, UIImage icon) {

			//Return new tile
			return AddTile(style, tileSize, title, subtitle, "", icon);
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle, string description) {

			//Return new tile
			return AddTile(style, tileSize, title, subtitle, description, null);
		}

		/// <summary>
		/// Adds a new <c>ACTile</c> to this <c>ACTileGroup</c>  
		/// </summary>
		/// <returns>The new <c>ACTile</c> created</returns>
		/// <param name="style">The <c>ACTileStyle</c></param>
		/// <param name="tileSize">The <c>ACTileSize</c> </param>
		/// <param name="title">Title.</param>
		/// <param name="subtitle">Subtitle.</param>
		/// <param name="description">Description.</param>
		/// <param name="icon">Icon.</param>
		public ACTile AddTile(ACTileStyle style, ACTileSize tileSize, string title, string subtitle, string description, UIImage icon) {
			ACTile tile;

			//Create new tile
			tile = new ACTile (this, defaultTileAppearance.Clone(), style, tileSize, title, subtitle, icon, description);

			//Add tile to collection and self
			_tiles.Add (tile);
			AddSubview (tile);

			//Are we updating the display?
			if (!_controller.suspendUpdates) {
				Redraw (true);
			}

			//Return new tile
			return tile;
		}

		/// <summary>
		/// Adds the tile to the group's collection of tiles.
		/// </summary>
		/// <param name="tile">Tile.</param>
		public void AddTile(ACTile tile) {

			//Add tile to collection and self
			_tiles.Add(tile);
			AddSubview(tile);

			//Are we updating the display?
			if (!_controller.suspendUpdates)
			{
				Redraw(true);
			}
		}

		/// <summary>
		/// Removes the <c>ACTile</c> at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveTileAt(int index){
			//Remove requested tile
			_tiles.RemoveAt (index);

			//Are we updating the display?
			if (!_controller.suspendUpdates) {
				Redraw (true);
			}
		}

		/// <summary>
		/// Removes all <c>ACTile</c> from this <c>ACTile</c> 
		/// </summary>
		public void ClearTiles(){
			//Remove all tiles
			_tiles.Clear ();

			//Are we updating the display?
			if (!_controller.suspendUpdates) {
				Redraw (true);
			}
		}

		/// <summary>
		/// Changes the background color of every <c>ACTile</c> in this <c>ACTileGroup</c>
		/// by creating a random brightness off the given base <c>ACColor</c> within the given <c>minimum</c> and <c>maximum</c> ranges
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="minimum">Minimum.</param>
		/// <param name="maximum">Maximum.</param>
		public void ChromaKeyTiles(ACColor background, int minimum, int maximum) {
			Random rnd = new Random();
			nfloat hue=0, saturation=0, brightness=0, alpha=0;

			//Breakdown input color
			background.GetHSBA (ref hue, ref saturation, ref brightness, ref alpha);

			//Process all tiles
			foreach (ACTile tile in _tiles) {
				tile.appearance.background = ACColor.FromHSBA (hue, saturation, rnd.Next (minimum, maximum) / 255f, alpha);
			}

			//Are we updating the display?
			if (!_controller.suspendUpdates) {
				Redraw (true);
			}
		}

		/// <summary>
		/// Forces this <c>ACTileGroup</c> to fully redraw itself
		/// </summary>
		public void Redraw(bool informController) {

			//Recalculate my size
			RecalculateSize ();

			//Force component to update view
			this.SetNeedsDisplay ();

			//If we are not suppressing updates do we need to inform the
			//controller that this group has changed size?
			if (!_controller.suspendUpdates && informController) {
				_controller.Redraw ();
			}
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Checks each tile in this <c>ACTileGroup</c> and sees if it has an attached
		/// <c>ACTileLiveUpdate</c> and executes it if needed 
		/// </summary>
		internal void LiveUpdateTiles(){

			//Check all of the tiles in this collection and see if they have a live update available
			foreach (ACTile tile in _tiles) {
				//Does this tile have a live update?
				if (tile.liveUpdateAction != null) {
					//Yes, execute the action
					tile.liveUpdateAction.PerformUpdate ();
				}

				//Tell the tile that it is live updating
				tile.RaiseLiveUpdating ();
			}

		}

		/// <summary>
		/// Recalculates the height and width of this <c>ACTileGroup</c> based on the size of the containing
		/// <c>ACTileController</c>, the <c>ACTileControllerScrollDirection</c>
		/// and the <c>ACTileGroupCellConstraint</c>s placed on this group. 
		/// </summary>
		internal void RecalculateSize() {

			//Mark as dirty
			_needsReflow = true;

			//Calculate the new size
			var width = CalculateGroupWidth ();
			var height = CalculateGroupHeight ();

			//Adjust size based on calculations
			Resize (width, height);

			//Do we still need to reflow all of the tiles?
			if (_needsReflow) {
				//Yes, do it based on the scroll direction
				switch (_controller.scrollDirection) {
				case ACTileControllerScrollDirection.Horizontal:
					ReflowTilesTopToBottom (width, height);
					break;
				case ACTileControllerScrollDirection.Vertical:
					ReflowTilesLeftToRight (width, height);
					break;
				}

				//Clear flag
				_needsReflow = false;
			}

		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal void MoveToPoint(nfloat x, nfloat y) {

			//Ensure that we are moving as expected
			_startLocation = new CGPoint (0, 0);

			//Create a new point and move to it
			MoveToPoint (new CGPoint(x,y));
		}

		/// <summary>
		/// Moves this <c>ACTile</c> to the given point and honors any
		/// <c>ACTileDragConstraint</c>s applied to the <c>X</c> or <c>Y</c> axis 
		/// </summary>
		/// <param name="pt">Point.</param>
		internal void MoveToPoint(CGPoint pt) {

			//Dragging?
			if (dragging) {

				//Grab frame
				var frame = this.Frame;

				//Process x coord constraint
				switch(xConstraint.constraintType) {
					case ACTileDragConstraintType.None:
					//Adjust frame location
					frame.X += pt.X - _startLocation.X;
					break;
					case ACTileDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACTileDragConstraintType.Constrained:
					//Adjust frame location
					frame.X += pt.X - _startLocation.X;

					//Outside constraints
					if (frame.X<xConstraint.minimumValue) {
						frame.X=xConstraint.minimumValue;
					} else if (frame.X>xConstraint.maximumValue) {
						frame.X=xConstraint.maximumValue;
					}
					break;
				}

				//Process y coord constraint
				switch(yConstraint.constraintType) {
					case ACTileDragConstraintType.None:
					//Adjust frame location
					frame.Y += pt.Y - _startLocation.Y;
					break;
					case ACTileDragConstraintType.Locked:
					//Don't move x coord
					break;
					case ACTileDragConstraintType.Constrained:
					//Adjust frame location
					frame.Y += pt.Y - _startLocation.Y;

					//Outside constraints
					if (frame.Y<yConstraint.minimumValue) {
						frame.Y=yConstraint.minimumValue;
					} else if (frame.Y>yConstraint.maximumValue) {
						frame.Y=yConstraint.maximumValue;
					}
					break;
				}

				//Apply new location
				this.Frame = frame;
			} else {
				//Move to the given location
				Frame = new CGRect (pt.X,pt.Y, Frame.Width, Frame.Height);
			}
		}

		/// <summary>
		/// Resize this <c>ACTile</c> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		internal void Resize(nfloat width, nfloat height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <c>ACTile</c> to the given degrees
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		internal void RotateTo(float degrees) {
			this.Transform=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));	
		}

		/// <summary>
		/// Tests to see if the given x and y coordinates are inside this <c>ACTile</c> 
		/// </summary>
		/// <returns><c>true</c>, if the point was inside, <c>false</c> otherwise.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		internal bool PointInside(nfloat x, nfloat y){
			//Is the give x inside
			if (x>=Frame.X && x<=(Frame.X+Frame.Width)) {
				if (y>=Frame.Y && y<=(Frame.Y+Frame.Height)) {
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
		internal bool PointInside(CGPoint pt){
			return PointInside (pt.X, pt.Y);
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
		internal void Purge(bool forceGarbageCollection){

			//Release any subviews that are attached to this view
			foreach(UIView view in Subviews){

				//Remove the view from it's superview
				view.RemoveFromSuperview ();

				//Trap any errors
				try {
					//Look for any speciality views and take extra cleaning action
					if (view is ACTile) {
						//Call child's purge routine
						((ACTile)view).Purge (false);
					} else if (view is UIImageView) {
						//Force the image view to release the memory being held by it's
						//image, if one exists
						if (((UIImageView)view).Image!=null) {
							((UIImageView)view).Image.Dispose ();
							((UIImageView)view).Image = null;
						}
						view.Dispose ();
					} else {
						//Force disposal of this subview
						view.Dispose ();
					}

				}
				catch {
					#if DEBUG
					//Report disposal issue
					Console.WriteLine ("Unable to purge {0}", view);
					#endif
				}
			}

			//Are we forcing garbage collection?
			if (forceGarbageCollection) {
				//Yes, tell garbage collector to kick-off
				System.GC.Collect();

				#if DEBUG
				Console.WriteLine("GC Memory usage {0}", System.GC.GetTotalMemory(true)); 
				Console.WriteLine("GC GEN:{0} Count:{1}",GC.GetGeneration(this), GC.CollectionCount(GC.GetGeneration(this)));
				#endif
			}

		}
		#endregion

		#region Private Drawing Methods
		/// <summary>
		/// Sets the corner.
		/// </summary>
		/// <returns>The corner.</returns>
		/// <param name="corners">Corners.</param>
		/// <param name="addCorner">Add corner.</param>
		private UIRectCorner SetCorner(UIRectCorner corners, UIRectCorner addCorner){

			//Has a corner been set yet?
			if (corners==UIRectCorner.AllCorners) {
				corners = addCorner;
			} else {
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
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			// Initial states
			nfloat titleHeight = 0, footerHeight = 0;

			//Calculate title height
			if (title != "") {
				titleHeight = ACFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (rect.Width - (_controller.appearance.cellGap * 2), 50f), UILineBreakMode.TailTruncation).Height;
				titleHeight += _controller.appearance.cellGap;
			}

			//Calculate footer height
			if (footer != "") {
				footerHeight = ACFont.StringSize (footer, UIFont.SystemFontOfSize (appearance.footerSize), new CGSize (rect.Width - (_controller.appearance.cellGap * 2), 35f), UILineBreakMode.WordWrap).Height;
			}

			//Create adjusted box for shadow
			CGRect groupRect = new CGRect (rect.Left + 5f, rect.Top + 2f + titleHeight, rect.Width - 10f, rect.Height - (10f+ titleHeight+footerHeight));

			//// General Declarations
			var context = UIGraphics.GetCurrentContext();

			//Does this group have a background?
			if (appearance.hasBackground) {
				UIBezierPath groupBodyPath;
				UIRectCorner corners = UIRectCorner.AllCorners;

				//// Shadow Declarations
				var groupShadow = appearance.shadow.CGColor;
				var groupShadowOffset = new CGSize (0.1f, 3.1f);
				var groupShadowBlurRadius = 5;

				//// groupBox
				{

					//// AlertBody Drawing
					if (appearance.isRect) {
						//It is a perfect rectangle
						groupBodyPath = UIBezierPath.FromRect (groupRect);
					} else if (appearance.isRoundRect) {
						//It is a perfect round rectangle
						groupBodyPath = UIBezierPath.FromRoundedRect (groupRect, appearance.borderRadius);
					} else {
						//It is a round rectangle with one or more square corners
						//Calculate corners
						if (appearance.roundTopLeftCorner)
							corners = SetCorner (corners, UIRectCorner.TopLeft);
						if (appearance.roundTopRightCorner)
							corners = SetCorner (corners, UIRectCorner.TopRight);
						if (appearance.roundBottomLeftCorner)
							corners = SetCorner (corners, UIRectCorner.BottomLeft);
						if (appearance.roundBottomRightCorner)
							corners = SetCorner (corners, UIRectCorner.BottomRight);

						//Make path
						groupBodyPath = UIBezierPath.FromRoundedRect (groupRect, corners, new CGSize (appearance.borderRadius, appearance.borderRadius));
					}
					context.SaveState ();
					context.SetShadow (groupShadowOffset, groupShadowBlurRadius, groupShadow);
					appearance.background.SetFill ();
					groupBodyPath.Fill ();
					context.RestoreState ();

					appearance.border.SetStroke ();
					groupBodyPath.LineWidth = appearance.borderWidth;
					groupBodyPath.Stroke ();

				}
			}

			//Does the group have a title?
			if (title != "") {
				//// Title Drawing
				var titleRect = new CGRect (_controller.appearance.cellGap, 10, rect.Width - (_controller.appearance.cellGap * 2), titleHeight);

				appearance.titleColor.SetFill();
				new NSString(title).DrawString(titleRect, UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.TailTruncation, UITextAlignment.Left);
			}

			//Does the group have a footer?
			if (footer != "") {
				//// Title Drawing
				var footerRect = new CGRect (_controller.appearance.cellGap, rect.Height - footerHeight, rect.Width - (_controller.appearance.cellGap * 2), footerHeight);

				appearance.footerColor.SetFill();
				new NSString(footer).DrawString(footerRect, UIFont.SystemFontOfSize(appearance.footerSize), UILineBreakMode.WordWrap, UITextAlignment.Left);
			}
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when one or more fingers touches the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			//Already dragging?
			if (_dragging) return;

			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			if (_bringToFrontOnTouched && this.Superview!=null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Sent when the <c>ACTile</c> is being dragged
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point
				_dragging=true;
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);

				//Inform caller of event
				RaiseMoved ();
			}

			//Pass call to base object
			base.TouchesMoved(touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			//Is this view draggable?
			if (draggable) {
				// Move relative to the original touch point 
				var pt = (touches.AnyObject as UITouch).LocationInView(this);
				MoveToPoint(pt);
				_dragging=false;
			}

			//Clear starting point
			_startLocation = new CGPoint (0, 0);

			//Inform caller of event
			RaiseReleased ();

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is touched 
		/// </summary>
		public delegate void ACTileGroupTouchedDelegate (ACTileGroup view);
		public event ACTileGroupTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is moved
		/// </summary>
		public delegate void ACTileGroupMovedDelegate (ACTileGroup view);
		public event ACTileGroupMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTileGroup</c> is released 
		/// </summary>
		public delegate void ACTileGroupReleasedDelegate (ACTileGroup view);
		public event ACTileGroupReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}

		/// <summary>
		/// Occurs when live updating has been kicked off by the <c>ACTileController</c> 
		/// </summary>
		public delegate void ACTileGroupLiveUpdatingDelegate (ACTileGroup group);
		public event ACTileGroupLiveUpdatingDelegate LiveUpdating;

		/// <summary>
		/// Raises the live updating.
		/// </summary>
		internal void RaiseLiveUpdating(){
			if (this.LiveUpdating != null)
				this.LiveUpdating (this);
		}
		#endregion 
	}
}

