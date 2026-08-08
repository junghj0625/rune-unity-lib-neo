using System;
using System.Collections.Generic;
using SOD.Input;

namespace Rune.Input
{
    /// <summary>
    /// 입력 소비자를 스택으로 관리하는 클래스.
    /// 이벤트 기반으로 활성 소비자에게 입력을 전달합니다.
    /// </summary>
    public class InputStack
    {
        private readonly Stack<IInputConsumer> _stack = new();

        /// <summary>
        /// 현재 맨 위의 소비자를 반환합니다. 없으면 null.
        /// </summary>
        public IInputConsumer Current => _stack.Count > 0 ? _stack.Peek() : null;

        /// <summary>
        /// 스택에 소비자가 있는지 여부.
        /// </summary>
        public bool HasConsumer => _stack.Count > 0;

        #region Public Services

        /// <summary>
        /// 소비자를 스택에 Push합니다.
        /// </summary>
        public void Push(IInputConsumer consumer)
        {
            _stack.Push(consumer);
        }

        /// <summary>
        /// 소비자를 스택에서 제거합니다.
        /// </summary>
        public void Remove(IInputConsumer consumer)
        {
            if (_stack.Count == 0) return;

            if (_stack.Peek() == consumer)
            {
                _stack.Pop();

                return;
            }

            var temp = new Stack<IInputConsumer>();

            while (_stack.Count > 0)
            {
                var item = _stack.Pop();

                if (item != consumer)
                {
                    temp.Push(item);
                }
            }

            while (temp.Count > 0)
            {
                _stack.Push(temp.Pop());
            }
        }

        /// <summary>
        /// 우선도가 가장 높은 소비자에게 액션을 전달합니다.
        /// </summary>
        public void Send(Action<IInputConsumer> action)
        {
            foreach (var consumer in _stack)
            {
                action(consumer);
                if (consumer.BlocksBelow) break;
            }
        }

        #endregion
    }
}
