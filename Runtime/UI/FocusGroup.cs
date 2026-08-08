using System.Collections.Generic;
using UnityEngine;

namespace Rune.UI
{
    /// <summary>
    /// 1차원 IFocusable 리스트의 포커스를 관리하는 컨트롤러.
    /// 아이템은 Add/Remove로 동적으로 등록합니다.
    /// </summary>
    public class FocusGroup
    {
        private List<IFocusable> _items = new();

        private int _currentIndex = -1;

        private bool _focusOnHover = true;

        /// <summary>
        /// 현재 포커스된 아이템.
        /// </summary>
        public IFocusable Current => _currentIndex >= 0 ? _items[_currentIndex] : null;

        /// <summary>
        /// 현재 포커스 인덱스.
        /// </summary>
        public int CurrentIndex => _currentIndex;

        /// <summary>
        /// 등록된 아이템 수.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// 아이템을 추가합니다.
        /// </summary>
        public void Add(IFocusable item)
        {
            _items.Add(item);

            if (_focusOnHover && item is IHoverable hoverable)
            {
                hoverable.OnStartHover = () => Focus(item);
            }
        }

        /// <summary>
        /// 아이템을 제거합니다.
        /// </summary>
        public void Remove(IFocusable item)
        {
            int index = _items.IndexOf(item);
            if (index < 0) return;

            _items.RemoveAt(index);

            if (_items.Count == 0)
            {
                _currentIndex = -1;
            }
            else if (index <= _currentIndex)
            {
                _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, _items.Count - 1);
            }
        }

        /// <summary>
        /// 모든 아이템을 제거합니다.
        /// </summary>
        public void Clear()
        {
            if (_currentIndex >= 0)
            {
                _items[_currentIndex].Unfocus();
            }

            _items.Clear();
            _currentIndex = -1;
        }

        /// <summary>
        /// 다음 아이템으로 포커스를 이동합니다.
        /// </summary>
        public void Next()
        {
            if (_items.Count == 0) return;
            Focus((_currentIndex + 1) % _items.Count);
        }

        /// <summary>
        /// 이전 아이템으로 포커스를 이동합니다.
        /// </summary>
        public void Previous()
        {
            if (_items.Count == 0) return;
            Focus((_currentIndex - 1 + _items.Count) % _items.Count);
        }

        /// <summary>
        /// 인덱스로 포커스를 설정합니다.
        /// </summary>
        public void Focus(int index)
        {
            if (_items.Count == 0) return;

            Unfocus();

            _currentIndex = Mathf.Clamp(index, 0, _items.Count - 1);

            _items[_currentIndex].Focus();
        }

        /// <summary>
        /// 아이템 객체로 포커스를 설정합니다.
        /// </summary>
        public void Focus(IFocusable item)
        {
            int index = _items.IndexOf(item);
            if (index >= 0) Focus(index);
        }

        /// <summary>
        /// 포커스를 해제합니다.
        /// </summary>
        public void Unfocus()
        {
            if (_items.Count == 0) return;

            if (_currentIndex >= 0)
            {
                _items[_currentIndex].Unfocus();
            }
        }

        /// <summary>
        /// 현재 포커스된 아이템이 IConfirmable이면 Confirm을 실행합니다.
        /// </summary>
        public void Confirm()
        {
            if (Current is IConfirmable confirmable)
            {
                confirmable.Confirm();
            }
        }
    }
}
