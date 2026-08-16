using UnityEngine;

namespace Rune.UI
{
    /// <summary>
    /// 콘솔에 질문을 던지고 답변을 대기하는 yield 객체.
    /// yield return으로 응답이 올 때까지 대기합니다.
    /// </summary>
    public class ConsolePrompt : CustomYieldInstruction
    {
        /// <summary>
        /// 표시할 질문.
        /// </summary>
        public string Question { get; }

        /// <summary>
        /// 유저의 답변. 응답 전에는 null.
        /// </summary>
        public string Answer { get; private set; }

        /// <summary>
        /// 응답이 왔는지 여부.
        /// </summary>
        public bool IsAnswered { get; private set; }

        /// <summary>
        /// 답변을 공백으로 분할한 결과. 응답 전에는 null.
        /// </summary>
        public string[] Args { get; private set; }

        public override bool keepWaiting => !IsAnswered;

        public ConsolePrompt(string question)
        {
            Question = question;
        }

        /// <summary>
        /// 답변을 설정하고 대기를 종료합니다.
        /// </summary>
        public void Respond(string answer)
        {
            Answer = answer;
            Args = answer.Trim().Split(' ');
            IsAnswered = true;
        }
    }
}
