# Duel Arena Mini Demo

<<<<<<< HEAD
A minimal layered architecture demonstration for a Unity duel arena scenario featuring a player-controlled combatant and an AI-driven enemy.

## Project Structure

- `Assets/Scripts/Core`: Domain contracts and entities (health, damage, combatant abstractions, domain events).
- `Assets/Scripts/Application`: Application services that coordinate domain logic.
- `Assets/Scripts/Infrastructure`: Infrastructure components such as logging and a lightweight service registry.
- `Assets/Scripts/Presentation`: Unity behaviours for input, AI, UI binding, and scene bootstrap.
- `Assets/Scenes/Sample.unity`: Empty scene used for the demo setup.

## Scene Setup Guide

1. Open `Assets/Scenes/Sample.unity`.
2. Create two cubes and position them a few units apart. Add a `CombatantBehaviour` component to each.
   - Mark one cube as the player and assign a distinctive `Identifier` (e.g., "Player").
   - Mark the second cube as the enemy with its own identifier (e.g., "Enemy").
3. Create an empty GameObject named `Bootstrap` and attach the `Bootstrap` component.
4. Add a `PlayerController` component to the player cube and assign references to the player and enemy combatants.
5. Add an `EnemyAI` component to the enemy cube and assign references to the enemy (self) and player combatants.
6. Create a Canvas with two UI `Slider` elements labelled `PlayerHealth` and `EnemyHealth`.
   - Attach a `HealthBehaviour` component to each slider and assign the corresponding combatant.
7. Add a UI `Text` element to the Canvas for displaying results. Attach the `GameResultController` component to a convenient GameObject and assign the player, enemy, and result text references.

## Controls

- **Space**: Executes the player's attack command.

## Gameplay Flow

- The player presses **Space** to attack the enemy, applying damage based on the configured `IDamagePolicy` strategy.
- The enemy idles until the player enters the chase range, pursues within the chase range, and automatically attacks once in range.
- Health changes update the UI sliders, and the `GameResultController` displays "Victory" or "Defeat" when a combatant is defeated.

## Extensibility Points

- Implement new `IDamagePolicy` strategies (e.g., critical hits, resistances) and register them in `Bootstrap`.
- Extend `CombatService` to support abilities, buffs, or combo mechanics.
- Add new `IInputCommand` implementations to support additional player actions.
- Expand the enemy state machine with more behaviours such as retreat, patrol, or special attacks.
- Integrate persistence or analytics by providing alternative implementations of `ILogger` and registering them in the service registry.

## Testing

No automated tests are included by default. Add standard NUnit test assemblies targeting the `Core` and `Application` layers for regression coverage if desired.
=======
Unity 기반의 듀얼 아레나 시나리오를 통해 계층형 아키텍처를 간단히 시연하는 프로젝트입니다. 플레이어와 AI 적 캐릭터 간의 상호작용을 통해 도메인 로직, 서비스 조합, 프레젠테이션 계층을 분리하는 구조를 확인할 수 있습니다.

## 아키텍처 개요

이 저장소는 **Core → Application → Infrastructure → Presentation** 순으로 흐르는 전형적인 계층형 아키텍처를 따릅니다.

- **Core**: 게임플레이의 규칙과 계약을 담는 순수 C# 계층입니다. 체력 관리(`IHealth`), 피해 정책(`IDamagePolicy`), 전투원 계약(`ICombatant`)과 같은 도메인 모델을 정의하며 Unity 의존성이 없습니다.
- **Application**: `CombatService`가 도메인 모델을 조합해 공격 시퀀스를 실행합니다. 서비스는 도메인 이벤트를 발생시키며 프레젠테이션에서 이를 구독할 수 있도록 설계했습니다.
- **Infrastructure**: `ServiceRegistry`를 통한 얇은 DI 컨테이너와 `UnityLogger` 같은 외부 리소스 어댑터를 제공합니다. 인터페이스 기반이므로 다른 로깅 구현으로 교체하기 쉽습니다.
- **Presentation**: Unity `MonoBehaviour`를 활용해 입력, AI, UI를 조작합니다. 도메인 계층에 대한 의존성을 최소화하고 `ServiceRegistry`에 의해 필요한 서비스 인스턴스를 주입받습니다.

## 적용한 디자인 패턴

- **Strategy**: `IDamagePolicy` 인터페이스와 `BasicDamagePolicy` 구현으로 피해 계산을 캡슐화했습니다. 전투 밸런스를 조정할 때 정책 교체만으로 동작을 변경할 수 있습니다.
- **Command**: `IInputCommand`와 `PlayerAttackCommand`는 입력 처리와 도메인 호출을 분리합니다. 입력 장치나 조작 방식을 바꿔도 전투 로직을 수정하지 않아도 됩니다.
- **State Machine**: `EnemyAI`는 Idle → Chase → Attack 상태 전이를 명확히 표현합니다. 각 상태는 독립된 내부 클래스로 관리되어 확장이 용이합니다.
- **Observer**: `Health`는 체력 변화 시 이벤트를 발생시키고 `HealthBehaviour`, `GameResultController` 등이 이를 구독합니다. UI 갱신과 도메인 상태를 느슨하게 결합했습니다.
- **Service Locator (얇은 DI)**: `ServiceRegistry`가 공용 서비스 구성을 캡슐화합니다. Bootstrap 단계에서 전략과 서비스를 등록하고 이후 컴포넌트는 필요 시 꺼내어 사용합니다.

## 기술적 하이라이트

- **POCO 도메인 모델**: Unity 엔진에 의존하지 않는 순수 C# 객체로 테스트와 재사용성을 높였습니다.
- **이벤트 기반 UI 연동**: 체력 변화, 사망 이벤트 등 도메인 이벤트를 바탕으로 UI를 동기화하여 프레임 기반 폴링을 최소화했습니다.
- **AI 상태 머신 추상화**: 선형 로직이 아닌 상태별 업데이트 함수로 전투 AI의 가독성과 유지보수성을 확보했습니다.
- **확장 가능한 입력 계층**: 추가 명령(회피, 스킬 등)을 단일 인터페이스로 확장할 수 있습니다.
- **서비스 구성 문서화**: `Bootstrap` 한 곳에서 정책, 서비스, 컴포넌트를 연결하여 프로젝트 온보딩 시 학습 비용을 줄입니다.

## 포트폴리오에서 강조할 수 있는 포인트

- Unity 프로젝트임에도 도메인 로직을 독립 계층으로 분리하여 테스트 친화적인 구조를 확보했습니다.
- 전투 흐름이 이벤트 드리븐으로 구성되어 있어 UI/AI/입력 각 요소가 느슨하게 결합됩니다.
- 서비스 레지스트리를 통해 간단한 의존성 주입을 구현, 유지보수와 확장성을 고려한 설계 방식을 보여 줍니다.
- 디자인 패턴을 명확한 목적과 함께 적용하여 설계 의도를 코드에서 바로 확인할 수 있습니다.

## 확장 아이디어

- `IDamagePolicy`를 다형적으로 확장하여 치명타, 상태 이상, 방어력 등을 도입합니다.
- `CombatService`에 턴 제어, 쿨다운, 리소스 관리 등을 추가해 더 복잡한 전투 시스템을 실험할 수 있습니다.
- `EnemyAI` 상태 머신에 순찰, 후퇴, 궁극기 같은 추가 상태를 도입하거나 행동 트리를 병합해 보세요.
- `ServiceRegistry`를 ScriptableObject 기반 설정이나 외부 DI 컨테이너로 교체해도 구조적으로 대응할 수 있습니다.

## 씬 구성 참고

프로토타입 씬은 `Assets/Scenes/Sample.unity`로 제공됩니다. README에서는 설계 관점에 집중했지만, 테스트를 위해서는 두 개의 큐브 전투원, 체력 슬라이더 UI, 결과 텍스트, `Bootstrap` 오브젝트를 배치하고 각 스크립트의 공개 필드를 연결하면 됩니다.
>>>>>>> origin/codex/create-mini-combat-demo-for-unity-cgo9js
