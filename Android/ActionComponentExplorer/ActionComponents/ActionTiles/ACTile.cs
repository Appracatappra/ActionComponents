using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace ActionComponents
{
	/// <summary>
	/// The <c>ACTile</c> is a custom <c>UIView</c> that defines several helper properties and methods that
	/// make it a great basis for any custom user interface controls. It defines helper events for
	/// being <c>Touched</c>, <c>Moved</c>, and/or <c>Released</c> and can be set to automatically become the front view when it is touched. And provides methods
	/// to make moving, rotating, and resizing the <c>ACTile</c> easier with less code.
	/// </summary>
	public class ACTile : UIView
	{
		#region Private Variables
		private bool _bringToFrontOnTouched;
		private CGPoint _startLocation;
		private ACTileGroup _group;
		private ACTileStyle _style;
		private ACTileSize _tileSize;
		private ACTileAppearance _appearance;
		private string _title = "";
		private string _subtitle = "";
		private string _description = "";
		private UIImage _icon;
		private ACTileLiveUpdate _liveUpdateAction;
		private int _customRowHeight = 1;
		private int _customColumnHeight = 1;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <c>ACTile</c> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; } 

		/// <summary>
		/// Gets or sets the <c>ACTileLiveUpdate</c> action that will be performed via an automatic update
		/// kicked off by the <c>liveUpdateTimer</c> in the parent <c>ACTileController</c>. 
		/// </summary>
		/// <value>The live update action.</value>
		public ACTileLiveUpdate liveUpdateAction{
			get { return _liveUpdateAction;}
			set{ _liveUpdateAction = value;}
		}

		/// <summary>
		/// Gets or sets the title for this <c>ACTile</c> 
		/// </summary>
		/// <value>The title.</value>
		public string title {
			get { return _title;}
			set {
				_title = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the subtitle for this <c>ACTile</c>
		/// </summary>
		/// <value>The subtitle.</value>
		public string subtitle {
			get { return _subtitle;}
			set {
				_subtitle = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the description for this <c>ACTile</c>
		/// </summary>
		/// <value>The description.</value>
		public string description {
			get { return _description;}
			set {
				_description = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the icon for this <c>ACTile</c> 
		/// </summary>
		/// <value>The icon.</value>
		public UIImage icon {
			get { return _icon;}
			set {
				_icon = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTile</c> for this <c>ACTile</c> 
		/// </summary>
		/// <value>The appearance.</value>
		public ACTileAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;
				Redraw ();

				//Wire-up events
				_appearance.AppearanceModified += () => {
					Redraw ();
				};
			}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileStyle</c> for this <c>ACTile</c>. 
		/// </summary>
		/// <value>The style.</value>
		public ACTileStyle style{
			get { return _style;}
			set {
				_style = value;
				Redraw ();
			}
		}

		/// <summary>
		/// Gets the <c>ACTileGroup</c> this <c>ACTile</c>.
		/// belongs to  
		/// </summary>
		/// <value>The group.</value>
		public ACTileGroup group {
			get { return _group;}
		}

		/// <summary>
		/// Gets or sets the <c>ACTileSize</c> of this <c>ACTile</c> 
		/// </summary>
		/// <value>The size of the tile.</value>
		public ACTileSize tileSize {
			get { return _tileSize;}
			set {
				_tileSize = value;

				//Update self
				Redraw ();

				//Inform parent of the size change
				if (_group != null)
					_group.RecalculateSize();
			}
		}

		/// <summary>
		/// Gets or sets the height of the custom row.
		/// </summary>
		/// <value>The height of the custom row.</value>
		public int customRowHeight {
			get { return _customRowHeight; }
			set {
				if (value < 1) {
					_customRowHeight = 1;
				} else {
					_customRowHeight = value;
				}

				//Update self
				AdjustSize();

				//Inform parent of the size change
				if (_group != null)
					_group.RecalculateSize();
			}
		}

		/// <summary>
		/// Gets or sets the height of the custom column.
		/// </summary>
		/// <value>The height of the custom column.</value>
		public int customColumnHeight {
			get { return _customColumnHeight; }
			set {
				if (value < 1) {
					_customColumnHeight = 1;
				} else {
					_customColumnHeight = value;
				}

				// Update self
				AdjustSize();

				//Inform parent of the size change
				if (_group != null)
					_group.RecalculateSize();
			}
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
		/// Initializes a new instance of the <c>ACTile</c> class.
		/// </summary>
		internal ACTile (ACTileGroup group, ACTileAppearance appearance, ACTileStyle style, ACTileSize tileSize, string title, string subtitle, UIImage icon, string description) : base()
		{
			//Save initial defaults
			this._group = group;
			this._appearance = appearance;
			this._style = style;
			this._tileSize = tileSize; 
			this._title = title;
			this._subtitle = subtitle;
			this._icon = icon;
			this._description = description;

			Initialize ();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			// Set defaults
			this.BackgroundColor = ACColor.Clear;
			this._bringToFrontOnTouched = false;
			this._startLocation = new CGPoint (0, 0);
			this.UserInteractionEnabled = true;
			this.MultipleTouchEnabled = true;
			this.ExclusiveTouch = false;

			// Wireup events
			_appearance.AppearanceModified += () => {
				Redraw ();
			};

			// Set initial size and position it out of view
			MoveToPoint (-1000, -1000);
			AdjustSize ();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Adjusts the size of this <c>ACTile</c> based on the size selected and the
		/// settings from the master <c>ACTileController</c>. 
		/// </summary>
		private void AdjustSize(){

			//Set the components size based on the size selected and the
			//settings from the master controller
			switch (tileSize) {
				case ACTileSize.Single:
					Resize (_group.controller.appearance.cellSize, _group.controller.appearance.cellSize);
					break;
				case ACTileSize.DoubleHorizontal:
					Resize ((_group.controller.appearance.cellSize * 2) + _group.controller.appearance.cellGap, _group.controller.appearance.cellSize);
					break;
				case ACTileSize.QuadHorizontal:
					Resize((_group.controller.appearance.cellSize * 4) + (_group.controller.appearance.cellGap * 2f), _group.controller.appearance.cellSize);
					break;
				case ACTileSize.DoubleVertical:
					Resize (_group.controller.appearance.cellSize, (_group.controller.appearance.cellSize * 2) + _group.controller.appearance.cellGap);
					break;
				case ACTileSize.Quad:
					Resize ((_group.controller.appearance.cellSize * 2) + _group.controller.appearance.cellGap, (_group.controller.appearance.cellSize * 2) + _group.controller.appearance.cellGap);
					break;
				case ACTileSize.Custom:
					Resize((_group.controller.appearance.cellSize * customColumnHeight) + (_group.controller.appearance.cellGap * (customColumnHeight - 1)), 
					       (_group.controller.appearance.cellSize * customRowHeight) + (_group.controller.appearance.cellGap * (customRowHeight - 1)));
					break;
			}
		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Shrinks this <c>ACTile</c> to single cell.
		/// </summary>
		internal void ShrinkTileToSingleCell(){
			//Set my type to single and redraw me
			_tileSize = ACTileSize.Single;
			AdjustSize ();
			Redraw ();
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

			//Move to the given location
			Frame = new CGRect (pt.X, pt.Y, Frame.Width, Frame.Height);
		}

		/// <summary>
		/// Resize this <c>ACTile</c> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		internal void Resize(float width, float height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
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
		#endregion 

		#region Public Methods
		/// <summary>
		/// Changes the background color of this <c>ACTile</c>
		/// by creating a random brightness off the given base <c>ACColor</c> within the given <c>minimum</c> and <c>maximum</c> ranges
		/// </summary>
		/// <param name="background">Background.</param>
		/// <param name="minimum">Minimum.</param>
		/// <param name="maximum">Maximum.</param>
		public void ChromaKeyTile(ACColor background, int minimum, int maximum) {
			Random rnd = new Random();
			nfloat hue=0, saturation=0, brightness=0, alpha=0;

			// Breakdown input color
			background.GetHSBA (ref hue, ref saturation, ref brightness, ref alpha);

			// Force the tile to compute a title color
			appearance.autoSetTextColor = true;

			// Process all tiles
			brightness = rnd.Next(minimum, maximum) / 255f;
			appearance.background = ACColor.FromHSBA(hue, saturation, brightness, alpha);

		}

		/// <summary>
		/// Forces this <c>ACTile</c> to fully redraw itself
		/// </summary>
		public void Redraw() {

			//Suspending updates?
			if (_group.controller.suspendUpdates)
				return;

			//Force component to update view
			this.SetNeedsDisplay ();
		}
		#endregion 

		#region Drawing Methods
		/// <summary>
		/// Sets the corner.
		/// </summary>
		/// <returns>The corner array.</returns>
		private float[] SetCorners(){
			List<float> corners = new List<float>() { 0, 0, 0, 0, 0, 0, 0, 0 };

			// Take action based on the corners speficied
			if (appearance.roundTopLeftCorner)
			{
				corners[0] = appearance.borderRadius;
				corners[1] = appearance.borderRadius;
			}
			if (appearance.roundTopRightCorner)
			{
				corners[2] = appearance.borderRadius;
				corners[3] = appearance.borderRadius;
			}
			if (appearance.roundBottomRightCorner)
			{
				corners[4] = appearance.borderRadius;
				corners[5] = appearance.borderRadius;
			}
			if (appearance.roundBottomLeftCorner)
			{
				corners[6] = appearance.borderRadius;
				corners[7] = appearance.borderRadius;
			}

			// Return adjusted corners
			return corners.ToArray();
		}

		/// <summary>
		/// Draws a scene type tile.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <summary>
		/// Draws a scene type tile.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void DrawScene(CGRect rect)
		{

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);
			var middle = rect.Height / 2f;
			var x = 10f;

			//Has icon?
			if (icon != null)
			{
				//Draw image
				context.SaveState();
				icon.Draw(new CGRect(10, middle - 12, 24, 24));
				x = 44f;
				context.RestoreState();
			}

			// Calculate text size
			nfloat titleHeight = UIFont.StringSize(title, UIFont.SystemFontOfSize(appearance.titleSize), new CGSize(rect.Width - (x + 10f), rect.Height), UILineBreakMode.TailTruncation).Height;

			//// title Drawing
			var titleRect = new CGRect(x, middle - (titleHeight / 2f), rect.Width - (x + 10f), titleHeight);
			appearance.titleColor.SetFill();
			new NSString(title).DrawString(titleRect, UIFont.SystemFontOfSize(appearance.titleSize), UILineBreakMode.TailTruncation, UITextAlignment.Left);

		}

		/// <summary>
		/// Draws the accessory type tile.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void DrawAccessory(CGRect rect)
		{
			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//Has icon?
			if (icon != null)
			{
				//Draw image
				context.SaveState();
				icon.Draw(new CGRect(10, 10, 24, 24));
				context.RestoreState();
			}

			// Calculate text size
			nfloat titleHeight = UIFont.StringSize(subtitle, UIFont.SystemFontOfSize(appearance.subtitleSize), new CGSize(rect.Width - 20f, 50f), UILineBreakMode.CharacterWrap).Height;

			//// title Drawing
			var titleRect = new CGRect(10f, rect.Height - (titleHeight + 10f), rect.Width - 20f, titleHeight);
			appearance.subtitleColor.SetFill();
			new NSString(subtitle).DrawString(titleRect, UIFont.SystemFontOfSize(appearance.subtitleSize), UILineBreakMode.CharacterWrap, UITextAlignment.Left);

			// Calculate text size
			titleHeight = UIFont.StringSize(title, UIFont.BoldSystemFontOfSize(appearance.titleSize), new CGSize(rect.Width - 20f, rect.Height - (titleHeight + 50f)), UILineBreakMode.CharacterWrap).Height;

			//// title Drawing
			titleRect = new CGRect(10f, titleRect.Y - (titleHeight + 5f), rect.Width - 20f, titleHeight);
			appearance.titleColor.SetFill();
			new NSString(title).DrawString(titleRect, UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.CharacterWrap, UITextAlignment.Left);
		}

		/// <summary>
		/// Draws the top title bar.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="withIcon">If set to <c>true</c> with icon.</param>
		public void DrawTopTitleBar(CGRect rect, bool withIcon){
			nfloat titleHeight = UIFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
			nfloat barHeight = titleHeight + 3;
			nfloat inset = -3;


			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			UIBezierPath titleBackgroundPath;
			var corners = SetCorners();

			//// TitleBackground Drawing
			//// AlertBody Drawing
			if (appearance.isRect) {
				//It is a perfect rectangle
				titleBackgroundPath = UIBezierPath.FromRect(new CGRect(0, 0, rect.Width, barHeight));
			} else {
				//It is a round rectangle with one or more square corners
				//Calculate corners
				if (appearance.roundTopLeftCorner) {
					//Adjust inset
					inset = 0 - (appearance.borderRadius / 2);
				}

				//Make path
				titleBackgroundPath = UIBezierPath.FromRoundedRect (new CGRect(0, 0, rect.Width, barHeight), corners, new CGSize (appearance.borderRadius, appearance.borderRadius));
			}
			appearance.titleBackground.SetFill();
			titleBackgroundPath.Fill();

			//Has icon?
			if (icon != null && withIcon) {
				//Draw image
				context.SaveState();
				icon.Draw(new CGRect(3, 3, 16, 16));
				context.RestoreState();
			}

			//// title Drawing
			var titleRect = new CGRect(0, 3, rect.Width, titleHeight);
			appearance.titleColor.SetFill();
			new NSString(title).DrawString(CGRect.Inflate(titleRect, inset, 0), UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.TailTruncation, withIcon ? UITextAlignment.Right : UITextAlignment.Left);

			//Has subtitle?
			if (subtitle != "" && !withIcon) {
				nfloat subtitleHeight = UIFont.StringSize (subtitle, UIFont.BoldSystemFontOfSize (appearance.subtitleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
				var subtitleRect = new CGRect(0, 3, rect.Width, subtitleHeight);
				appearance.subtitleColor.SetFill();
				new NSString(subtitle).DrawString(CGRect.Inflate(subtitleRect, inset, 0), UIFont.BoldSystemFontOfSize(appearance.subtitleSize), UILineBreakMode.TailTruncation, UITextAlignment.Right);
			}
		}

		/// <summary>
		/// Draws the bottom title bar.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void DrawBottomTitleBar(CGRect rect, bool withIcon){
			nfloat titleHeight = UIFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
			nfloat barHeight = titleHeight + 3;
			nfloat inset = -3;

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			UIBezierPath titleBackgroundPath;
			var corners = SetCorners();

			//// TitleBackground Drawing
			//// AlertBody Drawing
			if (appearance.isRect) {
				//It is a perfect rectangle
				titleBackgroundPath = UIBezierPath.FromRect(new CGRect(rect.GetMinX(), rect.GetMaxY() - barHeight, rect.Width, barHeight));
			} else {
				//It is a round rectangle with one or more square corners
				//Calculate corners
				if (appearance.roundBottomLeftCorner) {
					//Adjust inset
					inset = 0 - (appearance.borderRadius / 2);
				}

				//Make path
				titleBackgroundPath = UIBezierPath.FromRoundedRect (new CGRect(rect.GetMinX(), rect.GetMaxY() - barHeight, rect.Width, barHeight), corners, new CGSize (appearance.borderRadius, appearance.borderRadius));
			}
			appearance.titleBackground.SetFill();
			titleBackgroundPath.Fill();

			//Has icon?
			if (icon != null && withIcon) {
				//Draw image
				context.SaveState();
				icon.Draw(new CGRect(rect.GetMinX()+3, rect.GetMaxY() - barHeight, 16, 16));
				context.RestoreState();
			}

			//// title Drawing
			var titleRect = new CGRect(rect.GetMinX(), rect.GetMaxY() - titleHeight, rect.Width, titleHeight);
			appearance.titleColor.SetFill();
			new NSString(title).DrawString(CGRect.Inflate(titleRect, inset, 0), UIFont.BoldSystemFontOfSize(appearance.titleSize), UILineBreakMode.TailTruncation, withIcon ? UITextAlignment.Right : UITextAlignment.Left);

			//Has subtitle?
			if (subtitle != "" && !withIcon) {
				nfloat subtitleHeight = UIFont.StringSize (subtitle, UIFont.BoldSystemFontOfSize (appearance.subtitleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
				var subtitleRect = new CGRect(rect.GetMinX(), rect.GetMaxY() - subtitleHeight, rect.Width, subtitleHeight);
				appearance.subtitleColor.SetFill();
				new NSString(subtitle).DrawString(CGRect.Inflate(subtitleRect, inset, 0), UIFont.BoldSystemFontOfSize(appearance.subtitleSize), UILineBreakMode.TailTruncation, UITextAlignment.Right);
			}
		}

		/// <summary>
		/// Draws the description block.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawDescriptionBlock(CGRect rect) {
			nfloat titleHeight = UIFont.StringSize (title, UIFont.BoldSystemFontOfSize (appearance.titleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
			nfloat subtitleHeight = 0;

			//Has subtitle?
			if (subtitle != "" && tileSize != ACTileSize.Single) {
				subtitleHeight = UIFont.StringSize (subtitle, UIFont.BoldSystemFontOfSize (appearance.subtitleSize), new CGSize (rect.Width , 50f), UILineBreakMode.TailTruncation).Height+3;
				var titleRect = new CGRect(0, 3, rect.Width, subtitleHeight);
				appearance.subtitleColor.SetFill();
				new NSString(subtitle).DrawString(CGRect.Inflate(titleRect, -3, 0), UIFont.BoldSystemFontOfSize(appearance.subtitleSize), UILineBreakMode.TailTruncation, UITextAlignment.Left);
			}

			//// Desc Drawing
			var descRect = new CGRect(0, subtitleHeight, rect.Width, rect.Height - (subtitleHeight+titleHeight+3));
			appearance.descriptionColor.SetFill();
			new NSString(description).DrawString(CGRect.Inflate(descRect, -3, -3), UIFont.BoldSystemFontOfSize(appearance.descriptionSize), UILineBreakMode.WordWrap, UITextAlignment.Left);

			//Add title bar
			DrawBottomTitleBar (rect,true);
		}

		/// <summary>
		/// Draws the central icon.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="bodyPath">Body path.</param>
		private void DrawCentralIcon(CGRect rect, UIBezierPath imagePath){
			nfloat x = 0, y = 0;

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//Has icon?
			if (icon != null) {
				// Calculate the icon's location
				x = (Frame.Width / 2) - (icon.Size.Width / 2);
				y = (Frame.Height / 2) - (icon.Size.Height / 2);

				//Draw image
				context.SaveState();
				imagePath.AddClip();
				icon.Draw(new CGRect(x, y, icon.Size.Width, icon.Size.Height));
				context.RestoreState();
			}

			//Draw title
			DrawBottomTitleBar (rect,false);

		}

		/// <summary>
		/// Draws the big picture.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="imagePath">Image path.</param>
		private void DrawBigPicture(CGRect rect, UIBezierPath imagePath){

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//Has icon?
			if (icon != null) {

				//Draw image
				context.SaveState();
				imagePath.AddClip();
				//icon.Draw(new CGRect(0, 0, icon.Size.Width, icon.Size.Height));
				icon.Draw(rect);
				context.RestoreState();
			}

			//Draw title
			DrawBottomTitleBar (rect,false);

		}

		/// <summary>
		/// Draws the corner icon.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="imagePath">Image path.</param>
		private void DrawCornerIcon(CGRect rect, UIBezierPath imagePath){

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//Has icon?
			if (icon != null) {
				//Draw image
				context.SaveState();
				imagePath.AddClip();
				icon.Draw(new CGRect(3,rect.Height-35,32,32));
				context.RestoreState();
			}

			//Draw title
			DrawTopTitleBar (rect, false);

		}

		/// <summary>
		/// Draws the top title.
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="imagePath">Image path.</param>
		private void DrawTopTitle(CGRect rect, UIBezierPath imagePath){
			nfloat x = 0, y = 0;

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			//Has icon?
			if (icon != null) {
				// Calculate the icon's location
				x = (Frame.Width / 2) - (icon.Size.Width / 2);
				y = (Frame.Height / 2) - (icon.Size.Height / 2);

				//Draw image
				context.SaveState();
				imagePath.AddClip();
				icon.Draw(new CGRect(x, y, icon.Size.Width, icon.Size.Height));
				context.RestoreState();
			}

			//Draw title
			DrawTopTitleBar (rect,false);

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

			//Is this a custom drawn tile?
			if (style == ACTileStyle.CustomDrawn) {
				//Yes, request that the user draws the tile and return
				RaiseRequestCustomDraw (rect);
				return;
			}

			//// General Declarations
			var context = UIGraphics.GetCurrentContext(this);

			UIBezierPath groupBodyPath;
			var corners = SetCorners();

			//// Shadow Declarations
			var groupShadow = appearance.shadow;
			var groupShadowOffset = new CGSize (0.1f, 3.1f);
			var groupShadowBlurRadius = 5;

			//// groupBox
			{

				//// AlertBody Drawing
				if (appearance.isRect) {
					//It is a perfect rectangle
					groupBodyPath = UIBezierPath.FromRect (rect);
				} else if (appearance.isRoundRect) {
					//It is a perfect round rectangle
					groupBodyPath = UIBezierPath.FromRoundedRect (rect, appearance.borderRadius);
				} else {
					//Make path
					groupBodyPath = UIBezierPath.FromRoundedRect (rect, corners, new CGSize (appearance.borderRadius, appearance.borderRadius));
				}
				context.SaveState ();
				if (appearance.hasShadow) context.SetShadow (groupShadowOffset, groupShadowBlurRadius, groupShadow);
				appearance.background.SetFill ();
				groupBodyPath.Fill ();
				context.RestoreState ();

				appearance.border.SetStroke ();
				groupBodyPath.LineWidth = appearance.borderWidth;
				groupBodyPath.Stroke ();

			}

			//Take action based on the tile style
			switch (style) {
				case ACTileStyle.Default:
					DrawCentralIcon (rect, groupBodyPath);
					break;
				case ACTileStyle.CornerIcon:
					DrawCornerIcon (rect, groupBodyPath);
					break;
				case ACTileStyle.DescriptionBlock:
					DrawDescriptionBlock (rect);
					break;
				case ACTileStyle.TopTitle:
					DrawTopTitle (rect, groupBodyPath);
					break;
				case ACTileStyle.BigPicture:
					DrawBigPicture (rect, groupBodyPath);
					break;
				case ACTileStyle.Scene:
					DrawScene(rect);
					break;
				case ACTileStyle.Accessory:
					DrawAccessory(rect);
					break;	
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
			//Save starting location
			var pt = (touches.AnyObject as UITouch).LocationInView(this);
			_startLocation = pt;

			//Automatically bring view to front?
			// if (_bringToFrontOnTouched && this.Superview!=null) this.Superview.BringSubviewToFront(this);

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
			//Clear starting point
			_startLocation = new CGPoint (0, 0);

			//Inform caller of event
			RaiseReleased ();

			#if TRIAL
			Android.Widget.Toast.MakeText(this.Context, "ACTile by Appracatappra, LLC.", Android.Widget.ToastLength.Short).Show();
			#endif

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when this <c>ACTile</c> is touched 
		/// </summary>
		public delegate void ACTileTouchedDelegate (ACTile tile);
		public event ACTileTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTile</c> is moved
		/// </summary>
		public delegate void ACTileMovedDelegate (ACTile tile);
		public event ACTileMovedDelegate Moved;

		/// <summary>
		/// Raises the moved event
		/// </summary>
		private void RaiseMoved(){
			if (this.Moved != null)
				this.Moved (this);
		}

		/// <summary>
		/// Occurs when this <c>ACTile</c> is released 
		/// </summary>
		public delegate void ACTileReleasedDelegate (ACTile tile);
		public event ACTileReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}

		/// <summary>
		/// Occurs when the <c>ACTile</c> <c>Style</c> is set to <c>CustomDrawn</c>
		/// and the <c>ACTile</c> needs to be updated
		/// </summary>
		public delegate void ACTileRequestCustomDrawDelegate (ACTile tile, CGRect rect);
		public event ACTileRequestCustomDrawDelegate RequestCustomDraw;

		/// <summary>
		/// Raises the RequestCustomDraw.
		/// </summary>
		private void RaiseRequestCustomDraw(CGRect rect){
			if (this.RequestCustomDraw != null)
				this.RequestCustomDraw (this, rect);
		}

		/// <summary>
		/// Occurs when live updating has been kicked off by the <c>ACTileController</c> 
		/// </summary>
		public delegate void ACTileLiveUpdatingDelegate (ACTile tile);
		public event ACTileLiveUpdatingDelegate LiveUpdating;

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

