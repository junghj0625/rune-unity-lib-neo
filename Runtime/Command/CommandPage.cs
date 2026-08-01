using System.Collections.Generic;
using UnityEngine;

namespace Rune.Command
{
    /// <summary>
    /// GameCommand를 순서대로 담아두는 커맨드 페이지.
    /// ScriptableObject 에셋으로 저장하여 재사용합니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCommandPage", menuName = "Rune/Command Page")]
    public class CommandPage : ScriptableObject
    {
        [SerializeReference] private List<GameCommand> _commands = new();

        /// <summary>
        /// 등록된 모든 커맨드를 순서대로 실행합니다.
        /// </summary>
        public void Execute()
        {
            if (_commands == null) return;

            foreach (var command in _commands)
            {
                command?.Execute();
            }
        }

        /// <summary>
        /// 등록된 커맨드 수를 반환합니다.
        /// </summary>
        public int Count => _commands?.Count ?? 0;
    }
}
