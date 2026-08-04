namespace Rune.Input
{
    /// <summary>
    /// 입력을 소비할 수 있는 대상의 인터페이스.
    /// InputStack에서 이벤트 발생 시 활성 소비자에게 전달됩니다.
    /// </summary>
    public interface IInputConsumer
    {
        /// <summary>
        /// true이면 이 소비자 아래의 소비자는 입력을 받지 못합니다.
        /// </summary>
        bool BlocksBelow { get; }

        void OnUp() { }
        void OnDown() { }
        void OnLeft() { }
        void OnRight() { }
        void OnConfirm() { }
        void OnCancel() { }
    }
}
