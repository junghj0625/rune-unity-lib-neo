using System.Collections;
using System.Collections.Generic;
using Rune.Data;
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
        [SerializeField] private List<FlagCondition> _conditions = new();
        [SerializeField] private bool _once;
        [SubclassSelector]
        [SerializeReference] private List<GameCommand> _commands = new();

        /// <summary>
        /// 조건 리스트. 비어있으면 항상 실행 가능.
        /// </summary>
        public IReadOnlyList<FlagCondition> Conditions => _conditions;

        /// <summary>
        /// true이면 한 번 실행된 후 비활성화됩니다.
        /// </summary>
        public bool Once => _once;

        /// <summary>
        /// 조건을 모두 충족하는지 검사합니다.
        /// </summary>
        public bool CanExecute(FlagStore globalFlags, FlagStore stageFlags)
        {
            if (_conditions == null || _conditions.Count == 0) return true;

            foreach (var condition in _conditions)
            {
                var store = condition.Source == FlagSource.Global ? globalFlags : stageFlags;
                if (store == null) return false;

                if (!EvaluateCondition(condition, store)) return false;
            }

            return true;
        }

        /// <summary>
        /// 등록된 모든 커맨드를 순서대로 실행합니다.
        /// </summary>
        public IEnumerator Execute()
        {
            if (_commands == null) yield break;

            foreach (var command in _commands)
            {
                if (command != null)
                {
                    yield return command.Execute();
                }
            }
        }

        /// <summary>
        /// 등록된 커맨드 수를 반환합니다.
        /// </summary>
        public int Count => _commands?.Count ?? 0;

        private bool EvaluateCondition(FlagCondition condition, FlagStore store)
        {
            if (condition.Type == FlagType.String)
            {
                bool exists = store.Has(condition.Key);
                return condition.StringCheckType == StringCheck.Exists ? exists : !exists;
            }
            else
            {
                int value = store.GetInt(condition.Key);
                return value >= condition.IntValue;
            }
        }
    }
}
