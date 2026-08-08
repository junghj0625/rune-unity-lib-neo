using System;
using Rune.Core;
using Rune.Data;
using UnityEngine.EventSystems;

namespace Rune.UI
{
    /// <summary>
    /// 포커스/호버/컨펌 상태를 관리하는 피드백 베이스 클래스.
    /// 시각적 표현은 파생 클래스에서 구현합니다.
    /// </summary>
    public abstract class UIFeedbackBase : RuneBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private readonly Reactive<bool> _isHovered = new(false);
        public bool IsHovered { get => _isHovered.Value; set => _isHovered.Value = value; }

        private readonly Reactive<bool> _isFocused = new(false);
        public bool IsFocused { get => _isFocused.Value; set => _isFocused.Value = value; }

        public Action OnFocus { get; set; }
        public Action OnUnfocus { get; set; }
        public Action OnEnter { get; set; }
        public Action OnExit { get; set; }
        public Action OnConfirm { get; set; }
        public Action OnStartHover { get; set; }
        public Action OnEndHover { get; set; }

        #region Unity Messages

        protected override void Awake()
        {
            base.Awake();
            _isHovered.OnChanged += OnChangedIsHovered;
        }

        #endregion

        #region Public Services

        public virtual void Focus()
        {
            OnFocus?.Invoke();
        }

        public virtual void Unfocus()
        {
            OnUnfocus?.Invoke();
        }

        public virtual void Confirm()
        {
            OnConfirm?.Invoke();
        }

        public virtual void StartHover()
        {
            OnStartHover?.Invoke();
        }

        public virtual void EndHover()
        {
            OnEndHover?.Invoke();
        }

        #endregion

        #region Pointer Handlers

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHovered = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Confirm();
        }

        #endregion

        #region Private

        private void OnChangedIsHovered(bool value)
        {
            if (value)
            {
                StartHover();
            }
            else
            {
                EndHover();
            }
        }

        #endregion
    }
}
