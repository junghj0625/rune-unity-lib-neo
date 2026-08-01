using System.Collections;
using UnityEngine;

namespace Rune.Core
{
    /// <summary>
    /// 코루틴을 안전하게 관리하는 구조체.
    /// 새 코루틴 실행 시 이전 코루틴을 자동으로 취소합니다.
    /// </summary>
    public struct SafeCoroutine
    {
        private Coroutine _handle;

        /// <summary>
        /// 이전 코루틴을 취소하고 새 코루틴을 시작합니다.
        /// </summary>
        public void Run(MonoBehaviour owner, IEnumerator routine)
        {
            if (_handle != null)
            {
                owner.StopCoroutine(_handle);
            }

            _handle = owner.StartCoroutine(routine);
        }

        /// <summary>
        /// 현재 실행 중인 코루틴을 취소합니다.
        /// </summary>
        public void Stop(MonoBehaviour owner)
        {
            if (_handle != null)
            {
                owner.StopCoroutine(_handle);
                _handle = null;
            }
        }

        /// <summary>
        /// 코루틴이 실행 중인지 여부.
        /// </summary>
        public bool IsRunning => _handle != null;
    }
}
