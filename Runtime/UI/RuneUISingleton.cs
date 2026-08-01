using UnityEngine;

namespace Rune.UI
{
    /// <summary>
    /// 싱글톤 + UI 기능을 결합한 베이스 클래스.
    /// CanvasGroup 기반 Show/Hide를 제공하며, 시작 시 숨겨진 상태로 초기화됩니다.
    /// </summary>
    public abstract class RuneUISingleton<T> : RuneUI where T : RuneUISingleton<T>
    {
        private static T _instance;

        /// <summary>
        /// 싱글톤 인스턴스에 접근합니다.
        /// </summary>
        public static T Instance => _instance;

        protected override void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = (T)this;
            base.Awake();
        }

        protected override void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }

            base.OnDestroy();
        }
    }
}
