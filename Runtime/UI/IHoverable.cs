using System;

namespace Rune.UI
{
    /// <summary>
    /// 마우스 커서가 호버를 할 수 있는 UI 요소의 인터페이스.
    /// </summary>
    public interface IHoverable
    {
        /// <summary>
        /// 마우스 커서가 진입합니다.
        /// </summary>
        void StartHover();

        /// <summary>
        /// 마우스 커서가 벗어납니다.
        /// </summary>
        void EndHover();

        /// <summary>
        /// 현재 호버 상태인지 여부.
        /// </summary>
        bool IsHovered { get; }

        Action OnStartHover { get; set; }
        Action OnEndHover { get; set; }
    }
}