using System.Collections.Generic;
using Rune.Core;
using UnityEngine;

namespace Rune.Audio
{
    /// <summary>
    /// 자식 AudioSource를 풀로 관리하여 동시 재생을 지원합니다.
    /// 재생 시 랜덤 피치를 적용할 수 있습니다.
    /// </summary>
    public class AudioPlayer : RuneBehaviour
    {
        [SerializeField] private float _pitchMin = 0.9f;
        [SerializeField] private float _pitchMax = 1.1f;

        private List<AudioSource> _sources = new();

        protected override void Awake()
        {
            base.Awake();

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<AudioSource>(out var source))
                {
                    _sources.Add(source);
                }
            }
        }

        /// <summary>
        /// 기본 피치(1.0)로 클립을 재생합니다.
        /// </summary>
        public void Play(AudioClip clip, float volume = 1f)
        {
            var source = GetAvailableSource();
            source.pitch = 1f;
            source.PlayOneShot(clip, volume);
        }

        /// <summary>
        /// 랜덤 피치로 클립을 재생합니다.
        /// </summary>
        public void PlayRandomPitch(AudioClip clip, float volume = 1f)
        {
            var source = GetAvailableSource();
            source.pitch = Random.Range(_pitchMin, _pitchMax);
            source.PlayOneShot(clip, volume);
        }

        /// <summary>
        /// 지정 피치로 클립을 재생합니다.
        /// </summary>
        public void Play(AudioClip clip, float pitch, float volume)
        {
            var source = GetAvailableSource();
            source.pitch = pitch;
            source.PlayOneShot(clip, volume);
        }

        private AudioSource GetAvailableSource()
        {
            foreach (var source in _sources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }

            // 전부 재생 중이면 첫 번째를 재사용
            return _sources[0];
        }

        /// <summary>
        /// 풀 내 소스 개수.
        /// </summary>
        public int Count => _sources.Count;
    }
}
