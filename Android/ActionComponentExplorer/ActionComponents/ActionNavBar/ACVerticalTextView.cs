using System;

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
using Android.Text;

namespace ActionComponents
{
	/// <summary>
	/// Creates a vertical TextView that can be added to and Android View
	/// </summary>
	internal class ACVerticalTextView : TextView
	{
		#region Private Variables
		private Display _display;
		private bool _topDown;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="ActionComponents.ACVerticalTextView"/> draws text in the top down mode.
		/// </summary>
		/// <value><c>true</c> if top down; otherwise, <c>false</c>.</value>
		public bool topDown{
			get{return _topDown;}
			set{_topDown=value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACVerticalTextView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ACVerticalTextView(Context context)
			: base(context)
		{
			_topDown=false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACVerticalTextView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="display">Display.</param>
		public ACVerticalTextView(Context context, Display display)
			: base(context)
		{
			_display = display;
			_topDown=false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACVerticalTextView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public ACVerticalTextView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
			_topDown=false;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Raises the measure event.
		/// </summary>
		/// <param name="widthMeasureSpec">Width measure spec.</param>
		/// <param name="heightMeasureSpec">Height measure spec.</param>
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);

			SetMeasuredDimension(MeasuredHeight,MeasuredWidth);
		}

		/// <summary>
		/// Raises the draw event.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		protected override void OnDraw (Canvas canvas)
		{
			TextPaint textPaint=this.Paint;
			textPaint.Color=Color.White;

			canvas.Save();

			if (_topDown) {
				canvas.Translate(this.Width,0);
				canvas.Rotate(90);
			} else {
				canvas.Translate(0,this.Height);
				canvas.Rotate(-90);
			}

			canvas.Translate(this.CompoundPaddingLeft,this.ExtendedPaddingTop);

			this.Layout.Draw(canvas);
			canvas.Restore();

		}
		#endregion 
	}
}

