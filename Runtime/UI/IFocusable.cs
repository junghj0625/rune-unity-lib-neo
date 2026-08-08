using System;

namespace Rune.UI
{
    /// <summary>
    /// 포커스를 받을 수 있는 UI 요소의 인터페이스.
    /// </summary>
    public interface IFocusable
    {
        /// <summary>
        /// 포커스를 받습니다.
        /// </summary>
        void Focus();

        /// <summary>
        /// 포커스를 잃습니다.
        /// </summary>
        void Unfocus();

        /// <summary>
        /// 현재 포커스 상태인지 여부.
        /// </summary>
        bool IsFocused { get; }

        Action OnFocus { get; set; }
        Action OnUnfocus { get; set; }
    }
}
