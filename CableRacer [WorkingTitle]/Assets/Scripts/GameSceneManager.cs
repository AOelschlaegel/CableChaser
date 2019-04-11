using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	[SerializeField] private string _gameScene;
	[SerializeField] private string _gameOverScene;
	[SerializeField] private string _startScene;

	private UIManager _uiManager;

	private void Start()
	{
		_uiManager = FindObjectOfType<UIManager>();
	}

	public void LoadGameScene()
	{
		StartCoroutine(LoadScene(_gameScene));
	}

	public void LoadGameOverScene()
	{
		StartCoroutine(LoadScene(_gameOverScene));
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
		_uiManager.TransitionIn();
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(scene);
	}
}
