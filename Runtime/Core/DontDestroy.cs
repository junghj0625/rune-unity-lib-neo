using UnityEngine;

namespace Rune.Core
{
    /// <summary>
    /// 이 컴포넌트가 붙은 GameObject를 씬 전환 시에도 파괴하지 않습니다.
    /// 싱글톤이 아닌 일반 오브젝트에 사용합니다.
    /// </summary>
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
