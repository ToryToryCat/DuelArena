+# Duel Arena Mini Demo
 
+Unity 기반의 듀얼 아레나 시나리오를 통해 계층형 아키텍처, 패턴 기반 설계, 서비스 주입 전략을 한눈에 보여 주는 프로젝트입니다. 플레이어와 AI 적 캐릭터 간 상호작용을 위해 작성한 코드는 "도메인 로직 → 응용 서비스 → 프레임워크 연동" 순으로 흐르며, 포트폴리오나 면접에서 설계 역량을 어필할 수 있도록 정리했습니다.
+
+## 프로젝트 한눈에 보기
+
+| 레이어 | 책임 | 대표 스크립트 |
+| --- | --- | --- |
+| **Core** | 게임 규칙, 계약, 도메인 이벤트 정의. Unity 엔진과 완전히 분리된 순수 C# 모델. | `Health`, `BasicDamagePolicy`, `ICombatant`, `HealthChangedEvent` |
+| **Application** | 도메인 객체를 orchestration하여 전투 시나리오 실행. | `CombatService` |
+| **Infrastructure** | 로깅, 서비스 레지스트리 등 기술 자원을 캡슐화. | `UnityLogger`, `ServiceRegistry` |
+| **Presentation** | 입력, AI, UI, 씬 부트스트랩. 도메인 계층에 대한 얇은 의존성만 유지. | `PlayerController`, `EnemyAI`, `HealthBehaviour`, `Bootstrap` |
+
+> 면접 팁: 각 레이어가 독립 모듈처럼 동작하며, Unity 프로젝트임에도 POCO 모델을 유지해 테스트 가능성을 확보했다는 점을 강조하세요.
+
+## 설계 의사결정과 적용 패턴
+
+- **계층형 아키텍처**: Core → Application → Infrastructure → Presentation 순으로 단방향 의존성을 강제했습니다. Presentation은 `ServiceRegistry`를 통해서만 상위 계층을 참조하므로 씬 변경이 도메인에 파급되지 않습니다.
+- **Strategy (`IDamagePolicy`)**: 공격 피해 계산을 정책 객체로 분리해 밸런싱 요구에 정책 교체만으로 대응할 수 있습니다.
+- **Command (`IInputCommand`)**: 입력 장치와 전투 서비스 호출을 분리하여 키보드/패드/네트워크 등 입력 수단 확장이 용이합니다.
+- **State Machine (`EnemyAI`)**: Idle → Chase → Attack 흐름을 명확한 상태 전이로 구현하고, 상태별 Update 로직을 독립 클래스로 관리했습니다.
+- **Observer (`Health`)**: 체력 변화 이벤트를 발행하고 UI/게임 결과 컨트롤러가 이를 구독합니다. 프레임 폴링 없이 도메인 이벤트로 UI를 동기화했습니다.
+- **얇은 DI/Service Locator (`ServiceRegistry`)**: Bootstrap 시점에 서비스 및 전략을 등록하고, 필요 시 의존성을 조회하도록 설계했습니다. ScriptableObject나 외부 컨테이너로 확장 가능한 구조입니다.
+
+## 코드 흐름 설명
+
+1. **Bootstrap**이 `ServiceRegistry`를 초기화하고 `CombatService`, `BasicDamagePolicy`, `UnityLogger` 등을 등록합니다.
+2. **PlayerController**는 Unity 입력을 감지해 `PlayerAttackCommand`를 실행합니다. 커맨드는 레지스트리에서 `CombatService`를 요청해 플레이어와 적 `ICombatant` 간 전투를 수행합니다.
+3. **CombatService**는 공격자/방어자의 `IHealth`를 조회하고 `IDamagePolicy`에 따라 피해량을 계산한 뒤 체력을 갱신합니다.
+4. **Health** 객체는 체력 수치 변화 혹은 사망 시 `HealthChangedEvent`, `DeathEvent` 등을 발행합니다.
+5. **HealthBehaviour**와 **GameResultController**는 이벤트를 구독해 UI 슬라이더 및 승패 텍스트를 갱신합니다.
+6. **EnemyAI**는 상태 머신을 통해 플레이어를 추적하고, 공격 조건이 충족되면 `CombatService`를 호출해 반격합니다.
+
+> 설명 포인트: 입력 → 서비스 → 도메인 → 이벤트 → UI/AI 순으로 데이터 흐름이 명확하며, 각 단계가 인터페이스 기반으로 결합되어 테스트 더블을 쉽게 주입할 수 있습니다.
+
+## 프로젝트 구조 상세
+
+```
+Assets/
+ ├─ Scripts/
+ │   ├─ Core/              # 순수 도메인 모델과 이벤트
+ │   ├─ Application/       # CombatService 등 오케스트레이션
+ │   ├─ Infrastructure/    # 로깅, ServiceRegistry
+ │   └─ Presentation/      # MonoBehaviour 및 UI/입력
+ └─ Scenes/
+     └─ Sample.unity       # 데모 플레이용 기본 씬
+```
+
+- **Core/Health.cs**: 체력 값을 관리하고 `HealthChangedEvent`를 발행합니다.
+- **Application/CombatService.cs**: 공격자/피격자를 받아 데미지 계산과 도메인 이벤트 발행을 담당합니다.
+- **Presentation/EnemyAI.cs**: 상태 패턴을 활용한 FSM으로 플레이어 추적 및 공격 로직을 분리했습니다.
+- **Presentation/Bootstrap.cs**: 씬 로딩 시 ServiceRegistry 구성과 의존성 연결을 담당합니다.
+
+## 기술적 하이라이트
+
+- **POCO 도메인 모델**: Unity 엔진에 의존하지 않는 순수 C# 객체로 테스트와 재사용성을 높였습니다.
+- **이벤트 기반 UI 연동**: 체력 변화, 사망 이벤트 등 도메인 이벤트로 UI를 동기화해 프레임 기반 폴링을 최소화했습니다.
+- **AI 상태 머신 추상화**: 상태별 Update 함수로 전투 AI의 가독성과 유지보수성을 확보했습니다.
+- **확장 가능한 입력 계층**: 추가 명령(회피, 스킬 등)을 단일 인터페이스로 확장할 수 있습니다.
+- **서비스 구성 문서화**: `Bootstrap` 한 곳에서 정책, 서비스, 컴포넌트를 연결하여 온보딩 시 학습 비용을 줄였습니다.
+
+## 면접에서 활용할 수 있는 토픽
+
+- Unity 프로젝트에서도 도메인 로직을 독립 계층으로 분리하여 테스트 친화적인 구조를 구현한 사례
+- 이벤트 기반 전투 흐름으로 UI/AI/입력 결합도를 낮춘 사례
+- 상태 머신 설계로 AI 가독성을 높이고 확장 포인트를 확보한 과정
+- ServiceRegistry로 초기 부트스트랩 단계에서 의존성을 관리한 경험
+- Strategy/Command/Observer 패턴을 선택한 근거와 대안 비교
+
+## 확장 아이디어
+
+- `IDamagePolicy`를 다형적으로 확장하여 치명타, 상태 이상, 방어력 등을 도입합니다.
+- `CombatService`에 턴 제어, 쿨다운, 리소스 관리 등을 추가해 더 복잡한 전투 시스템을 실험할 수 있습니다.
+- `EnemyAI` 상태 머신에 순찰, 후퇴, 궁극기 같은 추가 상태를 도입하거나 행동 트리를 병합해 보세요.
+- `ServiceRegistry`를 ScriptableObject 기반 설정이나 외부 DI 컨테이너로 교체해도 구조적으로 대응할 수 있습니다.
+
+## 씬 구성 참고
+
+프로토타입 씬은 `Assets/Scenes/Sample.unity`로 제공됩니다. 빠르게 테스트하려면 두 개의 큐브 전투원, 체력 슬라이더 UI, 결과 텍스트, `Bootstrap` 오브젝트를 배치하고 각 스크립트의 공개 필드를 연결하면 됩니다.
