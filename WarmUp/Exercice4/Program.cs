using System;

int maxHealth = 100;

// Heal without going above the maximum health.
Console.WriteLine($"50 + 30 -> {Heal(50, 30, maxHealth)}");
Console.WriteLine($"90 + 30 -> {Heal(90, 30, maxHealth)}");
Console.WriteLine($"0 + 25 -> {Heal(0, 25, maxHealth)}");

int Heal(int health, int amount, int maxHealth)
{
	return Math.Min(health + amount, maxHealth);
}
