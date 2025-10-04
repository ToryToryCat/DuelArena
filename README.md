# Duel Arena Mini Demo

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
