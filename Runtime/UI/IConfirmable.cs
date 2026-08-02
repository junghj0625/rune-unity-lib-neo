namespace Rune.UI
{
    /// <summary>
    /// 확인(결정) 액션을 받을 수 있는 UI 요소의 인터페이스.
    /// </summary>
    public interface IConfirmable
    {
        /// <summary>
        /// 확인(결정)을 실행합니다.
        /// </summary>
        void Confirm();
    }
}
