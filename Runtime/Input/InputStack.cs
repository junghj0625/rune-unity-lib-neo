using System.Collections.Generic;
using Rune.Core;

namespace Rune.Input
{
    /// <summary>
    /// 입력 소비자를 스택으로 관리하는 싱글톤.
    /// 이벤트 기반으로 활성 소비자에게 입력을 전달합니다.
    /// </summary>
    public class InputStack : RuneSingleton<InputStack>
    {
        private readonly Stack<IInputConsumer> _stack = new();

        /// <summary>
        /// 소비자를 스택에 Push합니다.
        /// </summary>
        public static void Push(IInputConsumer consumer)
        {
            Instance._stack.Push(consumer);
        }

        /// <summary>
        /// 소비자를 스택에서 제거합니다.
        /// </summary>
        public static void Remove(IInputConsumer consumer)
        {
            if (Instance._stack.Count == 0) return;

            if (Instance._stack.Peek() == consumer)
            {
                Instance._stack.Pop();
                return;
            }

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

        #region Send Methods

        public static void SendUp()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnUp();
                if (consumer.BlocksBelow) break;
            }
        }

        public static void SendDown()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnDown();
                if (consumer.BlocksBelow) break;
            }
        }

        public static void SendLeft()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnLeft();
                if (consumer.BlocksBelow) break;
            }
        }

        public static void SendRight()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnRight();
                if (consumer.BlocksBelow) break;
            }
        }

        public static void SendConfirm()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnConfirm();
                if (consumer.BlocksBelow) break;
            }
        }

        public static void SendCancel()
        {
            foreach (var consumer in Instance._stack)
            {
                consumer.OnCancel();
                if (consumer.BlocksBelow) break;
            }
        }

        #endregion
    }
}
