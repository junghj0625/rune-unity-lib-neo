using System;
using System.Collections.Generic;

namespace Rune.UI
{
    /// <summary>
    /// 콘솔의 순수 로직 백엔드.
    /// 커맨드 등록/실행, 로그 축적, 프롬프트 관리를 담당합니다.
    /// UI와 분리되어 독립적으로 테스트/재사용 가능합니다.
    /// </summary>
    public class ConsoleBackend
    {
        private readonly Dictionary<string, Action<string[]>> _commands = new();

        private string _log = "";
        
        private ConsolePrompt _pendingPrompt;

        /// <summary>
        /// 로그가 추가될 때 발생합니다. 추가된 메시지를 전달합니다.
        /// </summary>
        public Action<string> OnLogAppended { get; set; }

        /// <summary>
        /// 현재까지 축적된 전체 로그.
        /// </summary>
        public string Log => _log;

        /// <summary>
        /// 현재 대기 중인 프롬프트가 있는지 여부.
        /// </summary>
        public bool HasPendingPrompt => _pendingPrompt != null;

        #region Public Services

        /// <summary>
        /// 커맨드를 등록합니다.
        /// </summary>
        public void Register(string command, Action<string[]> handler)
        {
            _commands[command] = handler;
        }

        /// <summary>
        /// 커맨드를 해제합니다.
        /// </summary>
        public void Unregister(string command)
        {
            _commands.Remove(command);
        }

        /// <summary>
        /// 질문을 표시하고 유저 응답을 대기합니다.
        /// </summary>
        public ConsolePrompt Ask(string question)
        {
            var prompt = new ConsolePrompt(question);
            _pendingPrompt = prompt;
            AppendLog(question);
            return prompt;
        }

        /// <summary>
        /// 유저 입력을 처리합니다.
        /// 프롬프트가 대기 중이면 응답으로, 아니면 커맨드로 실행합니다.
        /// </summary>
        public void Submit(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            AppendLog($"> {text}");

            if (_pendingPrompt != null)
            {
                _pendingPrompt.Respond(text);
                _pendingPrompt = null;
            }
            else
            {
                ExecuteCommand(text);
            }
        }

        /// <summary>
        /// 로그를 추가합니다.
        /// </summary>
        public void AppendLog(string message)
        {
            if (_log.Length > 0) _log += "\n";
            _log += message;
            OnLogAppended?.Invoke(message);
        }

        /// <summary>
        /// 로그를 초기화합니다.
        /// </summary>
        public void ClearLog()
        {
            _log = "";
        }

        #endregion

        #region Private

        private void ExecuteCommand(string input)
        {
            var parts = input.Trim().Split(' ');
            var command = parts[0].ToLower();
            var args = parts.Length > 1 ? parts[1..] : Array.Empty<string>();

            if (_commands.TryGetValue(command, out var handler))
            {
                handler(args);
            }
            else
            {
                AppendLog($"Unknown command: {command}");
            }
        }
        
        #endregion
    }
}
