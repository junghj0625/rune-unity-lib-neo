# Openable 패턴

열고 닫을 수 있는 UI 컴포넌트를 구현하는 표준 패턴입니다.

## 구성 요소

| 클래스 | 역할 |
|--------|------|
| `IOpenable` | 인터페이스. Open/Close + 코루틴 버전 정의 |
| `OpenableRoutine` | 헬퍼 struct. SetActive + CoroutineRunner + SafeCoroutine 캡슐화 |

## 사용법

```csharp
public class MyPanel : RuneUI, IOpenable
{
    [SerializeField] private float _duration = 0.3f;

    private OpenableRoutine _routine;

    public void Open() => _routine.Open(gameObject, OpenCoroutine());
    public void Close() => _routine.Close(gameObject, CloseCoroutine());
    public IEnumerator OpenAndWait() => _routine.OpenAndWait(gameObject, OpenCoroutine());
    public IEnumerator CloseAndWait() => _routine.CloseAndWait(gameObject, CloseCoroutine());

    private IEnumerator OpenCoroutine() { /* 열리는 연출 */ }
    private IEnumerator CloseCoroutine() { /* 닫히는 연출 */ }
}
```

## 동작 원리

### Open
1. `gameObject.SetActive(true)` — 오브젝트 활성화
2. `CoroutineRunner.Instance`에서 코루틴 실행 — 비활성 상태에서도 안전

### Close
1. `CoroutineRunner.Instance`에서 코루틴 실행
2. 코루틴 끝나면 `gameObject.SetActive(false)` — 자동 비활성화

### 안전성
- `SafeCoroutine` 기반이므로 Open/Close 연타 시 이전 코루틴 자동 취소
- 도중 반전해도 현재 상태에서 자연스럽게 전이

## 규칙

- UI 코루틴에서는 `Time.unscaledDeltaTime` 사용 (TimeScale 영향 없음)
- `WaitForSecondsRealtime` 사용 (`WaitForSeconds` 사용 금지)
- 이징은 `Rune.Util.Math.SmoothStep()` 활용

## 기존 구현 예시

- `Dim` — CanvasGroup alpha 페이드
- `Window` — Y scale 전이
