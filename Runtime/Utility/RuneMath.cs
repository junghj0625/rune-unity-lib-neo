namespace Rune.Util
{
    /// <summary>
    /// 수학 유틸리티.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// SmoothStep 이징. 0~1 범위의 t를 부드럽게 보간합니다.
        /// </summary>
        public static float SmoothStep(float t)
        {
            return t * t * (3f - 2f * t);
        }

        /// <summary>
        /// SmootherStep 이징. SmoothStep보다 더 부드러운 시작/끝.
        /// </summary>
        public static float SmootherStep(float t)
        {
            return t * t * t * (t * (6f * t - 15f) + 10f);
        }
    }
}
