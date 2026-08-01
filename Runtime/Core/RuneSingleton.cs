using UnityEngine;

namespace Rune.Core
{
    /// <summary>
    /// 싱글톤 패턴을 제공하는 RuneBehaviour 베이스 클래스.
    /// 씬에 하나만 존재할 수 있으며, 중복 시 자신을 파괴합니다.
    /// </summary>
    /// <typeparam name="T">싱글톤으로 사용할 구체 타입</typeparam>
    public abstract class RuneSingleton<T> : RuneBehaviour where T : RuneSingleton<T>
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
        }

        protected override void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
