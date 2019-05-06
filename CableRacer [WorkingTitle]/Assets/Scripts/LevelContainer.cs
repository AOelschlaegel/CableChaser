using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
	private Level _level;

	public string LevelName;
	public int Length;
	public int Speed;
	public Material MainMaterial;
	public int Difficulty;
	public List<GameObject> Tiles;

	public void SetLevel()
	{
		_level = new Level();
		_level.Length = Length;
		_level.LevelName = LevelName;
		_level.Speed = Speed;
		_level.MainMaterial = MainMaterial;
		_level.Difficulty = Difficulty;
	}
}
