namespace Rune.Input
{
    /// <summary>
    /// 입력을 소비할 수 있는 대상의 인터페이스.
    /// InputStack의 맨 위에 있을 때만 입력을 받습니다.
    /// </summary>
    public interface IInputConsumer
    {
        /// <summary>
        /// 이 소비자가 입력을 받을 수 있는 상태일 때 매 프레임 호출됩니다.
        /// </summary>
        void ProcessInput();

        /// <summary>
        /// true이면 이 소비자 아래의 소비자는 입력을 받지 못합니다.
        /// </summary>
        bool BlocksBelow { get; }
    }
}
