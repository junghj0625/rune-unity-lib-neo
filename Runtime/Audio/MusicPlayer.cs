using System.Collections;
using System.Collections.Generic;
using Rune.Core;
using UnityEngine;

namespace Rune.Audio
{
    /// <summary>
    /// 음악 스택 관리 컴포넌트.
    /// 나중에 재생한 곡이 이전 곡을 덮고, 정지하면 이전 곡이 복귀합니다.
    /// </summary>
    public class MusicPlayer : RuneBehaviour
    {
        private AudioSource _sourceA;
        private AudioSource SourceA => GetAt(ref _sourceA, "SourceA");

        private AudioSource _sourceB;
        private AudioSource SourceB => GetAt(ref _sourceB, "SourceB");

        private List<MusicEntry> _stack = new();
        private AudioSource _activeSrc;
        private SafeCoroutine _fade;

        protected override void Awake()
        {
            base.Awake();
            _activeSrc = SourceA;
        }

        /// <summary>
        /// 곡을 스택에 추가하여 재생합니다. 재생 중인 곡은 페이드 아웃 후 대기합니다.
        /// </summary>
        public void Play(AudioClip clip, bool loop = true, float fadeDuration = 0.5f)
        {
            PlayInternal(clip, loop, fadeDuration);
        }

        /// <summary>
        /// 지정한 곡을 스택에서 제거합니다. 최상위였다면 다음 곡이 복귀합니다.
        /// </summary>
        public void Stop(AudioClip clip, float fadeDuration = 0.5f)
        {
            StopInternal(clip, fadeDuration);
        }

        /// <summary>
        /// 모든 곡을 정지하고 스택을 비웁니다.
        /// </summary>
        public void StopAll(float fadeDuration = 0.5f)
        {
            StopAllInternal(fadeDuration);
        }

        private void PlayInternal(AudioClip clip, bool loop, float fadeDuration)
        {
            // 이미 스택에 있으면 제거 후 최상위로 이동
            for (int i = _stack.Count - 1; i >= 0; i--)
            {
                if (_stack[i].clip == clip)
                {
                    _stack.RemoveAt(i);
                    break;
                }
            }

            // 현재 재생 중인 곡의 위치를 저장
            if (_stack.Count > 0)
            {
                var current = _stack[_stack.Count - 1];
                current.time = _activeSrc.time;
            }

            _stack.Add(new MusicEntry { clip = clip, loop = loop, time = 0f });
            CrossFadeTo(clip, loop, 0f, fadeDuration);
        }

        private void StopInternal(AudioClip clip, float fadeDuration)
        {
            int index = _stack.FindIndex(e => e.clip == clip);
            if (index < 0) return;

            _stack.RemoveAt(index);

            // 제거된 것이 최상위였으면 다음 곡으로 전환
            if (index == _stack.Count)
            {
                if (_stack.Count > 0)
                {
                    var next = _stack[_stack.Count - 1];
                    CrossFadeTo(next.clip, next.loop, next.time, fadeDuration);
                }
                else
                {
                    FadeOut(fadeDuration);
                }
            }
        }

        private void StopAllInternal(float fadeDuration)
        {
            _stack.Clear();
            FadeOut(fadeDuration);
        }

        private void CrossFadeTo(AudioClip clip, bool loop, float startTime, float fadeDuration)
        {
            var outSrc = _activeSrc;
            var inSrc = (_activeSrc == SourceA) ? SourceB : SourceA;
            _activeSrc = inSrc;

            inSrc.clip = clip;
            inSrc.loop = loop;
            inSrc.time = startTime;
            inSrc.volume = 0f;
            inSrc.Play();

            _fade.Run(this, CrossFadeCoroutine(outSrc, inSrc, fadeDuration));
        }

        private void FadeOut(float fadeDuration)
        {
            var outSrc = _activeSrc;
            _fade.Run(this, FadeOutCoroutine(outSrc, fadeDuration));
        }

        private IEnumerator CrossFadeCoroutine(AudioSource outSrc, AudioSource inSrc, float duration)
        {
            float elapsed = 0f;
            float outStart = outSrc.volume;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / duration;
                outSrc.volume = Mathf.Lerp(outStart, 0f, t);
                inSrc.volume = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            outSrc.volume = 0f;
            outSrc.Stop();
            inSrc.volume = 1f;
        }

        private IEnumerator FadeOutCoroutine(AudioSource source, float duration)
        {
            float elapsed = 0f;
            float startVolume = source.volume;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / duration;
                source.volume = Mathf.Lerp(startVolume, 0f, t);
                yield return null;
            }

            source.volume = 0f;
            source.Stop();
        }

        private class MusicEntry
        {
            public AudioClip clip;
            public bool loop;
            public float time;
        }
    }
}
