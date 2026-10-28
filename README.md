# C# lab: requirements review

Reviewed against `csharp-lab-en.pdf`, including the four homework exercises in part 3.

| PDF requirement | Implementation | Status |
| --- | --- | --- |
| .NET 10 console project and Hello, World! | `WarmUp/FirstProgram` | Complete |
| Five typed character variables and computed experience | `WarmUp/Exercice1` | Complete |
| Health thresholds and zero-health message | `WarmUp/Exercice2` | Complete |
| Six-turn poison loop, health floor and early exit | `WarmUp/Exercice3` | Complete |
| Healing method and three example calls | `WarmUp/Exercice4` | Complete |
| Explicit `using System;` in each source file | All projects | Complete |
| Player properties, clamped health, two potions and healing | `Arena/Program.cs` | Complete |
| Abstract Enemy, Goblin and Orc with specified health and damage | `Arena/Program.cs` | Complete |
| Goblin, Orc, Goblin wave, listing and first enemy | `Arena/Program.cs` | Complete |
| Combat loop, random attacks and removal before counterattack | `Arena/Program.cs` | Complete |
| Safe input, attack, potion and running away | `Arena/Program.cs` | Complete |
| Victory/defeat and turn count | `Arena/Program.cs` | Complete |
| Boss: 60 HP, 6 armour, minimum 1 damage, attack 9–16 | `GoingFurther/Program.cs` | Complete |
| Virtual damage method and Boss override calling base | `GoingFurther/Program.cs` | Complete |
| Died event and subscription | `GoingFurther/Program.cs` | Complete |
| PlayGame, replay, victory list and LINQ best three | `GoingFurther/Program.cs` | Complete |
| Append results and catch both file exception types | `GoingFurther/Program.cs` | Complete |

## Checks performed

All seven projects built with zero warnings and errors. Runtime checks covered:

- Health values 45, 100, 12 and 0: injured, healthy, critical and `Aya is down`.
- Poison 8: 32, 24, 16, 8, 0, then `Aya is down.`; the sixth turn is skipped.
- Poison 5: 35, 30, 25, 20, 15, 10; all six turns run and Aya stays alive.
- The three healing outputs: 80, 100 and 25.
- Arena victory, defeat, exhausted potions, invalid text, invalid number, empty line, running away and end of input.
- Six consecutive extension games, Boss armour and attack, death notification, replay, sorted best-three victories and one saved line per game.
- Read-only results and a directory occupying the results filename: saving reports a failure and the program continues.

Compiler experiments were performed in temporary copies, leaving the working code valid:

| Experiment requested by the PDF | Observed error |
| --- | --- |
| Remove the semicolon after the first healing output | CS1002: `; expected` |
| Remove the last closing brace of the healing method | CS1513: `} expected` |
| Instantiate the abstract Enemy class | CS0144: cannot create an instance of an abstract class |
| Remove virtual from Enemy.TakeDamage while retaining the Boss override | CS0506: the inherited member is not virtual, abstract or override |

Invalid Arena input now keeps the same turn number and causes no counterattack, following the step 6 checkpoint. The PDF's example increments the number despite describing the same turn; this implementation follows the written requirement.

## Run and submit

From this folder, for example:

```powershell
dotnet run --project WarmUp/Exercice1
dotnet run --project Arena
cd GoingFurther
dotnet run
```

Run GoingFurther from its own folder to save `results.txt` there. See `GoingFurther/README.md` for the extension controls and behavior.

Part 4 is reference material and requires no additional program. The final hand-in instruction asks for a zip of Arena excluding `bin` and `obj`. Its source files can be packaged from this folder with:

```powershell
Compress-Archive -LiteralPath Arena/Program.cs, Arena/TerminalTranscript.cs, Arena/Arena.csproj -DestinationPath Arena.zip
```

The homework extensions are in the separate GoingFurther project; include that source folder too if your teacher requests the homework submission.

## Automatic terminal test files

Every project records its output, error output and submitted input lines automatically. Run it with the usual `dotnet run` command; no extra option is needed. The first line shows the full transcript path.

Files are UTF-8 text under `terminal-tests/<project name>/` in the folder from which you started the program. Each run uses a new timestamped filename. For example, running Arena from the repository folder saves to `terminal-tests/Arena/`; running GoingFurther from its own folder saves to `GoingFurther/terminal-tests/GoingFurther/`.

The recording is flushed as the program runs, so you can open it immediately. It includes invalid inputs, empty input lines, game choices and replay answers. GoingFurther still writes its separate `results.txt` summary.

These transcripts record the running program. Compiler errors and terminal commands happen outside the program: to save those too, use PowerShell's terminal recording around your tests:

```powershell
Start-Transcript -Path .\build-tests.txt
dotnet build Arena
# Run other build commands or tests here.
Stop-Transcript
```

If recording cannot write to disk, a message is displayed and the exercise continues. Each project includes its own `TerminalTranscript.cs`, so it remains usable when copied or submitted separately.
