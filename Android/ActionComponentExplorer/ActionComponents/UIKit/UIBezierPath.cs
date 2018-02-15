using System;
using System.Threading;
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
using Foundation;
using CoreGraphics;
using ActionComponents;

// Mappings "Unified" iOS types to Android types
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;

namespace UIKit
{
	/// <summary>
	/// Simulates a iOS <c>UIBezierPath</c> object for ease of porting UI code from iOS to Android. NOTE: Only a small
	/// percentage of the <c>UIBezierPath</c> features have been ported to support the Action Components.
	/// </summary>
	public class UIBezierPath
	{
		#region Static Methods
		/// <summary>
		/// Creates a new <c>UIBezierPath</c> from the given rect.
		/// </summary>
		/// <returns>The rect.</returns>
		/// <param name="rect">Rect.</param>
		public static UIBezierPath FromRect(CGRect rect) {
			var path = new UIBezierPath();
			var shape = new ShapeDrawable(new RectShape());

			// Configure
			shape.SetBounds((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);

			// Add to shape collection
			path.Elements.Add(shape);

			// Return results
			return path;
		}

		/// <summary>
		/// Creates a new <c>UIBezierPath</c> from the given round rect.
		/// </summary>
		/// <returns>The rounded rect.</returns>
		/// <param name="rect">Rect.</param>
		/// <param name="corners">Corners.</param>
		/// <param name="size">Size.</param>
		public static UIBezierPath FromRoundedRect(CGRect rect, float[] corners, CGSize size) {
			var path = new UIBezierPath();
			var shape = new ShapeDrawable(new RoundRectShape(corners, null, null));

			// Configure
			shape.SetBounds((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);

			// Add to shape collection
			path.Elements.Add(shape);

			// Return results
			return path;
		}

		/// <summary>
		/// Creates a new <c>UIBezierPath</c> from the given round rect.
		/// </summary>
		/// <returns>The rounded rect.</returns>
		/// <param name="rect">Rect.</param>
		/// <param name="radius">Radius.</param>
		public static UIBezierPath FromRoundedRect(CGRect rect, nfloat radius)
		{
			List<float> corners = new List<float>() { radius, radius, radius, radius, radius, radius, radius, radius };
			var path = new UIBezierPath();
			var shape = new ShapeDrawable(new RoundRectShape(corners.ToArray(), null, null));

			// Configure
			shape.SetBounds((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);

			// Add to shape collection
			path.Elements.Add(shape);

			// Return results
			return path;
		}
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the elements that make up this path.
		/// </summary>
		/// <value>The elements.</value>
		public List<ShapeDrawable> Elements { get; set; } = new List<ShapeDrawable>();

		/// <summary>
		/// Gets or sets the width of the line.
		/// </summary>
		/// <value>The width of the line.</value>
		public nfloat LineWidth { get; set; } = 1f;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIBezierPath"/> class.
		/// </summary>
		public UIBezierPath()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UIKit.UIBezierPath"/> class.
		/// </summary>
		/// <param name="shape">Shape.</param>
		public UIBezierPath(ShapeDrawable shape)
		{
			// Initialize
			Elements.Add(shape);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Draws a filled version of the path into the current graphics context.
		/// </summary>
		public void Fill() {
			var context = UIGraphics.CurrentContext;

			// Is a context currently open?
			if (context != null) {
				var canvas = UIGraphics.DrawCanvas;

				// Process all shapes
				foreach(ShapeDrawable shape in Elements) {
					var bounds = shape.Bounds;

					// Has shadow?
					if (context.HasShadow) {
						// Calculate new bounds
						var sBounds = (CGRect)bounds;
						sBounds.Offset(context.ShadowOffset);

						// Draw shadow shape
						shape.Paint.Color = context.ShadowColor;
						shape.Bounds = sBounds;
						shape.Paint.SetStyle(Paint.Style.Fill);
						shape.Draw(canvas);

						// Restore bounds
						shape.Bounds = bounds;
					}

					// Draw shape
					shape.Paint.Color = context.CurrentPaint.Color;
					shape.Paint.AntiAlias = true;
					shape.Paint.SetStyle(Paint.Style.Fill);
					shape.Draw(canvas);
				}
			}
		}

		/// <summary>
		/// Draws an outline version of the path into the current graphics context.
		/// </summary>
		public void Stroke()
		{
			var context = UIGraphics.CurrentContext;

			// Is a context currently open?
			if (context != null)
			{
				var canvas = UIGraphics.DrawCanvas;

				// Process all shapes
				foreach (ShapeDrawable shape in Elements)
				{
					var bounds = shape.Bounds;

					// Has shadow?
					if (context.HasShadow)
					{
						// Calculate new bounds
						var sBounds = (CGRect)bounds;
						sBounds.Offset(context.ShadowOffset);

						// Draw shadow shape
						shape.Paint.Color = context.ShadowColor;
						shape.Bounds = sBounds;
						shape.Paint.SetStyle(Paint.Style.Stroke);
						shape.Paint.StrokeWidth = LineWidth;
						shape.Draw(canvas);

						// Restore bounds
						shape.Bounds = bounds;
					}

					// Draw shape
					shape.Paint.Color = context.CurrentPaint.Color;
					shape.Paint.AntiAlias = true;
					shape.Paint.SetStyle(Paint.Style.Stroke);
					shape.Paint.StrokeWidth = LineWidth;
					shape.Draw(canvas);
				}
			}
		}

		/// <summary>
		/// Converts the path into a clipping path.
		/// </summary>
		public void AddClip() {
			
		}
		#endregion
	}
}
