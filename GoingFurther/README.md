# Going further

The four extensions of the duel are implemented together in `Program.cs`.

Run from this project folder so that `results.txt` is saved here:

```powershell
cd GoingFurther
dotnet run
```

- `1`: attack for 12 to 24 damage.
- `2`: drink a potion (+30 HP, maximum 100). Aya starts with two potions. The enemy still attacks after a potion attempt.
- Invalid input repeats the same turn without an enemy attack.
- After a game, enter `y` to play again; any other answer ends the session.

The wave contains a Goblin, an Orc, another Goblin, and a Boss. The Boss has 60 HP and 6 armour; every hit still deals at least 1 damage. Its attacks deal 9 to 16 damage.

`Player.Died` announces the transition from alive to dead. `PlayGame()` returns the victory's turn count, or 0 for a defeat. LINQ displays the three quickest victories of the current session. Each completed game appends its result to `results.txt`; game numbers restart at 1 for each session. File errors print a message and allow the program to continue.

If console input ends during a fight, that game counts as a defeat and the session ends.

## Compiler experiment

Removing `virtual` from `Enemy.TakeDamage` produces **CS0506**: `Boss.TakeDamage(int)` cannot override an inherited member unless it is marked `virtual`, `abstract`, or `override`. This was checked and `virtual` restored.

## Validation

The project builds without warnings or errors. Checks covered five consecutive games, victories and defeat, replay, potion limits, invalid input, the death notification, Boss armour and attacks, the best-three ranking, appended results, read-only results, other save failures, and end of input. Temporary game results were removed after checking.
