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
	/// Represents a simulated iOS <c>CGSize</c> used to ease the porting of UI code from iOS to Android. A <c>CGSize</c>
	/// can be implicitly converted to and from an Android <c>Size</c> or <c>SizeF</c>.
	/// </summary>
	public class CGSize
	{
		#region Override Operators
		/// <summary>
		/// Converts a <c>CGSize</c> to a <c>Size</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>Size</c>.</returns>
		/// <param name="size">Size.</param>
		public static implicit operator Size(CGSize size) {
			return new Size((int)size.Height, (int)size.Width);
		}

		/// <summary>
		/// Converts the <c>Size</c> to a <c>CGSize</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGSize</c>.</returns>
		/// <param name="size">Size.</param>
		public static implicit operator CGSize(Size size) {
			return new CGSize(size.Height, size.Width);
		}

		/// <summary>
		/// Converts a <c>CGSize</c> to a <c>SizeF</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>SizeF</c>.</returns>
		/// <param name="size">Size.</param>
		public static implicit operator SizeF(CGSize size)
		{
			return new SizeF(size.Height, size.Width);
		}

		/// <summary>
		/// Converts the <c>SizeF</c> to a <c>CGSize</c>.
		/// </summary>
		/// <returns>The implicitly converted <c>CGSize</c>.</returns>
		/// <param name="size">Size.</param>
		public static implicit operator CGSize(SizeF size)
		{
			return new CGSize(size.Height, size.Width);
		}

		/// <summary>
		/// Adds a <see cref="CoreGraphics.CGSize"/> to a <see cref="CoreGraphics.CGSize"/>, yielding a new <see cref="T:CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to add.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to add.</param>
		/// <returns>The <see cref="T:CoreGraphics.CGSize"/> that is the sum of the values of <c>a</c> and <c>b</c>.</returns>
		public static CGSize operator +(CGSize a, CGSize b) {
			return new CGSize(a.Height + b.Height, a.Width + b.Width);
		}

		/// <summary>
		/// Subtracts a <see cref="CoreGraphics.CGSize"/> from a <see cref="CoreGraphics.CGSize"/>, yielding a new <see cref="T:CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGSize"/> to subtract from (the minuend).</param>
		/// <param name="b">The <see cref="CoreGraphics.CGSize"/> to subtract (the subtrahend).</param>
		/// <returns>The <see cref="T:CoreGraphics.CGSize"/> that is the <c>a</c> minus <c>b</c>.</returns>
		public static CGSize operator -(CGSize a, CGSize b)
		{
			return new CGSize(a.Height + b.Height, a.Width + b.Width);
		}

		/// <summary>
		/// Computes the product of <c>a</c> and <c>b</c>, yielding a new <see cref="T:CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGSize"/> to multiply.</param>
		/// <param name="b">The <see cref="CoreGraphics.CGSize"/> to multiply.</param>
		/// <returns>The <see cref="T:CoreGraphics.CGSize"/> that is the <c>a</c> * <c>b</c>.</returns>
		public static CGSize operator *(CGSize a, CGSize b)
		{
			return new CGSize(a.Height * b.Height, a.Width * b.Width);
		}

		/// <summary>
		/// Computes the division of <c>a</c> and <c>b</c>, yielding a new <see cref="T:CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The <see cref="CoreGraphics.CGSize"/> to divide (the divident).</param>
		/// <param name="b">The <see cref="CoreGraphics.CGSize"/> to divide (the divisor).</param>
		/// <returns>The <see cref="T:CoreGraphics.CGSize"/> that is the <c>a</c> / <c>b</c>.</returns>
		public static CGSize operator /(CGSize a, CGSize b)
		{
			return new CGSize(a.Height / b.Height, a.Width / b.Width);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGSize"/> is equal to another specified <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are equal; otherwise, <c>false</c>.</returns>
		public static bool operator ==(CGSize a, CGSize b)
		{
			if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null))
			{
				return true;
			}
			else if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null))
			{
				return false;
			}
			return (a.Height == b.Height && a.Width == b.Width);
		}

		/// <summary>
		/// Determines whether a specified instance of <see cref="CoreGraphics.CGSize"/> is not equal to another specified <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> and <c>b</c> are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(CGSize a, CGSize b)
		{
			return !(a == b);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGSize"/> is lower than another specfied <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <(CGSize a, CGSize b)
		{
			return (a.Height < b.Height && a.Width < b.Width);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGSize"/> is greater than another specfied <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >(CGSize a, CGSize b)
		{
			return (a.Height > b.Height && a.Width > b.Width);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGSize"/> is lower than or equal to another specfied <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is lower than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator <=(CGSize a, CGSize b)
		{
			return (a.Height <= b.Height && a.Width <= b.Width);
		}

		/// <summary>
		/// Determines whether one specified <see cref="CoreGraphics.CGSize"/> is greater than or equal to another specfied <see cref="CoreGraphics.CGSize"/>.
		/// </summary>
		/// <param name="a">The first <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <param name="b">The second <see cref="CoreGraphics.CGSize"/> to compare.</param>
		/// <returns><c>true</c> if <c>a</c> is greater than or equal to <c>b</c>; otherwise, <c>false</c>.</returns>
		public static bool operator >=(CGSize a, CGSize b)
		{
			return (a.Height >= b.Height && a.Width >= b.Width);
		}
		#endregion

		#region Public Properties
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
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		public CGSize()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public CGSize(CGSize size)
		{
			Height = size.Height;
			Width = size.Width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="point">Point.</param>
		public CGSize(CGPoint point)
		{
			Height = point.X;
			Width = point.Y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="height">Height.</param>
		/// <param name="width">Width.</param>
		public CGSize(int width, int height)
		{
			Height = (float)height;
			Width = (float)width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="height">Height.</param>
		/// <param name="width">Width.</param>
		public CGSize(double width, double height)
		{
			Height = (float)height;
			Width = (float)width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="height">Height.</param>
		/// <param name="width">Width.</param>
		public CGSize(nfloat width, nfloat height)
		{
			Height = height;
			Width = width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public CGSize(Size size)
		{
			Height = size.Height;
			Width = size.Width;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CoreGraphics.CGSize"/> class.
		/// </summary>
		/// <param name="size">Size.</param>
		public CGSize(SizeF size)
		{
			Height = size.Height;
			Width = size.Width;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGSize"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:CoreGraphics.CGSize"/>.</returns>
		public override string ToString()
		{
			return string.Format("[CGSize: Height={0}, Width={1}]", Height, Width);
		}
		#endregion
	}
}
