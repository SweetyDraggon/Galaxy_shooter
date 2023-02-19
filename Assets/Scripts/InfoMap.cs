using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InfoMap
{
	public string TextWave;

	public int Width;

	public int row;

	public int column;

	public int idThienThach;

	public List<Vector3> posSafe = new List<Vector3>();

	public List<Vector3> posUnSafe = new List<Vector3>();

	public int TypeSort;

	public float Distance;

	public List<int> Bonus = new List<int>();

	public float Time;

	public float Speed;

	public List<string> namePath = new List<string>();

	public Vector2 posStart = default(Vector2);

	public bool rotation;

	public int Count;

	public bool Loop;

	public bool isFreeStyle;

	public List<GroupEnemy> Grid;
}
