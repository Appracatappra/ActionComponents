using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;


namespace ActionComponents
{
	/// <summary>
	/// The <see cref="ActionComponents.ACLabel"/> is a custom <c>UILabel</c> that contains events to easily handle being
	/// touched and adds helper methods to make it easier to move or resize the label.
	/// </summary>
	/// <remarks>Available in ActionPack Business or Enterprise only</remarks>
	[Register("ACLabel")]
	public class ACLabel : UILabel
	{
		#region Private Variables
		private bool _bringToFrontOnTouched;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the [OPTIONAL] tag that can be assosciated with this <see cref="ActionComponents.ACLabel"/> 
		/// </summary>
		/// <value>The tag.</value>
		public Object tag { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACLabel"/>
		/// is automatically brought to the front when touched.
		/// </summary>
		/// <value><c>true</c> if bring to front on touched; otherwise, <c>false</c>.</value>
		public bool bringToFrontOnTouched{
			get { return _bringToFrontOnTouched;}
			set { _bringToFrontOnTouched = value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponents.ACLabel"/>
		/// is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		/// <remarks>Enabling/disabling a <see cref="ActionComponents.ACLabel"/> automatically changes the value of it's
		/// <c>UserInteractionEnabled</c> flag</remarks>
		public new bool Enabled{
			get { return UserInteractionEnabled;}
			set { UserInteractionEnabled = value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		public ACLabel () : base(){
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ACLabel(NSCoder coder): base(coder){
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="flag">Flag.</param>
		public ACLabel(NSObjectFlag flag): base(flag){
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="bounds">Bounds.</param>
		public ACLabel(CGRect bounds): base(bounds){
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACLabel"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ACLabel(IntPtr ptr): base(ptr){
			Initialize();
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){

			//Set the default properties
			this.UserInteractionEnabled=true;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Moves this <see cref="ActionComponents.ACLabel"/> to the given point 
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void MoveToPoint(nfloat x, nfloat y){
			//Move to the given location
			Frame = new CGRect (x, y, Frame.Width, Frame.Height);
		}

		/// <summary>
		/// Moves this <see cref="ActionComponents.ACLabel"/> to the given point 
		/// </summary>
		/// <param name="pt">Point.</param>
		public void MoveToPoint(CGPoint pt){
			MoveToPoint (pt.X, pt.Y);
		}

		/// <summary>
		/// Resize this <see cref="ActionComponents.ACLabel"/> to the specified width and height.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public void Resize(nfloat width, nfloat height){
			//Resize this view
			Frame = new CGRect (Frame.Left, Frame.Top, width, height);
		}

		/// <summary>
		/// Rotates this <see cref="ActionComponents.ACLabel"/> to the given degrees 
		/// </summary>
		/// <param name="degrees">Degrees.</param>
		public void RotateTo(nfloat degrees) {
			this.Transform=CGAffineTransform.MakeRotation(ACImage.DegreesToRadians(degrees));	
		}
		#endregion 

		#region Override Methods
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

			//Automatically bring view to front?
			if (_bringToFrontOnTouched && this.Superview!=null) this.Superview.BringSubviewToFront(this);

			//Inform caller of event
			RaiseTouched ();

			//Pass call to base object
			base.TouchesBegan (touches, evt);
		}

		/// <Docs>Set containing the touches.</Docs>
		/// <summary>
		/// Send when one or more fingers are lifted from the screen.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			//Inform caller of event
			RaiseReleased ();

			#if TRIAL
			ACToast.MakeText("ACLabel by Appracatappra, LLC.",2f).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif

			//Pass call to base object
			base.TouchesEnded(touches, evt);
		} 
		#endregion

		#region Events
		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACLabel"/> is touched 
		/// </summary>
		public delegate void ACLabelTouchedDelegate (ACLabel view);
		public event ACLabelTouchedDelegate Touched;

		/// <summary>
		/// Raises the touched event
		/// </summary>
		private void RaiseTouched(){
			if (this.Touched != null)
				this.Touched (this);
		}

		/// <summary>
		/// Occurs when this <see cref="ActionComponents.ACLabel"/> is released 
		/// </summary>
		public delegate void ACLabelReleasedDelegate (ACLabel view);
		public event ACLabelReleasedDelegate Released;

		/// <summary>
		/// Raises the released.
		/// </summary>
		private void RaiseReleased(){
			if (this.Released != null)
				this.Released (this);
		}
		#endregion
	}
}

