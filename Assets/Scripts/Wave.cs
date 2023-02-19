using System;
using UnityEngine;

[Serializable]
public struct Wave
{
	public string Tilte;

	public int ThienThach;

	public Vector2 Start;

	public bool Loop;

	public float Time;

	public float Distance;

	public int TypeSort;

	public float Speed;

	public TypePath Path;

	public Vector2[] Safe;

	public Vector2[] UnSafe;

	public TypeBonus[] Bonus;

	public int[,] Grid;

	public InfoEnemy[] lsEnemy;
}
