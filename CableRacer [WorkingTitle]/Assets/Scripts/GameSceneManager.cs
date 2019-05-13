using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	[SerializeField] private string _endlessScene;
	[SerializeField] private string _levelScene;
	[SerializeField] private string _gameOverScene;
	[SerializeField] private string _startScene;

	private UIManager _uiManager;

	private void Start()
	{
		_uiManager = FindObjectOfType<UIManager>();
	}

	public void LoadLevelScene()
	{
		StartCoroutine(LoadScene(_levelScene));
	}

	public void LoadGameOverScene()
	{
		StartCoroutine(LoadScene(_gameOverScene));
	}

	public void LoadEndlessScene()
	{
		StartCoroutine(LoadScene(_endlessScene));
	}

	public void LoadStartScene()
	{
		StartCoroutine(LoadScene(_startScene));
	}

	public void QuitApplication()
	{
		Application.Quit();
	}

	private IEnumerator LoadScene(string scene)
	{
		_uiManager.TransitionOut();
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(scene);
	}
}
