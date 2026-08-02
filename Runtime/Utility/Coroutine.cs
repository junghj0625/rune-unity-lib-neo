using System.Collections;
using UnityEngine;


namespace Rune.Util
{
    /// <summary>
    /// 코루틴 관련 유틸리티.
    /// </summary>
    public static class Coroutine
    {
        /// <summary>
        /// 여러 코루틴을 동시에 실행하고 전부 끝날 때까지 대기합니다.
        /// </summary>
        public static IEnumerator WaitAll(MonoBehaviour owner, params IEnumerator[] routines)
        {
            int running = routines.Length;

            foreach (var routine in routines)
            {
                owner.StartCoroutine(WrapAndCount(routine, () => running--));
            }

            while (running > 0)
            {
                yield return null;
            }
        }

        private static IEnumerator WrapAndCount(IEnumerator routine, System.Action onComplete)
        {
            yield return routine;
            onComplete();
        }
    }
}
