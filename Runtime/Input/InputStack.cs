using System.Collections.Generic;
using Rune.Core;
using UnityEngine;

namespace Rune.Input
{
    /// <summary>
    /// 입력 소비자를 스택으로 관리하는 싱글톤.
    /// 맨 위의 소비자만 입력을 받습니다.
    /// </summary>
    public class InputStack : RuneSingleton<InputStack>
    {
        private readonly Stack<IInputConsumer> _stack = new();

        /// <summary>
        /// 소비자를 스택에 Push합니다. 이후 이 소비자만 입력을 받습니다.
        /// </summary>
        public static void Push(IInputConsumer consumer)
        {
            Instance._stack.Push(consumer);
        }

        /// <summary>
        /// 소비자를 스택에서 제거합니다.
        /// 맨 위가 아니어도 안전하게 제거합니다.
        /// </summary>
        public static void Remove(IInputConsumer consumer)
        {
            if (Instance._stack.Count == 0) return;

            if (Instance._stack.Peek() == consumer)
            {
                Instance._stack.Pop();
                return;
            }

            // 맨 위가 아닌 경우: 재구성
            var temp = new Stack<IInputConsumer>();
            while (Instance._stack.Count > 0)
            {
                var item = Instance._stack.Pop();
                if (item != consumer)
                {
                    temp.Push(item);
                }
            }
            while (temp.Count > 0)
            {
                Instance._stack.Push(temp.Pop());
            }
        }

        /// <summary>
        /// 현재 맨 위의 소비자를 반환합니다. 없으면 null.
        /// </summary>
        public static IInputConsumer Current =>
            Instance._stack.Count > 0 ? Instance._stack.Peek() : null;

        /// <summary>
        /// 스택에 소비자가 있는지 여부.
        /// </summary>
        public static bool HasConsumer => Instance._stack.Count > 0;

        private void Update()
        {
            foreach (var consumer in _stack)
            {
                consumer.ProcessInput();
                if (consumer.BlocksBelow) break;
            }
        }
    }
}
