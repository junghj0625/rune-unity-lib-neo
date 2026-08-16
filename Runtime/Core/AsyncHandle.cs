using System.Collections;
using UnityEngine;

namespace Rune.Core
{
    /// <summary>
    /// 비동기 작업의 완료/취소를 추적하는 핸들.
    /// fire-and-forget 후 폴링 또는 yield로 대기할 수 있습니다.
    /// </summary>
    public class AsyncHandle
    {
        /// <summary>
        /// 작업이 완료(또는 취소)되었는지 여부.
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// 작업이 취소되었는지 여부.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// 작업을 완료 상태로 전환합니다.
        /// </summary>
        public void Complete()
        {
            IsDone = true;
        }

        /// <summary>
        /// 작업을 취소 상태로 전환합니다.
        /// </summary>
        public void Cancel()
        {
            IsDone = true;
            IsCancelled = true;
        }

        /// <summary>
        /// 완료될 때까지 yield로 대기합니다.
        /// </summary>
        public CustomYieldInstruction Wait() => new WaitUntil(() => IsDone);

        public static AsyncHandle Run(MonoBehaviour owner, IEnumerator routine)
        {
            var handle = new AsyncHandle();
            owner.StartCoroutine(Wrap(routine, handle));
            return handle;
        }

        private static IEnumerator Wrap(IEnumerator routine, AsyncHandle handle)
        {
            yield return routine;
            handle.Complete();
        }
    }
}
