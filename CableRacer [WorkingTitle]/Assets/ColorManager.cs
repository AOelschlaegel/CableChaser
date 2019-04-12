using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
	[SerializeField] private Material _mainColor;
	[SerializeField] private Material _obstacleColor;

	private float timer;
	public int TransitionSpeed;

	public bool MainColorSwitch;
	public bool ObstacleSwitch;

	public void ChangeMainColor()
	{
		if (MainColorSwitch)
		{
			timer = Time.time;

			var wireFrameColor = _mainColor.GetColor("_WColor");

			var oldColor = wireFrameColor;
			var newColor = new Color(Random.Range(0.4f, 0.8f), Random.Range(0.4f, 0.8f), Random.Range(0.7f, 0.9f), 0.95f);

			var setColor = Color.Lerp(oldColor, newColor, timer);

			_mainColor.SetColor("_WColor", newColor);
		}
	}

	public void ChangeObstacleColor()
	{
		if (ObstacleSwitch)
		{
			timer = Time.time;

			var glowColor = _obstacleColor.GetColor("_EmissionColor");

			var oldColor = glowColor;
			var newColor = new Color(Random.Range(0.7f, 0.9f), Random.Range(0.2f, 0.8f), Random.Range(0.2f, 0.8f), 0.8f);

			var setColor = Color.Lerp(oldColor, newColor, timer);

			_obstacleColor.SetColor("_EmissionColor", newColor);
		}
	}

	private void Update()
	{
		timer = Time.time * TransitionSpeed;
	}
}
