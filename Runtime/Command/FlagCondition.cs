using System;
using UnityEngine;

namespace Rune.Command
{
    public enum FlagSource
    {
        Global,
        Stage,
    }

    public enum FlagType
    {
        String,
        Int,
    }

    public enum StringCheck
    {
        Exists,
        NotExists,
    }

    /// <summary>
    /// 커맨드 페이지 실행 조건을 정의합니다.
    /// </summary>
    [Serializable]
    public class FlagCondition
    {
        [SerializeField] private FlagSource _source;
        [SerializeField] private FlagType _type;
        [SerializeField] private string _key;
        [SerializeField] private StringCheck _stringCheck;
        [SerializeField] private int _intValue;

        public FlagSource Source => _source;
        public FlagType Type => _type;
        public string Key => _key;
        public StringCheck StringCheckType => _stringCheck;
        public int IntValue => _intValue;
    }
}
