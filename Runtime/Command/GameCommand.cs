using UnityEngine;

namespace Rune.Command
{
    /// <summary>
    /// 게임 내 단일 액션을 표현하는 커맨드 기본 클래스.
    /// SerializeReference로 인라인 직렬화하여 사용합니다.
    /// </summary>
    [System.Serializable]
    public abstract class GameCommand
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled => _enabled;

        /// <summary>
        /// 커맨드를 실행합니다. 비동기 동작이 필요하면 yield로 대기합니다.
        /// </summary>
        public abstract System.Collections.IEnumerator Execute();
    }
}
