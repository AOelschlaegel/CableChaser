using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
	private UIManager _uiManager;

	void Start()
	{
		_uiManager = FindObjectOfType<UIManager>();
		_uiManager.TransitionIn();
	}
}
