using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
	[SerializeField] private Material _mainColor;
	[SerializeField] private Material _obstacleColor;

	public void ChangeMainColor()
	{
		//_mainColor. = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
	}

	public void ChangeObstacleColor()
	{
		//_obstacleColor.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
	}
}
