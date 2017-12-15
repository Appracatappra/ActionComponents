using System;
using System.Text;

using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Runtime;


namespace ActionComponents
{
	/// <summary>
	/// Helper extensions when working with Android drawing Canvas
	/// </summary>
	public static class ActionCanvasExtensions
	{

		#region Static Methods
		/// <summary>
		/// Calculates the height of a block of text to be drawn in a canvas given the wrapping width and the maximum number of lines
		/// to draw
		/// </summary>
		/// <returns>The height.</returns>
		/// <param name="text">Text.</param>
		/// <param name="width">Width.</param>
		/// <param name="maxlines">Maxlines.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <remarks>This routines breaks on character not words</remarks>
		public static int TextHeight(string text, int width, int maxlines, Paint textPaint) {

			//Get the font metrix information and calculate line height
			var metrics = textPaint.GetFontMetricsInt ();
			var lineHeight = -metrics.Ascent + metrics.Descent;

			//Set starting points
			var height = lineHeight;
			var lines = 0;
			var lineWidth = 0;
			var n = 0;

			//Process all characters in the incoming string
			for (int i=0; i<text.Length; ++i) {
				//Take action based on the current character
				switch (text [i]) {
				case '\n':
				case '\r':
				case '\t':
					//Move to next line
					height += lineHeight;
					lineWidth = 0;

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return height;
					break;
				default:
					//Get the width of the current character
					n = (int)textPaint.MeasureText (text [i].ToString ());

					//Will this character fit on this line?
					if ((lineWidth + n) > width) {
						//Move to next line
						height += lineHeight;
						lineWidth = n;

						//Have we reached the maximum number of lines?
						if (++lines > maxlines)
							return height;
					} else {
						//Add to currrent line width
						lineWidth += n;
					}
					break;
				}
			}

			//Return calculated height
			return height;
		}

		/// <summary>
		/// Draws a block of text at the given x,y coords in a canvas wrapping the text on the given width and stopping at the mx number
		/// of lines specified
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="maxlines">Maxlines.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <remarks>This routine breaks on character not words</remarks>
		public static void DrawTextBoxInCanvas(Canvas canvas, string text, int x, int y, int width, int maxlines, Paint textPaint){

			//Get the font metrix information and calculate line height
			var metrics = textPaint.GetFontMetricsInt ();
			var lineHeight = -metrics.Ascent + metrics.Descent;

			//Set starting points
			StringBuilder sb = new StringBuilder ();
			var ty = y + lineHeight;
			var lines = 0;
			var lineWidth = 0;
			var n = 0;

			//Process all characters in the incoming string
			for (int i=0; i<text.Length; ++i) {
				//Take action based on the current character
				switch (text [i]) {
				case '\n':
				case '\r':
				case '\t':
					//Draw out current line
					canvas.DrawText (sb.ToString (), x, ty, textPaint);

					//Move to next line
					ty += lineHeight;
					lineWidth = 0;
					sb.Clear ();

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return;
					break;
				default:
					//Get the width of the current character
					n = (int)textPaint.MeasureText (text [i].ToString ());

					//Will this character fit on this line?
					if ((lineWidth + n) > width) {
						//Draw out current line
						canvas.DrawText (sb.ToString (), x, ty, textPaint);

						//Move to next line
						ty += lineHeight;
						lineWidth = n;

						//Reset string builder
						sb.Clear ();
						sb.Append (text [i]);

						//Have we reached the maximum number of lines?
						if (++lines > maxlines)
							return;
					} else {
						//Add to currrent line width
						lineWidth += n;

						//Add character to output string
						sb.Append (text [i]);
					}
					break;
				}
			}

			//Any remaining text?
			if (sb.Length != 0) {
				//Draw out the remaining text
				canvas.DrawText (sb.ToString (), x, ty, textPaint);
			}

		}

		/// <summary>
		/// Measures the height of the given text drawn with the given style properties, wrapping the text at the given width and stopping
		/// after a maximum number of lines.
		/// </summary>
		/// <returns>The text block.</returns>
		/// <param name="text">Text.</param>
		/// <param name="width">Width.</param>
		/// <param name="maxlines">Maxlines.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <remarks>This routine breaks on words</remarks>
		public static int MeasureTextBlock(string text, int width, int maxlines, Paint textPaint){

			//Preprocess newline and carriage returns
			text = text.Replace ("\n", "\n ");
			text = text.Replace ("\r", "\r ");

			//Fracture text into words
			char[] delimiterChars = {' '};
			string[] words = text.Split(delimiterChars);
			string word = "";

			//Get the font metrix information and calculate line height
			var metrics = textPaint.GetFontMetricsInt ();
			var lineHeight = -metrics.Ascent + metrics.Descent;

			//Set starting points
			var height = lineHeight;
			var lines = 0;
			var lineWidth = 0;
			var n = 0;


			//Process all words in text block
			foreach(string w in words) {
				//Add space back to word
				word = w + " ";

				//Does the word contain a newlline?
				if (word.Contains ("\n ")) {
					//Yes, strip newline
					word.Replace("\n ","");

					//Move to next line
					height += lineHeight;
					lineWidth = 0;

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return height;
				}

				//Does the word contain a carriage return?
				if (word.Contains ("\r ")) {
					//Yes, strip newline
					word.Replace("\r ","");

					//Move to next line
					height += lineHeight;
					lineWidth = 0;

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return height;
				}

				//Get the width of the current word
				n = (int)textPaint.MeasureText (word);

				//Will the word fit on this line?
				if ((lineWidth + n) > width) {
					//No, move to next line
					height += lineHeight;
					lineWidth = n;

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return height;
				} else {
					//Add to word width to line width
					lineWidth += n;
				}
			}

			//Return calculated height
			return height;
		}

		/// <summary>
		/// Draws the given text at the given x,y coordinates in the given style in the given canvas. The text will be wrapping at the given width
		/// and will not exceed the given number of lines. The lines of text will be broken on word bounderies and newline or carriage returns can
		/// be included to force a line break.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="maxlines">Maxlines.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <remarks>This routine breaks on words</remarks>
		public static void DrawTextBlockInCanvas(Canvas canvas, string text, int x, int y, int width, int maxlines, Paint textPaint){
			DrawTextBlockInCanvas (canvas, text, x, y, width, maxlines, textPaint, TextBlockAlignment.Left);
		}

		/// <summary>
		/// Draws the given text at the given x,y coordinates in the given style in the given canvas. The text will be wrapping at the given width
		/// and will not exceed the given number of lines. The lines of text will be broken on word bounderies and newline or carriage returns can
		/// be included to force a line break.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="maxlines">Maxlines.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <param name="align">The text alignment</param>
		/// <remarks>This routine breaks on words</remarks>
		public static void DrawTextBlockInCanvas(Canvas canvas, string text, int x, int y, int width, int maxlines, Paint textPaint, TextBlockAlignment align){

			//Preprocess newline and carriage returns
			text = text.Replace ("\n", "\n ");
			text = text.Replace ("\r", "\r ");

			//Fracture text into words
			char[] delimiterChars = {' '};
			string[] words = text.Split(delimiterChars);
			string word = "";

			//Get the font metrix information and calculate line height
			var metrics = textPaint.GetFontMetricsInt ();
			var lineHeight = -metrics.Ascent + metrics.Descent;

			//Set starting points
			StringBuilder sb = new StringBuilder ();
			var ty = y + lineHeight;
			var lines = 0;
			var lineWidth = 0;
			var n = 0;

			//Process all words in text block
			foreach(string w in words) {
				//Add space back to word
				word = w + " ";

				//Does the word contain a newlline?
				if (word.Contains ("\n ")) {
					//Draw out current line
					DrawAlignedText(canvas, sb.ToString (), x, ty, width, lineWidth, textPaint, align);

					//Yes, strip newline
					word.Replace("\n ","");

					//Move to next line
					ty += lineHeight;
					lineWidth = 0;
					sb.Clear();

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return;
				}

				//Does the word contain a carriage return?
				if (word.Contains ("\r ")) {
					//Draw out current line
					DrawAlignedText(canvas, sb.ToString (), x, ty, width, lineWidth, textPaint, align);

					//Yes, strip newline
					word.Replace("\r ","");

					//Move to next line
					ty += lineHeight;
					lineWidth = 0;
					sb.Clear();

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return;
				}

				//Get the width of the current word
				n = (int)textPaint.MeasureText (word);

				//Will this character fit on this line?
				if ((lineWidth + n) > width) {
					//Draw out current line
					DrawAlignedText(canvas, sb.ToString (), x, ty, width, lineWidth, textPaint, align);

					//Move to next line
					ty += lineHeight;
					lineWidth = n;

					//Reset string builder
					sb.Clear ();
					sb.Append (word);

					//Have we reached the maximum number of lines?
					if (++lines > maxlines)
						return;
				} else {
					//Add to currrent line width
					lineWidth += n;

					//Add character to output string
					sb.Append (word);
				}

			}

			//Any remaining text?
			if (sb.Length != 0) {
				//Draw out the remaining text
				DrawAlignedText(canvas, sb.ToString (), x, ty, width, lineWidth, textPaint, align);
			}

		}

		/// <summary>
		/// Given a line of text, a desired alignment, the maximum line width and the measured line width, this routine draws the text in the given
		/// style at the desired x,y coordinates.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="lineLength">Line length.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <param name="align">Align.</param>
		private static void DrawAlignedText(Canvas canvas, string text, int x, int y, int width, int lineLength, Paint textPaint, TextBlockAlignment align) {

			//Take action based on the text alignment specified
			switch (align) {
			case TextBlockAlignment.Left:
				//Left justified, no modification required
				break;
			case TextBlockAlignment.Center:
				x = x + ((width / 2) - (lineLength / 2));
				break;
			case TextBlockAlignment.Right:
				x = x + (width-lineLength);
				break;
			}

			//Draw text at the desired location
			canvas.DrawText (text, x, y, textPaint);

		}

		/// <summary>
		/// Draws a single line of text aligned horizontaly and vertically inside the rectangle specified by the x,y and given width, height.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <param name="alignHorizontal">Align horizontal.</param>
		/// <param name="alignVertical">Align vertical.</param>
		public static void DrawTextAligned(Canvas canvas, string text, int x, int y, int width, int height, Paint textPaint, TextBlockAlignment alignHorizontal, TextBlockAlignment alignVertical) {

			//Get the font metrix information and calculate line height
			var metrics = textPaint.GetFontMetricsInt ();
			var lineHeight = -metrics.Ascent + metrics.Descent;

			// Get the width of the current word
			var lineLength = (int)textPaint.MeasureText (text);

			//Take action based on the text alignment specified
			switch (alignHorizontal) {
			case TextBlockAlignment.Left:
				//Left justified, no modification required
				break;
			case TextBlockAlignment.Center:
				x = (x/2) + ((width / 2) - (lineLength / 2));
				break;
			case TextBlockAlignment.Right:
				x = (x/2) + (width-lineLength);
				break;
			}

			// Take action based on the text alignment specified
			switch (alignVertical) {
			case TextBlockAlignment.Top:
				y = lineHeight;
				break;
			case TextBlockAlignment.Center:
				y = ((height / 2) + (lineHeight/2)) - (y/2);
				break;
			case TextBlockAlignment.Bottom:
				y = height - (y/2);
				break;
			}

			//Draw text at the desired location
			canvas.DrawText (text, x, y, textPaint);

		}

		/// <summary>
		/// Draws a single line of text aligned horizontally inside the rectangle specified by the x, y and the given width
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		/// <param name="text">Text.</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="textPaint">Text paint.</param>
		/// <param name="alignHorizontal">Align horizontal.</param>
		public static void DrawTextAligned(Canvas canvas, string text, int x, int y, int width, int height, Paint textPaint, TextBlockAlignment alignHorizontal) {

			// Get the width of the current word
			var lineLength = (int)textPaint.MeasureText (text);

			//Take action based on the text alignment specified
			switch (alignHorizontal) {
			case TextBlockAlignment.Left:
				//Left justified, no modification required
				break;
			case TextBlockAlignment.Center:
				x += ((width / 2) - (lineLength / 2));
				break;
			case TextBlockAlignment.Right:
				x += (width-lineLength);
				break;
			}

			//Draw text at the desired location
			canvas.DrawText (text, x, y, textPaint);

		}
		#endregion 

	}
}

