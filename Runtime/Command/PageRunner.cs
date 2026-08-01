using System;
using System.Collections;
using System.Collections.Generic;
using Rune.Core;
using Rune.Data;
using UnityEngine;

namespace Rune.Command
{
    /// <summary>
    /// CommandPage 리스트를 보유하고, 조건에 따라 실행하는 공용 클래스.
    /// 페이지 실행이 끝나면 조건을 재평가하여 다음 페이지를 실행합니다.
    /// </summary>
    [Serializable]
    public class PageRunner
    {
        [SerializeField] private List<CommandPage> _pages;

        private SafeCoroutine _routine;
        private HashSet<int> _executedOnce = new();

        /// <summary>
        /// 조건을 평가하여 페이지를 실행합니다.
        /// 페이지가 끝나면 다시 조건을 평가하여 실행 가능한 페이지가 있으면 이어서 실행합니다.
        /// </summary>
        public void Execute(MonoBehaviour owner, FlagStore globalFlags, FlagStore stageFlags)
        {
            _routine.Run(owner, RunPages(globalFlags, stageFlags));
        }

        /// <summary>
        /// 현재 실행 중인 페이지를 중단합니다.
        /// </summary>
        public void Stop(MonoBehaviour owner)
        {
            _routine.Stop(owner);
        }

        /// <summary>
        /// 뒤에서부터 검사하여 첫 번째로 조건을 충족하는 페이지를 반환합니다.
        /// </summary>
        public CommandPage GetActivePage(FlagStore globalFlags, FlagStore stageFlags)
        {
            if (_pages == null || _pages.Count == 0) return null;

            for (int i = _pages.Count - 1; i >= 0; i--)
            {
                if (_pages[i] == null) continue;
                if (_pages[i].Once && _executedOnce.Contains(i)) continue;
                if (!_pages[i].CanExecute(globalFlags, stageFlags)) continue;

                return _pages[i];
            }

            return null;
        }

        private IEnumerator RunPages(FlagStore globalFlags, FlagStore stageFlags)
        {
            while (true)
            {
                int pageIndex = GetActivePageIndex(globalFlags, stageFlags);
                if (pageIndex < 0) yield break;

                var page = _pages[pageIndex];
                yield return page.Execute();

                if (page.Once)
                {
                    _executedOnce.Add(pageIndex);
                }

                yield return null; // 안전장치: 최소 한 프레임 대기
            }
        }

        private int GetActivePageIndex(FlagStore globalFlags, FlagStore stageFlags)
        {
            if (_pages == null || _pages.Count == 0) return -1;

            for (int i = _pages.Count - 1; i >= 0; i--)
            {
                if (_pages[i] == null) continue;
                if (_pages[i].Once && _executedOnce.Contains(i)) continue;
                if (!_pages[i].CanExecute(globalFlags, stageFlags)) continue;

                return i;
            }

            return -1;
        }
    }
}
