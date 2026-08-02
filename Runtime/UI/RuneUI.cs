using Rune.Core;
using UnityEngine;

namespace Rune.UI
{
    /// <summary>
    /// UI 컴포넌트의 베이스 클래스.
    /// CanvasGroup 기반 Show/Hide를 제공하며, 시작 시 숨겨진 상태로 초기화됩니다.
    /// </summary>
    public abstract class RuneUI : RuneBehaviour
    {
        [SerializeField] private bool _hideOnAwake = true;

        private CanvasGroup _canvasGroup;
        protected CanvasGroup Canvas => Get(ref _canvasGroup);

        protected override void Awake()
        {
            base.Awake();

            if (_hideOnAwake)
            {
                Hide();
            }
        }

        /// <summary>
        /// UI를 표시합니다.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// UI를 숨깁니다.
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// UI가 현재 활성 상태인지 여부.
        /// </summary>
        public bool IsVisible => gameObject.activeSelf;
    }
}
