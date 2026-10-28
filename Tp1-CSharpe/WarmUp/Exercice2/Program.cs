using System;

int health = 45; // I test the program with 0 12 et 100
string state;
if (health > 70)
{
	state = "healthy";
}
else if (health >= 31)
{
	state = "injured";
}
else if (health >= 1)
{
	state = "critical";
}
else
{
	state = "down";
}
Console.WriteLine(health == 0 ? "Aya is down" : $"Aya has {health} HP: {state}");
