using UnityEngine;

namespace Rune.Core
{
    /// <summary>
    /// MonoBehaviour를 래핑하는 Rune 기본 베이스 클래스.
    /// 컴포넌트 캐싱 헬퍼(Get, GetAt)를 제공합니다.
    /// </summary>
    /// <remarks>
    /// 파생 클래스에서의 사용 패턴:
    ///
    ///   private Animator _animator;
    ///   protected Animator Anim => Get(ref _animator);
    ///
    ///   private Rigidbody2D _rigidbody;
    ///   protected Rigidbody2D Rb => Get(ref _rigidbody);
    ///
    ///   private HitBox _hitBox;
    ///   protected HitBox HitBox => GetAt(ref _hitBox, "Body/HitBox");
    ///
    /// </remarks>
    public abstract class RuneBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            StartCoroutine(LateStartCoroutine());
        }
        
        protected virtual void OnDestroy()
        {
            
        }

        /// <summary>
        /// 모든 Start()가 실행된 다음 프레임에 호출됩니다.
        /// </summary>
        protected virtual void LateStart()
        {
            
        }

        private System.Collections.IEnumerator LateStartCoroutine()
        {
            yield return null;
            LateStart();
        }

        /// <summary>
        /// 자신의 GameObject에서 컴포넌트를 캐싱하여 반환합니다.
        /// </summary>
        protected T Get<T>(ref T field) where T : class
        {
            return field ??= GetComponent<T>();
        }

        /// <summary>
        /// 상대 경로로 자식을 찾아 컴포넌트를 캐싱하여 반환합니다.
        /// 경로는 Transform.Find 형식 (예: "Body/HitBox").
        /// </summary>
        protected T GetAt<T>(ref T field, string path) where T : class
        {
            if (field != null) return field;

            var child = transform.Find(path);
            if (child != null)
            {
                field = child.GetComponent<T>();
            }

            return field;
        }

        /// <summary>
        /// 상대 경로로 자식 GameObject를 캐싱하여 반환합니다.
        /// </summary>
        protected GameObject GetObject(ref GameObject field, string path)
        {
            if (field != null) return field;

            var child = transform.Find(path);
            if (child != null)
            {
                field = child.gameObject;
            }

            return field;
        }
    }
}
