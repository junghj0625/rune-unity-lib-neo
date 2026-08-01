# Rune Unity Library

프로젝트 간 재사용 가능한 Unity 유틸리티 라이브러리.

## 개요

Rune은 여러 Unity 프로젝트에서 공통으로 사용할 수 있는 유틸성 기능을 모아둔 라이브러리입니다.  
서브모듈로 가져와서 사용하며, 특정 프로젝트에 종속되지 않는 범용 코드만 포함합니다.

## 설치

```bash
git submodule add <Rune 레포 URL> Assets/Rune
git submodule update --init --recursive
```

## 폴더 구조

```
Rune/
├── Docs/               # 문서
├── Runtime/            # 런타임 유틸리티 코드
│   ├── Extensions/     # 확장 메서드 (Transform, Vector, Collection 등)
│   ├── Singleton/      # 싱글톤 베이스 클래스
│   ├── Events/         # 이벤트 시스템, 이벤트 버스
│   ├── Pool/           # 오브젝트 풀링
│   ├── State/          # 상태 머신
│   └── Utility/        # 기타 헬퍼
├── Editor/             # 에디터 전용 유틸리티
└── Tests/              # 테스트 코드
```

## 설계 원칙

- **프로젝트 무관성**: 특정 게임 로직에 의존하지 않을 것
- **최소 의존성**: Unity 기본 패키지 외 외부 의존성을 최소화할 것
- **Assembly Definition 분리**: Runtime / Editor / Tests 각각 .asmdef로 분리
- **문서화**: public API에 XML 주석 필수

## 포함 예정 기능

| 카테고리 | 기능 | 상태 |
|----------|------|------|
| Extensions | Transform, Vector2/3, Collection 확장 메서드 | 예정 |
| Singleton | MonoBehaviour 싱글톤 베이스 클래스 | 예정 |
| Events | 타입 기반 이벤트 버스 | 예정 |
| Pool | 제네릭 오브젝트 풀 | 예정 |
| State | FSM (Finite State Machine) | 예정 |
| Timer | 프레임/시간 기반 타이머 유틸 | 예정 |
| Math | 2D 수학 헬퍼 (거리, 각도, 보간 등) | 예정 |
| Coroutine | Coroutine 헬퍼 / 시퀀서 | 예정 |

## 사용법 예시

```csharp
// 싱글톤
public class GameManager : Singleton<GameManager>
{
    // ...
}

// 확장 메서드
transform.SetPositionX(5f);
var randomItem = myList.GetRandom();

// 오브젝트 풀
var bullet = ObjectPool<Bullet>.Get();
ObjectPool<Bullet>.Release(bullet);
```

## 기여 규칙

- project-sod의 코드 컨벤션을 동일하게 적용
- 새 기능 추가 시 이 문서의 "포함 예정 기능" 테이블 업데이트
- Runtime 코드 변경 시 Tests에 대응하는 테스트 작성 권장
