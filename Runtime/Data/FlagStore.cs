using System;
using System.Collections.Generic;

namespace Rune.Data
{
    /// <summary>
    /// string, int 키-값 쌍을 저장하는 범용 플래그 컨테이너.
    /// 글로벌 상태, 스테이지 상태 등에 활용합니다.
    /// </summary>
    [System.Serializable]
    public class FlagStore
    {
        private Dictionary<string, string> _strings = new();
        private Dictionary<string, int> _ints = new();

        /// <summary>
        /// 플래그가 변경되었을 때 발생합니다. 변경된 키를 전달합니다.
        /// </summary>
        public event Action<string> OnChanged;

        /// <summary>
        /// 문자열 플래그를 설정합니다.
        /// </summary>
        public void Set(string key, string value)
        {
            _strings[key] = value;
            OnChanged?.Invoke(key);
        }

        /// <summary>
        /// 문자열 플래그를 가져옵니다. 없으면 기본값 반환.
        /// </summary>
        public string Get(string key, string defaultValue = "")
        {
            return _strings.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 정수 플래그를 설정합니다.
        /// </summary>
        public void SetInt(string key, int value)
        {
            _ints[key] = value;
            OnChanged?.Invoke(key);
        }

        /// <summary>
        /// 정수 플래그를 가져옵니다. 없으면 기본값 반환.
        /// </summary>
        public int GetInt(string key, int defaultValue = 0)
        {
            return _ints.TryGetValue(key, out var value) ? value : defaultValue;
        }

        /// <summary>
        /// 문자열 플래그가 존재하는지 확인합니다.
        /// </summary>
        public bool Has(string key)
        {
            return _strings.ContainsKey(key);
        }

        /// <summary>
        /// 정수 플래그가 존재하는지 확인합니다.
        /// </summary>
        public bool HasInt(string key)
        {
            return _ints.ContainsKey(key);
        }

        /// <summary>
        /// 모든 플래그를 초기화합니다.
        /// </summary>
        public void Clear()
        {
            _strings.Clear();
            _ints.Clear();
        }
    }
}
