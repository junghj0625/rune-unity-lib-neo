# 캐싱 컴포넌트 패턴

RuneBehaviour 파생 클래스에서 다른 컴포넌트에 접근할 때 사용하는 표준 패턴입니다.

## 규칙

**private 필드 + protected/public 프로퍼티를 한 쌍으로, 2줄씩 나란히 정의합니다.**

```csharp
public class PlayerController : RuneBehaviour
{
    // ── 캐싱 컴포넌트 ──

    private Animator _animator;
    protected Animator Anim => Get(ref _animator);

    private Rigidbody2D _rigidbody;
    protected Rigidbody2D Rb => Get(ref _rigidbody);

    private HitBox _hitBox;
    protected HitBox HitBox => GetAt<HitBox>(ref _hitBox, "Body/HitBox");

    private SpriteRenderer _sprite;
    protected SpriteRenderer Sprite => GetAt<SpriteRenderer>(ref _sprite, "Visual");
}
```

## 헬퍼 메서드

| 메서드 | 검색 범위 |
|--------|-----------|
| `Get<T>(ref T field)` | 자기 자신 GameObject |
| `GetAt<T>(ref T field, string path)` | 상대 경로의 자식 (Transform.Find 형식) |

## 동작 방식

- 프로퍼티에 처음 접근할 때 GetComponent 또는 Transform.Find를 호출하여 캐싱
- 이후 접근은 캐싱된 참조를 즉시 반환 (비용 없음)
- 인터페이스도 지원 (`where T : class`)
- 경로는 `"Body/HitBox"` 처럼 슬래시로 계층을 구분

## 가이드라인

- 클래스 상단에 캐싱 컴포넌트를 모아서 선언할 것
- 필드는 `private`, 프로퍼티는 `protected` (외부 노출 필요 시 `public`)
- 필드명은 `_camelCase`, 프로퍼티명은 짧고 명확하게
- 빈 줄로 쌍을 구분하여 가독성 확보
