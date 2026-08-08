using System;
using Rune.Data;
using UnityEngine;

namespace Rune.Command
{
    public enum FlagType
    {
        String,
        Int,
    }

    public enum StringCheck
    {
        Exists = 0,
        NotExists = 1000,
        Equal = 2000,
        NotEqual = 3000,
    }

    /// <summary>
    /// 커맨드 페이지 실행 조건을 정의합니다.
    /// </summary>
    [Serializable]
    public class FlagCondition
    {
        [SerializeField] private FlagType _type;
        [SerializeField] private string _key;
        [SerializeField] private StringCheck _stringCheck;
        [SerializeField] private string _stringValue;
        [SerializeField] private int _intValue;
        
        public FlagType Type => _type;
        public string Key => _key;
        public StringCheck StringCheckType => _stringCheck;
        public int IntValue => _intValue;

        /// <summary>
        /// 주어진 FlagStore에 대해 이 조건을 평가합니다.
        /// </summary>
        public bool Evaluate(FlagStore store)
        {
            if (store == null) return false;

            if (_type == FlagType.String)
            {
                bool exists = store.Has(_key);
                string value = store.Get(_key);

                switch (_stringCheck)
                {
                    case StringCheck.Exists: return exists;
                    case StringCheck.NotExists: return !exists;
                    case StringCheck.Equal: return value == _stringValue;
                    case StringCheck.NotEqual: return value == _stringValue;
                    default: return true;
                }
            }
            else
            {
                int value = store.GetInt(_key);
                return value >= _intValue;
            }
        }
    }
}
