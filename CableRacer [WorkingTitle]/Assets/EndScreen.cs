using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
	private UIManager _uiManager;
	[SerializeField] TextMeshProUGUI _score;
	[SerializeField] private TextMeshProUGUI _highScore;

	void Start()
	{
		_uiManager = FindObjectOfType<UIManager>();
		_uiManager.TransitionIn();

		_score.text = PlayerPrefs.GetInt("Score").ToString();
		_highScore.text = PlayerPrefs.GetInt("HighScore").ToString();

	}
}
