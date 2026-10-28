using System;

int health = 40;

for (int turn = 1; turn <= 6; turn++)
{
	health -= 8;
	if (health < 0)
	{
		health = 0;
	}

	Console.WriteLine($"Health left: {health}");

	if (health == 0)
	{
		Console.WriteLine("Aya is down.");
		break;
	}
}

// With poison of 5 instead, Aya has 10 health after six turns and is not down.
