using System.Collections;

namespace Rune.UI
{
    /// <summary>
    /// 열고 닫을 수 있는 UI 컴포넌트 인터페이스.
    /// </summary>
    public interface IOpenable
    {
        /// <summary>
        /// UI를 엽니다 (fire-and-forget).
        /// </summary>
        void Open();

        /// <summary>
        /// UI를 닫습니다 (fire-and-forget).
        /// </summary>
        void Close();

        /// <summary>
        /// UI를 열고 완료될 때까지 대기할 수 있는 코루틴.
        /// </summary>
        IEnumerator OpenAndWait();

        /// <summary>
        /// UI를 닫고 완료될 때까지 대기할 수 있는 코루틴.
        /// </summary>
        IEnumerator CloseAndWait();
    }
}
