# Rune 필수 오브젝트

Rune 라이브러리가 정상 동작하려면 씬에 다음 싱글톤 오브젝트들이 배치되어야 합니다.
일반적으로 Core 프리팹(DontDestroyOnLoad)에 포함시킵니다.

## 필수 싱글톤

| 클래스 | 네임스페이스 | 역할 |
|--------|-------------|------|
| `CoroutineRunner` | Rune.Core | 비활성 오브젝트 대신 코루틴을 실행하는 호스트 |
| `InputStack` | Rune.Input | 입력 소비자 스택 관리 |

## 배치 예시

```
Core (DontDestroyOnLoad)
├── CoroutineRunner
├── InputStack
└── ... (프로젝트별 싱글톤)
```

## 사용법

### CoroutineRunner

비활성 오브젝트에서 코루틴을 실행해야 할 때:

```csharp
_routine.Run(CoroutineRunner.Instance, SomeCoroutine());
```

### InputStack

입력을 받고 싶은 소비자를 등록/해제:

```csharp
InputStack.Push(this);    // 입력 받기 시작
InputStack.Remove(this);  // 입력 받기 중단
```
