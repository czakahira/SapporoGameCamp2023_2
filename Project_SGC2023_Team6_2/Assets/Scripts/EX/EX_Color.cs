using UnityEngine;

namespace UnityEx
{
	/// <summary>
	/// カラー
	/// </summary>
	public static class ColorEX
	{
		#region Field

		#region colors 
		public static readonly Color crimson      = Code2Color("#dc143c");
		public static readonly Color red          = Code2Color("#ff0000");
		public static readonly Color orange       = Code2Color("#ffa500");
		public static readonly Color yellow       = Code2Color("#ffff00");
		public static readonly Color green        = Code2Color("#008000");
		public static readonly Color green_lime   = Code2Color("#00ff00");
		public static readonly Color green_spring = Code2Color("#00ff7f");
		public static readonly Color blue         = Code2Color("#0000ff");
		public static readonly Color blue_sky     = Code2Color("#0066FF");
		public static readonly Color blue_aqua    = Code2Color("#00ffff");
		public static readonly Color violet_blue  = Code2Color("#8a2be2");
		public static readonly Color violet_dark  = Code2Color("#9400d3");
		public static readonly Color magenta      = Code2Color("#ff00ff");
		public static readonly Color pink_hot     = Code2Color("#ff69b4");
		public static readonly Color black        = Code2Color("#000000");
		public static readonly Color gray         = Code2Color("#808080");
		public static readonly Color white        = Code2Color("#ffffff");
		public static readonly Color gold         = Code2Color("#ffd700");
		public static readonly Color silver       = Code2Color("#c0c0c0");
		#endregion

		public static readonly float MaxValue_HSV_H = 360.0f;
		#endregion

		#region Method
		public static Color Code2Color( string _code )
		{
			Color def = Color.white;
			if (!ColorUtility.TryParseHtmlString(_code, out def)) {
				DebugEX.LogErrorFormat("Color: {0}  is Not Find,", _code);
			}
			return def;
		}
		public static string Color2Code(Color _color)
		{
			return _color.ToCode();
		}
		public static string ToCode(this Color _color)
		{
			return $"#{ColorUtility.ToHtmlStringRGBA(_color)}";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="_h"></param>
		/// <returns></returns>
		public static Color HSVToRGB( float _h )
		{
			return Color.HSVToRGB(_h, 100/100, 100/100);
		}

		public static string ColorFormat(this string self, string _colorCode)
		{
			return $"<color={_colorCode}>{self}</color>";
		}

		#endregion
	}
}
