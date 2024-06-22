# Mockup math mob war

## Description
MockupMathMobWar is a mobile runner game developed in Unity. In this game, the player controls a character that runs through a series of portals, facing mobs and bosses while choosing mathematical operations that impact their skills and health. The attacks are automatic and can be of elements like fire, ice, or stone.

## Gameplay

### Portal Mechanics
- **Mathematical Operations**: 
  - Mathematical operations are randomly generated and can be addition, subtraction, multiplication, or division.
  - Control mechanisms are in place to ensure that if a debuff is too high, bonus respawn boxes are generated.
  
### Mobs and Checkpoints
- **Types of Mobs**:
  - Mobs are classified by rarity.
  - Mob difficulty is adjusted by an algorithm that analyzes portal choices and the player's maximum strength up to that point.
- **Checkpoint Bonuses**:
  - Possible bonuses include double attack, conductive attack, and forcing a specific element in attacks.
  
### Attack Elements
- **Fire**: Burns for 3 ticks.
- **Ice**: Slows down enemies.
- **Stone**: Deals direct damage.
- **Element Distribution**: 
  - Elements are randomly assigned when the player is instantiated or can be changed via bonuses. Player minions have its own random elemental atack.

### Boss Fights
- **Characteristics**:
  - Bosses have more HP and can summon smaller creatures.
  - Boss difficulty is adjusted based on the player's strength as calculated by the simulation algorithm.

### Interface and Controls
- **Controls**:
  - The player uses a joystick on the user interface (UI) to control the character's direction, which can move sideways to choose portals and forward.
- **Minion Mechanics**:
  - Each player minion has 1 base life and attacks with a random element.
  - While there are minions, the player is immune to attacks. If passing through an operation, the number of minions is changed proportionally.
  - The number of minions is displayed as a life bar, and attack strength increases proportionally to the number of minions.

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/username/magic-portal-runner.git
   ```
2. Open the project in Unity.

3. Import all necessary dependencies via the Package Manager.

## Contribution
1. Fork the project.
2. Create a new branch with your feature: `git checkout -b my-feature`.
3. Commit your changes: `git commit -m 'Add new feature'`.
4. Push to the branch: `git push origin my-feature`.
5. Open a Pull Request.

## License
This project is licensed under the AGPL-3.0 License - see the [LICENSE](https://www.gnu.org/licenses/agpl-3.0.en.html) file for details.

## Contact
- For questions and suggestions, contact: MockupMathMobWar@skykek.com
- More about me: https://valtingojer.dev

