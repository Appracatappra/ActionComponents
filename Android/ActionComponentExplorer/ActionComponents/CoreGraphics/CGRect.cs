using System;
using Android.Content;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Animation;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace CoreGraphics
{
	/// <summary>
	/// Represents a simulated iOS <c>CGRect</c> used to ease the porting of UI code from iOS to Android. A <c>CGRect</c>
	/// can be implicitly converted to and from an Android <c>Rect</c> or <c>RectF</c>.
	/// </summary>
	public class CGRect
	{
		#region Override Operators
		/// <summary>
		/// Converts the <c>CGRect</c> to a <c>Rect</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>Rect</c>.</returns>
		/// <param name="rect">Rect.</param>
		public static implicit operator Rect(CGRect rect) {
			return new Rect((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
		}

		/// <summary>
		/// Converts the <c>Rect</c> to a <c>CGRect</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGRect</c>.</returns>
		/// <param name="rect">Rect.</param>
		public static implicit operator CGRect(Rect rect) {
			return new CGRect(rect.Left, rect.Top, rect.Width(), rect.Height());
		}

		/// <summary>
		/// Converts the <c>CGRect</c> to a <c>RectF</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>RectF</c>.</returns>
		/// <param name="rect">Rect.</param>
		public static implicit operator RectF(CGRect rect)
		{
			return new RectF(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		/// <summary>
		/// Converts the <c>RectF</c> to a <c>CGRect</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGRect</c>.</returns>
		public static implicit operator CGRect(RectF rect)
		{
			return new CGRect(rect.Left, rect.Top, rect.Width(), rect.Height());
		}

		/// <summary>
		/// Adds a <see cref="CoreGraphics.CGRect"/> to a <see cref="CoreGraphics.CGRect"/>, yielding a new <see cref="T:CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to add.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to add.</param>
		/// <returns>The <see cref="T:CoreGraphics.CGRect"/> that is the sum of the values of <c>a</c> and <c>b</c>.</returns>
		public static CGRect operator +(CGRect a, CGRect b) {
			return new CGRect(a.X + b.X, a.Y + b.Y, a.Width + b.Width, a.Height + b.Height);
		}

		/// <summary>
		/// Subtracts a <see cref="CoreGraphics.CGRect"/> from a <see cref="CoreGraphics.CGRect"/>, yielding a new <see cref="T:CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGRect"/> to subtract from (the minuend).</param>
		/// <param name="b">The <see cref="CoreGraphics.CGRect"/> to subtract (the subtrahend).</param>
		/// <returns>The <see cref="T:CoreGraphics.CGRect"/> that is the <c>a</c> minus <c>b</c>.</returns>
		public static CGRect operator -(CGRect a, CGRect b)
		{
			return new CGRect(a.X - b.X, a.Y - b.Y, a.Width - b.Width, a.Height - b.Height);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGRect"/> is equal to another specified <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(CGRect a, CGRect b)  {
			if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null))
			{
				return true;
			}
			else if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null))
			{
				return false;
			}
			return (a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGRect"/> is not equal to another specified <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(CGRect a, CGRect b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGRect"/> is lower than another specfied <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <(CGRect a, CGRect b)
		{
			return (a.X < b.X && a.Y < b.Y && a.Width < b.Width && a.Height < b.Height);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGRect"/> is greater than another specfied <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >(CGRect a, CGRect b)
		{
			return (a.X > b.X && a.Y > b.Y && a.Width > b.Width && a.Height > b.Height);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGRect"/> is lower than or equal to another specfied <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <=(CGRect a, CGRect b)
		{
			return (a.X <= b.X && a.Y <= b.Y && a.Width <= b.Width && a.Height <= b.Height);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGRect"/> is greater than or equal to another specfied <see cref="CoreGraphics.CGRect"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGRect"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >=(CGRect a, CGRect b)
		{
			return (a.X >= b.X && a.Y >= b.Y && a.Width >= b.Width && a.Height >= b.Height);
		}
  		#endregion

		#region Static Methods
		/// <summary>
		/// Creates a <c>CGRect</c> structure with the specified edge locations.
		/// </summary>
		/// <returns>The new <c>CGRect</c>.</returns>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="right">Right.</param>
		/// <param name="bottom">Bottom.</param>
		public static CGRect FromLTRB(nfloat left, nfloat top, nfloat right, nfloat bottom) {
			return new CGRect(left, top, right - left, bottom - top);
		}

		/// <summary>
		/// Creates and returns an enlarged copy of the specified <c>CGRect</c> structure. The copy is enlarged by the 
		/// specified amount. The original <c>CGRect</c> structure remains unmodified.
		/// </summary>
		/// <returns>The new <c>CGRect</c>.</returns>
		/// <param name="rect">Rect.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public static CGRect Inflate(CGRect rect, nfloat width, nfloat height) {
			var r = new CGRect(rect);
			r.Inflate(width, height);
			return r;
		}

		/// <summary>
		/// Returns a new <c>CGRect</c> representing the intersection of the two given rectangles.
		/// </summary>
		/// <returns>The intersection as a new <c>CGRect</c>.</returns>
		/// <param name="a">Rect.</param>
		/// <param name="b">Rect.</param>
		public static CGRect Intersect(CGRect a, CGRect b) {
			var r = new CGRect(a);
			r.Intersect(b);
			return r;
		}

		/// <summary>
		/// Returns the union of the two rectangles
		/// </summary>
		/// <returns>The union.</returns>
		/// <param name="a">Rect.</param>
		/// <param name="b">Rect.</param>
		public static CGRect Union(CGRect a, CGRect b) {
			var rx1 = 0f;
			var ry1 = 0f;
			var rx2 = 0f;
			var ry2 = 0f;
			var r = new CGRect();

			if (a.X < b.X)
			{
				rx1 = a.X;
			}
			else
			{
				rx1 = b.X;
			}

			if (a.Y < b.Y)
			{
				ry1 = a.Y;
			}
			else
			{
				ry1 = b.Y;
			}

			if (a.Right < b.Right)
			{
				rx2 = b.Right;
			}
			else
			{
				rx2 = a.Right;
			}

			if (a.Bottom < b.Bottom)
			{
				ry2 = b.Bottom;
			}
			else
			{
				ry2 = a.Bottom;
			}

			// Adjust self to be intersection
			r.X = rx1;
			r.Y = ry1;
			r.Width = rx2 - rx1;
			r.Height = ry2 - ry1;

			// Return results
			return r;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the x coordinate.
		/// </summary>
		/// <value>The x.</value>
		public nfloat X { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the y coordinate.
		/// </summary>
		/// <value>The y.</value>
		public nfloat Y { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public nfloat Height { get; set; } = 0f;

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public nfloat Width { get; set; } = 0f;

		/// <summary>
		/// Gets the bottom.
		/// </summary>
		/// <value>The bottom.</value>
		public nfloat Bottom
		{
			get { return Y + Height; }
		}

		/// <summary>
		/// Gets the left.
		/// </summary>
		/// <value>The left.</value>
		public nfloat Left
		{
			get { return X; }
		}

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public CGPoint Location
		{
			get { return new CGPoint(X, Y); }
			set
			{
				X = value.X;
				Y = value.Y;
			}
		}

		/// <summary>
		/// Gets the right.
		/// </summary>
		/// <value>The right.</value>
		public nfloat Right
		{
			get { return X + Width; }
		}


		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public CGSize Size
		{
			get { return new CGSize(Width, Height); }
			set
			{
				Width = value.Width;
				Height = value.Height;
			}
		}

		/// <summary>
		/// Gets the top.
		/// </summary>
		/// <value>The top.</value>
		public nfloat Top
		{
			get { return Y; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		public CGRect()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public CGRect(CGRect rect)
		{
			X = rect.X;
			Y = rect.Y;
			Width = rect.Width;
			Height = rect.Height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="point">Point.</param>
		/// <param name="size">Size.</param>
		public CGRect(CGPoint point, CGSize size)
		{
			X = point.X;
			Y = point.Y;
			Width = size.Width;
			Height = size.Height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public CGRect(int x, int y, int width, int height)
		{
			X = (float)x;
			Y = (float)y;
			Width = (float)width;
			Height = (float)height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public CGRect(double x, double y, double width, double height)
		{
			X = (float)x;
			Y = (float)y;
			Width = (float)width;
			Height = (float)height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public CGRect(nfloat x, nfloat y, nfloat width, nfloat height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public CGRect(Rect rect)
		{
			X = rect.Left;
			Y = rect.Top;
			Width = rect.Right - rect.Left;
			Height = rect.Bottom - rect.Top;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGRect"/> class.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public CGRect(RectF rect)
		{
			X = rect.Left;
			Y = rect.Top;
			Width = rect.Right - rect.Left;
			Height = rect.Bottom - rect.Top;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Copy the specified rect into this rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void Copy(CGRect rect) {
			X = rect.X;
			Y = rect.Y;
			Width = rect.Width;
			Height = rect.Height;
		}

		/// <summary>
		/// Gets the minimum x.
		/// </summary>
		/// <returns>The minimum x.</returns>
		public nfloat GetMinX() {
			if (Left < Right) {
				return Left;
			} else {
				return Right;
			}
		}

		/// <summary>
		/// Gets the max x.
		/// </summary>
		/// <returns>The max x.</returns>
		public nfloat GetMaxX() {
			if (Left > Right) {
				return Left;
			} else {
				return Right;
			}
		}

		/// <summary>
		/// Gets the minimum y.
		/// </summary>
		/// <returns>The minimum y.</returns>
		public nfloat GetMinY() {
			if (Top < Bottom) {
				return Top;
			} else {
				return Bottom;
			}
		}

		/// <summary>
		/// Gets the max y.
		/// </summary>
		/// <returns>The max y.</returns>
		public nfloat GetMaxY() {
			if (Top > Bottom) {
				return Top;
			} else {
				return Bottom;
			}
		}

		/// <summary>
		/// Contains the specified point.
		/// </summary>
		/// <returns><c>true</c> in the point is inside of the rect, else returns <c>false</c>.</returns>
		/// <param name="point">Point.</param>
		public bool Contains(CGPoint point) {
			return (point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom);
		}

		/// <summary>
		/// Contains the specified rect.
		/// </summary>
		/// <returns><c>true</c> in the rect is inside of the rect, else returns <c>false</c>.</returns>
		/// <param name="rect">Rect.</param>
		public bool Contains(CGRect rect)
		{
			return (rect.Left >= Left && rect.Left <= Right && rect.Top >= Top && rect.Top <= Bottom) && 
				(rect.Right >= Left && rect.Right <= Right && rect.Bottom >= Top && rect.Bottom <= Bottom);
		}

		/// <summary>
		/// Contains the specified x and y.
		/// </summary>
		/// <returns><c>true</c> in the x,y point is inside of the rect, else returns <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool Contains(int x, int y)
		{
			return (x >= Left && x <= Right && y >= Top && y <= Bottom);
		}

		/// <summary>
		/// Contains the specified x and y.
		/// </summary>
		/// <returns><c>true</c> in the x,y point is inside of the rect, else returns <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool Contains(double x, double y)
		{
			return (x >= Left && x <= Right && y >= Top && y <= Bottom);
		}

		/// <summary>
		/// Contains the specified x and y.
		/// </summary>
		/// <returns><c>true</c> in the x,y point is inside of the rect, else returns <c>false</c>.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public bool Contains(nfloat x, nfloat y)
		{
			return (x >= Left && x <= Right && y >= Top && y <= Bottom);
		}

		/// <summary>
		/// Increases the size of this <c>CGRect</c>.
		/// </summary>
		/// <param name="size">Size.</param>
		public void Inflate(CGSize size)
		{
			X -= size.Width;
			Y -= size.Height;
			Width += (size.Width * 2);
			Height += (size.Height * 2);
		}

		/// <summary>
		/// Increases the size of this <c>CGRect</c>.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Inflate(int width, int height)
		{
			X -= width;
			Y -= height;
			Width += (width * 2);
			Height += (height * 2);
		}

		/// <summary>
		/// Increases the size of this <c>CGRect</c>.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Inflate(double width, double height)
		{
			X -= (float)width;
			Y -= (float)height;
			Width += (float)(width * 2);
			Height += (float)(height * 2);
		}

		/// <summary>
		/// Increases the size of this <c>CGRect</c>.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Inflate(nfloat width, nfloat height)
		{
			X -= width;
			Y -= height;
			Width += (width * 2);
			Height += (height * 2);
		}

		/// <summary>
		/// Replaces this <c>CGRect</c> with the intersection of itself and the specified <c>CGRect</c>.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public void Intersect(CGRect rect) {

			if (rect.Contains(this)) {
				// Nothing to do, this rect is contained inside the other
				return;
			} else if (Contains(rect)) {
				// This rect contains the other so it is the intersection
				Copy(rect);
				return;
			} else if (!IntersectsWith(rect)) {
				// Nullify rect
				X = 0;
				Y = 0;
				Width = 0;
				Height = 0;
			}

			var rx1 = Left;
			var ry1 = Top;
			var rx2 = Right;
			var ry2 = Bottom;

			if (X < rect.X) {
				rx1 = rect.X;
			} else {
				rx1 = X;
			}

			if (Y < rect.Y) {
				ry1 = rect.Y;
			} else {
				ry1 = Y;
			}

			if (Right < rect.Right) {
				rx2 = Right;
			} else {
				rx2 = rect.Right;
			}

			if (Bottom < rect.Bottom) {
				ry2 = Bottom;
			} else {
				ry2 = rect.Bottom;
			}

			// Adjust self to be intersection
			X = rx1;
			Y = ry1;
			Width = rx2 - rx1;
			Height = ry2 - ry1;
		}

		/// <summary>
		/// Test to see if the given rectangle intersects with this rectangle.
		/// </summary>
		/// <returns><c>true</c>, if the rectangles intersect, <c>false</c> otherwise.</returns>
		/// <param name="rect">Rect.</param>
		public bool IntersectsWith(CGRect rect) {

			// Calculate points
			var a1 = new CGPoint(X, Y);
			var a2 = new CGPoint(X + Width, Y);
			var a3 = new CGPoint(X + Width, Y + Height);
			var a4 = new CGPoint(X, Y + Height);

			var b1 = new CGPoint(rect.X, rect.Y);
			var b2 = new CGPoint(rect.X + rect.Width, rect.Y);
			var b3 = new CGPoint(rect.X + rect.Width, rect.Y + rect.Height);
			var b4 = new CGPoint(rect.X, rect.Y + rect.Height);

			// Test for overlapping areas
			if (b1.IsBetweenHorizontally(a1, a2) || b2.IsBetweenHorizontally(a1, a2)) {
				if (b1.IsBetweenHorizontally(a1, a4) || b4.IsBetweenHorizontally(a1, a4)) {
					return true;
				} else if (a1.IsBetweenVertically(b1, b4) || a4.IsBetweenVertically(b1, b4)) {
					return true;
				}
			} else if (a1.IsBetweenHorizontally(b1, b2) || a2.IsBetweenHorizontally(b1, b2)) {
				if (b1.IsBetweenHorizontally(a1, a4) || b4.IsBetweenHorizontally(a1, a4))
				{
					return true;
				}
				else if (a1.IsBetweenVertically(b1, b4) || a4.IsBetweenVertically(b1, b4))
				{
					return true;
				}
			}

			// No intersection
			return false;
		}

		/// <summary>
		/// Moves the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="point">Point.</param>
		public void Offset(CGPoint point) {
			X += point.X;
			Y += point.Y;
		}

		/// <summary>
		/// Moves the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void Offset(int x, int y) {
			X += x;
			Y += y;
		}

		/// <summary>
		/// Moves the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void Offset(double x, double y)
		{
			X += (float)x;
			Y += (float)y;
		}

		/// <summary>
		/// Moves the location of this rectangle by the specified amount.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void Offset(nfloat x, nfloat y)
		{
			X += x;
			Y += y;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGRect"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGRect"/>.</returns>
		public override string ToString()
		{
			return string.Format("[CGRect: X={0}, Y={1}, Height={2}, Width={3}]", X, Y, Height, Width);
		}
  		#endregion
	}
}
