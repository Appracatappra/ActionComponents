using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;
using ObjCRuntime;


namespace ActionComponents
{

	public class ACToast : NSObject
	{
		#region Static Make Methods
		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		public static ACToast MakeText(string text) {
			return new ACToast(text);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast MakeText(string text, ACToastGravity gravity){
			return new ACToast(text,gravity);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, ACToastGravity gravity, CGPoint offset){
			return new ACToast(text,gravity,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast MakeText(string text, ACToastGravity gravity, ACToastAppearance appearance){
			return new ACToast(text,gravity,appearance);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			return new ACToast(text,gravity,appearance,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		public static ACToast MakeText(string text, float duration){
			return new ACToast(text,duration);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast MakeText(string text, float duration, ACToastGravity gravity){
			return new ACToast (text, duration, gravity);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, float duration, ACToastGravity gravity, CGPoint offset){
			return new ACToast(text,duration,gravity,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast MakeText(string text, float duration, ACToastGravity gravity, ACToastAppearance appearance){
			return new ACToast(text,duration,gravity,appearance);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, float duration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			return new ACToast(text,duration,gravity,appearance,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, float duration, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			return new ACToast (text, duration, fadeInDuration, fadeOutDuration, gravity, appearance, offset);
		}

		//--------

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		public static ACToast MakeText(string text, ACToastLength length){
			return new ACToast(text,length);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast MakeText(string text, ACToastLength length, ACToastGravity gravity){
			return new ACToast (text, length, gravity);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, ACToastLength length, ACToastGravity gravity, CGPoint offset){
			return new ACToast(text,length,gravity,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast MakeText(string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance){
			return new ACToast(text,length,gravity,appearance);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			return new ACToast(text,length,gravity,appearance,offset);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast MakeText(string text, ACToastLength length, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			return new ACToast (text, length, fadeInDuration, fadeOutDuration, gravity, appearance, offset);
		}
		#endregion 

		#region Static Show Methods
		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		public static ACToast ShowText(string text) {
			var toast = new ACToast(text);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast ShowText(string text, ACToastGravity gravity){
			var toast = new ACToast(text,gravity);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, ACToastGravity gravity, CGPoint offset){
			var toast = new ACToast(text,gravity,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast ShowText(string text, ACToastGravity gravity, ACToastAppearance appearance){
			var toast = new ACToast(text,gravity,appearance);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			var toast = new ACToast(text,gravity,appearance,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		public static ACToast ShowText(string text, float duration){
			var toast = new ACToast(text,duration);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast ShowText(string text, float duration, ACToastGravity gravity){
			var toast = new ACToast (text, duration, gravity);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, float duration, ACToastGravity gravity, CGPoint offset){
			var toast = new ACToast(text,duration,gravity,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast ShowText(string text, float duration, ACToastGravity gravity, ACToastAppearance appearance){
			var toast = new ACToast(text,duration,gravity,appearance);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, float duration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			var toast = new ACToast(text,duration,gravity,appearance,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, float duration, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			var toast = new ACToast (text, duration, fadeInDuration, fadeOutDuration, gravity, appearance, offset);
			toast.Show ();
			return toast;
		}

		//--------

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		public static ACToast ShowText(string text, ACToastLength length){
			var toast = new ACToast(text,length);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		public static ACToast ShowText(string text, ACToastLength length, ACToastGravity gravity){
			var toast = new ACToast (text, length, gravity);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, ACToastLength length, ACToastGravity gravity, CGPoint offset){
			var toast = new ACToast(text,length,gravity,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public static ACToast ShowText(string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance){
			var toast = new ACToast(text,length,gravity,appearance);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			var toast = new ACToast(text,length,gravity,appearance,offset);
			toast.Show ();
			return toast;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="ActionComponents.ACToast"/> message with the given
		/// properties and returns it
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public static ACToast ShowText(string text, ACToastLength length, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset){
			var toast =  new ACToast (text, length, fadeInDuration, fadeOutDuration, gravity, appearance, offset);
			toast.Show ();
			return toast;
		}
		#endregion

		#region Private Variables
		private float _duration=1000f;
		private float _fadeInDuration = 0.3f;
		private float _fadeOutDuration = 0.5f;
		private ACToastGravity _gravity = ACToastGravity.Bottom;
		private ACToastAppearance _appearance = new ACToastAppearance();
		private CGPoint _offset = new CGPoint(0, 0);
		private CGPoint _position = new CGPoint(0, 0);
		private string _text = "";
		private UIView _view;
		#endregion 

		#region Computed Properties
		/// <summary>
		/// Gets or sets the duration in seconds that this <see cref="ActionComponents.ACToast"/> message will be displayed.
		/// </summary>
		/// <value>The duration.</value>
		/// <remarks>Set the duration to zero (0) to display the message forever until dismissed by the user or programmatically. This provides an alternative to
		/// setting the <c>length</c> property to a <see cref="ActionComponents.ACToastLength"/>.</remarks>
		public float duration{
			get { return _duration / 1000;}
			set { _duration = value * 1000;}
		}

		/// <summary>
		/// Gets or sets the length that the <see cref="ActionComponents.ACToast"/> message will be displayed
		/// </summary>
		/// <value>The length.</value>
		/// <remarks>This provides an alternative to setting the <c>duration</c> in seconds</remarks>
		public ACToastLength length{
			get {
				//Decode duration to get length
				if (duration <= 0f) {
					return ACToastLength.Forever;
				} else if (duration <= 1.0f) {
					return ACToastLength.Short;
				} else if (duration > 1.0f && duration <= 2.0f) {
					return ACToastLength.Medium;
				} else {
					return ACToastLength.Long;
				}
			}
			set {
				//Take action based on the request length
				switch (value) {
				case ACToastLength.Forever:
					duration = 0f;
					break;
				case ACToastLength.Short:
					duration = 1.0f;
					break;
				case ACToastLength.Medium:
					duration = 2.0f;
					break;
				case ACToastLength.Long:
					duration = 5.0f;
					break;
				}
			}
		}

		/// <summary>
		/// Gets or sets the duration of the fade in animation for this <see cref="ActionComponents.ACToast"/> message
		/// </summary>
		/// <value>The duration of the fade in.</value>
		public float fadeInDuration{
			get { return _fadeInDuration;}
			set { _fadeInDuration = value;}
		}

		/// <summary>
		/// Gets or sets the duration of the fade out animation for this <see cref="ActionComponents.ACToast"/> message
		/// </summary>
		/// <value>The duration of the fade in.</value>
		public float fadeOutDuration{
			get { return _fadeOutDuration;}
			set { _fadeOutDuration = value;}
		}

		/// <summary>
		/// Gets or sets the gravity for this <see cref="ActionComponents.ACToast"/> message 
		/// </summary>
		/// <value>The gravity.</value>
		public ACToastGravity gravity{
			get { return _gravity;}
			set { _gravity = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="ActionComponents.ACToastAppearance"/> for this
		/// <see cref="ActionComponents.ACToast"/> message
		/// </summary>
		/// <value>The appearance.</value>
		public ACToastAppearance appearance{
			get { return _appearance;}
			set {
				_appearance = value;

				//Wireup change event
				_appearance.AppearanceModified += () => {
					//Ignore changes for now
				};
			}
		}

		/// <summary>
		/// Gets or sets the text displayed in this <see cref="ActionComponents.ACToast"/> message 
		/// </summary>
		/// <value>The text.</value>
		public string text {
			get { return _text;}
			set { _text = value;}
		}

		/// <summary>
		/// Gets the view controlled by this <see cref="ActionComponents.ACToast"/> message
		/// </summary>
		/// <value>The view.</value>
		public UIView view {
			get { return _view;}
		}

		/// <summary>
		/// Gets or sets the position this <see cref="ActionComponents.ACToast"/> message will be
		/// displayed at
		/// </summary>
		/// <value>The position.</value>
		/// <remarks>The <c>position</c> property is only honored if the <see cref="ActionComponents.ACToastGravity"/> is set to
		/// <c>Custom</c> </remarks>
		public CGPoint position{
			get { return _position;}
			set { _position = value;}
		}

		/// <summary>
		/// Gets or sets the offset that can be applied to the <c>position</c> automatically calculated for this <see cref="ActionComponents.ACToast"/>
		/// based on the selected <see cref="ActionComponents.ACToastGravity"/> 
		/// </summary>
		/// <value>The offset.</value>
		public CGPoint offset{
			get { return _offset;}
			set { _offset = value;}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		public ACToast (string text) {
			//Initialize
			this.text = text;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		public ACToast (string text, ACToastGravity gravity) {
			//Initialize
			this.text = text;
			this.gravity = gravity;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, ACToastGravity gravity, CGPoint offset) {
			//Initialize
			this.text = text;
			this.gravity = gravity;
			this.offset = offset;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public ACToast (string text, ACToastGravity gravity, ACToastAppearance appearance) {
			//Initialize
			this.text = text;
			this.gravity = gravity;
			this.appearance = appearance;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset) {
			//Initialize
			this.text = text;
			this.gravity = gravity;
			this.appearance = appearance;
			this.offset = offset;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		public ACToast (string text, float duration)
		{
			//Initialize
			this.text = text;
			this.duration = duration;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		public ACToast (string text, float duration, ACToastGravity gravity)
		{
			//Initialize
			this.text = text;
			this.duration = duration;
			this.gravity = gravity;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, float duration, ACToastGravity gravity, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.duration = duration;
			this.gravity = gravity;
			this.offset = offset;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public ACToast (string text, float duration, ACToastGravity gravity, ACToastAppearance appearance)
		{
			//Initialize
			this.text = text;
			this.duration = duration;
			this.gravity = gravity;
			this.appearance = appearance;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, float duration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.duration = duration;
			this.gravity = gravity;
			this.appearance = appearance;
			this.offset = offset;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="duration">Duration.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, float duration, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.duration = duration;
			this.fadeInDuration = fadeInDuration;
			this.fadeOutDuration = fadeOutDuration;
			this.gravity = gravity;
			this.appearance = appearance;
			this.offset = offset;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		public ACToast (string text, ACToastLength length)
		{
			//Initialize
			this.text = text;
			this.length = length;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		public ACToast (string text, ACToastLength length, ACToastGravity gravity)
		{
			//Initialize
			this.text = text;
			this.length = length;
			this.gravity = gravity;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, ACToastLength length, ACToastGravity gravity, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.length = length;
			this.gravity = gravity;
			this.offset = offset;

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		public ACToast (string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance)
		{
			//Initialize
			this.text = text;
			this.length = length;
			this.gravity = gravity;
			this.appearance = appearance;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, ACToastLength length, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.length = length;
			this.gravity = gravity;
			this.appearance = appearance;
			this.offset = offset;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACToast"/> class.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="length">Length.</param>
		/// <param name="fadeInDuration">Fade in duration.</param>
		/// <param name="fadeOutDuration">Fade out duration.</param>
		/// <param name="gravity">Gravity.</param>
		/// <param name="appearance">Appearance.</param>
		/// <param name="offset">Offset.</param>
		public ACToast (string text, ACToastLength length, float fadeInDuration, float fadeOutDuration, ACToastGravity gravity, ACToastAppearance appearance, CGPoint offset)
		{
			//Initialize
			this.text = text;
			this.length = length;
			this.fadeInDuration = fadeInDuration;
			this.fadeOutDuration = fadeOutDuration;
			this.gravity = gravity;
			this.appearance = appearance;
			this.offset = offset;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Show the <see cref="ActionComponents.ACToast"/> message 
		/// </summary>
		public void Show()
		{
			//Create a new button at the "body" of the toast popup
			UIButton v = UIButton.FromType(UIButtonType.Custom);
			_view = v;

			//Calculate the message metrix
			UIFont font = UIFont.SystemFontOfSize(appearance.textSize);
			CGSize textSize = UIStringDrawing.StringSize(text, font, new CGSize(280, 60));


			//Create a label to hold the message
			UILabel label = new UILabel(new CGRect(0, 0, textSize.Width + 5, textSize.Height + 5));
			label.BackgroundColor = UIColor.Clear;
			label.TextColor = appearance.textColor;
			label.Font = font;
			label.Text = text;
			label.Lines = 0;
			if (!appearance.flat)
			{
				label.ShadowColor = appearance.textShadow;
				label.ShadowOffset = new CGSize(1, 1);
			}

			//Adjust button body to hold the message
			v.Frame = new CGRect(0, 0, textSize.Width + 10, textSize.Height + 10);
			label.Center = new CGPoint(v.Frame.Size.Width / 2, v.Frame.Height / 2);
			v.AddSubview(label);

			//Style the message
			v.BackgroundColor = appearance.background;
			v.Layer.CornerRadius = appearance.cornerRadius;

			//Gain access to the main window and calculate it's center position
			UIWindow window = UIApplication.SharedApplication.Windows[0];
			CGRect bounds = iOSDevice.AvailableScreenBounds;
			CGPoint point = new CGPoint(bounds.Width / 2, bounds.Height / 2);

			//Adjust point based on the gravity settings
			switch (gravity)
			{
				case ACToastGravity.Top:
					point = new CGPoint(bounds.Width / 2, 45);
					break;
				case ACToastGravity.Center:
					point = new CGPoint(bounds.Width / 2, bounds.Height / 2);
					break;
				case ACToastGravity.Bottom:
					point = new CGPoint(bounds.Width / 2, bounds.Height - 45);
					break;
				case ACToastGravity.Custom:
					point = position;
					break;
			}

			//Make the final position adjustments
			point = new CGPoint(point.X + offset.X, point.Y + offset.Y);
			v.Center = point;
			v.Alpha = 0.0f;

			//Insert message into window
			window.RootViewController.View.AddSubview(v);

			//Wire-up touch events
			v.AllTouchEvents += (sender, e) => {
				//Hide the toast message
				HideToast();
			};

			//Define Animation
			UIView.BeginAnimations("FadeIn");
			UIView.SetAnimationDuration(fadeInDuration);

			//Set end of Animation handler
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("FadeInComplete"));

			//Adjust property
			v.Alpha = 1.0f;

			//Execute Animation
			UIView.CommitAnimations();

		}

		/// <summary>
		/// Hides this <see cref="ActionComponents.ACToast"/> message 
		/// </summary>
		[Export("HideToast")]
		public void HideToast ()
		{
			//Anything to process?
			if (view == null)
				return;

			//Define Animation
			UIView.BeginAnimations("FadeOut");
			UIView.SetAnimationDuration(fadeOutDuration);

			//Set end of Animation handler
			UIView.SetAnimationDelegate(this);
			UIView.SetAnimationDidStopSelector(new Selector("FadeOutComplete"));

			//Adjust property
			view.Alpha = 0f;

			//Execute Animation
			UIView.CommitAnimations();
		}

		/// <summary>
		/// Removes the <see cref="ActionComponents.ACToast"/> from the parent <c>window</c> and releases it from
		/// memory
		/// </summary>
		public void RemoveToast ()
		{
			if (view !=null) {
				view.RemoveFromSuperview ();
				_view = null;
			}
		}
		#endregion 

		#region Internal Methods
		/// <summary>
		/// Called when the fade in animation has completed
		/// </summary>
		[Export("FadeInComplete")]
		internal virtual void FadeInComplete(){

			//Is this message set to display forever?
			if (duration>0f) {
				//No, set a timer to remove the message from the screen
				NSTimer.CreateScheduledTimer ((double)duration, (NSTimer obj) => {
					HideToast();
				});
			}

		}

		/// <summary>
		/// Called when the fade out animation has completed
		/// </summary>
		[Export("FadeOutComplete")]
		internal virtual void FadeOutComplete(){

			//Remove from the parent view
			RemoveToast ();
		}
		#endregion 

	}
}

