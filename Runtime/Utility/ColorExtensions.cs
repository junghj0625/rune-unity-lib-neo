using UnityEngine;

namespace Rune.Util
{
    /// <summary>
    /// Color 확장 메서드.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// RGB는 유지하고 alpha만 변경한 색을 반환합니다.
        /// </summary>
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
