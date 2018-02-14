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
using System;

namespace CoreGraphics
{
	/// <summary>
	/// Structure defining a 2D point.
	/// </summary>
	public class CGPoint
	{
		#region Overload Operators
		/// <summary>
		/// Converts the <c>CGPoint</c> to a <c>Point</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>Point</c>.</returns>
		/// <param name="point">Point.</param>
		public static implicit operator Point(CGPoint point) {
			return new Point((int)point.X, (int)point.Y);
		}

		/// <summary>
		/// Converts the <c>Point</c> to a <c>CGPoint</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGPoint</c>.</returns>
		/// <param name="point">Point.</param>
		public static implicit operator CGPoint(Point point) {
			return new CGPoint(point);
		}

		/// <summary>
		/// Converts the <c>CGPoint</c> to a <c>PointF</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>PointF</c>.</returns>
		/// <param name="point">Point.</param>
		public static implicit operator PointF(CGPoint point)
		{
			return new PointF(point.X, point.Y);
		}

		/// <summary>
		/// Converts the <c>PointF</c> to a <c>CGPoint</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGPoint</c>.</returns>
		/// <param name="point">Point.</param>
		public static implicit operator CGPoint(PointF point)
		{
			return new CGPoint(point);
		}

		/// <summary>
		/// Converts the <c>CGPoint</c> to a <c>CGSize</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGSize</c>.</returns>
		/// <param name="point">Point.</param>
		public static implicit operator CGSize(CGPoint point) {
			return new CGSize(point.X, point.Y);
		}

		/// <summary>
		/// Converts the <c>CGSize</c> to a <c>CGPoint</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGPoint</c>.</returns>
		/// <param name="size">Size.</param>
		public static implicit operator CGPoint(CGSize size) {
			return new CGPoint(size.Width, size.Height);
		}

		/// <summary>
		/// Adds a <see cref="CoreGraphics.CGPoint"/> to a <see cref="CoreGraphics.CGPoint"/>, yielding a new <see cref="T:CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to add.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to add.</param>
		/// <returns>The <see cref="T:CoreGraphics.CGPoint"/> that is the sum of the values of <c>a</c> and <c>b</c>.</returns>
		public static CGPoint operator +(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X + b.X, a.Y + b.Y);
		}

		/// <summary>
		/// Subtracts a <see cref="CoreGraphics.CGPoint"/> from a <see cref="CoreGraphics.CGPoint"/>, yielding a new <see cref="T:CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGPoint"/> to subtract from (the minuend).</param>
		/// <param name="b">The <see cref="CoreGraphics.CGPoint"/> to subtract (the subtrahend).</param>
		/// <returns>The <see cref="T:CoreGraphics.CGPoint"/> that is the <c>a</c> minus <c>b</c>.</returns>
		public static CGPoint operator -(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X - b.X, a.Y - b.Y);
		}

		/// <summary>
		/// Computes the product of <c>a</c> and <c>b</c>, yielding a new <see cref="T:CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGPoint"/> to multiply.</param>
		/// <param name="b">The <see cref="CoreGraphics.CGPoint"/> to multiply.</param>
		/// <returns>The <see cref="T:CoreGraphics.CGPoint"/> that is the <c>a</c> * <c>b</c>.</returns>
		public static CGPoint operator *(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X * b.X, a.Y * b.Y);
		}

		/// <summary>
		/// Computes the division of <c>a</c> and <c>b</c>, yielding a new <see cref="T:CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGPoint"/> to divide (the divident).</param>
		/// <param name="b">The <see cref="CoreGraphics.CGPoint"/> to divide (the divisor).</param>
		/// <returns>The <see cref="T:CoreGraphics.CGPoint"/> that is the <c>a</c> / <c>b</c>.</returns>
		public static CGPoint operator /(CGPoint a, CGPoint b)
		{
			return new CGPoint(a.X / b.X, a.Y / b.Y);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGPoint"/> is equal to another specified <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(CGPoint a, CGPoint b) {
			if (Object.ReferenceEquals(a,null) && Object.ReferenceEquals(b, null)) {
				return true;
			} else if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null)) {
				return false;
			}
			return (a.X == b.X && a.Y == b.Y);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGPoint"/> is not equal to another specified <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(CGPoint a, CGPoint b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGPoint"/> is lower than another specfied <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <(CGPoint a, CGPoint b)
		{
			return (a.X < b.X && a.Y < b.Y);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGPoint"/> is greater than another specfied <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >(CGPoint a, CGPoint b)
		{
			return (a.X > b.X && a.Y > b.Y);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGPoint"/> is lower than or equal to another specfied <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <=(CGPoint a, CGPoint b)
		{
			return (a.X <= b.X && a.Y <= b.Y);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGPoint"/> is greater than or equal to another specfied <see cref="CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGPoint"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >=(CGPoint a, CGPoint b)
		{
			return (a.X >= b.X && a.Y >= b.Y);
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
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		public CGPoint()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="point">Point.</param>
		public CGPoint(CGPoint point)
		{
			X = point.X;
			Y = point.Y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public CGPoint(CGSize size)
		{
			X = size.Height;
			Y = size.Width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public CGPoint(int x, int y)
		{
			X = (float)x;
			Y = (float)y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public CGPoint(double x, double y)
		{
			X = (float)x;
			Y = (float)y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public CGPoint(nfloat x, nfloat y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="point">Point.</param>
		public CGPoint(Point point)
		{
			X = point.X;
			Y = point.Y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGPoint"/> class.
		/// </summary>
		/// <param name="point">Point.</param>
		public CGPoint(PointF point)
		{
			X = point.X;
			Y = point.Y;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Check to see if this point is horizontally between the two given points
		/// </summary>
		/// <returns><c>true</c>, if between horizontally, <c>false</c> otherwise.</returns>
		/// <param name="a">The first point.</param>
		/// <param name="b">The second point.</param>
		public bool IsBetweenHorizontally(CGPoint a, CGPoint b) {
			return (X >= a.X && X <= b.X);
		}

		/// <summary>
		/// Check to see if this point is Vertically between the two given points
		/// </summary>
		/// <returns><c>true</c>, if between Vertically, <c>false</c> otherwise.</returns>
		/// <param name="a">The first point.</param>
		/// <param name="b">The second point.</param>
		public bool IsBetweenVertically(CGPoint a, CGPoint b)
		{
			return (Y >= a.Y && Y <= b.Y);
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGPoint"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGPoint"/>.</returns>
		public override string ToString()
		{
			return string.Format("[CGPoint: X={0}, Y={1}]", X, Y);
		}
  		#endregion
	}
}
