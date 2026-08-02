namespace Rune.Util
{
    /// <summary>
    /// Color 확장 메서드.
    /// </summary>
    public static class Color
    {
        /// <summary>
        /// RGB는 유지하고 alpha만 변경한 색을 반환합니다.
        /// </summary>
        public static UnityEngine.Color WithAlpha(this UnityEngine.Color color, float alpha)
        {
            return new UnityEngine.Color(color.r, color.g, color.b, alpha);
        }
    }
}
