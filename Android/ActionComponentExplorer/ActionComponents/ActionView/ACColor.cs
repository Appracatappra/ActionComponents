using System;
using Android.Graphics;
using UIKit;

namespace ActionComponents
{
	/// <summary>
	/// A cross-platform tool for working with colors in the iOS and Android Mobile Operating systems. <c>ACColor</c> represents a union of
	/// nearly all of the options and features of both <c>UIColor</c> from iOS and the <c>Color</c> object from Android so the property or method used on one platform can be used on another
	/// without change in syntax. Several new features are supported such creating a color from or returning a color as a CSS/Web Hex String in the form #RRGGBB or #AARRGGBB.
	/// <c>ACColor</c> is fully compatible with <c>UIColor</c> and <c>Color</c> so it can be used anywhere those objects would be used and can be
	/// cast to and from those types implicitly.  
	/// </summary>
	public class ACColor 
	{
		#region Static Action Color Presets
		/// <summary>
		/// Gets the orange.
		/// </summary>
		/// <value>The orange.</value>
		public static ACColor ActionOrange
		{
			get { return ACColor.FromRGB(251, 173, 49); }
		}

		/// <summary>
		/// Gets the yellow orange.
		/// </summary>
		/// <value>The yellow orange.</value>
		public static ACColor ActionYellowOrange
		{
			get { return ACColor.FromRGB(223, 175, 9); }
		}

		/// <summary>
		/// Gets the yellow.
		/// </summary>
		/// <value>The yellow.</value>
		public static ACColor ActionYellow
		{
			get { return ACColor.FromRGB(244, 227, 39); }
		}

		/// <summary>
		/// Gets the yellow green.
		/// </summary>
		/// <value>The yellow green.</value>
		public static ACColor ActionYellowGreen
		{
			get { return ACColor.FromRGB(125, 178, 24); }
		}

		/// <summary>
		/// Gets the green tea.
		/// </summary>
		/// <value>The green tea.</value>
		public static ACColor ActionGreenTea
		{
			get { return ACColor.FromRGB(244, 227, 39); }
		}

		/// <summary>
		/// Gets the teal.
		/// </summary>
		/// <value>The teal.</value>
		public static ACColor ActionTeal
		{
			get { return ACColor.FromRGB(61, 150, 84); }
		}

		/// <summary>
		/// Gets the cyan.
		/// </summary>
		/// <value>The cyan.</value>
		public static ACColor ActionCyan
		{
			get { return ACColor.FromRGB(108, 193, 154); }
		}

		/// <summary>
		/// Gets the navy blue.
		/// </summary>
		/// <value>The navy blue.</value>
		public static ACColor ActionNavyBlue
		{
			get { return ACColor.FromRGB(59, 118, 160); }
		}

		/// <summary>
		/// Gets the sky blue.
		/// </summary>
		/// <value>The sky blue.</value>
		public static ACColor ActionSkyBlue
		{
			get { return ACColor.FromRGB(116, 178, 224); }
		}

		/// <summary>
		/// Gets the grape.
		/// </summary>
		/// <value>The grape.</value>
		public static ACColor ActionGrape
		{
			get { return ACColor.FromRGB(100, 88, 158); }
		}

		/// <summary>
		/// Gets the lavendar.
		/// </summary>
		/// <value>The lavendar.</value>
		public static ACColor ActionLavendar
		{
			get { return ACColor.FromRGB(163, 142, 195); }
		}

		/// <summary>
		/// Gets the dusty rose.
		/// </summary>
		/// <value>The dusty rose.</value>
		public static ACColor ActionDustyRose
		{
			get { return ACColor.FromRGB(168, 64, 102); }
		}

		/// <summary>
		/// Gets the pink.
		/// </summary>
		/// <value>The pink.</value>
		public static ACColor ActionPink
		{
			get { return ACColor.FromRGB(214, 133, 170); }
		}

		/// <summary>
		/// Gets the brick red.
		/// </summary>
		/// <value>The brick red.</value>
		public static ACColor ActionBrickRed
		{
			get { return ACColor.FromRGB(209, 28, 17); }
		}

		/// <summary>
		/// Gets the pink grapefruit.
		/// </summary>
		/// <value>The pink grapefruit.</value>
		public static ACColor ActionPinkGrapefruit
		{
			get { return ACColor.FromRGB(243, 118, 96); }
		}

		/// <summary>
		/// Gets the red orange.
		/// </summary>
		/// <value>The red orange.</value>
		public static ACColor ActionRedOrange
		{
			get { return ACColor.FromRGB(243, 83, 2); }
		}

		/// <summary>
		/// Gets the Appracatappra ActionPack Component Suite branded Orange color.
		/// </summary>
		/// <value>The action pack orange.</value>
		public static ACColor ActionBrightOrange
		{
			get { return ACColor.FromRGB(255, 59, 48); }
		}
		#endregion

		#region Static iOS color presets
		/// <summary>
		/// Gets the black color
		/// </summary>
		/// <value>The black.</value>
		public static ACColor Black {
			get { return Color.Black;}
		}

		/// <summary>
		/// Gets the blue.
		/// </summary>
		/// <value>The blue.</value>
		public static ACColor Blue {
			get { return Color.Blue;}
		}

		/// <summary>
		/// Gets the brown.
		/// </summary>
		/// <value>The brown.</value>
		public static ACColor Brown {
			get { return Color.Brown;}
		}

		/// <summary>
		/// Gets the clear.
		/// </summary>
		/// <value>The clear.</value>
		public static ACColor Clear {
			get { return Color.Transparent;}
		}

		/// <summary>
		/// Gets the cyan.
		/// </summary>
		/// <value>The cyan.</value>
		public static ACColor Cyan {
			get { return Color.Cyan;}
		}

		/// <summary>
		/// Gets the dark gray.
		/// </summary>
		/// <value>The dark gray.</value>
		public static ACColor DarkGray {
			get { return Color.DarkGray;}
		}

		/// <summary>
		/// Gets the color of the dark text.
		/// </summary>
		/// <value>The color of the dark text.</value>
		public static ACColor DarkTextColor {
			get { return Color.Black;}
		}

		/// <summary>
		/// Gets the gray.
		/// </summary>
		/// <value>The gray.</value>
		public static ACColor Gray {
			get { return Color.Gray;}
		}

		/// <summary>
		/// Gets the green.
		/// </summary>
		/// <value>The green.</value>
		public static ACColor Green {
			get { return Color.Green;}
		}

		/// <summary>
		/// Gets the color of the group table view background.
		/// </summary>
		/// <value>The color of the group table view background.</value>
		public static ACColor GroupTableViewBackgroundColor {
			get { return Color.White;}
		}

		/// <summary>
		/// Gets the light gray.
		/// </summary>
		/// <value>The light gray.</value>
		public static ACColor LightGray {
			get { return Color.LightGray;}
		}

		/// <summary>
		/// Gets the color of the light text.
		/// </summary>
		/// <value>The color of the light text.</value>
		public static ACColor LightTextColor {
			get { return ACColor.FromRGBA(255,255,255,153);}
		}

		/// <summary>
		/// Gets the magenta.
		/// </summary>
		/// <value>The magenta.</value>
		public static ACColor Magenta {
			get { return Color.Magenta;}
		}

		/// <summary>
		/// Gets the orange.
		/// </summary>
		/// <value>The orange.</value>
		public static ACColor Orange {
			get { return Color.Orange;}
		}

		/// <summary>
		/// Gets the purple.
		/// </summary>
		/// <value>The purple.</value>
		public static ACColor Purple {
			get { return Color.Purple;}
		}

		/// <summary>
		/// Gets the red.
		/// </summary>
		/// <value>The red.</value>
		public static ACColor Red {
			get { return Color.Red;}
		}

		/// <summary>
		/// Gets the color of the scroll view textured background.
		/// </summary>
		/// <value>The color of the scroll view textured background.</value>
		public static ACColor ScrollViewTexturedBackgroundColor {
			get { return Color.DarkGray;}
		}

		/// <summary>
		/// Gets the color of the under page background.
		/// </summary>
		/// <value>The color of the under page background.</value>
		public static ACColor UnderPageBackgroundColor {
			get { return Color.LightGray;}
		}

		/// <summary>
		/// Gets the color of the view flipside background.
		/// </summary>
		/// <value>The color of the view flipside background.</value>
		public static ACColor ViewFlipsideBackgroundColor {
			get { return Color.Black;}
		}

		/// <summary>
		/// Gets the white.
		/// </summary>
		/// <value>The white.</value>
		public static ACColor White {
			get { return Color.White;}
		}

		/// <summary>
		/// Gets the yellow.
		/// </summary>
		/// <value>The yellow.</value>
		public static ACColor Yellow {
			get { return Color.Yellow;}
		}
		#endregion

		#region Static Android Color Presets
		/// <summary>
		/// Gets the ivory.
		/// </summary>
		/// <value>The ivory.</value>
		public static ACColor Ivory{
			get { return ACColor.FromHexString ("FFFFF0");}
		}

		/// <summary>
		/// Gets the light yellow.
		/// </summary>
		/// <value>The light yellow.</value>
		public static ACColor LightYellow{
			get { return ACColor.FromHexString ("FFFFE0");}
		}

		/// <summary>
		/// Gets the snow.
		/// </summary>
		/// <value>The snow.</value>
		public static ACColor Snow{
			get { return ACColor.FromHexString ("FFFAFA");}
		}

		/// <summary>
		/// Gets the floral white.
		/// </summary>
		/// <value>The floral white.</value>
		public static ACColor FloralWhite{
			get { return ACColor.FromHexString ("FFFAF0");}
		}

		/// <summary>
		/// Gets the lemon chiffon.
		/// </summary>
		/// <value>The lemon chiffon.</value>
		public static ACColor LemonChiffon{
			get { return ACColor.FromHexString ("FFFACD");}
		}

		/// <summary>
		/// Gets the cornsilk.
		/// </summary>
		/// <value>The cornsilk.</value>
		public static ACColor Cornsilk{
			get { return ACColor.FromHexString ("FFF8DC");}
		}

		/// <summary>
		/// Gets the seashell.
		/// </summary>
		/// <value>The seashell.</value>
		public static ACColor Seashell{
			get { return ACColor.FromHexString ("FFF5EE");}
		}

		/// <summary>
		/// Gets the lavender blush.
		/// </summary>
		/// <value>The lavender blush.</value>
		public static ACColor LavenderBlush{
			get { return ACColor.FromHexString ("FFF0F5");}
		}

		/// <summary>
		/// Gets the papaya whip.
		/// </summary>
		/// <value>The papaya whip.</value>
		public static ACColor PapayaWhip{
			get { return ACColor.FromHexString ("FFEFD5");}
		}

		/// <summary>
		/// Gets the blanched almond.
		/// </summary>
		/// <value>The blanched almond.</value>
		public static ACColor BlanchedAlmond{
			get { return ACColor.FromHexString ("FFEBCD");}
		}

		/// <summary>
		/// Gets the misty rose.
		/// </summary>
		/// <value>The misty rose.</value>
		public static ACColor MistyRose{
			get { return ACColor.FromHexString ("FFE4E1");}
		}

		/// <summary>
		/// Gets the bisque.
		/// </summary>
		/// <value>The bisque.</value>
		public static ACColor Bisque{
			get { return ACColor.FromHexString ("FFE4C4");}
		}

		/// <summary>
		/// Gets the moccasin.
		/// </summary>
		/// <value>The moccasin.</value>
		public static ACColor Moccasin{
			get { return ACColor.FromHexString ("FFE4B5");}
		}

		/// <summary>
		/// Gets the navajo white.
		/// </summary>
		/// <value>The navajo white.</value>
		public static ACColor NavajoWhite{
			get { return ACColor.FromHexString ("FFDEAD");}
		}

		/// <summary>
		/// Gets the peach puff.
		/// </summary>
		/// <value>The peach puff.</value>
		public static ACColor PeachPuff{
			get { return ACColor.FromHexString ("FFDAB9");}
		}

		/// <summary>
		/// Gets the gold.
		/// </summary>
		/// <value>The gold.</value>
		public static ACColor Gold{
			get { return ACColor.FromHexString ("FFD700");}
		}

		/// <summary>
		/// Gets the pink.
		/// </summary>
		/// <value>The pink.</value>
		public static ACColor Pink{
			get { return ACColor.FromHexString ("FFC0CB");}
		}

		/// <summary>
		/// Gets the light pink.
		/// </summary>
		/// <value>The light pink.</value>
		public static ACColor LightPink{
			get { return ACColor.FromHexString ("FFB6C1");}
		}

		/// <summary>
		/// Gets the light salmon.
		/// </summary>
		/// <value>The light salmon.</value>
		public static ACColor LightSalmon{
			get { return ACColor.FromHexString ("FFA07A");}
		}

		/// <summary>
		/// Gets the dark orange.
		/// </summary>
		/// <value>The dark orange.</value>
		public static ACColor DarkOrange{
			get { return ACColor.FromHexString ("FF8C00");}
		}

		/// <summary>
		/// Gets the coral.
		/// </summary>
		/// <value>The coral.</value>
		public static ACColor Coral{
			get { return ACColor.FromHexString ("FF7F50");}
		}

		/// <summary>
		/// Gets the hot pink.
		/// </summary>
		/// <value>The hot pink.</value>
		public static ACColor HotPink{
			get { return ACColor.FromHexString ("FF69B4");}
		}

		/// <summary>
		/// Gets the tomato.
		/// </summary>
		/// <value>The tomato.</value>
		public static ACColor Tomato{
			get { return ACColor.FromHexString ("FF6347");}
		}

		/// <summary>
		/// Gets the orange red.
		/// </summary>
		/// <value>The orange red.</value>
		public static ACColor OrangeRed{
			get { return ACColor.FromHexString ("FF4500");}
		}

		/// <summary>
		/// Gets the deep pink.
		/// </summary>
		/// <value>The deep pink.</value>
		public static ACColor DeepPink{
			get { return ACColor.FromHexString ("FF1493");}
		}

		/// <summary>
		/// Gets the fuchsia.
		/// </summary>
		/// <value>The fuchsia.</value>
		public static ACColor Fuchsia{
			get { return ACColor.FromHexString ("FF00FF");}
		}

		/// <summary>
		/// Gets the old lace.
		/// </summary>
		/// <value>The old lace.</value>
		public static ACColor OldLace{
			get { return ACColor.FromHexString ("FDF5E6");}
		}

		/// <summary>
		/// Gets the light goldenrod yellow.
		/// </summary>
		/// <value>The light goldenrod yellow.</value>
		public static ACColor LightGoldenrodYellow{
			get { return ACColor.FromHexString ("FAFAD2");}
		}

		/// <summary>
		/// Gets the linen.
		/// </summary>
		/// <value>The linen.</value>
		public static ACColor Linen{
			get { return ACColor.FromHexString ("FAF0E6");}
		}

		/// <summary>
		/// Gets the antique white.
		/// </summary>
		/// <value>The antique white.</value>
		public static ACColor AntiqueWhite{
			get { return ACColor.FromHexString ("FAEBD7");}
		}

		/// <summary>
		/// Gets the salmon.
		/// </summary>
		/// <value>The salmon.</value>
		public static ACColor Salmon{
			get { return ACColor.FromHexString ("FA8072");}
		}

		/// <summary>
		/// Gets the ghost white.
		/// </summary>
		/// <value>The ghost white.</value>
		public static ACColor GhostWhite{
			get { return ACColor.FromHexString ("F8F8FF");}
		}

		/// <summary>
		/// Gets the mint cream.
		/// </summary>
		/// <value>The mint cream.</value>
		public static ACColor MintCream{
			get { return ACColor.FromHexString ("F5FFFA");}
		}

		/// <summary>
		/// Gets the white smoke.
		/// </summary>
		/// <value>The white smoke.</value>
		public static ACColor WhiteSmoke{
			get { return ACColor.FromHexString ("F5F5F5");}
		}

		/// <summary>
		/// Gets the beige.
		/// </summary>
		/// <value>The beige.</value>
		public static ACColor Beige{
			get { return ACColor.FromHexString ("F5F5DC");}
		}

		/// <summary>
		/// Gets the wheat.
		/// </summary>
		/// <value>The wheat.</value>
		public static ACColor Wheat{
			get { return ACColor.FromHexString ("F5DEB3");}
		}

		/// <summary>
		/// Gets the sandy brown.
		/// </summary>
		/// <value>The sandy brown.</value>
		public static ACColor SandyBrown{
			get { return ACColor.FromHexString ("F4A460");}
		}

		/// <summary>
		/// Gets the azure.
		/// </summary>
		/// <value>The azure.</value>
		public static ACColor Azure{
			get { return ACColor.FromHexString ("F0FFFF");}
		}

		/// <summary>
		/// Gets the honeydew.
		/// </summary>
		/// <value>The honeydew.</value>
		public static ACColor Honeydew{
			get { return ACColor.FromHexString ("F0FFF0");}
		}

		/// <summary>
		/// Gets the alice blue.
		/// </summary>
		/// <value>The alice blue.</value>
		public static ACColor AliceBlue{
			get { return ACColor.FromHexString ("F0F8FF");}
		}

		/// <summary>
		/// Gets the khaki.
		/// </summary>
		/// <value>The khaki.</value>
		public static ACColor Khaki{
			get { return ACColor.FromHexString ("F0E68C");}
		}

		/// <summary>
		/// Gets the light coral.
		/// </summary>
		/// <value>The light coral.</value>
		public static ACColor LightCoral{
			get { return ACColor.FromHexString ("F08080");}
		}

		/// <summary>
		/// Gets the pale goldenrod.
		/// </summary>
		/// <value>The pale goldenrod.</value>
		public static ACColor PaleGoldenrod{
			get { return ACColor.FromHexString ("EEE8AA");}
		}

		/// <summary>
		/// Gets the violet.
		/// </summary>
		/// <value>The violet.</value>
		public static ACColor Violet{
			get { return ACColor.FromHexString ("EE82EE");}
		}

		/// <summary>
		/// Gets the dark salmon.
		/// </summary>
		/// <value>The dark salmon.</value>
		public static ACColor DarkSalmon{
			get { return ACColor.FromHexString ("E9967A");}
		}

		/// <summary>
		/// Gets the lavender.
		/// </summary>
		/// <value>The lavender.</value>
		public static ACColor Lavender{
			get { return ACColor.FromHexString ("E6E6FA");}
		}

		/// <summary>
		/// Gets the light cyan.
		/// </summary>
		/// <value>The light cyan.</value>
		public static ACColor LightCyan{
			get { return ACColor.FromHexString ("E0FFFF");}
		}

		/// <summary>
		/// Gets the burly wood.
		/// </summary>
		/// <value>The burly wood.</value>
		public static ACColor BurlyWood{
			get { return ACColor.FromHexString ("DEB887");}
		}

		/// <summary>
		/// Gets the plum.
		/// </summary>
		/// <value>The plum.</value>
		public static ACColor Plum{
			get { return ACColor.FromHexString ("DDA0DD");}
		}

		/// <summary>
		/// Gets the gainsboro.
		/// </summary>
		/// <value>The gainsboro.</value>
		public static ACColor Gainsboro{
			get { return ACColor.FromHexString ("DCDCDC");}
		}

		/// <summary>
		/// Gets the crimson.
		/// </summary>
		/// <value>The crimson.</value>
		public static ACColor Crimson{
			get { return ACColor.FromHexString ("DC143C");}
		}

		/// <summary>
		/// Gets the pale violet red.
		/// </summary>
		/// <value>The pale violet red.</value>
		public static ACColor PaleVioletRed{
			get { return ACColor.FromHexString ("DB7093");}
		}

		/// <summary>
		/// Gets the goldenrod.
		/// </summary>
		/// <value>The goldenrod.</value>
		public static ACColor Goldenrod{
			get { return ACColor.FromHexString ("DAA520");}
		}

		/// <summary>
		/// Gets the orchid.
		/// </summary>
		/// <value>The orchid.</value>
		public static ACColor Orchid{
			get { return ACColor.FromHexString ("DA70D6");}
		}

		/// <summary>
		/// Gets the thistle.
		/// </summary>
		/// <value>The thistle.</value>
		public static ACColor Thistle{
			get { return ACColor.FromHexString ("D8BFD8");}
		}

		/// <summary>
		/// Gets the light grey.
		/// </summary>
		/// <value>The light grey.</value>
		public static ACColor LightGrey{
			get { return ACColor.FromHexString ("D3D3D3");}
		}

		/// <summary>
		/// Gets the tan.
		/// </summary>
		/// <value>The tan.</value>
		public static ACColor Tan{
			get { return ACColor.FromHexString ("D2B48C");}
		}

		/// <summary>
		/// Gets the chocolate.
		/// </summary>
		/// <value>The chocolate.</value>
		public static ACColor Chocolate{
			get { return ACColor.FromHexString ("D2691E");}
		}

		/// <summary>
		/// Gets the peru.
		/// </summary>
		/// <value>The peru.</value>
		public static ACColor Peru{
			get { return ACColor.FromHexString ("CD853F");}
		}

		/// <summary>
		/// Gets the indian red.
		/// </summary>
		/// <value>The indian red.</value>
		public static ACColor IndianRed{
			get { return ACColor.FromHexString ("CD5C5C");}
		}

		/// <summary>
		/// Gets the medium violet red.
		/// </summary>
		/// <value>The medium violet red.</value>
		public static ACColor MediumVioletRed{
			get { return ACColor.FromHexString ("C71585");}
		}

		/// <summary>
		/// Gets the silver.
		/// </summary>
		/// <value>The silver.</value>
		public static ACColor Silver{
			get { return ACColor.FromHexString ("C0C0C0");}
		}

		/// <summary>
		/// Gets the dark khaki.
		/// </summary>
		/// <value>The dark khaki.</value>
		public static ACColor DarkKhaki{
			get { return ACColor.FromHexString ("BDB76B");}
		}

		/// <summary>
		/// Gets the rosy brown.
		/// </summary>
		/// <value>The rosy brown.</value>
		public static ACColor RosyBrown{
			get { return ACColor.FromHexString ("BC8F8F");}
		}

		/// <summary>
		/// Gets the medium orchid.
		/// </summary>
		/// <value>The medium orchid.</value>
		public static ACColor MediumOrchid{
			get { return ACColor.FromHexString ("BA55D3");}
		}

		/// <summary>
		/// Gets the dark goldenrod.
		/// </summary>
		/// <value>The dark goldenrod.</value>
		public static ACColor DarkGoldenrod{
			get { return ACColor.FromHexString ("B8860B");}
		}

		/// <summary>
		/// Gets the fire brick.
		/// </summary>
		/// <value>The fire brick.</value>
		public static ACColor FireBrick{
			get { return ACColor.FromHexString ("B22222");}
		}

		/// <summary>
		/// Gets the powder blue.
		/// </summary>
		/// <value>The powder blue.</value>
		public static ACColor PowderBlue{
			get { return ACColor.FromHexString ("B0E0E6");}
		}

		/// <summary>
		/// Gets the light steel blue.
		/// </summary>
		/// <value>The light steel blue.</value>
		public static ACColor LightSteelBlue{
			get { return ACColor.FromHexString ("B0C4DE");}
		}

		/// <summary>
		/// Gets the pale turquoise.
		/// </summary>
		/// <value>The pale turquoise.</value>
		public static ACColor PaleTurquoise{
			get { return ACColor.FromHexString ("AFEEEE");}
		}

		/// <summary>
		/// Gets the green yellow.
		/// </summary>
		/// <value>The green yellow.</value>
		public static ACColor GreenYellow{
			get { return ACColor.FromHexString ("ADFF2F");}
		}

		/// <summary>
		/// Gets the light blue.
		/// </summary>
		/// <value>The light blue.</value>
		public static ACColor LightBlue{
			get { return ACColor.FromHexString ("ADD8E6");}
		}

		/// <summary>
		/// Gets the sienna.
		/// </summary>
		/// <value>The sienna.</value>
		public static ACColor Sienna{
			get { return ACColor.FromHexString ("A0522D");}
		}

		/// <summary>
		/// Gets the yellow green.
		/// </summary>
		/// <value>The yellow green.</value>
		public static ACColor YellowGreen{
			get { return ACColor.FromHexString ("9ACD32");}
		}

		/// <summary>
		/// Gets the dark orchid.
		/// </summary>
		/// <value>The dark orchid.</value>
		public static ACColor DarkOrchid{
			get { return ACColor.FromHexString ("9932CC");}
		}

		/// <summary>
		/// Gets the pale green.
		/// </summary>
		/// <value>The pale green.</value>
		public static ACColor PaleGreen{
			get { return ACColor.FromHexString ("98FB98");}
		}

		/// <summary>
		/// Gets the dark violet.
		/// </summary>
		/// <value>The dark violet.</value>
		public static ACColor DarkViolet{
			get { return ACColor.FromHexString ("9400D3");}
		}

		/// <summary>
		/// Gets the medium purple.
		/// </summary>
		/// <value>The medium purple.</value>
		public static ACColor MediumPurple{
			get { return ACColor.FromHexString ("9370DB");}
		}

		/// <summary>
		/// Gets the light green.
		/// </summary>
		/// <value>The light green.</value>
		public static ACColor LightGreen{
			get { return ACColor.FromHexString ("90EE90");}
		}

		/// <summary>
		/// Gets the dark sea green.
		/// </summary>
		/// <value>The dark sea green.</value>
		public static ACColor DarkSeaGreen{
			get { return ACColor.FromHexString ("8FBC8F");}
		}

		/// <summary>
		/// Gets the saddle brown.
		/// </summary>
		/// <value>The saddle brown.</value>
		public static ACColor SaddleBrown{
			get { return ACColor.FromHexString ("8B4513");}
		}

		/// <summary>
		/// Gets the dark magenta.
		/// </summary>
		/// <value>The dark magenta.</value>
		public static ACColor DarkMagenta{
			get { return ACColor.FromHexString ("8B008B");}
		}

		/// <summary>
		/// Gets the dark red.
		/// </summary>
		/// <value>The dark red.</value>
		public static ACColor DarkRed{
			get { return ACColor.FromHexString ("8B0000");}
		}

		/// <summary>
		/// Gets the blue violet.
		/// </summary>
		/// <value>The blue violet.</value>
		public static ACColor BlueViolet{
			get { return ACColor.FromHexString ("8A2BE2");}
		}

		/// <summary>
		/// Gets the light sky blue.
		/// </summary>
		/// <value>The light sky blue.</value>
		public static ACColor LightSkyBlue{
			get { return ACColor.FromHexString ("87CEFA");}
		}

		/// <summary>
		/// Gets the sky blue.
		/// </summary>
		/// <value>The sky blue.</value>
		public static ACColor SkyBlue{
			get { return ACColor.FromHexString ("87CEEB");}
		}

		/// <summary>
		/// Gets the olive.
		/// </summary>
		/// <value>The olive.</value>
		public static ACColor Olive{
			get { return ACColor.FromHexString ("808000");}
		}

		/// <summary>
		/// Gets the maroon.
		/// </summary>
		/// <value>The maroon.</value>
		public static ACColor Maroon{
			get { return ACColor.FromHexString ("800000");}
		}

		/// <summary>
		/// Gets the aquamarine.
		/// </summary>
		/// <value>The aquamarine.</value>
		public static ACColor Aquamarine{
			get { return ACColor.FromHexString ("7FFFD4");}
		}

		/// <summary>
		/// Gets the chartreuse.
		/// </summary>
		/// <value>The chartreuse.</value>
		public static ACColor Chartreuse{
			get { return ACColor.FromHexString ("7FFF00");}
		}

		/// <summary>
		/// Gets the lawn green.
		/// </summary>
		/// <value>The lawn green.</value>
		public static ACColor LawnGreen{
			get { return ACColor.FromHexString ("7CFC00");}
		}

		/// <summary>
		/// Gets the medium slate blue.
		/// </summary>
		/// <value>The medium slate blue.</value>
		public static ACColor MediumSlateBlue{
			get { return ACColor.FromHexString ("7B68EE");}
		}

		/// <summary>
		/// Gets the light slate gray.
		/// </summary>
		/// <value>The light slate gray.</value>
		public static ACColor LightSlateGray{
			get { return ACColor.FromHexString ("778899");}
		}

		/// <summary>
		/// Gets the slate gray.
		/// </summary>
		/// <value>The slate gray.</value>
		public static ACColor SlateGray{
			get { return ACColor.FromHexString ("708090");}
		}

		/// <summary>
		/// Gets the olive drab.
		/// </summary>
		/// <value>The olive drab.</value>
		public static ACColor OliveDrab{
			get { return ACColor.FromHexString ("6B8E23");}
		}

		/// <summary>
		/// Gets the slate blue.
		/// </summary>
		/// <value>The slate blue.</value>
		public static ACColor SlateBlue{
			get { return ACColor.FromHexString ("6A5ACD");}
		}

		/// <summary>
		/// Gets the dim gray.
		/// </summary>
		/// <value>The dim gray.</value>
		public static ACColor DimGray{
			get { return ACColor.FromHexString ("696969");}
		}

		/// <summary>
		/// Gets the medium aquamarine.
		/// </summary>
		/// <value>The medium aquamarine.</value>
		public static ACColor MediumAquamarine{
			get { return ACColor.FromHexString ("66CDAA");}
		}

		/// <summary>
		/// Gets the cornflower blue.
		/// </summary>
		/// <value>The cornflower blue.</value>
		public static ACColor CornflowerBlue{
			get { return ACColor.FromHexString ("6495ED");}
		}

		/// <summary>
		/// Gets the cadet blue.
		/// </summary>
		/// <value>The cadet blue.</value>
		public static ACColor CadetBlue{
			get { return ACColor.FromHexString ("5F9EA0");}
		}

		/// <summary>
		/// Gets the dark olive green.
		/// </summary>
		/// <value>The dark olive green.</value>
		public static ACColor DarkOliveGreen{
			get { return ACColor.FromHexString ("556B2F");}
		}

		/// <summary>
		/// Gets the indigo.
		/// </summary>
		/// <value>The indigo.</value>
		public static ACColor Indigo{
			get { return ACColor.FromHexString ("4B0082");}
		}

		/// <summary>
		/// Gets the medium turquoise.
		/// </summary>
		/// <value>The medium turquoise.</value>
		public static ACColor MediumTurquoise{
			get { return ACColor.FromHexString ("48D1CC");}
		}

		/// <summary>
		/// Gets the dark slate blue.
		/// </summary>
		/// <value>The dark slate blue.</value>
		public static ACColor DarkSlateBlue{
			get { return ACColor.FromHexString ("483D8B");}
		}

		/// <summary>
		/// Gets the steel blue.
		/// </summary>
		/// <value>The steel blue.</value>
		public static ACColor SteelBlue{
			get { return ACColor.FromHexString ("4682B4");}
		}

		/// <summary>
		/// Gets the royal blue.
		/// </summary>
		/// <value>The royal blue.</value>
		public static ACColor RoyalBlue{
			get { return ACColor.FromHexString ("4169E1");}
		}

		/// <summary>
		/// Gets the turquoise.
		/// </summary>
		/// <value>The turquoise.</value>
		public static ACColor Turquoise{
			get { return ACColor.FromHexString ("40E0D0");}
		}

		/// <summary>
		/// Gets the medium sea green.
		/// </summary>
		/// <value>The medium sea green.</value>
		public static ACColor MediumSeaGreen{
			get { return ACColor.FromHexString ("3CB371");}
		}

		/// <summary>
		/// Gets the lime green.
		/// </summary>
		/// <value>The lime green.</value>
		public static ACColor LimeGreen{
			get { return ACColor.FromHexString ("32CD32");}
		}

		/// <summary>
		/// Gets the dark slate gray.
		/// </summary>
		/// <value>The dark slate gray.</value>
		public static ACColor DarkSlateGray{
			get { return ACColor.FromHexString ("2F4F4F");}
		}

		/// <summary>
		/// Gets the sea green.
		/// </summary>
		/// <value>The sea green.</value>
		public static ACColor SeaGreen{
			get { return ACColor.FromHexString ("2E8B57");}
		}

		/// <summary>
		/// Gets the forest green.
		/// </summary>
		/// <value>The forest green.</value>
		public static ACColor ForestGreen{
			get { return ACColor.FromHexString ("228B22");}
		}

		/// <summary>
		/// Gets the light sea green.
		/// </summary>
		/// <value>The light sea green.</value>
		public static ACColor LightSeaGreen{
			get { return ACColor.FromHexString ("20B2AA");}
		}

		/// <summary>
		/// Gets the dodger blue.
		/// </summary>
		/// <value>The dodger blue.</value>
		public static ACColor DodgerBlue{
			get { return ACColor.FromHexString ("1E90FF");}
		}

		/// <summary>
		/// Gets the midnight blue.
		/// </summary>
		/// <value>The midnight blue.</value>
		public static ACColor MidnightBlue{
			get { return ACColor.FromHexString ("191970");}
		}

		/// <summary>
		/// Gets the aqua.
		/// </summary>
		/// <value>The aqua.</value>
		public static ACColor Aqua{
			get { return ACColor.FromHexString ("00FFFF");}
		}

		/// <summary>
		/// Gets the spring green.
		/// </summary>
		/// <value>The spring green.</value>
		public static ACColor SpringGreen{
			get { return ACColor.FromHexString ("00FF7F");}
		}

		/// <summary>
		/// Gets the lime.
		/// </summary>
		/// <value>The lime.</value>
		public static ACColor Lime{
			get { return ACColor.FromHexString ("00FF00");}
		}

		/// <summary>
		/// Gets the medium spring green.
		/// </summary>
		/// <value>The medium spring green.</value>
		public static ACColor MediumSpringGreen{
			get { return ACColor.FromHexString ("00FA9A");}
		}

		/// <summary>
		/// Gets the dark turquoise.
		/// </summary>
		/// <value>The dark turquoise.</value>
		public static ACColor DarkTurquoise{
			get { return ACColor.FromHexString ("00CED1");}
		}

		/// <summary>
		/// Gets the deep sky blue.
		/// </summary>
		/// <value>The deep sky blue.</value>
		public static ACColor DeepSkyBlue{
			get { return ACColor.FromHexString ("00BFFF");}
		}

		/// <summary>
		/// Gets the dark cyan.
		/// </summary>
		/// <value>The dark cyan.</value>
		public static ACColor DarkCyan{
			get { return ACColor.FromHexString ("008B8B");}
		}

		/// <summary>
		/// Gets the teal.
		/// </summary>
		/// <value>The teal.</value>
		public static ACColor Teal{
			get { return ACColor.FromHexString ("008080");}
		}

		/// <summary>
		/// Gets the dark green.
		/// </summary>
		/// <value>The dark green.</value>
		public static ACColor DarkGreen{
			get { return ACColor.FromHexString ("006400");}
		}

		/// <summary>
		/// Gets the medium blue.
		/// </summary>
		/// <value>The medium blue.</value>
		public static ACColor MediumBlue{
			get { return ACColor.FromHexString ("0000CD");}
		}

		/// <summary>
		/// Gets the dark blue.
		/// </summary>
		/// <value>The dark blue.</value>
		public static ACColor DarkBlue{
			get { return ACColor.FromHexString ("00008B");}
		}

		/// <summary>
		/// Gets the navy.
		/// </summary>
		/// <value>The navy.</value>
		public static ACColor Navy{
			get { return ACColor.FromHexString ("000080");}
		}

		/// <summary>
		/// Gets the transparent.
		/// </summary>
		/// <value>The transparent.</value>
		public static ACColor Transparent{
			get { return Color.Transparent;}
		}
		#endregion 

		#region Static Public Methods
		/// <summary>
		/// Return a color from alpha, red, green, blue components. These component values should be [0..255].
		/// </summary>
		/// <param name="alpha">Alpha.</param>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		public static ACColor Argb(int alpha, int red, int green, int blue) {
			return Color.Argb (alpha, red, green, blue);
		}

		/// <summary>
		/// Gets the Alpha component from an integer color.
		/// </summary>
		/// <returns>The alpha component.</returns>
		/// <param name="color">Color.</param>
		public static int GetAlphaComponent(int color){
			return Color.GetAlphaComponent (color);
		}

		/// <summary>
		/// Gets the Blue component from an integer color.
		/// </summary>
		/// <returns>The blue component.</returns>
		/// <param name="color">Color.</param>
		public static int GetBlueComponent(int color){
			return Color.GetBlueComponent (color);
		}

		/// <summary>
		/// Gets the green component from an integer color
		/// </summary>
		/// <returns>The green component.</returns>
		/// <param name="color">Color.</param>
		public static int GetGreenComponent(int color) {
			return Color.GetGreenComponent (color);
		}

		/// <summary>
		/// Gets the red component from an integer color
		/// </summary>
		/// <returns>The red component.</returns>
		/// <param name="color">Color.</param>
		public static int GetRedComponent(int color) {
			return Color.GetRedComponent (color);
		}

		/// <summary>
		/// Return a color from red, green, blue components. These component values should be [0..255].
		/// </summary>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		public static ACColor Rgb(int red, int green, int blue){
			return Color.Rgb (red, blue, green);
		}

		/// <summary>
		/// Returns a gray color for the value given where 255 equals Pure White and 0 equals Pure Black
		/// </summary>
		/// <returns>The scale.</returns>
		/// <param name="value">Value.</param>
		public static ACColor GrayScale(int value) {
			return Color.Rgb (value, value, value);
		}

		/// <summary>
		/// Converts a CSS/Web Color string specification in the form #RGB, #RRGGBB or #AARRGGBB to a <c>ACColor</c> 
		/// </summary>
		/// <returns>The hex string.</returns>
		/// <param name="hexValue">Hex value.</param>
		public static ACColor FromHexString (string hexValue)
		{
			var colorString = hexValue.Replace ("#", "");
			float red, green, blue, alpha;

			// Convert color based on length
			switch (colorString.Length) {
			case 3 : // #RGB
				red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
				green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
				blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
				return ACColor.FromRGB(red, green, blue);
			case 6 : // #RRGGBB
				red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
				green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
				blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
				return ACColor.FromRGB(red, green, blue);
			case 8 : // #AARRGGBB
				alpha = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
				red = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
				green = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
				blue = Convert.ToInt32(colorString.Substring(6, 2), 16) / 255f;
				return ACColor.FromRGBA(red, green, blue, alpha);
			default :
				throw new ArgumentOutOfRangeException(string.Format("Invalid color value '{0}'. It should be a hex value of the form #RBG, #RRGGBB or #AARRGGBB", hexValue));
			}

		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		public static ACColor FromRGB(byte red, byte green, byte blue) {
			return Color.Rgb (red, green, blue);
		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		public static ACColor FromRGB(int red, int green, int blue) {
			return Color.Rgb (red, green, blue);
		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		public static ACColor FromRGB(float red, float green, float blue) {
			return Color.Rgb ((int)(255f*red),(int)(255f*green),(int)(255f*blue));
		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values with the given alpha value
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <param name="alpha">Alpha.</param>
		public static ACColor FromRGBA(byte red, byte green, byte blue, byte alpha) {
			return Color.Argb (alpha, red, green, blue);
		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values with the given alpha value
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <param name="alpha">Alpha.</param>
		public static ACColor FromRGBA(int red, int green, int blue, int alpha) {
			return Color.Argb (alpha, red, green, blue);
		}

		/// <summary>
		/// Creates a new color from the given red, green and blue values with the given alpha value
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <param name="alpha">Alpha.</param>
		public static ACColor FromRGBA(float red, float green, float blue, float alpha) {
			return Color.Argb ((int)(255f*alpha),(int)(255f*red),(int)(255f*green),(int)(255f*blue));
		}

		/// <summary>
		/// Creates a color from using the hue, saturation and brightness components.
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="hue">Hue.</param>
		/// <param name="saturation">Saturation.</param>
		/// <param name="brightness">Brightness.</param>
		public static ACColor FromHSB(float hue, float saturation, float brightness) {
			float r = 0, g = 0, b = 0;
			float i;
			float f, p, q, t;
					
			// If no saturation calculate color and return
			if (saturation == 0) {
				return ACColor.FromRGB(brightness,brightness,brightness);
			}

			// Compute color angles
			hue = hue / 60;
			i = (float)Math.Floor (hue);
			f = hue - i;
			p = brightness *  (1 - saturation);
			q = brightness * (1 - saturation * f);
			t = brightness * (1 - saturation * (1 - f));


			// Take action based on the i value
			switch ((int)i) {
			case 0:
				r = brightness;
				g = t;
				b = p;
				break;
			case 1:
				r = q;
				g = brightness;
				b = p;
				break;
			case 2:
				r = p;
				g = brightness;
				b = t;
				break;
			case 3:
				r = p;
				g = q;
				b = brightness;
				break;
			case 4:
				r = t;
				g = p;
				b = brightness;
				break;
			case 5:
				r = brightness;
				g = p;
				b = q;
				break;
			}

			//Return computed color value
			return ACColor.FromRGB (r, g, b);
		}

		/// <summary>
		/// Creates a color from using the hue, saturation and brightness components with the given alpha value
		/// </summary>
		/// <returns>The new color.</returns>
		/// <param name="hue">Hue.</param>
		/// <param name="saturation">Saturation.</param>
		/// <param name="brightness">Brightness.</param>
		/// <param name="alpha">Alpha.</param>
		public static ACColor FromHSBA(float hue, float saturation, float brightness, float alpha) {
			Color c = ACColor.FromHSB (hue, saturation, brightness);
			c.A = (byte)(255f * alpha);
			return c;
		}

		/// <summary>
		/// Creates a grayscale color, based on the current colorspace.
		/// </summary>
		/// <returns>The new grayscale color.</returns>
		/// <param name="grayscale">Grayscale.</param>
		/// <param name="alpha">Alpha.</param>
		public static ACColor FromWhiteAlpha(float grayscale, float alpha){
			return ACColor.FromRGBA (grayscale, grayscale, grayscale, alpha);
		}
		#endregion

		#region Private Variables
		private Color _color = Color.White;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets alpha value for the color
		/// </summary>
		/// <value>A.</value>
		public byte A {
			get { return _color.A;}
			set { _color.A = value;}
		}

		/// <summary>
		/// Gets or sets the red value for the color
		/// </summary>
		/// <value>The r.</value>
		public byte R {
			get { return _color.R;}
			set { _color.R = value;}
		}

		/// <summary>
		/// Gets or sets the green value for the color
		/// </summary>
		/// <value>The g.</value>
		public byte G {
			get { return _color.G;}
			set { _color.G = value;}
		}

		/// <summary>
		/// Gets or sets the blue value for the color
		/// </summary>
		/// <value>The b.</value>
		public byte B {
			get { return _color.B;}
			set { _color.B = value;}
		}
		#endregion 

		#region Conversion Operators
		/// <summary>
		/// Converts a <c>ACColor</c> to an Android <c>Color</c>
		/// </summary>
		/// <param name="color">Color.</param>
		public static implicit operator Color(ACColor color)
		{
			return color.ToColor ();
		}

		/// <summary>
		/// Converts an Android <c>Color</c> to a <c>ACColor</c> 
		/// </summary>
		/// <param name="color">Color.</param>
		public static implicit operator ACColor(Color color)
		{
			return new ACColor (color.A, color.R, color.G, color.B);
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class with the color value
		/// set to pure white.
		/// </summary>
		public ACColor() {

		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="hexValue">Hex value.</param>
		public ACColor(string hexValue) {
			_color = ACColor.FromHexString (hexValue);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <remarks>Android <c>Color</c> style constructor</remarks>
		public ACColor(byte red, byte green, byte blue) {
			//Set default color
			_color = Color.Rgb(red, green, blue);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <remarks>Android <c>Color</c> style constructor</remarks>
		public ACColor(int red, int green, int blue) {
			//Set default color
			_color = Color.Rgb(red, green, blue);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="alpha">Alpha.</param>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <remarks>Android <c>Color</c> style constructor</remarks>
		public ACColor(byte alpha, byte red, byte green, byte blue) {
			//Set default color
			_color = Color.Argb(alpha, red, green, blue);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="alpha">Alpha.</param>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <remarks>Android <c>Color</c> style constructor</remarks>
		public ACColor(int alpha, int red, int green, int blue) {
			//Set default color
			_color = Color.Argb(alpha, red, green, blue);
		}

		/// <summary>
		/// Initializes a new instance of the <c>ACColor</c> class.
		/// </summary>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <param name="alpha">Alpha.</param>
		/// <remarks>iOS <c>UIColor</c> style constructor</remarks>
		public ACColor(float red, float green, float blue, float alpha) {
			//Set default color
			_color = Color.Argb((int)(255f * alpha), (int)(255f * red), (int)(255f * green), (int)(255f * blue));
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Converts the <c>ACColor</c> to a default <c>Android</c> color and returns it
		/// </summary>
		/// <returns>The color.</returns>
		public Color ToColor(){
			return _color;
		}

		/// <summary>
		/// Gets the brightness.
		/// </summary>
		/// <returns>The brightness.</returns>
		public float GetBrightness(){
			return _color.GetBrightness ();
		}

		/// <summary>
		/// Gets the hue.
		/// </summary>
		/// <returns>The hue.</returns>
		public float GetHue(){
			return _color.GetHue ();
		}

		/// <summary>
		/// Gets the saturation.
		/// </summary>
		/// <returns>The saturation.</returns>
		public float GetSaturation(){
			return _color.GetSaturation ();
		}

		/// <summary>
		/// Tos the ARGB.
		/// </summary>
		/// <returns>The ARGB.</returns>
		public int ToArgb(){
			return _color.ToArgb ();
		}

		/// <summary>
		/// Converts this <c>ACColor</c> to a CSS/Web style hex color specification in
		/// the form #RRGGBB or ##AARRGGBB
		/// </summary>
		/// <returns>The hex string.</returns>
		/// <param name="withAlpha">If set to <c>true</c> with alpha.</param>
		public string ToHexString(bool withAlpha) {
			//With the alpha value?
			if (withAlpha) {
				return String.Format ("#{0:X2}{1:X2}{2:X2}{2:X2}", _color.A, _color.R, _color.G, _color.B);
			} else {
				return String.Format ("#{0:X2}{1:X2}{2:X2}", _color.R, _color.G, _color.B);
			}
		}

		/// <summary>
		/// Creates a new color with the specified alpha channel from a reference color.
		/// </summary>
		/// <returns>The with alpha.</returns>
		/// <param name="alpha">Alpha.</param>
		public ACColor ColorWithAlpha(float alpha){
			//Adjust the alpha of the current color
			_color.A = (byte)(255f * alpha);

			//Return adjust color
			return _color;
		}
		/// <summary>
		/// Returns the hue, saturation, brightness and alpha components of the color.
		/// </summary>
		/// <param name="hue">Hue.</param>
		/// <param name="saturation">Saturation.</param>
		/// <param name="brightness">Brightness.</param>
		/// <param name="alpha">Alpha.</param>
		public void GetHSBA(ref float hue, ref float saturation, ref float brightness, ref float alpha) {
			//Grab requested values
			hue = _color.GetHue ();
			saturation = _color.GetSaturation ();
			brightness = _color.GetBrightness ();
			alpha = (float)(_color.A) / 255f;
		}

		/// <summary>
		/// Returns the red, green, blue and alpha components of this color.
		/// </summary>
		/// <param name="red">Red.</param>
		/// <param name="green">Green.</param>
		/// <param name="blue">Blue.</param>
		/// <param name="alpha">Alpha.</param>
		public void GetRGBA(ref float red, ref float green, ref float blue, ref float alpha){
			//Grab requested values
			red = (float)(_color.R) / 255f;
			green = (float)(_color.G) / 255f;
			blue = (float)(_color.B) / 255f;
			alpha = (float)(_color.A) / 255f;
		}

		/// <summary>
		/// Sets the fill color in the current paint for the current canvas specified by the global <c>UIGraphics</c>
		/// class.
		/// </summary>
		/// <remarks>This methods is used to ease the porting of graphics code from iOS to Android.</remarks>
		public void SetFill() {
			// Is a canvas currently open?
			if (UIGraphics.CurrentContext !=null) {
				UIGraphics.CurrentContext.CurrentPaint.Color = this;
				UIGraphics.CurrentContext.CurrentPaint.SetStyle(Paint.Style.Fill);
			}
		}

		/// <summary>
		/// Sets the stroke color in the current paint for the current canvas specified by the global <c>UIGraphics</c>
		/// class.
		/// </summary>
		/// <remarks>This methods is used to ease the porting of graphics code from iOS to Android.</remarks>
		public void SetStroke()
		{
			// Is a canvas currently open?
			if (UIGraphics.CurrentContext != null)
			{
				UIGraphics.CurrentContext.CurrentPaint.Color = this;
				UIGraphics.CurrentContext.CurrentPaint.SetStyle(Paint.Style.Stroke);
			}
		}
		#endregion 
	}
}

