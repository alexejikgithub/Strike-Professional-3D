using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private MainCharacterController _mainCharacter;
	[SerializeField] private FireController _fireController;
	[SerializeField] private GameObject _startMessage;
	[SerializeField] private InteractableCanvasObject _input;

	private bool _isGamplayOn;
	public bool IsGameplayOn => _isGamplayOn;


	private void Awake()
	{
		_mainCharacter.OnLastWPReached += EndLevel;
		_input.OnPointerDownEvent += OnTap;
		_isGamplayOn = false;
		_mainCharacter.SetManager(this);
		_fireController.SetManager(this);
	}
	private void OnTap(Vector3 position)
	{
		if(_isGamplayOn)
		{
			return;
		}
		_startMessage.gameObject.SetActive(false);
		StartCoroutine(StartGameplay());

	}

	private IEnumerator StartGameplay()
	{
		yield return new WaitForSeconds(0.5f);
		_isGamplayOn = true;
	}

	private void EndLevel()
	{
		RestartLevel();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	private void OnDestroy()
	{
		_mainCharacter.OnLastWPReached -= EndLevel;
		_input.OnPointerDownEvent -= OnTap;
	}
}
