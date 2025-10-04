# Duel Arena Mini Demo

Unity 기반의 듀얼 아레나 시나리오를 통해 계층형 아키텍처를 간단히 시연하는 프로젝트입니다. 플레이어가 조작하는 전투원과 AI로 움직이는 적 캐릭터, 체력 UI, 전투 결과 표시를 포함합니다.

## 프로젝트 구조

- `Assets/Scripts/Core`: 체력, 피해 정책, 전투원 추상화, 도메인 이벤트 등 도메인 계약과 엔터티를 정의합니다.
- `Assets/Scripts/Application`: 전투 로직을 조정하는 응용 서비스 계층입니다.
- `Assets/Scripts/Infrastructure`: 로깅과 서비스 레지스트리 등 인프라 구성을 제공합니다.
- `Assets/Scripts/Presentation`: 입력 처리, AI, UI 바인딩, 부트스트랩 등 Unity 컴포넌트를 포함합니다.
- `Assets/Scenes/Sample.unity`: 데모 설정을 위한 빈 씬입니다.

## 씬 세팅 가이드

1. `Assets/Scenes/Sample.unity` 씬을 엽니다.
2. 큐브 두 개를 생성하여 서로 떨어뜨려 배치하고 각 오브젝트에 `CombatantBehaviour` 컴포넌트를 추가합니다.
   - 플레이어 오브젝트에는 식별자(`Identifier`)를 "Player" 등 구분 가능한 값으로 설정합니다.
   - 적 오브젝트에도 "Enemy" 등 별도의 식별자를 지정합니다.
3. `Bootstrap`이라는 빈 게임 오브젝트를 만들고 `Bootstrap` 컴포넌트를 부착합니다.
4. 플레이어 큐브에 `PlayerController` 컴포넌트를 추가하고 플레이어 및 적 전투원 참조를 연결합니다.
5. 적 큐브에 `EnemyAI` 컴포넌트를 추가하고 자신(적)과 플레이어 전투원 참조를 연결합니다.
6. Canvas를 생성하고 `PlayerHealth`, `EnemyHealth` 라벨을 가진 UI `Slider` 두 개를 배치합니다.
   - 각 슬라이더에 `HealthBehaviour` 컴포넌트를 부착하고 대응하는 전투원 참조를 연결합니다.
7. 전투 결과를 표시할 UI `Text` 요소를 Canvas에 추가합니다. 적절한 오브젝트에 `GameResultController` 컴포넌트를 부착하고 플레이어, 적, 결과 텍스트 참조를 할당합니다.

## 조작법

- **Space**: 플레이어 공격 명령을 실행합니다.

## 게임플레이 흐름

- 플레이어는 **Space** 키를 눌러 적에게 공격을 가하며, 이는 설정된 `IDamagePolicy` 전략에 따라 피해를 계산합니다.
- 적은 대기 상태에서 플레이어가 추적 범위에 들어오면 추격하고, 공격 범위에 도달하면 자동으로 공격합니다.
- 체력 변화는 UI 슬라이더에 반영되고, 전투원이 쓰러지면 `GameResultController`가 "Victory" 혹은 "Defeat" 메시지를 표시합니다.

## 확장 포인트

- `Bootstrap`에서 등록할 수 있는 새로운 `IDamagePolicy` 전략(치명타, 저항 등)을 구현해 보세요.
- `CombatService`를 확장하여 스킬, 버프, 콤보와 같은 추가 전투 메커닉을 지원할 수 있습니다.
- 추가 플레이어 행동을 위해 새로운 `IInputCommand` 구현을 도입할 수 있습니다.
- 적 상태 머신을 확장하여 후퇴, 순찰, 특수 공격 등 다양한 행동을 설계해 보세요.
- `ILogger`의 다른 구현을 제공하고 서비스 레지스트리에 등록하여 분석 또는 저장 기능을 통합할 수 있습니다.

## 테스트

기본적으로 자동화된 테스트는 포함되어 있지 않습니다. 필요하다면 `Core`와 `Application` 계층을 대상으로 하는 NUnit 테스트 어셈블리를 추가하여 회귀 테스트를 구성하세요.
