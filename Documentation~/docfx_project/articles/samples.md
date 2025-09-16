The <span class="brand-font">Quantum Forge</span> Unity package ships with a number of samples. Currently, the samples are meant to be demonstrations of using <span class="brand-font">Quantum Forge</span> to introduce quantum elements into games through the editor. They're very rough, and are not meant to convey full gameplay experiences yet. As the package matures, we will add more fleshed out examples.

# Installation
In the package manager window, select the <span class="brand-font">Quantum Forge</span> package. Find the Samples tab, then click the 'import' button next to each sample you want to import into your project.

## Actions
This sample is meant to be a fairly comprehensive example of how to create and interact with the various features of the package. You can find examples of connecting Actions to buttons, using trackers, and creating quantum interaction between QuantumProperties of different dimensions.

## Roshambo
This is a sample of how to modify a classic game like Rock Paper Scissors to include quantum elements. Each "player" has a symbol that they can place in superposition, so their choice remains undetermined until a measurement. They can apply various single, and multi-property actions to the symbols. This is not a gameplay experience, as there are no formal rules associated with it yet. It is for demonstration purposes.

## Platformer
This is an example of how you might include quantum elements in a platformer style game. It was built on top of an existing, free platformer example from the Unity asset store. The character can split into two different lanes by jumping into a Hadamard zone. They can then manipulate the probability of being on the top or the bottom lane by jumping into the Hadamrd zone again, or alternatively jumping into the phase rotation zone first, then into the Hadamard.

## CHSH

This is an example of the CHSH game: [What is the CHSH Game? (YouTube)](https://www.youtube.com/watch?v=v7jctqKsUMA)  
It is a clear example of quantum advantage in a probabilistic game.

This can be used as the base for introducing probability advantage for certain mechanics in other games.

## What is the CHSH Game?

The CHSH game is a two-player cooperative game used in quantum information theory to demonstrate the difference between classical and quantum strategies. It is named after Clauser, Horne, Shimony, and Holt.

## Rules

- There are two players, Alice and Bob, who cannot communicate once the game starts.
- Each round, the referee gives Alice a random bit `x` (0 or 1), and Bob a random bit `y` (0 or 1).
- Alice outputs a bit `a`, and Bob outputs a bit `b`.
- The table shows how Alice and Bob win.

| Referee bits | Alice's bit | Bob's bit | Win Condition |
|:-----------:|:-----------:|:---------:|:-------------:|
| x = 0, y = 0 |     0 or 1  |   0 or 1  | Alice and Bob must output the same bit (both 0 or both 1) |
| x = 0, y = 1 |     0 or 1  |   0 or 1  | Alice and Bob must output the same bit (both 0 or both 1) |
| x = 1, y = 0 |     0 or 1  |   0 or 1  | Alice and Bob must output the same bit (both 0 or both 1) |
| x = 1, y = 1 |     0 or 1  |   0 or 1  | Alice and Bob must output different bits (one 0, one 1)   |

- The referee gives Alice a random bit (x) and Bob a random bit (y).
- Alice and Bob each respond with a bit (0 or 1), without communicating.
- They win if they meet the win condition for the given referee bits.

## Classical vs Quantum Strategies

- **Classical strategy:** The best possible success rate is 75%.
- **Quantum strategy:** By sharing entangled qubits, Alice and Bob can achieve a success rate of about 85.4%.

## This demo
Alice and Bob each have a quantum coin in superposition. They can choose to measure that coin one of two ways. If the Referee supplies a 0, they choose to use the Measurement 0 result as their answer. If the Referee supplies a 1, they choose to use the Measurement 1 result as their answer. They can agree before the round on how to orient their measurement. They set their measurement by turning the dial in a particular direction.

The game can be played in classical, or in quantum mode. In classical mode, the coins are not entangled. The game is then just the classical version of the probabilistic game, and can be won 75% of the time at best. There are specific measurement settings that can achieve this result.

In quantum mode, their coins are entangled. The game can now be won 85% of the time with the right measurement settings. Experiment with different settings to find the winning condition! (Hint: watch the [video](https://www.youtube.com/watch?v=v7jctqKsUMA) )

## How to Run the Example

1. Open this sample in Unity.
2. Press Play to start the simulation.
3. Drag the dials to set the measurement settings.
4. Press "Go" to run the game for N rounds. (Use slider to adjust number of rounds to run)
5. The results show the number of wins vs rounds played, and the percentag won.

## References

- [CHSH Game (Wikipedia)](https://en.wikipedia.org/wiki/CHSH_inequality)
- [Quantum Advantage in Games (YouTube)](https://www.youtube.com/watch?v=v7jctqKsUMA)

---
