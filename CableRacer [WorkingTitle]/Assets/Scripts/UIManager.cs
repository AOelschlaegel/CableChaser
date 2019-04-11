using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject _transitionCanvas;

	private Animator _anim;

	private void Awake()
	{
		_anim = _transitionCanvas.GetComponent<Animator>();
	}

	public void TransitionIn()
	{
		_anim.SetBool("isTransitionIn", true);
		_anim.SetBool("isTransitionOut", false);
	}

	public void TransitionOut()
	{
		_anim.SetBool("isTransitionOut", true);
		_anim.SetBool("isTransitionIn", false);
	}
}
