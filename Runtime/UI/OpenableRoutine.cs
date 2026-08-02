using System.Collections;
using Rune.Core;
using UnityEngine;

namespace Rune.UI
{
    /// <summary>
    /// IOpenable 구현을 위한 컴포지션 헬퍼.
    /// SetActive + CoroutineRunner + SafeCoroutine 패턴을 캡슐화합니다.
    /// </summary>
    public struct OpenableRoutine
    {
        private SafeCoroutine _coroutine;

        /// <summary>
        /// 오브젝트를 활성화하고 열기 코루틴을 실행합니다 (fire-and-forget).
        /// </summary>
        public void Open(GameObject go, IEnumerator routine)
        {
            go.SetActive(true);
            _coroutine.Run(CoroutineRunner.Instance, routine);
        }

        /// <summary>
        /// 닫기 코루틴을 실행하고 끝나면 오브젝트를 비활성화합니다 (fire-and-forget).
        /// </summary>
        public void Close(GameObject go, IEnumerator routine)
        {
            _coroutine.Run(CoroutineRunner.Instance, CloseWrapper(go, routine));
        }

        /// <summary>
        /// 오브젝트를 활성화하고 열기 코루틴을 반환합니다 (yield 가능).
        /// </summary>
        public IEnumerator OpenAndWait(GameObject go, IEnumerator routine)
        {
            go.SetActive(true);
            return routine;
        }

        /// <summary>
        /// 닫기 코루틴 + 비활성화를 반환합니다 (yield 가능).
        /// </summary>
        public IEnumerator CloseAndWait(GameObject go, IEnumerator routine)
        {
            return CloseWrapper(go, routine);
        }

        private IEnumerator CloseWrapper(GameObject go, IEnumerator routine)
        {
            yield return routine;
            go.SetActive(false);
        }
    }
}
