using Rune.Core;
using UnityEngine;

namespace Rune.Camera
{
    public enum FollowMethod
    {
        Instant,
        Lerp,
        SmoothDamp,
    }

    /// <summary>
    /// 카메라가 타겟을 따라가도록 하는 컴포넌트.
    /// 타겟이 없으면 현재 위치에서 멈춥니다.
    /// </summary>
    public class CameraFollow : RuneBehaviour
    {
        [SerializeField] private FollowMethod _method = FollowMethod.SmoothDamp;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _smoothTime = 0.3f;

        private Transform _target;
        private Vector3 _velocity;

        /// <summary>
        /// 따라갈 타겟을 설정합니다.
        /// </summary>
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null) return;

            Vector3 destination = _target.position;
            destination.z = transform.position.z; // Z 고정 (2D)

            transform.position = _method switch
            {
                FollowMethod.Instant => destination,
                FollowMethod.Lerp => Vector3.Lerp(transform.position, destination, _speed * Time.deltaTime),
                FollowMethod.SmoothDamp => Vector3.SmoothDamp(transform.position, destination, ref _velocity, _smoothTime),
                _ => destination,
            };
        }
    }
}
