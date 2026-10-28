using System;
using System.Collections.Generic;

Player player = new Player("Aya", 100, 2);
List<Enemy> enemies = new List<Enemy>();
enemies.Add(new Goblin());
enemies.Add(new Orc());
enemies.Add(new Goblin());

Random rng = new Random();
int turn = 0;

Console.WriteLine("=== Arena ===");
Console.WriteLine($"{player.Name} faces {enemies.Count} enemies:");
foreach (Enemy enemy in enemies)
{
	Console.WriteLine($"- {enemy.Name}, {enemy.Health} HP");
}
Console.WriteLine($"First in line: {enemies[0].Name}");

while (player.IsAlive && enemies.Count > 0)
{
	Enemy enemy = enemies[0];
	Console.WriteLine();
	Console.WriteLine($"Turn {turn + 1}: {player.Name} {player.Health} HP, {player.Potions} potions | {enemy.Name} {enemy.Health} HP");
	Console.Write("1 attack, 2 drink a potion, 3 run away > ");
	string? text = Console.ReadLine();

	// End cleanly when redirected input ends or the terminal is closed.
	if (text == null)
	{
		Console.WriteLine("Input ended. Leaving the arena.");
		return;
	}

	if (!int.TryParse(text, out int choice))
	{
		Console.WriteLine("Type 1, 2 or 3.");
		continue;
	}

	switch (choice)
	{
		case 1:
			enemy.TakeDamage(rng.Next(12, 25));
			break;
		case 2:
			player.DrinkPotion();
			break;
		case 3:
			Console.WriteLine("You run away.");
			return;
		default:
			Console.WriteLine("Type 1, 2 or 3.");
			continue;
	}

	// Only a valid attack or potion attempt consumes a turn.
	turn++;
	// A defeated enemy is removed before it can strike back.
	if (!enemy.IsAlive)
	{
		Console.WriteLine($"{enemy.Name} is defeated!");
		enemies.RemoveAt(0);
		continue;
	}

	enemy.Attack(player, rng);
}

Console.WriteLine();
if (player.IsAlive)
{
	Console.WriteLine($"Victory in {turn} turns!");
}
else
{
	Console.WriteLine("Defeat...");
}

public class Player
{
	private int health;

	public string Name { get; }
	public int MaxHealth { get; }
	public int Potions { get; private set; }

	public int Health
	{
		get { return health; }
		private set { health = Math.Clamp(value, 0, MaxHealth); }
	}

	public bool IsAlive => Health > 0;

	public Player(string name, int maxHealth, int potions)
	{
		Name = name;
		MaxHealth = maxHealth;
		Potions = potions;
		Health = MaxHealth;
	}

	public void TakeDamage(int amount)
	{
		Health -= amount;
		Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");
	}

	public void DrinkPotion()
	{
		if (Potions == 0)
		{
			Console.WriteLine("No potion left!");
			return;
		}

		Potions--;
		Health += 30;
		Console.WriteLine($"{Name} drinks a potion ({Health} HP)");
	}
}

public abstract class Enemy
{
	public string Name { get; }
	public int Health { get; private set; }
	public bool IsAlive => Health > 0;

	protected Enemy(string name, int health)
	{
		Name = name;
		Health = health;
	}

	public void TakeDamage(int amount)
	{
		Health = Math.Max(Health - amount, 0);
		Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");
	}

	public abstract void Attack(Player target, Random rng);
}

public class Goblin : Enemy
{
	public Goblin() : base("Goblin", 30)
	{
	}

	public override void Attack(Player target, Random rng)
	{
		Console.WriteLine("Goblin stabs!");
		target.TakeDamage(rng.Next(4, 9));
	}
}

public class Orc : Enemy
{
	public Orc() : base("Orc", 60)
	{
	}

	public override void Attack(Player target, Random rng)
	{
		Console.WriteLine("Orc swings its club!");
		target.TakeDamage(rng.Next(8, 15));
	}
}
