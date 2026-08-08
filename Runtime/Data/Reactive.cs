using System;

namespace Rune.Data
{
    /// <summary>
    /// 값 변경 시 이벤트를 발생시키는 제네릭 리액티브 컨테이너.
    /// </summary>
    /// <typeparam name="T">저장할 값의 타입</typeparam>
    [Serializable]
    public class Reactive<T>
    {
        private T _value;

        public Reactive() : this(default) { }

        public Reactive(T initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// 값이 변경되었을 때 새 값을 전달합니다.
        /// </summary>
        public event Action<T> OnChanged;

        /// <summary>
        /// 값이 변경되었을 때 이전 값과 새 값을 전달합니다.
        /// </summary>
        public event Action<T, T> OnChangedFromTo;

        /// <summary>
        /// 현재 값을 가져오거나 설정합니다.
        /// 설정 시 이전 값과 다를 때만 이벤트가 발생합니다.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;

                var old = _value;
                _value = value;
                OnChangedFromTo?.Invoke(old, _value);
                OnChanged?.Invoke(_value);
            }
        }

        /// <summary>
        /// 이벤트를 발생시키지 않고 값을 설정합니다.
        /// 초기화나 역직렬화 등에서 사용합니다.
        /// </summary>
        public void SetValueWithoutNotify(T value)
        {
            _value = value;
        }

        /// <summary>
        /// 구독과 동시에 현재 값을 즉시 콜백으로 전달합니다.
        /// UI 바인딩 등에서 초기 상태 동기화에 유용합니다.
        /// </summary>
        public void Subscribe(Action<T> callback)
        {
            OnChanged += callback;
            callback?.Invoke(_value);
        }

        /// <summary>
        /// Subscribe로 등록한 콜백을 해제합니다.
        /// </summary>
        public void Unsubscribe(Action<T> callback)
        {
            OnChanged -= callback;
        }

        private static bool Equals(T a, T b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return a.Equals(b);
        }

        public override string ToString() => _value?.ToString() ?? "null";

        public static implicit operator T(Reactive<T> reactive) => reactive._value;
    }
}
